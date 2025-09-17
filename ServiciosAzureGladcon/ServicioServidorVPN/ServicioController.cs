using ServicioServidorVPN.clases;
using ServicioServidorVPN.DAL;
using ServicioServidorVPN.Jobs.Ludopatas;
using ServicioServidorVPN.utilitarios;
using ServicioServidorVPN.WGDB_000.dal;
using ServicioServidorVPN.WGDB_000.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace ServicioServidorVPN {
    public class ServicioController : ApiController {
        //DATAWHEREHOUSE
        private readonly ContadoresOnlineDataWhereHouseDAL _contadoresDAL = new ContadoresOnlineDataWhereHouseDAL();


        private readonly CMP_SesionDAL _sesionDAL = new CMP_SesionDAL();
        private readonly CMP_SesionSorteoSalaDAL _sesionSorteoSalaDAL = new CMP_SesionSorteoSalaDAL();
        private readonly CMP_JugadaDAL _jugadaDAL = new CMP_JugadaDAL();
        private readonly CMP_SorteoSalaDAL _sorteoSalaDAL = new CMP_SorteoSalaDAL();
        private readonly CMP_SesionClienteDAL _sesionClienteDAL = new CMP_SesionClienteDAL();

        //Excalibur 
        private readonly CMP_SesionExcaDAL _sesionExcaDAL = new CMP_SesionExcaDAL();
        private readonly CMP_SesionSorteoSalaExcaDAL _sesionSorteoSalaExcaDAL = new CMP_SesionSorteoSalaExcaDAL();
        private readonly CMP_JugadaExcaDAL _jugadaExcaDAL = new CMP_JugadaExcaDAL();
        private readonly CMP_SorteoSalaExcaDAL _sorteoSalaExcaDAL = new CMP_SorteoSalaExcaDAL();
        private readonly CMP_SesionClienteExcaDAL _sesionClienteExcaDAL = new CMP_SesionClienteExcaDAL();

        private readonly SalaDAL _salaDAL = new SalaDAL();

        //Informacion Maquinas 
        private readonly Asignacion_M_TDAL _asignacionMTDAL = new Asignacion_M_TDAL();
        private readonly MarcasDAL _marcasDAL = new MarcasDAL();
        private readonly TCM_ModeloDAL _modeloDAL = new TCM_ModeloDAL();

        //Nuevo Sorteos
        private readonly SorteoSalaDAL _nuevoSorteoSalaDAL = new SorteoSalaDAL();
        private readonly SesionDAL _nuevoSesionDAL = new SesionDAL();
        private readonly SesionSorteoSalaDAL _nuevoSesionSorteoSalaDAL = new SesionSorteoSalaDAL();
        private readonly JugadaDAL _nuevoJugadaDAL = new JugadaDAL();

        [HttpPost]
        public IHttpActionResult DevolverDatos() {
            return Ok("datos");
        }
        [HttpPost]
        public IHttpActionResult RecepcionarDataMigracion(dynamic jsonData) {


            bool recepcionado = true;
            List<CMP_Sesion> listaSesiones = new List<CMP_Sesion>();
            List<CMP_SesionSorteoSala> listaDetalles = new List<CMP_SesionSorteoSala>();
            List<CMP_SorteoSala> listaSorteos = new List<CMP_SorteoSala>();
            int CodSala = 0;
            string NombreSala = string.Empty;
            int sesionesRegistradas = 0;
            try {
                dynamic items = jsonData;
                if(items.codSala != null) {
                    CodSala = items.codSala.ToObject<int>();
                }
                if(CodSala == 0) {
                    return Json(new { respuesta = false });
                }
                if(items.sesiones != null) {
                    listaSesiones = items.sesiones.ToObject<List<CMP_Sesion>>();
                }

                if(items.detalles != null) {
                    listaDetalles = items.detalles.ToObject<List<CMP_SesionSorteoSala>>();
                }
                if(items.nombreSala != null) {
                    NombreSala = items.nombreSala.ToObject<string>();
                }
                if(items.sorteosActivos != null) {
                    listaSorteos = items.sorteosActivos.ToObject<List<CMP_SorteoSala>>();
                }

                foreach(var item in listaSesiones) {
                    item.CodSala = CodSala;
                    int registrado = _sesionDAL.GuardarSesion(item);
                    if(registrado == -1) {
                        sesionesRegistradas++;
                        continue;
                    }
                    if(registrado > 0) {
                        sesionesRegistradas++;
                        var detallesSesion = listaDetalles.Where(x => x.SesionId == item.SesionId).ToList();
                        foreach(var det in detallesSesion) {
                            det.CodSala = CodSala;
                            int detalleRegistrado = _sesionSorteoSalaDAL.GuardarSesionSorteSala(det);

                            var jugada = det.Jugada;
                            jugada.JugadaId = det.JugadaId;
                            jugada.CodSala = CodSala;
                            int jugadaRegistrada = _jugadaDAL.GuardarJugada(jugada);
                        }
                    }

                }
                foreach(var item in listaSorteos) {
                    item.CodSala = CodSala;
                    int registrado = _sorteoSalaDAL.GuardarSorteoSala(item);
                }
                if(sesionesRegistradas != listaSesiones.Count) {
                    return Json(new { respuesta = false });
                }
                if(sesionesRegistradas > 0) {
                    var cabeceras = from sesion in listaSesiones
                                    group sesion by new {
                                        sesion.NroDocumento,
                                        sesion.NombreTipoDocumento,
                                        sesion.TipoDocumentoId
                                    } into grupo
                                    select new {
                                        NroDocumento = grupo.Key.NroDocumento,
                                        nombretipodocumento = grupo.Key.NombreTipoDocumento,
                                        tipodocumentoid = grupo.Key.TipoDocumentoId,
                                        CantidadSesiones = grupo.Count(),
                                        NombreCliente = grupo.Max(s => s.NombreCliente),
                                        Mail = grupo.Max(s => s.Mail),
                                        ClienteIdIas = grupo.Max(s => s.ClienteIdIas),
                                        PrimeraSesion = grupo.Min(s => s.FechaInicio),
                                        UltimaSesion = grupo.Max(s => s.FechaInicio)
                                    };

                    foreach(var cabecera in cabeceras) {
                        CMP_SesionCliente sesionClienteInsertar = new CMP_SesionCliente() {
                            NroDocumento = cabecera.NroDocumento,
                            NombreTipoDocumento = cabecera.nombretipodocumento,
                            TipoDocumentoId = cabecera.tipodocumentoid,
                            CantidadSesiones = cabecera.CantidadSesiones,
                            NombreCliente = cabecera.NombreCliente,
                            Mail = cabecera.Mail,
                            PrimeraSesion = cabecera.PrimeraSesion,
                            CodSala = CodSala,
                            UltimaSesion = cabecera.UltimaSesion
                        };
                        _sesionClienteDAL.GuardarSesionCliente(sesionClienteInsertar);
                    }
                }

                Sala sala = new Sala() {
                    CodSala = CodSala,
                    Nombre = NombreSala
                };

                _salaDAL.GuardarSala(sala);

                return Json(new { respuesta = true });
            } catch(Exception ex) {
                funciones.logueo("ERROR  - RecepcionarDataMigracion " + ex.Message, "Error");
                return Json(new { respuesta = false });
            }
        }
        [HttpPost]
        public IHttpActionResult RecepcionarDataMigracionExcaliburAnt(dynamic jsonData) {
            bool recepcionado = true;
            List<CMP_SesionExca> listaSesiones = new List<CMP_SesionExca>();
            List<CMP_SesionSorteoSalaExca> listaDetalles = new List<CMP_SesionSorteoSalaExca>();
            int sesionesRegistradas = 0;
            try {
                dynamic items = jsonData;
                if(items.sesiones != null) {
                    listaSesiones = items.sesiones.ToObject<List<CMP_SesionExca>>();
                }

                if(items.detalles != null) {
                    listaDetalles = items.detalles.ToObject<List<CMP_SesionSorteoSalaExca>>();
                }

                foreach(var item in listaSesiones) {
                    int registrado = _sesionExcaDAL.GuardarSesion(item);
                    if(registrado > 0) {
                        sesionesRegistradas++;
                        var detallesSesion = listaDetalles.Where(x => x.SesionId == item.SesionId).ToList();
                        foreach(var det in detallesSesion) {
                            int detalleRegistrado = _sesionSorteoSalaExcaDAL.GuardarSesionSorteSala(det);

                            var jugada = det.Jugada;
                            jugada.JugadaId = det.JugadaId;
                            int jugadaRegistrada = _jugadaExcaDAL.GuardarJugada(jugada);
                        }
                    }

                }
                if(sesionesRegistradas > 0) {
                    var cabeceras = from sesion in listaSesiones
                                    group sesion by new {
                                        sesion.NroDocumento,
                                    } into grupo
                                    select new {
                                        NroDocumento = grupo.Key.NroDocumento,
                                        CantidadSesiones = grupo.Count(),
                                        NombreCliente = grupo.Max(s => s.NombreCliente),
                                        Mail = grupo.Max(s => s.Mail),
                                        ClienteIdIas = grupo.Max(s => s.ClienteIdIas),
                                        PrimeraSesion = grupo.Min(s => s.FechaInicio),
                                    };

                    foreach(var cabecera in cabeceras) {
                        CMP_SesionClienteExca sesionClienteInsertar = new CMP_SesionClienteExca() {
                            NroDocumento = cabecera.NroDocumento,
                            CantidadSesiones = cabecera.CantidadSesiones,
                            NombreCliente = cabecera.NombreCliente,
                            Mail = cabecera.Mail,
                            PrimeraSesion = cabecera.PrimeraSesion,
                        };
                        _sesionClienteExcaDAL.GuardarSesionCliente(sesionClienteInsertar);
                    }
                }
                return Json(new { respuesta = true });
            } catch(Exception ex) {
                funciones.logueo("ERROR  - RecepcionarDataMigracionExcaliburAnt " + ex.Message, "Error");
                return Json(new { respuesta = false });
            }
        }
        [HttpPost]
        public IHttpActionResult RecepcionarDataMaquinas(dynamic jsonData) {
            List<Asignacion_M_T> listaMaquinas = new List<Asignacion_M_T>();
            List<Marcas> listaMarcas = new List<Marcas>();
            List<TCM_Modelo> listaModelos = new List<TCM_Modelo>();
            int CodSala = 0;
            try {
                dynamic items = jsonData;
                if(items.listaMaquinas != null) {
                    listaMaquinas = items.listaMaquinas.ToObject<List<Asignacion_M_T>>();
                }
                if(items.listaMarcas != null) {
                    listaMarcas = items.listaMarcas.ToObject<List<Marcas>>();
                }
                if(items.listaModelos != null) {
                    listaModelos = items.listaModelos.ToObject<List<TCM_Modelo>>();
                }
                if(items.codSala != null) {
                    CodSala = items.codSala.ToObject<int>();
                }
                foreach(var item in listaMaquinas) {
                    item.COD_SALA = Convert.ToString(CodSala);
                    var insertado = _asignacionMTDAL.GuardarAsignacionMT(item);
                }
                //foreach(var item in listaModelos)
                //{
                //    item.CodSala = CodSala;
                //    var insertado=_modeloDAL.GuardarModelos(item);
                //}
                //foreach(var item in listaMarcas)
                //{
                //    item.CodSala = CodSala;
                //    var insertado=_marcasDAL.GuardarMarcas(item);
                //}
                return Json(new { respuesta = true });
            } catch(Exception ex) {
                funciones.logueo("ERROR  - RecepcionarDataMaquinas " + ex.Message, "Error");
                return Json(new { respuesta = false });
            }
        }


        [HttpPost]
        public IHttpActionResult ObtenerFechaDeUltimoContadorPorCodSala(int codSala) {
            DateTime fecha = _contadoresDAL.ObtenerFechaDeUltimoContadorPorCodSala(codSala);
            var result = new { success = true, data = fecha };
            return Json(result);

        }

        [HttpPost]
        public IHttpActionResult GuardarContadoresOnline(List<ContadoresOnline> contadoresOnline) {
            bool success = _contadoresDAL.GuarGuardarContadoresOnline(contadoresOnline);
            string displayMessage = success ? $"{contadoresOnline.Count} contadores migrados correctamente." : "Error al migrar los Contadores.";
            var result = new { success, displayMessage };
            return Json(result);
        }
        #region WGDB_000
        [HttpPost]
        public IHttpActionResult AccountMovementsGetLastId(string databaseName) {
            try {
                var accountMovementsDal = new account_movements_dal(databaseName);
                var result = accountMovementsDal.GetLastIdInserted();
                return Json(result);
            } catch(Exception) {
                return Json(-1);
            }
        }
        [HttpPost]
        public IHttpActionResult AccountMovementsSave(dynamic jsonData) {
            try {
                List<account_movements> models = new List<account_movements>();
                string databaseName = string.Empty;
                dynamic items = jsonData;
                if(items.items != null) {
                    models = items.items.ToObject<List<account_movements>>();
                }
                if(items.databaseName != null) {
                    databaseName = items.databaseName.ToObject<string>();
                }
                models = models.OrderBy(x => x.am_account_id).ToList();
                var total = models.Count;
                var counter = 0;
                var accountMovementsDal = new account_movements_dal(databaseName);
                foreach(var item in models) {
                    var result = accountMovementsDal.SaveAccountMovements(item);
                    if(result == true) {
                        counter++;
                    }
                }
                if(counter == total)
                    return Json(true);
                else
                    return Json(false);
            } catch(Exception) {
                return Json(false);
            }
        }
        [HttpPost]
        public IHttpActionResult AccountOperationsGetLastId(string databaseName) {
            try {
                var accountOperationsDal = new account_operations_dal(databaseName);
                var result = accountOperationsDal.GetLastIdInserted();
                return Json(result);
            } catch(Exception) {
                return Json(-1);
            }
        }
        [HttpPost]
        public IHttpActionResult AccountOperationsSave(dynamic jsonData) {
            try {
                List<account_operations> models = new List<account_operations>();
                string databaseName = string.Empty;
                dynamic items = jsonData;
                if(items.items != null) {
                    models = items.items.ToObject<List<account_operations>>();
                }
                if(items.databaseName != null) {
                    databaseName = items.databaseName.ToObject<string>();
                }
                models = models.OrderBy(x => x.ao_account_id).ToList();
                var total = models.Count;
                var counter = 0;

                var accountOperationsDal = new account_operations_dal(databaseName);
                foreach(var item in models) {
                    var result = accountOperationsDal.SaveAccountOperations(item);
                    if(result == true) {
                        counter++;
                    }
                }
                if(counter == total)
                    return Json(true);
                else
                    return Json(false);
            } catch(Exception) {
                return Json(false);
            }
        }
        [HttpPost]
        public IHttpActionResult AccountPromotionsGetLastId(string databaseName) {
            try {
                var accountPromotionsDal = new account_promotions_dal(databaseName);
                var result = accountPromotionsDal.GetLastIdInserted();
                return Json(result);
            } catch(Exception) {
                return Json(-1);
            }
        }
        [HttpPost]
        public IHttpActionResult AccountPromotionsSave(dynamic jsonData) {
            try {
                List<account_promotions> models = new List<account_promotions>();
                string databaseName = string.Empty;
                dynamic items = jsonData;
                if(items.items != null) {
                    models = items.items.ToObject<List<account_promotions>>();
                }
                if(items.databaseName != null) {
                    databaseName = items.databaseName.ToObject<string>();
                }
                models = models.OrderBy(x => x.acp_account_id).ToList();
                var total = models.Count;
                var counter = 0;
                var accountPromotionsDal = new account_promotions_dal(databaseName);
                foreach(var item in models) {
                    var result = accountPromotionsDal.SaveAccountPromotions(item);
                    if(result == true) {
                        counter++;
                    }
                }
                if(counter == total)
                    return Json(true);
                else
                    return Json(false);
            } catch(Exception) {
                return Json(false);
            }
        }
        [HttpPost]
        public IHttpActionResult AccountsGetTotal(string databaseName) {
            try {
                var accountsDal = new accounts_dal(databaseName);
                var result = accountsDal.GetTotalAccounts();
                return Json(result);
            } catch(Exception) {
                return Json(-1);
            }
        }
        [HttpPost]
        public IHttpActionResult AccountsSave(dynamic jsonData) {
            try {
                List<accounts> models = new List<accounts>();
                string databaseName = string.Empty;
                dynamic items = jsonData;
                if(items.items != null) {
                    models = items.items.ToObject<List<accounts>>();
                }
                if(items.databaseName != null) {
                    databaseName = items.databaseName.ToObject<string>();
                }
                var total = models.Count;
                var counter = 0;
                var accountsDal = new accounts_dal(databaseName);
                foreach(var item in models) {
                    var result = accountsDal.SaveAccounts(item);
                    if(result == true) {
                        counter++;
                    }
                }
                if(counter == total)
                    return Json(true);
                else
                    return Json(false);
            } catch(Exception) {
                return Json(false);
            }
        }
        [HttpPost]
        public IHttpActionResult AreasGetLastId(string databaseName) {
            try {
                var areasDal = new areas_dal(databaseName);
                var result = areasDal.GetLastIdInserted();
                return Json(result);
            } catch(Exception) {
                return Json(-1);
            }
        }
        [HttpPost]
        public IHttpActionResult AreasSave(dynamic jsonData) {
            try {
                List<areas> models = new List<areas>();
                string databaseName = string.Empty;
                dynamic items = jsonData;
                if(items.items != null) {
                    models = items.items.ToObject<List<areas>>();
                }
                if(items.databaseName != null) {
                    databaseName = items.databaseName.ToObject<string>();
                }
                models = models.OrderBy(x => x.ar_area_id).ToList();
                var total = models.Count;
                var counter = 0;
                var areasDal = new areas_dal(databaseName);
                foreach(var item in models) {
                    var result = areasDal.SaveAreas(item);
                    if(result == true) {
                        counter++;
                    }
                }
                if(counter == total)
                    return Json(true);
                else
                    return Json(false);
            } catch(Exception) {
                return Json(false);
            }
        }
        [HttpPost]
        public IHttpActionResult BanksGetLastId(string databaseName) {
            try {
                var banksDal = new banks_dal(databaseName);
                var result = banksDal.GetLastIdInserted();
                return Json(result);
            } catch(Exception) {
                return Json(-1);
            }
        }
        [HttpPost]
        public IHttpActionResult BanksSave(dynamic jsonData) {
            try {
                List<banks> models = new List<banks>();
                string databaseName = string.Empty;
                dynamic items = jsonData;
                if(items.items != null) {
                    models = items.items.ToObject<List<banks>>();
                }
                if(items.databaseName != null) {
                    databaseName = items.databaseName.ToObject<string>();
                }
                models = models.OrderBy(x => x.bk_bank_id).ToList();
                var total = models.Count;
                var counter = 0;
                var banksDal = new banks_dal(databaseName);
                foreach(var item in models) {
                    var result = banksDal.SaveBanks(item);
                    if(result == true) {
                        counter++;
                    }
                }
                if(counter == total)
                    return Json(true);
                else
                    return Json(false);
            } catch(Exception) {
                return Json(false);
            }
        }
        [HttpPost]
        public IHttpActionResult CashierSessionsGetLastId(string databaseName) {
            try {
                var cashierSessionsDal = new cashier_sessions_dal(databaseName);
                var result = cashierSessionsDal.GetLastIdInserted();
                return Json(result);
            } catch(Exception) {
                return Json(-1);
            }
        }
        [HttpPost]
        public IHttpActionResult CashierSessionsSave(dynamic jsonData) {
            try {
                List<cashier_sessions> models = new List<cashier_sessions>();
                string databaseName = string.Empty;
                dynamic items = jsonData;
                if(items.items != null) {
                    models = items.items.ToObject<List<cashier_sessions>>();
                }
                if(items.databaseName != null) {
                    databaseName = items.databaseName.ToObject<string>();
                }
                models = models.OrderBy(x => x.cs_session_id).ToList();
                var total = models.Count;
                var counter = 0;
                var cashierSessionsDal = new cashier_sessions_dal(databaseName);
                foreach(var item in models) {
                    var result = cashierSessionsDal.SaveCashierSessions(item);
                    if(result == true) {
                        counter++;
                    }
                }
                if(counter == total)
                    return Json(true);
                else
                    return Json(false);
            } catch(Exception) {
                return Json(false);
            }
        }
        [HttpPost]
        public IHttpActionResult GeneralParamsGetTotal(string databaseName) {
            try {
                var generalParamsDal = new general_params_dal(databaseName);
                var result = generalParamsDal.GetTotalGeneralParams();
                return Json(result);
            } catch(Exception) {
                return Json(-1);
            }
        }
        [HttpPost]
        public IHttpActionResult GeneralParamsSave(dynamic jsonData) {
            try {
                List<general_params> models = new List<general_params>();
                string databaseName = string.Empty;
                dynamic items = jsonData;
                if(items.items != null) {
                    models = items.items.ToObject<List<general_params>>();
                }
                if(items.databaseName != null) {
                    databaseName = items.databaseName.ToObject<string>();
                }
                var total = models.Count;
                var counter = 0;

                var generalParamsDal = new general_params_dal(databaseName);
                foreach(var item in models) {
                    var result = generalParamsDal.SaveGeneralParams(item);
                    if(result == true) {
                        counter++;
                    }
                }
                if(counter == total)
                    return Json(true);
                else
                    return Json(false);
            } catch(Exception) {
                return Json(false);
            }
        }
        [HttpPost]
        public IHttpActionResult GiftInstancesGetLastId(string databaseName) {
            try {
                var giftInstancesDal = new gift_instances_dal(databaseName);
                var result = giftInstancesDal.GetLastIdInserted();
                return Json(result);
            } catch(Exception) {
                return Json(-1);
            }
        }
        [HttpPost]
        public IHttpActionResult GiftInstancesSave(dynamic jsonData) {
            try {
                List<gift_instances> models = new List<gift_instances>();
                string databaseName = string.Empty;
                dynamic items = jsonData;
                if(items.items != null) {
                    models = items.items.ToObject<List<gift_instances>>();
                }
                if(items.databaseName != null) {
                    databaseName = items.databaseName.ToObject<string>();
                }
                models = models.OrderBy(x => x.gin_gift_instance_id).ToList();
                var total = models.Count;
                var counter = 0;
                var giftInstancesDal = new gift_instances_dal(databaseName);
                foreach(var item in models) {
                    var result = giftInstancesDal.SaveGiftInstances(item);
                    if(result == true) {
                        counter++;
                    }
                }
                if(counter == total)
                    return Json(true);
                else
                    return Json(false);
            } catch(Exception) {
                return Json(false);
            }
        }
        [HttpPost]
        public IHttpActionResult GuiUsersGetTotal(string databaseName) {
            try {
                var guiUsersDal = new gui_users_dal(databaseName);
                var result = guiUsersDal.GetTotalGuiUsers();
                return Json(result);
            } catch(Exception) {
                return Json(-1);
            }
        }
        [HttpPost]
        public IHttpActionResult GuiUsersSave(dynamic jsonData) {
            try {
                List<gui_users> models = new List<gui_users>();
                string databaseName = string.Empty;
                dynamic items = jsonData;
                if(items.items != null) {
                    models = items.items.ToObject<List<gui_users>>();
                }
                if(items.databaseName != null) {
                    databaseName = items.databaseName.ToObject<string>();
                }
                var total = models.Count;
                var counter = 0;
                var guiUsersDal = new gui_users_dal(databaseName);
                foreach(var item in models) {
                    var result = guiUsersDal.SaveGuiUsers(item);
                    if(result == true) {
                        counter++;
                    }

                }
                if(counter == total)
                    return Json(true);
                else
                    return Json(false);
            } catch(Exception) {
                return Json(false);
            }
        }
        [HttpPost]
        public IHttpActionResult MobileBanksGetLastId(string databaseName) {
            try {
                var mobileBanksDal = new mobile_banks_dal(databaseName);
                var result = mobileBanksDal.GetLastIdInserted();
                return Json(result);
            } catch(Exception) {
                return Json(-1);
            }
        }
        [HttpPost]
        public IHttpActionResult MobileBanksSave(dynamic jsonData) {
            try {
                List<mobile_banks> models = new List<mobile_banks>();
                string databaseName = string.Empty;
                dynamic items = jsonData;
                if(items.items != null) {
                    models = items.items.ToObject<List<mobile_banks>>();
                }
                if(items.databaseName != null) {
                    databaseName = items.databaseName.ToObject<string>();
                }
                models = models.OrderBy(x => x.mb_account_id).ToList();
                var total = models.Count;
                var counter = 0;

                var mobileBanksDal = new mobile_banks_dal(databaseName);
                foreach(var item in models) {
                    var result = mobileBanksDal.SaveMobileBanks(item);
                    if(result == true) {
                        counter++;
                    }
                }
                if(counter == total)
                    return Json(true);
                else
                    return Json(false);
            } catch(Exception) {
                return Json(false);
            }
        }
        [HttpPost]
        public IHttpActionResult PlaySessionsGetLastId(string databaseName) {
            try {
                var playSessionsDal = new play_sessions_dal(databaseName);
                var result = playSessionsDal.GetLastIdInserted();
                return Json(result);
            } catch(Exception) {
                return Json(-1);
            }
        }
        [HttpPost]
        public IHttpActionResult PlaySessionsSave(dynamic jsonData) {
            try {
                List<play_sessions> models = new List<play_sessions>();
                string databaseName = string.Empty;
                dynamic items = jsonData;
                if(items.items != null) {
                    models = items.items.ToObject<List<play_sessions>>();
                }
                if(items.databaseName != null) {
                    databaseName = items.databaseName.ToObject<string>();
                }
                models = models.OrderBy(x => x.ps_play_session_id).ToList();
                var total = models.Count;
                var counter = 0;
                var playSessionsDal = new play_sessions_dal(databaseName);
                foreach(var item in models) {
                    var result = playSessionsDal.SavePlaySessions(item);
                    if(result == true) {
                        counter++;
                    }
                }
                if(counter == total)
                    return Json(true);
                else
                    return Json(false);
            } catch(Exception) {
                return Json(false);
            }
        }
        [HttpPost]
        public IHttpActionResult PromoGamesGetLastId(string databaseName) {
            try {
                var promogamesDal = new promogames_dal(databaseName);
                var result = promogamesDal.GetLastIdInserted();
                return Json(result);
            } catch(Exception) {
                return Json(-1);
            }
        }
        [HttpPost]
        public IHttpActionResult PromoGamesSave(dynamic jsonData) {
            try {
                List<promogames> models = new List<promogames>();
                string databaseName = string.Empty;
                dynamic items = jsonData;
                if(items.items != null) {
                    models = items.items.ToObject<List<promogames>>();
                }
                if(items.databaseName != null) {
                    databaseName = items.databaseName.ToObject<string>();
                }
                models = models.OrderBy(x => x.pg_id).ToList();
                var total = models.Count;
                var counter = 0;
                var promogamesDal = new promogames_dal(databaseName);
                foreach(var item in models) {
                    var result = promogamesDal.SavePromoGames(item);
                    if(result == true) {
                        counter++;
                    }
                }
                if(counter == total)
                    return Json(true);
                else
                    return Json(false);
            } catch(Exception) {
                return Json(false);
            }
        }
        [HttpPost]
        public IHttpActionResult PromotionsGetLastId(string databaseName) {
            try {
                var promotionsDal = new promotions_dal(databaseName);
                var result = promotionsDal.GetLastIdInserted();
                return Json(result);
            } catch(Exception) {
                return Json(-1);
            }
        }
        [HttpPost]
        public IHttpActionResult PromotionsSave(dynamic jsonData) {
            try {
                List<promotions> models = new List<promotions>();
                string databaseName = string.Empty;
                dynamic items = jsonData;
                if(items.items != null) {
                    models = items.items.ToObject<List<promotions>>();
                }
                if(items.databaseName != null) {
                    databaseName = items.databaseName.ToObject<string>();
                }
                models = models.OrderBy(x => x.pm_promotion_id).ToList();
                var total = models.Count;
                var counter = 0;
                var promotionsDal = new promotions_dal(databaseName);
                foreach(var item in models) {
                    var result = promotionsDal.SavePromotions(item);
                    if(result == true) {
                        counter++;
                    }
                }
                if(counter == total)
                    return Json(true);
                else
                    return Json(false);
            } catch(Exception) {
                return Json(false);
            }
        }
        [HttpPost]
        public IHttpActionResult TerminalsGetLastId(string databaseName) {
            try {
                var terminalsDal = new terminals_dal(databaseName);
                var result = terminalsDal.GetLastIdInserted();
                return Json(result);
            } catch(Exception) {
                return Json(-1);
            }
        }
        [HttpPost]
        public IHttpActionResult TerminalsSave(dynamic jsonData) {
            try {
                List<terminals> models = new List<terminals>();
                string databaseName = string.Empty;
                dynamic items = jsonData;
                if(items.items != null) {
                    models = items.items.ToObject<List<terminals>>();
                }
                if(items.databaseName != null) {
                    databaseName = items.databaseName.ToObject<string>();
                }
                models = models.OrderBy(x => x.te_terminal_id).ToList();
                var total = models.Count;
                var counter = 0;
                var terminalsDal = new terminals_dal(databaseName);
                foreach(var item in models) {
                    var result = terminalsDal.SaveTerminals(item);
                    if(result == true) {
                        counter++;
                    }
                }
                if(counter == total)
                    return Json(true);
                else
                    return Json(false);
            } catch(Exception) {
                return Json(false);
            }
        }
        [HttpPost]
        public IHttpActionResult VenuesGetLastId(string databaseName) {
            try {
                var venuesDal = new venues_dal(databaseName);
                var result = venuesDal.GetLastIdInserted();
                return Json(result);
            } catch(Exception) {
                return Json(-1);
            }
        }
        [HttpPost]
        public IHttpActionResult VenuesSave(dynamic jsonData) {
            try {
                List<venues> models = new List<venues>();
                string databaseName = string.Empty;
                dynamic items = jsonData;
                if(items.items != null) {
                    models = items.items.ToObject<List<venues>>();
                }
                if(items.databaseName != null) {
                    databaseName = items.databaseName.ToObject<string>();
                }
                models = models.OrderBy(x => x.ve_venue_id).ToList();
                var total = models.Count;
                var counter = 0;
                var venuesDal = new venues_dal(databaseName);
                foreach(var item in models) {
                    var result = venuesDal.SaveVenues(item);
                    if(result == true) {
                        counter++;
                    }
                }

                if(counter == total)
                    return Json(true);
                else
                    return Json(false);
            } catch(Exception) {
                return Json(false);
            }
        }
        #endregion
        [HttpPost]
        public IHttpActionResult NuevoRecepcionarDataMigracion2(dynamic jsonData) {


            bool recepcionado = true;
            List<Sesion> listaSesiones = new List<Sesion>();
            List<SesionSorteoSala> listaDetalles = new List<SesionSorteoSala>();
            List<SorteoSala> listaSorteos = new List<SorteoSala>();
            int CodSala = 0;
            string NombreSala = string.Empty;
            int sesionesRegistradas = 0;
            try {
                dynamic items = jsonData;
                if(items.codSala != null) {
                    CodSala = items.codSala.ToObject<int>();
                }
                if(items.sesiones != null) {
                    listaSesiones = items.sesiones.ToObject<List<Sesion>>();
                }

                if(items.detalles != null) {
                    listaDetalles = items.detalles.ToObject<List<SesionSorteoSala>>();
                }
                if(items.nombreSala != null) {
                    NombreSala = items.nombreSala.ToObject<string>();
                }
                if(items.sorteosActivos != null) {
                    listaSorteos = items.sorteosActivos.ToObject<List<SorteoSala>>();
                }
                foreach(var item in listaSesiones) {
                    item.CodSala = CodSala;
                    int registrado = _nuevoSesionDAL.GuardarSesion(item);
                    if(registrado > 0) {
                        sesionesRegistradas++;
                        var detallesSesion = listaDetalles.Where(x => x.SesionId == item.SesionId).ToList();
                        foreach(var det in detallesSesion) {
                            det.CodSala = CodSala;
                            int detalleRegistrado = _nuevoSesionSorteoSalaDAL.GuardarSesionSorteSala(det);

                            var jugada = det.Jugada;
                            jugada.JugadaId = det.JugadaId;
                            jugada.CodSala = CodSala;
                            int jugadaRegistrada = _nuevoJugadaDAL.GuardarJugada(jugada);
                        }
                    }

                }
                foreach(var item in listaSorteos) {
                    item.CodSala = CodSala;
                    int registrado = _nuevoSorteoSalaDAL.GuardarSorteoSala(item);
                }

                //if(sesionesRegistradas > 0)
                //{
                //    var cabeceras = from sesion in listaSesiones
                //                    group sesion by new
                //                    {
                //                        sesion.NroDocumento,
                //                        sesion.NombreTipoDocumento,
                //                        sesion.TipoDocumentoId
                //                    } into grupo
                //                    select new
                //                    {
                //                        NroDocumento = grupo.Key.NroDocumento,
                //                        nombretipodocumento = grupo.Key.NombreTipoDocumento,
                //                        tipodocumentoid = grupo.Key.TipoDocumentoId,
                //                        CantidadSesiones = grupo.Count(),
                //                        NombreCliente = grupo.Max(s => s.NombreCliente),
                //                        Mail = grupo.Max(s => s.Mail),
                //                        ClienteIdIas = grupo.Max(s => s.ClienteIdIas),
                //                        PrimeraSesion = grupo.Min(s => s.FechaInicio),
                //                        UltimaSesion = grupo.Max(s => s.FechaInicio)
                //                    };

                //    foreach(var cabecera in cabeceras)
                //    {
                //        CMP_SesionCliente sesionClienteInsertar = new CMP_SesionCliente()
                //        {
                //            NroDocumento = cabecera.NroDocumento,
                //            NombreTipoDocumento = cabecera.nombretipodocumento,
                //            TipoDocumentoId = cabecera.tipodocumentoid,
                //            CantidadSesiones = cabecera.CantidadSesiones,
                //            NombreCliente = cabecera.NombreCliente,
                //            Mail = cabecera.Mail,
                //            PrimeraSesion = Convert.ToDateTime(cabecera.PrimeraSesion),
                //            CodSala = CodSala,
                //            UltimaSesion = Convert.ToDateTime(cabecera.UltimaSesion)
                //        };
                //        _sesionClienteDAL.GuardarSesionCliente(sesionClienteInsertar);
                //    }
                //}

                Sala sala = new Sala() {
                    CodSala = CodSala,
                    Nombre = NombreSala
                };

                _salaDAL.GuardarSala(sala);

                return Json(new { respuesta = true });
            } catch(Exception ex) {
                funciones.logueo("ERROR  - RecepcionarDataMigracion " + ex.Message, "Error");
                return Json(new { respuesta = false });
            }
        }
        [HttpPost]
        public IHttpActionResult NuevoRecepcionarDataMigracion(dynamic jsonData) {
            bool respuesta = false;
            List<Sesion> listaSesiones = new List<Sesion>();
            List<SesionSorteoSala> listaDetalles = new List<SesionSorteoSala>();
            List<SorteoSala> listaSorteos = new List<SorteoSala>();
            int CodSala = 0;
            string NombreSala = string.Empty;
            int sesionesRegistradas = 0;
            try {
                dynamic items = jsonData;
                if(items.codSala != null) {
                    CodSala = items.codSala.ToObject<int>();
                }
                if(CodSala == 0) {
                    return Json(new { respuesta = false });
                }
                if(items.sesiones != null) {
                    listaSesiones = items.sesiones.ToObject<List<Sesion>>();
                }

                if(items.detalles != null) {
                    listaDetalles = items.detalles.ToObject<List<SesionSorteoSala>>();
                }
                if(items.nombreSala != null) {
                    NombreSala = items.nombreSala.ToObject<string>();
                }
                if(items.sorteosActivos != null) {
                    listaSorteos = items.sorteosActivos.ToObject<List<SorteoSala>>();
                }
                foreach(var item in listaSesiones) {
                    item.CodSala = CodSala;
                    var existeSesion = _nuevoSesionDAL.ExisteSesion(item.SesionId, CodSala);
                    if(existeSesion == 1) {
                        //La sesion se encuentra en base de datos de dw, borrar la data y volverla a insertar
                        _nuevoSesionDAL.EliminarTodoPorSesionYSala(item.SesionId, CodSala);
                    }
                    int registrado = _nuevoSesionDAL.GuardarSesionSiNoExiste(item);
                    if(registrado > 0) {

                        var detalles = listaDetalles.Where(x => x.SesionId == item.SesionId).ToList();

                        foreach(var det in detalles) {
                            det.Jugada.JugadaId = det.JugadaId;
                            det.Jugada.CodSala = CodSala;
                            det.CodSala = CodSala;
                        }

                        //var detallesSesion = detalles.Select(x=>new SesionSorteoSala {
                        //    SesionId = x.SesionId,
                        //    SorteoId = x.SorteoId,
                        //    JugadaId = x.JugadaId,
                        //    CantidadCupones = x.CantidadCupones,
                        //    FechaRegistro = x.FechaRegistro,
                        //    SerieIni = x.SerieIni,
                        //    SerieFin = x.SerieFin,
                        //    NombreSorteo = x.NombreSorteo,
                        //    CondicionWin = x.CondicionWin,
                        //    WinCalculado = x.WinCalculado,
                        //    CondicionBet = x.CondicionBet,
                        //    BetCalculado = x.BetCalculado,
                        //    TopeCuponesxJugada = x.TopeCuponesxJugada,
                        //    ParametrosImpresion = x.ParametrosImpresion,
                        //    Factor = x.Factor,
                        //    DescartePorFactor = x.DescartePorFactor,
                        //    CodSala = CodSala
                        //}).ToList();

                        bool respuestaSesionSorteoSala = _nuevoSesionSorteoSalaDAL.GuardarSesionSorteSalaBulk(detalles);
                        if(respuestaSesionSorteoSala == false) {
                            _nuevoSesionDAL.EliminarTodoPorSesionYSala(item.SesionId, CodSala);
                            continue;
                        }

                        var jugadas = detalles.Select(x => x.Jugada).ToList();

                        // Eliminar duplicados basándose en JugadaId (dejando el primero de cada duplicado)
                        var jugadasSinDuplicados = jugadas
                            .GroupBy(j => j.JugadaId)
                            .Select(g => g.First()) // Selecciona el primer elemento de cada grupo
                            .ToList();

                        bool respuestaJugadas = _nuevoJugadaDAL.GuardarJugadaBulk(jugadasSinDuplicados);
                        if(respuestaJugadas == false) {
                            _nuevoSesionDAL.EliminarTodoPorSesionYSala(item.SesionId, CodSala);
                            continue;
                        }
                        sesionesRegistradas++;
                    }

                }
                foreach(var item in listaSorteos) {
                    item.CodSala = CodSala;
                    int registrado = _nuevoSorteoSalaDAL.GuardarSorteoSala(item);
                }

                Sala sala = new Sala() {
                    CodSala = CodSala,
                    Nombre = NombreSala
                };

                _salaDAL.GuardarSala(sala);
                if(sesionesRegistradas == listaSesiones.Count) {
                    respuesta = true;
                }

                return Json(new { respuesta = respuesta });
            } catch(Exception ex) {
                funciones.logueo("ERROR  - RecepcionarDataMigracion " + ex.Message, "Error");
                return Json(new { respuesta = respuesta });
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> SincronizarLudopatasMincetur() {
            try {
                SincronizacionLudopatasMinceturJob job = new SincronizacionLudopatasMinceturJob();
                await job.Execute(null);
                return Ok(new { success = true, message = "Sincronización de ludopatas MINCETUR ejecutada manualmente." });
            } catch(Exception ex) {
                funciones.logueo($"Error ejecutando sincronización manual. Exception: {ex.Message} {Environment.NewLine}StackTrace: {ex.StackTrace}");
                return InternalServerError(ex);
            }
        }
    }
}


