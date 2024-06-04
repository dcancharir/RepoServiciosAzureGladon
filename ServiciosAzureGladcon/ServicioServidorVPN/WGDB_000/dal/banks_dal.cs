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
    public class banks_dal
    {
        private readonly string bd_username = string.Empty;
        private readonly string bd_password = string.Empty;
        private readonly string bd_datasource = string.Empty;
        private readonly string connectionString = string.Empty;
        public banks_dal(string database_name)
        {
            bd_username = ConfigurationManager.AppSettings["bd_username"];
            bd_password = ConfigurationManager.AppSettings["bd_password"];
            bd_datasource = ConfigurationManager.AppSettings["bd_datasource"];
            connectionString = $"Data Source={bd_datasource};Initial Catalog={database_name};Integrated Security=False;User ID={bd_username};Password={bd_password}";

        }
        public int SaveBanks(banks item)
        {
            //bool respuesta = false;
            int IdInsertado = 0;
            string consulta = @"
INSERT INTO [dbo].[banks]
           ([bk_bank_id]
           ,[bk_area_id]
           ,[bk_name]
           ,[bk_multiposition]
           ,[bk_external_id]
           ,[bk_shape_code]
           ,[bk_shape_w]
           ,[bk_shape_h]
           ,[bk_play_safe_distance])
		   --output inserted.bk_bank_id
     VALUES
           (@bk_bank_id
           ,@bk_area_id
           ,@bk_name
           ,@bk_multiposition
           ,@bk_external_id
           ,@bk_shape_code
           ,@bk_shape_w
           ,@bk_shape_h
           ,@bk_play_safe_distance)
                      ";
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@bk_bank_id", ManejoNulos.ManageNullInteger(item.bk_bank_id));
                    query.Parameters.AddWithValue("@bk_area_id", ManejoNulos.ManageNullInteger(item.bk_area_id));
                    query.Parameters.AddWithValue("@bk_name", ManejoNulos.ManageNullStr(item.bk_name));
                    query.Parameters.AddWithValue("@bk_multiposition", ManejoNulos.ManegeNullBool(item.bk_multiposition));
                    query.Parameters.AddWithValue("@bk_external_id", ManejoNulos.ManageNullStr(item.bk_external_id));
                    query.Parameters.AddWithValue("@bk_shape_code", ManejoNulos.ManageNullStr(item.bk_shape_code));
                    query.Parameters.AddWithValue("@bk_shape_w", ManejoNulos.ManageNullInteger(item.bk_shape_w));
                    query.Parameters.AddWithValue("@bk_shape_h", ManejoNulos.ManageNullInteger(item.bk_shape_h));
                    query.Parameters.AddWithValue("@bk_play_safe_distance", ManejoNulos.ManageNullInteger(item.bk_play_safe_distance));
                    //IdInsertado = Convert.ToInt32(query.ExecuteScalar());
                    query.ExecuteNonQuery();
                    IdInsertado = item.bk_bank_id;
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
            select top 1 bk_bank_id as lastid from 
            [dbo].[banks]
            order by bk_bank_id desc
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
