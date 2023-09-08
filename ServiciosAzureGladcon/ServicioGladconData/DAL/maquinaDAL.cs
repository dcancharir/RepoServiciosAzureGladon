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
    public class maquinaDAL
    {
        private string _conexion = string.Empty;
        private string _esquema = string.Empty;
        
        public maquinaDAL()
        {
            _conexion = ConfigurationManager.ConnectionStrings["connectionBD"].ConnectionString;
            _esquema = ConfigurationManager.AppSettings["PGScheme"];
        }
        public List<maquina> ListarMaquina()
        {
            List<maquina> lista = new List<maquina>();
            string consulta = $@"SELECT id_maquina, fecha_ultimo_ingreso, marca, cod_maquina, serie, marca_modelo, isla, zona, tipo_maquina, id_sala, juego, estado_maquina, posicion
	FROM {_esquema}.maquina
                                order by id_maquina asc;";
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
                                var detalle = new maquina
                                {
                                    id_maquina = ManejoNulos.ManageNullInteger(dr["id_maquina"]),
                                    fecha_ultimo_ingreso = ManejoNulos.ManageNullDate(dr["fecha_ultimo_ingreso"]),
                                    marca = ManejoNulos.ManageNullStr(dr["marca"]),
                                    cod_maquina = ManejoNulos.ManageNullStr(dr["cod_maquina"]),
                                    serie = ManejoNulos.ManageNullStr(dr["serie"]),
                                    marca_modelo = ManejoNulos.ManageNullStr(dr["marca_modelo"]),
                                    isla = ManejoNulos.ManageNullStr(dr["isla"]),
                                    zona = ManejoNulos.ManageNullStr(dr["zona"]),
                                    tipo_maquina = ManejoNulos.ManageNullStr(dr["tipo_maquina"]),
                                    id_sala = ManejoNulos.ManageNullInteger(dr["id_sala"]),
                                    juego = ManejoNulos.ManageNullStr(dr["juego"]),
                                    estado_maquina = ManejoNulos.ManageNullStr(dr["estado_maquina"]),
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
                lista = new List<maquina>();
            }
            return lista;
        }
    }
}
