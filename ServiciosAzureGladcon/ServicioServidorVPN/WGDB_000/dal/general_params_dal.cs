using ServicioServidorVPN.WGDB_000.model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioServidorVPN.WGDB_000.dal
{
    public class general_params_dal
    {
        private readonly string bd_username = string.Empty;
        private readonly string bd_password = string.Empty;
        private readonly string bd_datasource = string.Empty;
        private readonly string connectionString = string.Empty;
        public general_params_dal(string database_name)
        {
            bd_username = ConfigurationManager.AppSettings["bd_username"];
            bd_password = ConfigurationManager.AppSettings["bd_password"];
            bd_datasource = ConfigurationManager.AppSettings["bd_datasource"];
            connectionString = $"Data Source={bd_datasource};Initial Catalog={database_name};Integrated Security=False;User ID={bd_username};Password={bd_password}";

        }
        public bool SaveGeneralParams(general_params item)
        {
            bool respuesta = false;
            //int IdInsertado = 0;
            string consulta = @"
INSERT INTO [dbo].[general_params]
           ([gp_group_key]
           ,[gp_subject_key]
           ,[gp_key_value]
           ,[gp_description])
     VALUES
           (@gp_group_key
           ,@gp_subject_key
           ,@gp_key_value
           ,@gp_description)
                      ";
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@gp_group_key", (string)item.gp_group_key);
                    query.Parameters.AddWithValue("@gp_subject_key",(string)item.gp_subject_key);
                    query.Parameters.AddWithValue("@gp_key_value", item.gp_key_value == null ? SqlString.Null : (string)item.gp_key_value);
                    query.Parameters.AddWithValue("@gp_description", item.gp_description == null? SqlString.Null : (string)item.gp_description);
                    //IdInsertado = Convert.ToInt32(query.ExecuteScalar());
                    query.ExecuteNonQuery();
                    respuesta = true;
                    //IdInsertado = item.gu_user_id;
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
            }
            return respuesta;
        }
        public int GetTotalGeneralParams()
        {
            int total = 0;

            string query = @"
            select count(*) as total from 
            [dbo].[general_params]
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
                            total = (int)data["total"];
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
