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
    public class maquinas_auditDAL
    {
        private string _conexion = string.Empty;
        public maquinas_auditDAL()
        {
            _conexion = ConfigurationManager.ConnectionStrings["connectionBDGladconData"].ConnectionString;
        }
        public List<maquinas_audit> ListarMaquinasAudit(int maquinas_audit_id)
        {
            List<maquinas_audit> lista = new List<maquinas_audit>();
            string consulta = @"SELECT id_audit, fecha_hora, id_maquina, fecha_ultimo_ingreso, marca, cod_maquina, serie, 
                                        marca_modelo, isla, zona, tipo_maquina, id_sala, operacion, posicion, estado_maquina, juego
	FROM maquinas_audit WHERE maquinas_audit_id>@maquinas_audit_id
                                order by maquinas_audit_id asc;";
            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@maquinas_audit_id", maquinas_audit_id);

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

        public int InsertarMaquinasAudit(maquinas_audit obj)
        {
            int idInsertado = -1;
            string consulta = @"
IF NOT EXISTS (SELECT * FROM maquinas_audit 
                   WHERE id_audit=@id_audit)
   BEGIN

INSERT INTO [maquinas_audit]
           ([id_audit]
           ,[fecha_hora]
           ,[id_maquina]
           ,[fecha_ultimo_ingreso]
           ,[marca]
           ,[cod_maquina]
           ,[serie]
           ,[marca_modelo]
           ,[isla]
           ,[zona]
           ,[tipo_maquina]
           ,[id_sala]
           ,[operacion]
           ,[posicion]
           ,[estado_maquina]
           ,[juego])
     Output Inserted.maquinas_audit_id
     VALUES
           (@id_audit
           ,@fecha_hora
           ,@id_maquina
           ,@fecha_ultimo_ingreso
           ,@marca
           ,@cod_maquina
           ,@serie
           ,@marca_modelo
           ,@isla
           ,@zona
           ,@tipo_maquina
           ,@id_sala
           ,@operacion
           ,@posicion
           ,@estado_maquina
           ,@juego)
    END
";
            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@id_audit", obj.id_audit);
                    query.Parameters.AddWithValue("@fecha_hora", obj.fecha_hora);
                    query.Parameters.AddWithValue("@id_maquina", obj.id_maquina);
                    query.Parameters.AddWithValue("@fecha_ultimo_ingreso", obj.fecha_ultimo_ingreso);
                    query.Parameters.AddWithValue("@marca", obj.marca);
                    query.Parameters.AddWithValue("@cod_maquina", obj.cod_maquina);
                    query.Parameters.AddWithValue("@serie", obj.serie);
                    query.Parameters.AddWithValue("@marca_modelo", obj.marca_modelo);
                    query.Parameters.AddWithValue("@isla", obj.isla);
                    query.Parameters.AddWithValue("@zona", obj.zona);
                    query.Parameters.AddWithValue("@tipo_maquina", obj.tipo_maquina);
                    query.Parameters.AddWithValue("@id_sala", obj.id_sala);
                    query.Parameters.AddWithValue("@operacion", obj.operacion);
                    query.Parameters.AddWithValue("@posicion", obj.posicion);
                    query.Parameters.AddWithValue("@estado_maquina", obj.estado_maquina);
                    query.Parameters.AddWithValue("@juego", obj.juego);
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
