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
    internal class maquinaDAL
    {
        private string _conexion = string.Empty;
        public maquinaDAL()
        {
            _conexion = ConfigurationManager.ConnectionStrings["connectionBD"].ConnectionString;
        }
        public int InsertarMaquina(maquina obj)
        {
            int idInsertado = 0;
            string consulta = @"
IF EXISTS (SELECT * FROM [dbo].[maquina] (nolock) WHERE maquina_id_ias = @maquina_id_ias)
        BEGIN
           SELECT 1 
        END
        ELSE
        BEGIN
INSERT INTO [maquina]
           ([maquina_id_ias],[id_maquina]
           ,[fecha_ultimo_ingreso]
           ,[marca]
           ,[cod_maquina]
           ,[serie]
           ,[marca_modelo]
           ,[isla]
           ,[zona]
           ,[tipo_maquina]
           ,[id_sala]
           ,[juego]
           ,[estado_maquina]
           ,[posicion])
     Output Inserted.maquina_id
     VALUES
           (@maquina_id_ias,@id_maquina
           ,@fecha_ultimo_ingreso
           ,@marca
           ,@cod_maquina
           ,@serie
           ,@marca_modelo
           ,@isla
           ,@zona
           ,@tipo_maquina
           ,@id_sala
           ,@juego
           ,@estado_maquina
           ,@posicion)
end";
            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@maquina_id_ias", obj.maquina_id_ias);
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
                    query.Parameters.AddWithValue("@juego", obj.juego);
                    query.Parameters.AddWithValue("@estado_maquina", obj.estado_maquina);
                    query.Parameters.AddWithValue("@posicion", obj.posicion);
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
