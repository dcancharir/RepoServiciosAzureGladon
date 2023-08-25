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
    public class maquinaDAL
    {
        private string _conexion = string.Empty;
        public maquinaDAL()
        {
            _conexion = ConfigurationManager.ConnectionStrings["connectionBD"].ConnectionString;
        }
        public List<maquina> ListarMaquina(int maquina_id)
        {
            List<maquina> lista = new List<maquina>();
            string consulta = @"SELECT id_maquina, fecha_ultimo_ingreso, marca, cod_maquina, serie, marca_modelo, isla, zona, tipo_maquina, id_sala, juego, estado_maquina, posicion
	FROM maquina WHERE maquina_id>@maquina_id
                                order by maquina_id asc;";
            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@maquina_id", maquina_id);

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

        public int InsertarMaquina(maquina obj)
        {
            int idInsertado = 0;
            string consulta = @"INSERT INTO [maquina]
           ([id_maquina]
           ,[fecha_ultimo_ingreso]
           ,[marca]
           ,[cod_maquina]
           ,[serie]
           ,[marca_modelo]
           ,[isla]
           ,[zona]
           ,[tipo_maquina]
           ,[id_sala]
           ,[juego]
           ,[estado_maquina]
           ,[posicion])
     Output Inserted.maquina_id
     VALUES
           (@id_maquina
           ,@fecha_ultimo_ingreso
           ,@marca
           ,@cod_maquina
           ,@serie
           ,@marca_modelo
           ,@isla
           ,@zona
           ,@tipo_maquina
           ,@id_sala
           ,@juego
           ,@estado_maquina
           ,@posicion)";
            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
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
                    query.Parameters.AddWithValue("@juego", obj.juego);
                    query.Parameters.AddWithValue("@estado_maquina", obj.estado_maquina);
                    query.Parameters.AddWithValue("@posicion", obj.posicion);
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
