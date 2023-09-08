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
    public class salaDAL
    {
        private string _conexion = string.Empty;
        private string _esquema = string.Empty;
        public salaDAL()
        {
            _conexion = ConfigurationManager.ConnectionStrings["connectionBD"].ConnectionString;
            _esquema = ConfigurationManager.AppSettings["PGScheme"];
        }
        public List<sala> ListarSala()
        {
            List<sala> lista = new List<sala>();
            string consulta = $@"SELECT id_sala, nombre_sala, nombre_operador, departamento_sala, provincia_sala
	FROM {_esquema}.sala
                                order by id_sala asc;";
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
                                var detalle = new sala
                                {
                                    id_sala = ManejoNulos.ManageNullInteger(dr["id_sala"]),
                                    nombre_sala = ManejoNulos.ManageNullStr(dr["nombre_sala"]),
                                    nombre_operador = ManejoNulos.ManageNullStr(dr["nombre_operador"]),
                                    departamento_sala = ManejoNulos.ManageNullStr(dr["departamento_sala"]),
                                    provincia_sala = ManejoNulos.ManageNullStr(dr["provincia_sala"]),
                                };

                                lista.Add(detalle);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lista = new List<sala>();
            }
            return lista;
        }
    }
}
