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
    public class salaDAL
    {
        private string _conexion = string.Empty;
        public salaDAL()
        {
            _conexion = ConfigurationManager.ConnectionStrings["connectionBD"].ConnectionString;
        }
        public List<sala> ListarSala()
        {
            List<sala> lista = new List<sala>();
            string consulta = @"SELECT id_sala, nombre_sala, nombre_operador, departamento_sala, provincia_sala
	FROM sala
                                order by id_sala asc;";
            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);

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

        public int InsertarSala(sala obj)
        {
            int idInsertado = 0;
            string consulta = @"INSERT INTO [sala]
           ([id_sala]
           ,[nombre_sala]
           ,[nombre_operador]
           ,[departamento_sala]
           ,[provincia_sala])
     Output Inserted.sala_id
     VALUES
           (@id_sala
           ,@nombre_sala
           ,@nombre_operador
           ,@departamento_sala
           ,@provincia_sala)";
            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@id_sala", obj.id_sala);
                    query.Parameters.AddWithValue("@nombre_sala", obj.nombre_sala);
                    query.Parameters.AddWithValue("@nombre_operador", obj.nombre_operador);
                    query.Parameters.AddWithValue("@departamento_sala", obj.departamento_sala);
                    query.Parameters.AddWithValue("@provincia_sala", obj.provincia_sala);
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
