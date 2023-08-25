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
    public class maquinas_auditDAL
    {
        private string _conexion = string.Empty;
        public maquinas_auditDAL()
        {
            _conexion = ConfigurationManager.ConnectionStrings["connectionBD"].ConnectionString;
        }
        public List<maquinas_audit> ListarMaquinasAudit()
        {
            List<maquinas_audit> lista = new List<maquinas_audit>();
            string consulta = @"SELECT id_audit, fecha_hora, id_maquina, fecha_ultimo_ingreso, marca, cod_maquina, serie, 
                                        marca_modelo, isla, zona, tipo_maquina, id_sala, operacion, posicion, estado_maquina, juego
	FROM public.maquinas_audit
                                order by id_audit asc;";
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
                                var detalle = new maquinas_audit
                                {
                                    id_audit = ManejoNulos.ManageNullInteger(dr["id_audit"]),
                                    fecha_hora = ManejoNulos.ManageNullDate(dr["fecha_hora"]),
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
                                    operacion = ManejoNulos.ManageNullStr(dr["operacion"]),
                                    posicion = ManejoNulos.ManageNullStr(dr["posicion"]),
                                    estado_maquina = ManejoNulos.ManageNullStr(dr["estado_maquina"]),
                                    juego = ManejoNulos.ManageNullStr(dr["juego"]),

                                };

                                lista.Add(detalle);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lista = new List<maquinas_audit>();
            }
            return lista;
        }
    }
}
