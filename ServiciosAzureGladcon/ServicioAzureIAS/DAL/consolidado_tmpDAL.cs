using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServicioAzureIAS.Clases.GladconData;
using ServicioAzureIAS.utilitarios;

namespace ServicioAzureIAS.DAL
{
    public class consolidado_tmpDAL
    {
        private string _conexion = string.Empty;
        public consolidado_tmpDAL()
        {
            _conexion = ConfigurationManager.ConnectionStrings["connectionBDGladconData"].ConnectionString;
        }
        public List<consolidado_tmp> ListarConsolidadoTMP(int consolidado_tmp_id)
        {
            List<consolidado_tmp> lista = new List<consolidado_tmp>();
            string consulta = @"SELECT id_consolidado_tmp, fecha, sala, cod_maquina, serie, coin_in, net_win, average_bet, game_played
	FROM consolidado_tmp where consolidado_tmp_id>@consolidado_tmp_id
                                order by consolidado_tmp_id asc;";
            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@consolidado_tmp_id", consolidado_tmp_id);

                    using (var dr = query.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                var detalle = new consolidado_tmp
                                {
                                    id_consolidado_tmp = ManejoNulos.ManageNullInteger(dr["id_consolidado_tmp"]),
                                    fecha = ManejoNulos.ManageNullDate(dr["fecha"]),
                                    sala = ManejoNulos.ManageNullInteger(dr["sala"]),
                                    cod_maquina = ManejoNulos.ManageNullStr(dr["cod_maquina"]),
                                    serie = ManejoNulos.ManageNullStr(dr["serie"]),
                                    coin_in = ManejoNulos.ManageNullDouble(dr["coin_in"]),
                                    net_win = ManejoNulos.ManageNullDouble(dr["net_win"]),
                                    average_bet = ManejoNulos.ManageNullDouble(dr["average_bet"]),
                                    game_played = ManejoNulos.ManageNullInteger(dr["game_played"]),
                                };

                                lista.Add(detalle);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                lista = new List<consolidado_tmp>();
            }
            return lista;
        }


        public int InsertarConsolidadoTMP(consolidado_tmp obj)
        {
            int idInsertado = -1;
            string consulta = @"
IF NOT EXISTS (SELECT * FROM consolidado_tmp 
                   WHERE id_consolidado_tmp=@id_consolidado_tmp)
   BEGIN

INSERT INTO [consolidado_tmp]
            ([id_consolidado_tmp]
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
           (@id_consolidado_tmp
           ,@fecha
           ,@sala
           ,@cod_maquina
           ,@serie
           ,@coin_in
           ,@net_win
           ,@average_bet
           ,@game_played)
    END
";
            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
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
                idInsertado = -1;
            }

            return idInsertado;
        }

    }
}
