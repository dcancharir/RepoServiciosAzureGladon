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
    public class detalle_maquinaDAL
    {
        private string _conexion = string.Empty;
        public detalle_maquinaDAL()
        {
            _conexion = ConfigurationManager.ConnectionStrings["connectionBD"].ConnectionString;
        }
        public int InsertarDetalleMaquina(detalle_maquina obj)
        {
            int idInsertado = 0;
            string consulta = @"
 IF EXISTS (SELECT * FROM [dbo].[detalle_maquina] (nolock) WHERE detalle_maquina_id_ias = @detalle_maquina_id_ias)
        BEGIN
           SELECT 1 
        END
        ELSE
        BEGIN
INSERT INTO [detalle_maquina]
           ([detalle_maquina_id_ias],[id]
           ,[serie]
           ,[marca_modelo]
           ,[tipo_sistema]
           ,[progresivo]
           ,[modelo_comercial]
           ,[codigo_modelo]
           ,[juego]
           ,[propietario]
           ,[propiedad]
           ,[tipo_contrato]
           ,[tipo_maquina]
           ,[cod_maquina])
     Output Inserted.detalle_maquina_id
     VALUES
           (@detalle_maquina_id_ias,@id
           ,@serie
           ,@marca_modelo
           ,@tipo_sistema
           ,@progresivo
           ,@modelo_comercial
           ,@codigo_modelo
           ,@juego
           ,@propietario
           ,@propiedad
           ,@tipo_contrato
           ,@tipo_maquina
           ,@cod_maquina)
end";
            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@detalle_maquina_id_ias", obj.detalle_maquina_id_ias);
                    query.Parameters.AddWithValue("@id", obj.id);
                    query.Parameters.AddWithValue("@serie", obj.serie);
                    query.Parameters.AddWithValue("@marca_modelo", obj.marca_modelo);
                    query.Parameters.AddWithValue("@tipo_sistema", obj.tipo_sistema);
                    query.Parameters.AddWithValue("@progresivo", obj.progresivo);
                    query.Parameters.AddWithValue("@modelo_comercial", obj.modelo_comercial);
                    query.Parameters.AddWithValue("@codigo_modelo", obj.codigo_modelo);
                    query.Parameters.AddWithValue("@juego", obj.juego);
                    query.Parameters.AddWithValue("@propietario", obj.propietario);
                    query.Parameters.AddWithValue("@propiedad", obj.propiedad);
                    query.Parameters.AddWithValue("@tipo_contrato", obj.tipo_contrato);
                    query.Parameters.AddWithValue("@tipo_maquina", obj.tipo_maquina);
                    query.Parameters.AddWithValue("@cod_maquina", obj.cod_maquina);
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
