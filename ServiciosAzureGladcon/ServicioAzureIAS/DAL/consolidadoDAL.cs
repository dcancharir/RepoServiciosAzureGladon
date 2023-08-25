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
    public class consolidadoDAL
    {
        private string _conexion = string.Empty;
        public consolidadoDAL()
        {
            _conexion = ConfigurationManager.ConnectionStrings["connectionBDGladconData"].ConnectionString;
        }
        public List<consolidado> ListarConsolidado(int consolidado_id)
        {
            List<consolidado> lista = new List<consolidado>();
            string consulta = @"SELECT fecha, id_sala_consolidado, id_maquina, juego, cod_maquina, serie, coin_in, net_win, average_bet, game_played, isla, zona, 
tipo_maquina, fecha_ultimo_ingre, marca_modelo, posicion, id_consolidad
	FROM consolidado where consolidado_id>@consolidado_id
                                order by consolidado_id asc;";
            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@consolidado_id", consolidado_id);

                    using (var dr = query.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                var detalle = new consolidado
                                {
                                    fecha = ManejoNulos.ManageNullDate(dr["fecha"]),
                                    id_sala_consolidado = ManejoNulos.ManageNullInteger(dr["id_sala_consolidado"]),
                                    id_maquina = ManejoNulos.ManageNullInteger(dr["id_maquina"]),
                                    juego = ManejoNulos.ManageNullStr(dr["juego"]),
                                    cod_maquina = ManejoNulos.ManageNullStr(dr["cod_maquina"]),
                                    serie = ManejoNulos.ManageNullStr(dr["serie"]),
                                    coin_in = ManejoNulos.ManageNullDouble(dr["coin_in"]),
                                    net_win = ManejoNulos.ManageNullDouble(dr["net_win"]),
                                    average_bet = ManejoNulos.ManageNullDouble(dr["average_bet"]),
                                    game_played = ManejoNulos.ManageNullInteger(dr["game_played"]),
                                    isla = ManejoNulos.ManageNullStr(dr["isla"]),
                                    zona = ManejoNulos.ManageNullStr(dr["zona"]),
                                    tipo_maquina = ManejoNulos.ManageNullStr(dr["tipo_maquina"]),
                                    fecha_ultimo_ingre = ManejoNulos.ManageNullDate(dr["fecha_ultimo_ingreso"]),
                                    marca_modelo = ManejoNulos.ManageNullStr(dr["marca_modelo"]),
                                    posicion = ManejoNulos.ManageNullStr(dr["posicion"]),
                                    id_consolidad = ManejoNulos.ManageNullInteger(dr["id_consolidad"]),
                                };

                                lista.Add(detalle);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                lista = new List<consolidado>();
            }
            return lista;
        }
        public int InsertarConsolidado(consolidado obj)
        {
            int idInsertado = -1;
            string consulta = @"
IF NOT EXISTS (SELECT * FROM consolidado 
                   WHERE id_consolidad=@id_consolidad)
   BEGIN


INSERT INTO [consolidado]
           ([fecha]
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
           ,[posicion]
           ,[id_consolidad])
     Output Inserted.consolidado_id
     VALUES
           (@fecha
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
           ,@posicion
           ,@id_consolidad)
    END
";
            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
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
                    query.Parameters.AddWithValue("@id_consolidad", obj.id_consolidad);
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
