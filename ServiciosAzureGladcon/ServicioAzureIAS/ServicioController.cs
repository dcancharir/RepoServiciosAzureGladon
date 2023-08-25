using ServicioAzureIAS.Clases.GladconData;
using ServicioAzureIAS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ServicioAzureIAS
{
    public class ServicioController : ApiController
    {

        consolidado_deleteDAL consolidado_deleteDAL = new consolidado_deleteDAL();
        consolidado_tmpDAL consolidado_tmpDAL = new consolidado_tmpDAL();
        consolidadoDAL consolidadoDAL = new consolidadoDAL();
        detalle_maquinaDAL detalle_maquinaDAL = new detalle_maquinaDAL();
        detalle_maquinas_auditDAL detalle_maquinas_auditDAL = new detalle_maquinas_auditDAL();
        maquinaDAL maquinaDAL = new maquinaDAL();
        maquinas_auditDAL maquinas_auditDAL = new maquinas_auditDAL();
        salaDAL salaDAL = new salaDAL();

        /// METODOS DEL SERVIDOR
        [HttpPost]
        public dynamic DevolverDatos()
        {
            return "datos";
        }

        [HttpPost]
        public dynamic DevolverEstado()
        {
            return true;
        }


        [HttpPost]
        public dynamic RecepcionarGladconData(dynamic jsonData)
        {

            bool recepcionado = true;

            try
            {

                dynamic items = jsonData;
                if (items.consolidados != null)
                {
                    List<consolidado> consolidados = items.consolidados.ToObject<List<consolidado>>();
                    if (consolidados != null)
                    {
                        foreach (var item in consolidados)
                        {
                            int insertado = consolidadoDAL.InsertarConsolidado(item);

                            if (insertado == -1)
                            {
                                return "Error al insertar en la tabla consolidado";
                            }

                        }
                    }
                }
                if (items.consolidados_delete != null)
                {
                    List<consolidado_delete> consolidados_delete = items.consolidados_delete.ToObject<List<consolidado_delete>>();
                    if (consolidados_delete != null)
                    {
                        foreach (var item in consolidados_delete)
                        {
                            int insertado = consolidado_deleteDAL.InsertarConsolidadoDelete(item);

                            if (insertado == -1)
                            {
                                return "Error al insertar en la tabla consolidado";
                            }

                        }
                    }
                }
                if (items.consolidados_tmp != null)
                {
                    List<consolidado_tmp> consolidados_tmp = items.consolidados_tmp.ToObject<List<consolidado_tmp>>();
                    if (consolidados_tmp != null)
                    {
                        foreach (var item in consolidados_tmp)
                        {
                            int insertado = consolidado_tmpDAL.InsertarConsolidadoTMP(item);

                            if (insertado == -1)
                            {
                                return "Error al insertar en la tabla consolidado";
                            }

                        }
                    }
                }
                if (items.detalles_maquinas != null)
                {
                    List<detalle_maquina> detalles_maquinas = items.detalles_maquinas.ToObject<List<detalle_maquina>>();
                    if (detalles_maquinas != null)
                    {
                        foreach (var item in detalles_maquinas)
                        {
                            int insertado = detalle_maquinaDAL.InsertarDetalleMaquina(item);

                            if (insertado == -1)
                            {
                                return "Error al insertar en la tabla consolidado";
                            }

                        }
                    }
                }
                if (items.detalles_maquinas_audit != null)
                {
                    List<detalle_maquinas_audit> detalles_maquinas_audit = items.detalles_maquinas_audit.ToObject<List<detalle_maquinas_audit>>();
                    if (detalles_maquinas_audit != null)
                    {
                        foreach (var item in detalles_maquinas_audit)
                        {
                            int insertado = detalle_maquinas_auditDAL.InsertarDetalleMaquinasAudit(item);

                            if (insertado == -1)
                            {
                                return "Error al insertar en la tabla consolidado";
                            }

                        }
                    }
                }
                if (items.maquinas != null)
                {
                    List<maquina> maquinas = items.maquinas.ToObject<List<maquina>>();
                    if (maquinas != null)
                    {
                        foreach (var item in maquinas)
                        {
                            int insertado = maquinaDAL.InsertarMaquina(item);

                            if (insertado == -1)
                            {
                                return "Error al insertar en la tabla consolidado";
                            }

                        }
                    }
                }
                if (items.maquinas_audit != null)
                {
                    List<maquinas_audit> maquinas_audit = items.maquinas_audit.ToObject<List<maquinas_audit>>();
                    if (maquinas_audit != null)
                    {
                        foreach (var item in maquinas_audit)
                        {
                            int insertado = maquinas_auditDAL.InsertarMaquinasAudit(item);

                            if (insertado == -1)
                            {
                                return "Error al insertar en la tabla consolidado";
                            }

                        }
                    }
                }
                if (items.salas != null)
                {
                    List<sala> salas = items.salas.ToObject<List<sala>>();
                    if (salas != null)
                    {
                        foreach (var item in salas)
                        {
                            int insertado = salaDAL.InsertarSala(item);

                            if (insertado == -1)
                            {
                                return "Error al insertar en la tabla consolidado";
                            }

                        }
                    }
                }

                



            }catch(Exception ex)
            {
                recepcionado = false;
            }





            return recepcionado;
        }

        #region
        [HttpGet]
        public IHttpActionResult ListarConsolidado([FromUri] int Id)
        {
            try
            { 
                var result= consolidadoDAL.ListarConsolidado(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public IHttpActionResult ListarConsolidadoDelete([FromUri] int Id)
        {
            try
            {
                var result = consolidado_deleteDAL.ListarConsolidadoDelete(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public IHttpActionResult ListarConsolidadoTmp([FromUri] int Id)
        {
            try
            {
                var result = consolidado_tmpDAL.ListarConsolidadoTMP(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public IHttpActionResult ListarDetalleMaquina([FromUri] int Id)
        {
            try
            {
                var result = detalle_maquinaDAL.ListarDetalleMaquina(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public IHttpActionResult ListarDetalleMaquinasAudit([FromUri] int Id)
        {
            try
            {
                var result = detalle_maquinas_auditDAL.ListarDetalleMaquinasAudit(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public IHttpActionResult ListarMaquina([FromUri] int Id)
        {
            try
            {
                var result = maquinaDAL.ListarMaquina(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public IHttpActionResult ListarMaquinasAudit([FromUri] int Id)
        {
            try
            {
                var result = maquinas_auditDAL.ListarMaquinasAudit(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public IHttpActionResult ListarSala()
        {
            try
            {
                var result = salaDAL.ListarSala();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
