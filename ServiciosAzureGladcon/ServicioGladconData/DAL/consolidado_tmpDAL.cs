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
    public class consolidado_tmpDAL
    {
        private string _conexion = string.Empty;
        private string _esquema = string.Empty;
        public consolidado_tmpDAL()
        {
            _conexion = ConfigurationManager.ConnectionStrings["connectionBD"].ConnectionString;
            _esquema = ConfigurationManager.AppSettings["PGScheme"];
        }
        public List<consolidado_tmp> ListarConsolidadoTMPPorFechaOperacion(DateTime fechaOperacion)
        {
            List<consolidado_tmp> lista = new List<consolidado_tmp>();
            string consulta = $@"SELECT id_consolidado_tmp, fecha, sala, cod_maquina, serie, coin_in, net_win, average_bet, game_played
	FROM 
{_esquema}.consolidado_tmp WHERE fecha=@fechaOperacion
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
        public List<consolidado_tmp> ListarConsolidadoTMPTodo()
        {
            List<consolidado_tmp> lista = new List<consolidado_tmp>();
            string consulta = $@"SELECT id_consolidado_tmp, fecha, sala, cod_maquina, serie, coin_in, net_win, average_bet, game_played
	FROM {_esquema}.consolidado_tmp
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
    }
}
