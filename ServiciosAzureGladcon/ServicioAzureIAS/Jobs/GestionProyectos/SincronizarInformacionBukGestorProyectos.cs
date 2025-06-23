using Quartz;
using ServicioAzureIAS.Clases.GestioProyectos;
using ServicioAzureIAS.DAL.GestionProyectos;
using ServicioAzureIAS.Service;
using ServicioAzureIAS.utilitarios;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicioAzureIAS.Jobs.GestionProyectos {
    public class SincronizarInformacionBukGestorProyectos : IJob {
        private readonly EmpleadoDAL empleadoDAL;
        private readonly AreaDAL areaDAL;
        private readonly EmpresaDAL empresaDAL;
        private readonly UsuarioDAL usuarioDAL;
        private readonly GestorBukService gestorBukService;

        public SincronizarInformacionBukGestorProyectos() {
            empleadoDAL = new EmpleadoDAL();
            areaDAL = new AreaDAL();
            empresaDAL = new EmpresaDAL();
            usuarioDAL = new UsuarioDAL();
            gestorBukService = new GestorBukService();
        }

        public async Task Execute(IJobExecutionContext context) {

            var empresasBD = await empresaDAL.ObtenerEmpresas();
            var empresasApi = await gestorBukService.ObtenerEmpresaDesdeApi();
            var areasBD = await areaDAL.ObtenerAreas();
            var empleadosBD = await empleadoDAL.ObtenerEmpleados();


            #region empresas
            foreach(var empresaApi in empresasApi) {
                var empresaLocal = empresasBD.FirstOrDefault(e => e.ID_BUK == empresaApi.ID_BUK);
                Empresa empresa = new Empresa {
                    ID_BUK = empresaApi.ID_BUK,
                    CO_EMPR = empresaApi.CO_EMPR,
                    DE_NOMB = empresaApi.DE_NOMB,
                    NU_RUCS = empresaApi.NU_RUCS,
                    DE_DIRE = empresaApi.DE_DIRE
                };
                if(empresaLocal != null) {
                    if(empresaLocal.isDifferentEmpresaAPi(empresaApi)) {
                        await empresaDAL.ActualizarEmpresa(empresa);
                    }
                } else {
                    await empresaDAL.InsertarEmpresa(empresa);
                }

                #region empleados
                var empleadosApi = await gestorBukService.ObtenerEmpleadosDesdeApi(empresaApi.ID_BUK);

                foreach(var empleadoApi in empleadosApi) {
                    empleadoApi.IdEmpresa = empresaApi.ID_BUK;

                    var empleadosLocales = empleadosBD.Where(e => e.IdBuk == empleadoApi.IdBuk).ToList();
                    var empleadoEnEmpresaActual = empleadosLocales.FirstOrDefault(e => e.IdEmpresa == empresaApi.ID_BUK);
                    Empleado empleado = new Empleado {
                        TipoDocumento = empleadoApi.TipoDocumento,
                        NumeroDocumento = empleadoApi.NumeroDocumento,
                        Nombres = empleadoApi.Nombres,
                        ApellidoPaterno = empleadoApi.ApellidoPaterno,
                        ApellidoMaterno = empleadoApi.ApellidoMaterno,
                        NombreCompleto = empleadoApi.NombreCompleto,
                        IdEmpresa = empleadoApi.IdEmpresa,
                        Empresa = empleadoApi.Empresa,
                        Correo = empleadoApi.Correo,
                        Celular = empleadoApi.Celular,
                        EstadoCese = empleadoApi.EstadoCese,
                        IdAreaBuk = empleadoApi.IdAreaBuk,
                        Area = empleadoApi.Area,
                        IdBuk = empleadoApi.IdBuk,
                    };
                    var areaDyd = areasBD.FirstOrDefault(a => a.IdAreaBuk == empleadoApi.IdAreaBuk);

                    Area areaNew = new Area();
                    if(areaDyd != null) {
                        areaNew.IdArea = areaDyd.IdArea;
                        areaNew.area = empleadoApi.Area;
                        areaNew.IdAreaBuk = empleadoApi.IdAreaBuk;
                        if(areaNew.IsDifferentAreaApi(areaDyd)) {
                            await areaDAL.ActualizarArea(areaNew);
                        }
                    } else {
                        areaNew.area = empleadoApi.Area;
                        areaNew.IdAreaBuk = empleadoApi.IdAreaBuk;
                        areaNew.estado = true;
                        await areaDAL.InsertarArea(areaNew);
                    }

                    if(empleadoEnEmpresaActual != null) {
                        if(empleadoEnEmpresaActual.isDifferentEmpleadoApi(empleadoApi)) {
                            empleado.Id = empleadoEnEmpresaActual.Id;
                            await empleadoDAL.ActualizarEmpleado(empleado);
                        }
                    } else {
                        await empleadoDAL.InsertarEmpleado(empleado);
                    }
                }
                #endregion
            }
            #endregion

            #region usuarios
            var empleadosActualizadosBD = await empleadoDAL.ObtenerEmpleados();
            var usuariosBD = await usuarioDAL.ObtenerUsuarios();
            var empresasActualizadosBD = await empresaDAL.ObtenerEmpresas();
            var areasActualizadosBD = await areaDAL.ObtenerAreas();

            foreach(var usuarioBD in usuariosBD) {
                var empleadoUsuarioLocal = empleadosActualizadosBD.FirstOrDefault(e => e.Id == usuarioBD.EmpleadoId);
                Usuario usuario = new Usuario {
                    Nombres = usuarioBD.Nombres,
                    Apellidos = usuarioBD.Apellidos,
                    Correo = usuarioBD.Correo,
                    Telefono = usuarioBD.Telefono,
                    EmpresaId = usuarioBD.EmpresaId,
                    AreaId = usuarioBD.AreaId,
                    Estado = usuarioBD.Estado,
                };

                Usuario empleado = new Usuario {
                    Nombres = empleadoUsuarioLocal.Nombres,
                    Apellidos = string.IsNullOrWhiteSpace(empleadoUsuarioLocal.ApellidoMaterno)
                                ? empleadoUsuarioLocal.ApellidoPaterno
                                : $"{empleadoUsuarioLocal.ApellidoPaterno} {empleadoUsuarioLocal.ApellidoMaterno}",
                    Correo = empleadoUsuarioLocal.Correo,
                    Telefono = empleadoUsuarioLocal.Celular,
                    EmpresaId = empleadoUsuarioLocal.IdEmpresa,
                    AreaId = empleadoUsuarioLocal.IdAreaBuk,
                    Estado = !empleadoUsuarioLocal.EstadoCese,
                };

                if(usuario.isDifferentUsuario(empleado)) {
                    empleado.UsuarioId = usuarioBD.UsuarioId;
                    var empresaLocal = empresasActualizadosBD.FirstOrDefault(e => e.ID_BUK == empleado.EmpresaId);
                    var areaLocal = areasActualizadosBD.FirstOrDefault(e => e.IdAreaBuk == empleado.AreaId);
                    empleado.EmpresaId = empresaLocal.EmpresaId;
                    empleado.AreaId = areaLocal.IdArea;
                    await usuarioDAL.ActualizarUsuario(empleado);
                }
            }
            #endregion

            #region empresas
            List<Empresa> empresasGestor = await empresaDAL.ObtenerEmpresas();
            List<Empresa> empresasPro = await empresaDAL.ObtenerEmpresasHolding();

            List<Empleado> empleadosGestor = await empleadoDAL.ObtenerEmpleados();
            List<Empleado> empleadosPro = await empleadoDAL.ObtenerEmpleadosHolding();

            List<Area> areasGestor = await areaDAL.ObtenerAreas();
            List<Area> areasPro = await areaDAL.ObtenerAreasHolding();


            List<Usuario> usuariosPro = await usuarioDAL.ObtenerUsuariosHolding();

            funciones.logueo("iniciar copiado de empresas a gestor pro:");
            foreach(var empresaGestor in empresasGestor) {
                var empresaPro = empresasPro.FirstOrDefault(e => e.ID_BUK == empresaGestor.ID_BUK);
                if(empresaPro == null) {
                    await empresaDAL.InsertarEmpresaHolding(empresaGestor);
                }
                if(empresaPro != null && empresaPro.isDifferentEmpresa(empresaGestor)) {
                    empresaGestor.EmpresaId = empresaPro.EmpresaId;
                    await empresaDAL.ActualizarEmpresaHolding(empresaGestor);
                }
            }
            #endregion empresas

            #region Areas
            funciones.logueo("iniciar copiado areas a gestor pro:");

            foreach(var areaGestor in areasGestor) {
                var areaPro = areasPro.FirstOrDefault(a => a.IdAreaBuk == areaGestor.IdAreaBuk);

                if(areaPro == null) {
                    await areaDAL.InsertarAreaHolding(areaGestor);
                } else if(areaPro.IsDifferentAreaApi(areaGestor)) {
                    areaGestor.IdArea = areaPro.IdArea;
                    await areaDAL.ActualizarAreaHolding(areaGestor);
                }
            }
            #endregion Areas

            #region empleados
            foreach(var empleadoGestor in empleadosGestor) {
                var empleadoPro = empleadosPro.FirstOrDefault(e => e.IdBuk == empleadoGestor.IdBuk && e.IdEmpresa == empleadoGestor.IdEmpresa);

                if(empleadoPro == null) {
                    await empleadoDAL.InsertarEmpleadoHolding(empleadoGestor);
                } else if(empleadoPro.isDifferentEmpleado(empleadoGestor)) {
                    empleadoGestor.Id = empleadoPro.Id;
                    await empleadoDAL.ActualizarEmpleadoHolding(empleadoGestor);
                }
            }
            #endregion empleados

            #region Usuarios
            funciones.logueo("inicio copiado usuarios a gestor pro:");
            foreach(var usuarioProUpdate in usuariosPro) {
                var empleadoUsuarioLocal = empleadosPro.FirstOrDefault(e => e.Id == usuarioProUpdate.EmpleadoId);

                Usuario usuario = new Usuario {
                    Nombres = usuarioProUpdate.Nombres,
                    Apellidos = usuarioProUpdate.Apellidos,
                    Correo = usuarioProUpdate.Correo,
                    Telefono = usuarioProUpdate.Telefono,
                    EmpresaId = usuarioProUpdate.EmpresaId,
                    AreaId = usuarioProUpdate.AreaId,
                    Estado = usuarioProUpdate.Estado,
                };

                Usuario usuarioActualizado = new Usuario {
                    Nombres = empleadoUsuarioLocal.Nombres,
                    Apellidos = string.IsNullOrWhiteSpace(empleadoUsuarioLocal.ApellidoMaterno)
                                   ? empleadoUsuarioLocal.ApellidoPaterno
                                   : $"{empleadoUsuarioLocal.ApellidoPaterno} {empleadoUsuarioLocal.ApellidoMaterno}",
                    Correo = empleadoUsuarioLocal.Correo,
                    Telefono = empleadoUsuarioLocal.Celular,
                    EmpresaId = empleadoUsuarioLocal.IdEmpresa,
                    AreaId = empleadoUsuarioLocal.IdAreaBuk,
                    Estado = !empleadoUsuarioLocal.EstadoCese
                };

                if(usuario.isDifferentUsuario(usuarioActualizado)) {
                    usuarioActualizado.UsuarioId = usuarioProUpdate.UsuarioId;

                    // Actualizar equivalencias
                    var empresaPro = empresasPro.FirstOrDefault(e => e.ID_BUK == usuarioActualizado.EmpresaId);
                    var areaPro = areasPro.FirstOrDefault(a => a.IdAreaBuk == usuarioActualizado.AreaId);

                    usuarioActualizado.EmpresaId = empresaPro.EmpresaId;
                    usuarioActualizado.AreaId = areaPro.IdArea;

                    await usuarioDAL.ActualizarUsuarioHolding(usuarioActualizado);
                }
            }
            funciones.logueo("fin del job: sincronizar data buk");
            #endregion Usuarios
        }
    }
}