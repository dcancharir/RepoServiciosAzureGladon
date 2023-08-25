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
    public class detalle_maquinaDAL
    {
        private string _conexion = string.Empty;
        public detalle_maquinaDAL()
        {
            _conexion = ConfigurationManager.ConnectionStrings["connectionBD"].ConnectionString;
        }
        public List<detalle_maquina> ListarDetalleMaquina(int detalle_maquina_id)
        {
            List<detalle_maquina> lista = new List<detalle_maquina>();
            string consulta = @"SELECT id, serie, marca_modelo, tipo_sistema, progresivo, modelo_comercial, codigo_modelo, juego, propietario, propiedad, tipo_contrato, tipo_maquina, cod_maquina
	FROM detalle_maquina WHERE detalle_maquina_id>@detalle_maquina_id
                                order by detalle_maquina_id asc;";
            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@detalle_maquina_id", detalle_maquina_id);

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

        public int InsertarDetalleMaquina(detalle_maquina obj)
        {
            int idInsertado = 0;
            string consulta = @"INSERT INTO [maquina]
           ([id]
           ,[serie]
           ,[marca_modelo]
           ,[tipo_sistema]
           ,[progresivo]
           ,[modelo_comercial]
           ,[codigo_modelo]
           ,[juego]
           ,[propietario]
           ,[propiedad]
           ,[tipo_contrato]
           ,[tipo_maquina]
           ,[cod_maquina])
     Output Inserted.detalle_maquina_id
     VALUES
           (@id
           ,@serie
           ,@marca_modelo
           ,@tipo_sistema
           ,@progresivo
           ,@modelo_comercial
           ,@codigo_modelo
           ,@juego
           ,@propietario
           ,@propiedad
           ,@tipo_contrato
           ,@tipo_maquina
           ,@cod_maquina)";
            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@id", obj.id);
                    query.Parameters.AddWithValue("@serie", obj.serie);
                    query.Parameters.AddWithValue("@marca_modelo", obj.marca_modelo);
                    query.Parameters.AddWithValue("@tipo_sistema", obj.tipo_sistema);
                    query.Parameters.AddWithValue("@progresivo", obj.progresivo);
                    query.Parameters.AddWithValue("@modelo_comercial", obj.modelo_comercial);
                    query.Parameters.AddWithValue("@codigo_modelo", obj.codigo_modelo);
                    query.Parameters.AddWithValue("@juego", obj.juego);
                    query.Parameters.AddWithValue("@propietario", obj.propietario);
                    query.Parameters.AddWithValue("@propiedad", obj.propiedad);
                    query.Parameters.AddWithValue("@tipo_contrato", obj.tipo_contrato);
                    query.Parameters.AddWithValue("@tipo_maquina", obj.tipo_maquina);
                    query.Parameters.AddWithValue("@cod_maquina", obj.cod_maquina);
                    idInsertado = Convert.ToInt32(query.ExecuteScalar());

                }
            }
            catch (Exception ex)
            {
                idInsertado = 0;
            }

            return idInsertado;
        }

    }
}
