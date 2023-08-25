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
    public class consolidado_deleteDAL
    {
        private string _conexion = string.Empty;
        public consolidado_deleteDAL()
        {
            _conexion = ConfigurationManager.ConnectionStrings["connectionBD"].ConnectionString;
        }
        public int InsertarConsolidadoDelete(consolidado_delete obj)
        {
            int idInsertado = 0;
            string consulta = @"
  IF EXISTS (SELECT * FROM [dbo].[consolidado_delete] (nolock) WHERE consolidado_delete_id_ias = @consolidado_delete_id_ias)
        BEGIN
           SELECT 1 
        END
        ELSE
        BEGIN
    INSERT INTO [consolidado_delete]
           ([consolidado_delete_id_ias],[fecha]
           ,[id_sala_consolidado]
           ,[id_maquina]
           ,[juego]
           ,[cod_maquina]
           ,[serie]
           ,[coin_in]
           ,[net_win]
           ,[average_bet]
           ,[game_played]
           ,[isla]
           ,[zona]
           ,[tipo_maquina]
           ,[fecha_ultimo_ingre]
           ,[marca_modelo]
           ,[posicion])
     Output Inserted.consolidado_delete_id
     VALUES
           (@consolidado_delete_id_ias,@fecha
           ,@id_sala_consolidado
           ,@id_maquina
           ,@juego
           ,@cod_maquina
           ,@serie
           ,@coin_in
           ,@net_win
           ,@average_bet
           ,@game_played
           ,@isla
           ,@zona
           ,@tipo_maquina
           ,@fecha_ultimo_ingre
           ,@marca_modelo
           ,@posicion)
        end

";
            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@consolidado_delete_id_ias", obj.consolidado_delete_id_ias);
                    query.Parameters.AddWithValue("@fecha", obj.fecha);
                    query.Parameters.AddWithValue("@id_sala_consolidado", obj.id_sala_consolidado);
                    query.Parameters.AddWithValue("@id_maquina", obj.id_maquina);
                    query.Parameters.AddWithValue("@juego", obj.juego);
                    query.Parameters.AddWithValue("@cod_maquina", obj.cod_maquina);
                    query.Parameters.AddWithValue("@serie", obj.serie);
                    query.Parameters.AddWithValue("@coin_in", obj.coin_in);
                    query.Parameters.AddWithValue("@net_win", obj.net_win);
                    query.Parameters.AddWithValue("@average_bet", obj.average_bet);
                    query.Parameters.AddWithValue("@game_played", obj.game_played);
                    query.Parameters.AddWithValue("@isla", obj.isla);
                    query.Parameters.AddWithValue("@zona", obj.zona);
                    query.Parameters.AddWithValue("@tipo_maquina", obj.tipo_maquina);
                    query.Parameters.AddWithValue("@fecha_ultimo_ingre", obj.fecha_ultimo_ingre);
                    query.Parameters.AddWithValue("@marca_modelo", obj.marca_modelo);
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
