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
    public class detalle_maquinas_auditDAL
    {
        private string _conexion = string.Empty;
        public detalle_maquinas_auditDAL()
        {
            _conexion = ConfigurationManager.ConnectionStrings["connectionBDGladconData"].ConnectionString;
        }
        public List<detalle_maquinas_audit> ListarDetalleMaquinasAudit(int detalle_maquinas_audit_id)
        {
            List<detalle_maquinas_audit> lista = new List<detalle_maquinas_audit>();
            string consulta = @"SELECT id_audit, fecha_hora, marca_modelo, cod_maquina, serie, modelo_comercial, tipo_maquina, progresivo, juego, propietario, tipo_contrato, tipo_sistema, propiedad, operacion
	FROM detalle_maquinas_audit
where detalle_maquinas_audit_id>@detalle_maquinas_audit_id
                                order by detalle_maquinas_audit_id asc;";
            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@detalle_maquinas_audit_id", detalle_maquinas_audit_id);
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

        public int InsertarDetalleMaquinasAudit(detalle_maquinas_audit obj)
        {
            int idInsertado = -1;
            string consulta = @"

IF NOT EXISTS (SELECT * FROM detalle_maquinas_audit 
                   WHERE id_audit=@id_audit)
   BEGIN

INSERT INTO [detalle_maquinas_audit]
           ([id_audit]
           ,[fecha_hora]
           ,[marca_modelo]
           ,[cod_maquina]
           ,[serie]
           ,[modelo_comercial]
           ,[tipo_maquina]
           ,[progresivo]
           ,[juego]
           ,[propietario]
           ,[tipo_contrato]
           ,[tipo_sistema]
           ,[propiedad]
           ,[operacion])
     Output Inserted.detalle_maquinas_audit_id
     VALUES
           (@id_audit
           ,@fecha_hora
           ,@marca_modelo
           ,@cod_maquina
           ,@serie
           ,@modelo_comercial
           ,@tipo_maquina
           ,@progresivo
           ,@juego
           ,@propietario
           ,@tipo_contrato
           ,@tipo_sistema
           ,@propiedad
           ,@operacion)
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
                    query.Parameters.AddWithValue("@marca_modelo", obj.marca_modelo);
                    query.Parameters.AddWithValue("@cod_maquina", obj.cod_maquina);
                    query.Parameters.AddWithValue("@serie", obj.serie);
                    query.Parameters.AddWithValue("@modelo_comercial", obj.modelo_comercial);
                    query.Parameters.AddWithValue("@tipo_maquina", obj.tipo_maquina);
                    query.Parameters.AddWithValue("@progresivo", obj.progresivo);
                    query.Parameters.AddWithValue("@juego", obj.juego);
                    query.Parameters.AddWithValue("@propietario", obj.propietario);
                    query.Parameters.AddWithValue("@tipo_contrato", obj.tipo_contrato);
                    query.Parameters.AddWithValue("@tipo_sistema", obj.tipo_sistema);
                    query.Parameters.AddWithValue("@propiedad", obj.propiedad);
                    query.Parameters.AddWithValue("@operacion", obj.operacion);
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
