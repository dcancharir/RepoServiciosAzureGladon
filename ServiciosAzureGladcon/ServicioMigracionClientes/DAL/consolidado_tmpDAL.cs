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
    public class consolidado_tmpDAL
    {
        private string _conexion = string.Empty;
        public consolidado_tmpDAL()
        {
            _conexion = ConfigurationManager.ConnectionStrings["connectionBD"].ConnectionString;
        }
        public int InsertarConsolidadoTMP(consolidado_tmp obj)
        {
            int idInsertado = 0;
            string consulta = @"

 IF EXISTS (SELECT * FROM [dbo].[consolidado_tmp] (nolock) WHERE consolidado_tmp_id_ias = @consolidado_tmp_id_ias)
        BEGIN
           SELECT 1 
        END
        ELSE
        BEGIN
INSERT INTO [consolidado_tmp]
            ([consolidado_tmp_id_ias],[id_consolidado_tmp]
           ,[fecha]
           ,[sala]
           ,[cod_maquina]
           ,[serie]
           ,[coin_in]
           ,[net_win]
           ,[average_bet]
           ,[game_played])
     Output Inserted.consolidado_tmp_id
     VALUES
           (@consolidado_tmp_id_ias,@id_consolidado_tmp
           ,@fecha
           ,@sala
           ,@cod_maquina
           ,@serie
           ,@coin_in
           ,@net_win
           ,@average_bet
           ,@game_played)

end
";
            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@consolidado_tmp_id_ias", obj.consolidado_tmp_id_ias);
                    query.Parameters.AddWithValue("@id_consolidado_tmp", obj.id_consolidado_tmp);
                    query.Parameters.AddWithValue("@fecha", obj.fecha);
                    query.Parameters.AddWithValue("@sala", obj.sala);
                    query.Parameters.AddWithValue("@cod_maquina", obj.cod_maquina);
                    query.Parameters.AddWithValue("@serie", obj.serie);
                    query.Parameters.AddWithValue("@coin_in", obj.coin_in);
                    query.Parameters.AddWithValue("@net_win", obj.net_win);
                    query.Parameters.AddWithValue("@average_bet", obj.average_bet);
                    query.Parameters.AddWithValue("@game_played", obj.game_played);
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
