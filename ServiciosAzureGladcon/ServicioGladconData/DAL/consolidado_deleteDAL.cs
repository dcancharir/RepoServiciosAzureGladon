using Npgsql;
using ServicioGladconData.Clases;
using ServicioGladconData.utilitarios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioGladconData.DAL
{
    public class consolidado_deleteDAL
    {
        private string _conexion = string.Empty;
        private string _esquema = string.Empty;
        public consolidado_deleteDAL()
        {
            _conexion = ConfigurationManager.ConnectionStrings["connectionBD"].ConnectionString;
            _esquema = ConfigurationManager.AppSettings["PGScheme"];
        }
        public List<consolidado_delete> ListarConsolidadoDeletePorFechaOperacion(DateTime fechaOperacion)
        {
            List<consolidado_delete> lista = new List<consolidado_delete>();
            string consulta = $@"SELECT fecha, id_sala_consolidado, id_maquina, juego, cod_maquina, serie, coin_in, 
net_win, average_bet, game_played, isla, zona, tipo_maquina, fecha_ultimo_ingre, marca_modelo, posicion
	FROM {_esquema}.consolidado_delete where fecha=@fechaOperacion
                                order by fecha asc;";
            try
            {
                using (var con = new NpgsqlConnection(_conexion))
                {
                    con.Open();
                    var query = new NpgsqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@fechaOperacion", fechaOperacion);

                    using (var dr = query.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                var detalle = new consolidado_delete
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
        public List<consolidado_delete> ListarConsolidadoDeleteTodo()
        {
            List<consolidado_delete> lista = new List<consolidado_delete>();
            string consulta = $@"SELECT fecha, id_sala_consolidado, id_maquina, juego, cod_maquina, serie, coin_in, net_win,
average_bet, game_played, isla, zona, tipo_maquina, fecha_ultimo_ingre, marca_modelo, posicion
	FROM {_esquema}.consolidado_delete
                                order by fecha asc;";
            try
            {
                using (var con = new NpgsqlConnection(_conexion))
                {
                    con.Open();
                    var query = new NpgsqlCommand(consulta, con);

                    using (var dr = query.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                var detalle = new consolidado_delete
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
    }
}
