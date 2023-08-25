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
    public class detalle_maquina_auditDAL
    {
        private string _conexion = string.Empty;
        public detalle_maquina_auditDAL()
        {
            _conexion = ConfigurationManager.ConnectionStrings["connectionBD"].ConnectionString;
        }
        public List<detalle_maquinas_audit> ListarDetalleMaquinaAuditPorFechaOperacion(DateTime fechaOperacion)
        {
            List<detalle_maquinas_audit> lista = new List<detalle_maquinas_audit>();
            string consulta = @"SELECT id_audit, fecha_hora, marca_modelo, cod_maquina, serie, modelo_comercial, tipo_maquina, progresivo, juego, propietario, tipo_contrato, tipo_sistema, propiedad, operacion
	FROM public.detalle_maquinas_audit
where fecha_hora=@fechaOperacion
                                order by id_audit asc;";
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
                                var detalle = new detalle_maquinas_audit
                                {
                                    id_audit = ManejoNulos.ManageNullInteger(dr["id_audit"]),
                                    fecha_hora = ManejoNulos.ManageNullDate(dr["fecha_hora"]),
                                    marca_modelo = ManejoNulos.ManageNullStr(dr["marca_modelo"]),
                                    cod_maquina = ManejoNulos.ManageNullStr(dr["cod_maquina"]),
                                    serie = ManejoNulos.ManageNullStr(dr["serie"]),
                                    modelo_comercial = ManejoNulos.ManageNullStr(dr["modelo_comercial"]),
                                    tipo_maquina = ManejoNulos.ManageNullStr(dr["tipo_maquina"]),
                                    progresivo = ManejoNulos.ManageNullStr(dr["progresivo"]),
                                    juego = ManejoNulos.ManageNullStr(dr["juego"]),
                                    propietario = ManejoNulos.ManageNullStr(dr["propietario"]),
                                    tipo_contrato = ManejoNulos.ManageNullStr(dr["tipo_contrato"]),
                                    tipo_sistema = ManejoNulos.ManageNullStr(dr["tipo_sistema"]),
                                    propiedad = ManejoNulos.ManageNullStr(dr["propiedad"]),
                                    operacion = ManejoNulos.ManageNullStr(dr["operacion"]),
                                };
                                lista.Add(detalle);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lista = new List<detalle_maquinas_audit>();
            }
            return lista;
        }
        public List<detalle_maquinas_audit> ListarDetalleMaquinaAuditTodo()
        {
            List<detalle_maquinas_audit> lista = new List<detalle_maquinas_audit>();
            string consulta = @"SELECT id_audit, fecha_hora, marca_modelo, cod_maquina, serie, modelo_comercial, tipo_maquina, progresivo, juego, propietario, tipo_contrato, tipo_sistema, propiedad, operacion
	FROM public.detalle_maquinas_audit
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
                                var detalle = new detalle_maquinas_audit
                                {
                                    id_audit = ManejoNulos.ManageNullInteger(dr["id_audit"]),
                                    fecha_hora = ManejoNulos.ManageNullDate(dr["fecha_hora"]),
                                    marca_modelo = ManejoNulos.ManageNullStr(dr["marca_modelo"]),
                                    cod_maquina = ManejoNulos.ManageNullStr(dr["cod_maquina"]),
                                    serie = ManejoNulos.ManageNullStr(dr["serie"]),
                                    modelo_comercial = ManejoNulos.ManageNullStr(dr["modelo_comercial"]),
                                    tipo_maquina = ManejoNulos.ManageNullStr(dr["tipo_maquina"]),
                                    progresivo = ManejoNulos.ManageNullStr(dr["progresivo"]),
                                    juego = ManejoNulos.ManageNullStr(dr["juego"]),
                                    propietario = ManejoNulos.ManageNullStr(dr["propietario"]),
                                    tipo_contrato = ManejoNulos.ManageNullStr(dr["tipo_contrato"]),
                                    tipo_sistema = ManejoNulos.ManageNullStr(dr["tipo_sistema"]),
                                    propiedad = ManejoNulos.ManageNullStr(dr["propiedad"]),
                                    operacion = ManejoNulos.ManageNullStr(dr["operacion"]),
                                };
                                lista.Add(detalle);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lista = new List<detalle_maquinas_audit>();
            }
            return lista;
        }
    }
}
