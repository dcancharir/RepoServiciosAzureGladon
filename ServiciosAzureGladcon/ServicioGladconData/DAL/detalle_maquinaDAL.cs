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
    public class detalle_maquinaDAL
    {
        private string _conexion = string.Empty;
        private string _esquema = string.Empty;
        public detalle_maquinaDAL()
        {
            _conexion = ConfigurationManager.ConnectionStrings["connectionBD"].ConnectionString;
            _esquema = ConfigurationManager.AppSettings["PGScheme"];
        }
        public List<detalle_maquina> ListarDetalleMaquina()
        {
            List<detalle_maquina> lista = new List<detalle_maquina>();
            string consulta = $@"SELECT id, serie, marca_modelo, tipo_sistema, progresivo, modelo_comercial, codigo_modelo, juego, propietario, propiedad, tipo_contrato, tipo_maquina, cod_maquina
	FROM {_esquema}.detalle_maquina
                                order by id asc;";
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
                                var detalle = new detalle_maquina
                                {
                                    id = ManejoNulos.ManageNullInteger(dr["id"]),
                                    serie = ManejoNulos.ManageNullStr(dr["serie"]),
                                    marca_modelo = ManejoNulos.ManageNullStr(dr["marca_modelo"]),
                                    tipo_sistema = ManejoNulos.ManageNullStr(dr["tipo_sistema"]),
                                    progresivo = ManejoNulos.ManageNullStr(dr["progresivo"]),
                                    modelo_comercial = ManejoNulos.ManageNullStr(dr["modelo_comercial"]),
                                    codigo_modelo = ManejoNulos.ManageNullStr(dr["codigo_modelo"]),
                                    juego = ManejoNulos.ManageNullStr(dr["juego"]),
                                    propietario = ManejoNulos.ManageNullStr(dr["propietario"]),
                                    propiedad = ManejoNulos.ManageNullStr(dr["propiedad"]),
                                    tipo_contrato = ManejoNulos.ManageNullStr(dr["tipo_contrato"]),
                                    tipo_maquina = ManejoNulos.ManageNullStr(dr["tipo_maquina"]),
                                    cod_maquina = ManejoNulos.ManageNullStr(dr["cod_maquina"]),
                                };

                                lista.Add(detalle);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                lista = new List<detalle_maquina>();
            }
            return lista;
        }
    }
}
