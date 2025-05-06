using Quartz;
using ServicioServidorVPN.clases.Clientes;
using ServicioServidorVPN.DAL.Clientes;
using ServicioServidorVPN.Service.Ias.Clientes;
using ServicioServidorVPN.utilitarios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace ServicioServidorVPN.Jobs.Clientes {

    [DisallowConcurrentExecution]
    public class MigracionIngresoClientesSalaJob : IJob {
        private readonly IngresoClienteSalaService _ingresoClienteSalaService;
        private readonly IngresoClienteSalaDAL _ingresoClienteSalaDAL;

        public MigracionIngresoClientesSalaJob() {
            _ingresoClienteSalaService = new IngresoClienteSalaService();
            _ingresoClienteSalaDAL = new IngresoClienteSalaDAL();
        }

        public async Task Execute(IJobExecutionContext context) {
            DateTime fechaMigracionDwh = DateTime.Now.ToLocalTime();
            string cantidadRegistrosMigradosStr = ConfigurationManager.AppSettings["CantidadRegistrosMigradosPorEjecucionIngresoClienteSala"] ?? "2000";
            if(!int.TryParse(cantidadRegistrosMigradosStr, out int cantidadRegistrosMigrados)) {
                cantidadRegistrosMigrados = 2000;
            }

            List<CAL_IngresoClienteSala> ingresosClientes = new List<CAL_IngresoClienteSala>();
            List<int> ids = new List<int>();
            try {
                ingresosClientes = await _ingresoClienteSalaService.ObtenerIngresosClientesIas(cantidadRegistrosMigrados);
                bool hayIngresosClientes = ingresosClientes.Count > 0;
                string logIngresosClientes = hayIngresosClientes ? $"{ingresosClientes.Count} ingresos de clientes a sala para migrar." : "No hay ingresos de clientes para migrar.";
                funciones.logueo(logIngresosClientes);
                if(!hayIngresosClientes) {
                    return;
                }
                ingresosClientes.ForEach(ingreso => ingreso.FechaMigracion = fechaMigracionDwh);
                ids = ingresosClientes.Select(x => x.Id).ToList();

                List<CAL_IngresoClienteSala> ingresosClientesExistentes = await _ingresoClienteSalaDAL.ObtenerIngresosClientesPorIds(ids);
                ingresosClientes.RemoveAll(x => ingresosClientesExistentes.Any(y => y.Id == x.Id));

                bool migracionCompleta = await _ingresoClienteSalaDAL.InsertarIngresosClientesMasivamente(ingresosClientes);
                string logMigracionCompleta = migracionCompleta ? $"{ingresosClientes.Count} ingresos de clientes a sala migrados correctamente, desde {ingresosClientes.FirstOrDefault()?.Id ?? 0} - {ingresosClientes.LastOrDefault()?.Id ?? 0}." : $"No se pudo insertar masivamente los {ingresosClientes.Count} ingresos de clientes a sala.";
                funciones.logueo(logMigracionCompleta);
                if(!migracionCompleta) {
                    return;
                }

                bool marcadoComoMigrado = await _ingresoClienteSalaService.MarcarComoMigrados(ids, fechaMigracionDwh);
                string logMarcadoComoMigrado = marcadoComoMigrado ? $"Se marcó como migrado {ids.Count} ingresos de clientes a sala en el IAS, desde {ids.FirstOrDefault()} - {ids.LastOrDefault()}." : $"No se pudo marcar como migrado {ids.Count} ingresos de clientes a sala ({string.Join(",", ids)}).";
                funciones.logueo(logMarcadoComoMigrado);
            } catch(Exception ex) {
                funciones.logueo($"Error al intentar migrar ingresos de clientes a sala, desde: {ingresosClientes.FirstOrDefault()?.Id ?? 0} - {ingresosClientes.LastOrDefault()?.Id ?? 0}. {ex.Message}", "Error");
                if(ids.Count > 0) {
                    bool migracionRevertida = await _ingresoClienteSalaService.RevertirEstadoMigracion(ids);
                    string logMigracionRevertida = migracionRevertida ? $"Se revirtió el estado de migración de {ids.Count} ingresos de clientes a sala, desde {ids.FirstOrDefault()} - {ids.LastOrDefault()}." : $"No se pudo revertir el estado de migración de {ids.Count} ingresos de clientes a sala ({string.Join(",", ids)}).";
                    funciones.logueo(logMigracionRevertida);
                }
            }
            funciones.logueo($"Finalizado Job Migración Ingreso Clientes a Sala");
        }
    }
}
