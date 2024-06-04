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
    public class gift_instances_dal
    {
        private readonly string bd_username = string.Empty;
        private readonly string bd_password = string.Empty;
        private readonly string bd_datasource = string.Empty;
        private readonly string connectionString = string.Empty;
        public gift_instances_dal(string database_name)
        {
            bd_username = ConfigurationManager.AppSettings["bd_username"];
            bd_password = ConfigurationManager.AppSettings["bd_password"];
            bd_datasource = ConfigurationManager.AppSettings["bd_datasource"];
            connectionString = $"Data Source={bd_datasource};Initial Catalog={database_name};Integrated Security=False;User ID={bd_username};Password={bd_password}";

        }
        public long SaveGiftInstances(gift_instances item)
        {
            //bool respuesta = false;
            long IdInsertado = 0;
            string consulta = @"
INSERT INTO [dbo].[gift_instances]
           ([gin_gift_instance_id]
           ,[gin_oper_request_id]
           ,[gin_oper_delivery_id]
           ,[gin_account_id]
           ,[gin_gift_id]
           ,[gin_gift_name]
           ,[gin_gift_type]
           ,[gin_points]
           ,[gin_conversion_to_nrc]
           ,[gin_spent_points]
           ,[gin_requested]
           ,[gin_delivered]
           ,[gin_expiration]
           ,[gin_request_status]
           ,[gin_num_items]
           ,[gin_data_01]
           ,[gin_notification])
--output inserted.gin_gift_instance_id
     VALUES
           (@gin_gift_instance_id
           ,@gin_oper_request_id
           ,@gin_oper_delivery_id
           ,@gin_account_id
           ,@gin_gift_id
           ,@gin_gift_name
           ,@gin_gift_type
           ,@gin_points
           ,@gin_conversion_to_nrc
           ,@gin_spent_points
           ,@gin_requested
           ,@gin_delivered
           ,@gin_expiration
           ,@gin_request_status
           ,@gin_num_items
           ,@gin_data_01
           ,@gin_notification)
                      ";
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@gin_gift_instance_id", ManejoNulos.ManageNullInteger64(item.gin_gift_instance_id));
                    query.Parameters.AddWithValue("@gin_oper_request_id", ManejoNulos.ManageNullInteger64(item.gin_oper_request_id));
                    query.Parameters.AddWithValue("@gin_oper_delivery_id", ManejoNulos.ManageNullInteger64(item.gin_oper_delivery_id));
                    query.Parameters.AddWithValue("@gin_account_id", ManejoNulos.ManageNullInteger64(item.gin_account_id));
                    query.Parameters.AddWithValue("@gin_gift_id", ManejoNulos.ManageNullInteger64(item.gin_gift_id));
                    query.Parameters.AddWithValue("@gin_gift_name", ManejoNulos.ManageNullStr(item.gin_gift_name));
                    query.Parameters.AddWithValue("@gin_gift_type", ManejoNulos.ManageNullInteger(item.gin_gift_type));
                    query.Parameters.AddWithValue("@gin_points", ManejoNulos.ManageNullDecimal(item.gin_points));
                    query.Parameters.AddWithValue("@gin_conversion_to_nrc", ManejoNulos.ManageNullDecimal(item.gin_conversion_to_nrc));
                    query.Parameters.AddWithValue("@gin_spent_points", ManejoNulos.ManageNullDecimal(item.gin_spent_points));
                    query.Parameters.AddWithValue("@gin_requested", ManejoNulos.ManageNullDate(item.gin_requested));
                    query.Parameters.AddWithValue("@gin_delivered", ManejoNulos.ManageNullDate(item.gin_delivered));
                    query.Parameters.AddWithValue("@gin_expiration", ManejoNulos.ManageNullDate(item.gin_expiration));
                    query.Parameters.AddWithValue("@gin_request_status", ManejoNulos.ManageNullInteger(item.gin_request_status));
                    query.Parameters.AddWithValue("@gin_num_items", ManejoNulos.ManageNullInteger(item.gin_num_items));
                    query.Parameters.AddWithValue("@gin_data_01", ManejoNulos.ManageNullInteger64(item.gin_data_01));
                    query.Parameters.AddWithValue("@gin_notification", ManejoNulos.ManageNullInteger(item.gin_notification));
                    //query.Parameters.AddWithValue("@gin_row_version", ManejoNulos.ManageNullInteger64(item.gin_row_version));
                    //IdInsertado = Convert.ToInt32(query.ExecuteScalar());
                    query.ExecuteNonQuery();
                    IdInsertado = item.gin_gift_instance_id;
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
            select top 1 gin_gift_instance_id as lastid from 
            [dbo].[gift_instances]
            order by gin_gift_instance_id desc
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
