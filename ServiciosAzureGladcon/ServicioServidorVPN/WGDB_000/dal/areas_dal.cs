using ServicioServidorVPN.utilitarios;
using ServicioServidorVPN.WGDB_000.model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioServidorVPN.WGDB_000.dal
{
    public class areas_dal
    {
        private readonly string bd_username = string.Empty;
        private readonly string bd_password = string.Empty;
        private readonly string bd_datasource = string.Empty;
        private readonly string connectionString = string.Empty;
        public areas_dal(string database_name)
        {
            bd_username = ConfigurationManager.AppSettings["bd_username"];
            bd_password = ConfigurationManager.AppSettings["bd_password"];
            bd_datasource = ConfigurationManager.AppSettings["bd_datasource"];
            connectionString = $"Data Source={bd_datasource};Initial Catalog={database_name};Integrated Security=False;User ID={bd_username};Password={bd_password}";

        }
        public int SaveAreas(areas item)
        {
            //bool respuesta = false;
            int IdInsertado = 0;
            string consulta = @"
INSERT INTO [dbo].[areas]
           ([ar_area_id]
           ,[ar_name]
           ,[ar_smoking]
           ,[ar_venue_id]
           ,[ar_external_id])
--output inserted.ar_area_id
     VALUES
           (@ar_area_id
           ,@ar_name
           ,@ar_smoking
           ,@ar_venue_id
           ,@ar_external_id)
                      ";
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@ar_area_id", item.ar_area_id == null ? DBNull.Value : (object)item.ar_area_id);
                    query.Parameters.AddWithValue("@ar_name ", item.ar_name == null ? DBNull.Value : (object)item.ar_name);
                    query.Parameters.AddWithValue("@ar_smoking", item.ar_smoking == null ? DBNull.Value : (object)item.ar_smoking);
                    query.Parameters.AddWithValue("@ar_timestamp", item.ar_timestamp == null ? DBNull.Value : (object)item.ar_timestamp);
                    query.Parameters.AddWithValue("@ar_venue_id", item.ar_venue_id == null ? DBNull.Value : (object)item.ar_venue_id);
                    query.Parameters.AddWithValue("@ar_external_id", item.ar_external_id == null ? DBNull.Value : (object)item.ar_external_id);
                    //query.Parameters.AddWithValue("@ar_timestamp", ManejoNulos.ManageNullByteArray(item.ar_timestamp));
                    //IdInsertado = Convert.ToInt32(query.ExecuteScalar());
                    query.ExecuteNonQuery();
                    IdInsertado = item.ar_area_id;
                }
            }
            catch (Exception ex)
            {
                IdInsertado = 0;
            }
            return IdInsertado;
        }
        public int GetLastIdInserted()
        {
            int total = 0;

            string query = @"
            select top 1 ar_area_id as lastid from 
            [dbo].[areas]
            order by ar_area_id desc
";

            try
            {
                using (SqlConnection conecction = new SqlConnection(connectionString))
                {
                    conecction.Open();
                    SqlCommand command = new SqlCommand(query, conecction);
                    using (SqlDataReader data = command.ExecuteReader())
                    {
                        if (data.Read())
                        {
                            total = (int)data["lastid"];
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                total = 0;
            }

            return total;
        }
    }
}
