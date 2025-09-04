using Quartz;
using ServicioServidorVPN.clases.Clientes.ControlAcceso;
using ServicioServidorVPN.DAL.ClientesControlAcceso;
using ServicioServidorVPN.Service.Ias.Clientes.ControlAcceso;
using ServicioServidorVPN.utilitarios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace ServicioServidorVPN.Jobs.ClientesControlAcceso {
    [DisallowConcurrentExecution]

    public class MigracionClientesControlAccesoJob : IJob {
        private readonly ClientesControlAccesoService _clientesControlAccesoService;
        private readonly ClientesControlAccesoDAL _clientesControlAccesoDAL;

        public MigracionClientesControlAccesoJob(ClientesControlAccesoService clientesControlAccesoService, ClientesControlAccesoDAL clientesControlAccesoDAL) {
            _clientesControlAccesoService = clientesControlAccesoService;
            _clientesControlAccesoDAL = clientesControlAccesoDAL;
        }

        public async Task Execute(IJobExecutionContext context) {
            DateTime fechaMigracionDwh = DateTime.Now.ToLocalTime();
            string cantidadRegistrosMigradosStr = ConfigurationManager.AppSettings["CantidadRegistrosMigradosPorEjecucionClientesControlAcceso"] ?? "2000";
            if(!int.TryParse(cantidadRegistrosMigradosStr, out int cantidadRegistrosMigrados)) {
                cantidadRegistrosMigrados = 2000;
            }

            List<ClienteControlAccesoEntidad> clientesControlAcceso = new List<ClienteControlAccesoEntidad>();
            List<ClienteControlAccesoIds> ids = new List<ClienteControlAccesoIds>();
            try {
                clientesControlAcceso = await _clientesControlAccesoService.ObtenerClientesControlAccesoIas(cantidadRegistrosMigrados);
                bool hayClientes = clientesControlAcceso.Count > 0;
                string logClientesControlAcceso = hayClientes ? $"{clientesControlAcceso.Count} clientes de control de acceso para migrar." : "No hay ingresos de clientes de control de acceso para migrar.";
                funciones.logueo(logClientesControlAcceso);
                if(!hayClientes) {
                    return;
                }
                clientesControlAcceso.ForEach(x => x.FechaMigracion = fechaMigracionDwh);
                ids = clientesControlAcceso.Select(x => new ClienteControlAccesoIds {
                    IdCliente = x.IdCliente,
                    CodSala = x.CodSala,
                }).ToList();

                List<ClienteControlAccesoEntidad> clientesControlAccesoExistentes = await _clientesControlAccesoDAL.ObtenerClientesControlAcceso(ids);
                clientesControlAcceso.RemoveAll(x => clientesControlAccesoExistentes.Any(y => y.IdCliente == x.IdCliente && y.CodSala == x.CodSala));

                bool migracionCompleta = await _clientesControlAccesoDAL.InsertarClientesControlAccesiMasivamente(clientesControlAcceso);
                string logMigracionCompleta = migracionCompleta ? $"{clientesControlAcceso.Count} clientes de control de acceso migrados correctamente, desde {clientesControlAcceso.FirstOrDefault()?.IdCliente ?? 0} - {clientesControlAcceso.LastOrDefault()?.IdCliente ?? 0}." : $"No se pudo insertar masivamente los {clientesControlAcceso.Count} ingresos de clientes a sala.";
                funciones.logueo(logMigracionCompleta);
                if(!migracionCompleta) {
                    return;
                }

                bool marcadoComoMigrado = await _clientesControlAccesoService.MarcarComoMigrados(ids, fechaMigracionDwh);
                string logMarcadoComoMigrado = marcadoComoMigrado ? $"Se marcó como migrado {ids.Count} ingresos de clientes a sala en el IAS, desde {ids.FirstOrDefault()} - {ids.LastOrDefault()}." : $"No se pudo marcar como migrado {ids.Count} ingresos de clientes a sala ({string.Join(",", ids)}).";
                funciones.logueo(logMarcadoComoMigrado);
            } catch(Exception ex) {
                funciones.logueo($"Error al intentar migrar ingresos de clientes a sala, desde: {clientesControlAcceso.FirstOrDefault()?.IdCliente ?? 0} - {clientesControlAcceso.LastOrDefault()?.IdCliente ?? 0}. {ex.Message}", "Error");
                if(ids.Count > 0) {
                    bool migracionRevertida = await _clientesControlAccesoService.RevertirEstadoMigracion(ids);
                    string logMigracionRevertida = migracionRevertida ? $"Se revirtió el estado de migración de {ids.Count} ingresos de clientes a sala, desde {ids.FirstOrDefault()} - {ids.LastOrDefault()}." : $"No se pudo revertir el estado de migración de {ids.Count} ingresos de clientes a sala ({string.Join(",", ids)}).";
                    funciones.logueo(logMigracionRevertida);
                }
            }
            funciones.logueo($"Finalizado Job Migración de Clientes de Control de Acceso.");
        }
    }
}
