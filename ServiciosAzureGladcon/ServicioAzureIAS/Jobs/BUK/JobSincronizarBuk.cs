using Newtonsoft.Json;
using Quartz;
using ServicioAzureIAS.Clases.BUK;
using ServicioAzureIAS.DAL.BUK;
using ServicioAzureIAS.utilitarios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ServicioAzureIAS.Jobs.BUK {
    public class JobSincronizarBuk : IJob {
        private static string mensajeLog = "Sincronizando Empleados y Cargos de BUK";
        private readonly BUK_CargoDAL _bukCargoDAL = new BUK_CargoDAL();
        private readonly BUK_EmpleadoDAL _bukEmpleadoDAL = new BUK_EmpleadoDAL();
        private readonly BUK_EquivalenciaEmpresaDAL _bukEquivalenciaEmpresaDAL = new BUK_EquivalenciaEmpresaDAL();
        private readonly string burApiUri = ConfigurationManager.AppSettings["BukApiUri"];
        public Task Execute(IJobExecutionContext context) {
            SincronizarBuk();
            return Task.CompletedTask;
        }
        public void SincronizarBuk() {
            funciones.logueo("INICIO - " + mensajeLog);
            try {
                ////Migrar Cargos
                //funciones.logueo($"Migrando Cargos");
                //var cargosResponse = ObtenerCargosBuk();
                //if(cargosResponse.Count > 0) {
                //    foreach(var item in cargosResponse) {
                //        _bukCargoDAL.InsertarCargo(item);
                //    }
                //}
                var listaEditar = new List<BUK_Empleado>();
                var listaInsertar = new List<BUK_Empleado>();
                funciones.logueo($"Migrando Empleados desde Buk a tabla BUK_Empleado");
                var listaEmpresas = _bukEquivalenciaEmpresaDAL.ObtenerEmpresas();
                foreach(var item in listaEmpresas) {
                    try {
                        funciones.logueo($"Empresa - {item.Nombre}");
                        var empleadosResponse = ObtenerEmpleadosPorEmpresaBuk(item.IdEmpresaBuk);

                        if (empleadosResponse.success == true && empleadosResponse.data.Count > 0) {
                            funciones.logueo("Cantidad Empleados : " + empleadosResponse.data.Count);
                            empleadosResponse.data.ForEach(x => x.IdEmpresa = item.IdEmpresaBuk);
                            //se encontraron empleados en BUK
                            var empleadosIAS = _bukEmpleadoDAL.ListarEmpleadosPorEmpresaBuk(item.IdEmpresaBuk);
                            if (empleadosIAS.Count == 0) {
                                listaInsertar = empleadosResponse.data;
                            }
                            else {
                                listaEditar = empleadosIAS.Where(x => empleadosResponse.data.Select(y => y.IdBuk).Contains(x.IdBuk)).ToList();
                                listaInsertar = empleadosIAS.Where(x => !empleadosResponse.data.Select(y => y.IdBuk).Contains(x.IdBuk)).ToList();
                            }
                            foreach (var empleado in listaInsertar) {
                                _bukEmpleadoDAL.InsertarEmpleado(empleado);
                            }
                            foreach (var empleado in listaEditar) {
                                _bukEmpleadoDAL.EditarEmpleado(empleado);
                            }
                        }
                    }
                    catch (Exception err) {
                        funciones.logueo($"Empresa - {err.Message}");
                    }
                
                }
                funciones.logueo($"Finalizo Migracion de Empleados desde Buk a tabla BUK_Empleado");
            } catch(Exception ex) {

                funciones.logueo($"Error metodo Job JobSincronizarBuk - {ex.Message}");
            }
        }
        public List<BUK_Cargo> ObtenerCargosBuk() {
            var response = new List<BUK_Cargo>();

            try {
                var url = $"{burApiUri}/api/rrhh/ListadoCargos";
                using(HttpClient httpClient = new HttpClient()) {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    using(HttpResponseMessage httpResponse = httpClient.GetAsync(url).Result) {
                        if(httpResponse.IsSuccessStatusCode) {
                            var result = httpResponse.Content.ReadAsStringAsync().Result;
                            response = JsonConvert.DeserializeObject<List<BUK_Cargo>>(result);
                        }
                    }
                }
                return response;
            } catch(Exception ex) {
                funciones.logueo($"Error metodo ObtenerCargosBuk - {ex.Message}");
                response = new List<BUK_Cargo>();
                return response;
            }
        }
        public BUK_Response<List<BUK_Empleado>> ObtenerEmpleadosPorEmpresaBuk(int empresa) {
            var response = new BUK_Response<List<BUK_Empleado>>();

            try {
                var url = $"{burApiUri}/api/EntradaSalida/companies/{empresa}/employees";
                using(HttpClient httpClient = new HttpClient()) {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    httpClient.Timeout = TimeSpan.FromMinutes(5);
                    using (HttpResponseMessage httpResponse = httpClient.GetAsync(url).Result) {
                        if(httpResponse.IsSuccessStatusCode) {
                            var result = httpResponse.Content.ReadAsStringAsync().Result;
                            response = JsonConvert.DeserializeObject<BUK_Response<List<BUK_Empleado>>>(result);
                        }
                    }
                }
                return response;
            } catch(Exception ex) {
                funciones.logueo($"Error metodo ObtenerEmpleadosPorEmpresaBuk - {ex.Message}");
                response = new BUK_Response<List<BUK_Empleado>>();
                return response;
            }
        }
        private string VerificarCambiosEmpleado(BUK_Empleado empleadoRemoto, BUK_Empleado empleadoLocal) {
            var resumenCambios = new List<string>();
            bool cambios = false;
            try {
                if(empleadoRemoto.TipoDocumento != empleadoLocal.TipoDocumento) {
                    resumenCambios.Add("TipoDocumento");
                    cambios = true;
                }
                if(empleadoRemoto.NumeroDocumento != empleadoLocal.NumeroDocumento) {
                    resumenCambios.Add("NumeroDocumento");
                    cambios = true;
                }
                if(empleadoRemoto.Nombres != empleadoLocal.Nombres) {
                    resumenCambios.Add("Nombres");
                    cambios = true;
                }
                if(empleadoRemoto.ApellidoPaterno != empleadoLocal.ApellidoPaterno) {
                    resumenCambios.Add("ApellidoPaterno");
                    cambios = true;
                }
                if(empleadoRemoto.ApellidoMaterno != empleadoLocal.ApellidoMaterno) {
                    resumenCambios.Add("ApellidoMaterno");
                    cambios = true;
                }
                if(empleadoRemoto.NombreCompleto != empleadoLocal.NombreCompleto) {
                    resumenCambios.Add("NombreCompleto");
                    cambios = true;
                }
                if(empleadoRemoto.IdCargo != empleadoLocal.IdCargo) {
                    resumenCambios.Add("IdCargo");
                    cambios = true;
                }
                if(empleadoRemoto.Cargo != empleadoLocal.Cargo) {
                    resumenCambios.Add("Cargo");
                    cambios = true;
                }
                if(empleadoRemoto.IdEmpresa != empleadoLocal.IdEmpresa) {
                    resumenCambios.Add("IdEmpresa");
                    cambios = true;
                }
                if(empleadoRemoto.Empresa != empleadoLocal.Empresa) {
                    resumenCambios.Add("Empresa");
                    cambios = true;
                }
                if(empleadoRemoto.FechaCese != empleadoLocal.FechaCese) {
                    resumenCambios.Add("FechaCese");
                    cambios = true;
                }
                if(empleadoRemoto.EstadoCese != empleadoLocal.EstadoCese) {
                    resumenCambios.Add("EstadoCese");
                    cambios = true;
                }
            } catch(Exception) {
                return string.Empty;
            }
            return String.Join("", resumenCambios);
        }
    }
}
