using ServicioMigracionClientes.Clases;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioMigracionClientes.DAL
{
    public class detalle_maquinas_auditDAL
    {
        private string _conexion = string.Empty;
        public detalle_maquinas_auditDAL()
        {
            _conexion = ConfigurationManager.ConnectionStrings["connectionBD"].ConnectionString;
        }
        public int InsertarDetalleMaquinasAudit(detalle_maquinas_audit obj)
        {
            int idInsertado = 0;
            string consulta = @"
IF EXISTS (SELECT * FROM [dbo].[detalle_maquinas_audit] (nolock) WHERE detalle_maquinas_audit_id_ias = @detalle_maquinas_audit_id_ias)
        BEGIN
           SELECT 1 
        END
        ELSE
        BEGIN
INSERT INTO [detalle_maquinas_audit]
           ([detalle_maquinas_audit_id_ias],[id_audit]
           ,[fecha_hora]
           ,[marca_modelo]
           ,[cod_maquina]
           ,[serie]
           ,[modelo_comercial]
           ,[tipo_maquina]
           ,[progresivo]
           ,[juego]
           ,[propietario]
           ,[tipo_contrato]
           ,[tipo_sistema]
           ,[propiedad]
           ,[operacion])
     Output Inserted.detalle_maquinas_audit_id
     VALUES
           (@detalle_maquinas_audit_id_ias,@id_audit
           ,@fecha_hora
           ,@marca_modelo
           ,@cod_maquina
           ,@serie
           ,@modelo_comercial
           ,@tipo_maquina
           ,@progresivo
           ,@juego
           ,@propietario
           ,@tipo_contrato
           ,@tipo_sistema
           ,@propiedad
           ,@operacion)

end";
            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@detalle_maquinas_audit_id_ias", obj.detalle_maquinas_audit_id_ias);
                    query.Parameters.AddWithValue("@id_audit", obj.id_audit);
                    query.Parameters.AddWithValue("@fecha_hora", obj.fecha_hora);
                    query.Parameters.AddWithValue("@marca_modelo", obj.marca_modelo);
                    query.Parameters.AddWithValue("@cod_maquina", obj.cod_maquina);
                    query.Parameters.AddWithValue("@serie", obj.serie);
                    query.Parameters.AddWithValue("@modelo_comercial", obj.modelo_comercial);
                    query.Parameters.AddWithValue("@tipo_maquina", obj.tipo_maquina);
                    query.Parameters.AddWithValue("@progresivo", obj.progresivo);
                    query.Parameters.AddWithValue("@juego", obj.juego);
                    query.Parameters.AddWithValue("@propietario", obj.propietario);
                    query.Parameters.AddWithValue("@tipo_contrato", obj.tipo_contrato);
                    query.Parameters.AddWithValue("@tipo_sistema", obj.tipo_sistema);
                    query.Parameters.AddWithValue("@propiedad", obj.propiedad);
                    query.Parameters.AddWithValue("@operacion", obj.operacion);
                    idInsertado = Convert.ToInt32(query.ExecuteScalar());

                }
            }
            catch (Exception ex)
            {
                idInsertado = 0;
            }

            return idInsertado;
        }
    }
}
