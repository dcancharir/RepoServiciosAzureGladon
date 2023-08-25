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
    public class maquinas_auditDAL
    {
        private string _conexion = string.Empty;
        public maquinas_auditDAL()
        {
            _conexion = ConfigurationManager.ConnectionStrings["connectionBD"].ConnectionString;
        }
        public int InsertarMaquinasAudit(maquinas_audit obj)
        {
            int idInsertado = 0;
            string consulta = @"
IF EXISTS (SELECT * FROM [dbo].[maquinas_audit] (nolock) WHERE maquinas_audit_id_ias = @maquinas_audit_id_ias)
        BEGIN
           SELECT 1 
        END
        ELSE
        BEGIN

INSERT INTO [maquinas_audit]
           ([maquinas_audit_id_ias],[id_audit]
           ,[fecha_hora]
           ,[id_maquina]
           ,[fecha_ultimo_ingreso]
           ,[marca]
           ,[cod_maquina]
           ,[serie]
           ,[marca_modelo]
           ,[isla]
           ,[zona]
           ,[tipo_maquina]
           ,[id_sala]
           ,[operacion]
           ,[posicion]
           ,[estado_maquina]
           ,[juego])
     Output Inserted.maquinas_audit_id
     VALUES
           (@maquinas_audit_id_ias,@id_audit
           ,@fecha_hora
           ,@id_maquina
           ,@fecha_ultimo_ingreso
           ,@marca
           ,@cod_maquina
           ,@serie
           ,@marca_modelo
           ,@isla
           ,@zona
           ,@tipo_maquina
           ,@id_sala
           ,@operacion
           ,@posicion
           ,@estado_maquina
           ,@juego)
end
";
            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@maquinas_audit_id_ias", obj.maquinas_audit_id_ias);
                    query.Parameters.AddWithValue("@id_audit", obj.id_audit);
                    query.Parameters.AddWithValue("@fecha_hora", obj.fecha_hora);
                    query.Parameters.AddWithValue("@id_maquina", obj.id_maquina);
                    query.Parameters.AddWithValue("@fecha_ultimo_ingreso", obj.fecha_ultimo_ingreso);
                    query.Parameters.AddWithValue("@marca", obj.marca);
                    query.Parameters.AddWithValue("@cod_maquina", obj.cod_maquina);
                    query.Parameters.AddWithValue("@serie", obj.serie);
                    query.Parameters.AddWithValue("@marca_modelo", obj.marca_modelo);
                    query.Parameters.AddWithValue("@isla", obj.isla);
                    query.Parameters.AddWithValue("@zona", obj.zona);
                    query.Parameters.AddWithValue("@tipo_maquina", obj.tipo_maquina);
                    query.Parameters.AddWithValue("@id_sala", obj.id_sala);
                    query.Parameters.AddWithValue("@operacion", obj.operacion);
                    query.Parameters.AddWithValue("@estado_maquina", obj.estado_maquina);
                    query.Parameters.AddWithValue("@juego", obj.juego);
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
