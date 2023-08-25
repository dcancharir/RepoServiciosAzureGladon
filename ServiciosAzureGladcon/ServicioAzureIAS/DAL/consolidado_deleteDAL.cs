using ServicioAzureIAS.Clases.GladconData;
using ServicioAzureIAS.utilitarios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioAzureIAS.DAL
{
    public class consolidado_deleteDAL
    {
        private string _conexion = string.Empty;
        public consolidado_deleteDAL()
        {
            _conexion = ConfigurationManager.ConnectionStrings["connectionBDGladconData"].ConnectionString;
        }
        public List<consolidado_delete> ListarConsolidadoDelete(int consolidado_delete_id)
        {
            List<consolidado_delete> lista = new List<consolidado_delete>();
            string consulta = @"SELECT consolidado_delete_id,fecha, id_sala_consolidado, id_maquina, juego, cod_maquina, serie, coin_in, 
net_win, average_bet, game_played, isla, zona, tipo_maquina, fecha_ultimo_ingre, marca_modelo, posicion
	FROM consolidado_delete where consolidado_delete_id>@consolidado_delete_id
                                order by consolidado_delete_id asc;";
            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@consolidado_delete_id", consolidado_delete_id);

                    using (var dr = query.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                var detalle = new consolidado_delete
                                {
                                    consolidado_delete_id = ManejoNulos.ManageNullDate(dr["consolidado_delete_id"]),
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
                                    fecha_ultimo_ingre = ManejoNulos.ManageNullDate(dr["fecha_ultimo_ingre"]),
                                    marca_modelo = ManejoNulos.ManageNullStr(dr["marca_modelo"]),
                                    posicion = ManejoNulos.ManageNullStr(dr["posicion"]),
                                };

                                lista.Add(detalle);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                lista = new List<consolidado_delete>();
            }
            return lista;
        }

        public int InsertarConsolidadoDelete (consolidado_delete obj)
        {
            int idInsertado = -1;
            string consulta = @"
IF NOT EXISTS (SELECT * FROM consolidado_delete 
                   WHERE id_sala_consolidado=@id_sala_consolidado AND id_maquina=@id_maquina AND fecha=@fecha)
   BEGIN

INSERT INTO [consolidado_delete]
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
           ,[posicion])
     Output Inserted.consolidado_delete_id
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
           ,@posicion)
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
