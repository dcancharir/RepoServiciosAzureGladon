using ServicioServidorVPN.utilitarios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServicioServidorVPN.WGDB_000.model;
using System.Configuration;
using System.Data.SqlTypes;
namespace ServicioServidorVPN.WGDB_000.dal
{
    public class account_movements_dal
    {
        private readonly string bd_username = string.Empty;
        private readonly string bd_password = string.Empty;
        private readonly string bd_datasource = string.Empty;
        private readonly string connectionString = string.Empty;
        public account_movements_dal(string database_name)
        {
            bd_username = ConfigurationManager.AppSettings["bd_username"];
            bd_password = ConfigurationManager.AppSettings["bd_password"];
            bd_datasource = ConfigurationManager.AppSettings["bd_datasource"];
            connectionString = $"Data Source={bd_datasource};Initial Catalog={database_name};Integrated Security=False;User ID={bd_username};Password={bd_password}";

        }
        public long SaveAccountMovements(account_movements item)
        {
            //bool respuesta = false;
            long IdInsertado = 0;
            string consulta = @"
INSERT INTO [dbo].[account_movements]
           ([am_movement_id]
           ,[am_play_session_id]
           ,[am_account_id]
           ,[am_terminal_id]
           ,[am_wcp_sequence_id]
           ,[am_wcp_transaction_id]
           ,[am_datetime]
           ,[am_type]
           ,[am_initial_balance]
           ,[am_sub_amount]
           ,[am_add_amount]
           ,[am_final_balance]
           ,[am_cashier_id]
           ,[am_cashier_name]
           ,[am_terminal_name]
           ,[am_operation_id]
           ,[am_details]
           ,[am_reasons]
           ,[am_undo_status]
           ,[am_track_data]
           ,[am_modified_bucket_reason]
           ,[am_data_before]
           ,[am_data_after])
--output inserted.am_movement_id
     VALUES
           (@am_movement_id
           ,@am_play_session_id
           ,@am_account_id
           ,@am_terminal_id
           ,@am_wcp_sequence_id
           ,@am_wcp_transaction_id
           ,@am_datetime
           ,@am_type
           ,@am_initial_balance
           ,@am_sub_amount
           ,@am_add_amount
           ,@am_final_balance
           ,@am_cashier_id
           ,@am_cashier_name
           ,@am_terminal_name
           ,@am_operation_id
           ,@am_details
           ,@am_reasons
           ,@am_undo_status
           ,@am_track_data
           ,@am_modified_bucket_reason
           ,@am_data_before
           ,@am_data_after)

                      ";
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@am_movement_id", item.am_movement_id == null ? DBNull.Value : (object)item.am_movement_id);
                    query.Parameters.AddWithValue("@am_play_session_id", item.am_play_session_id == null ? DBNull.Value : (object)item.am_play_session_id);
                    query.Parameters.AddWithValue("@am_account_id", item.am_account_id == null ? DBNull.Value : (object)item.am_account_id);
                    query.Parameters.AddWithValue("@am_terminal_id", item.am_terminal_id == null ? DBNull.Value : (object)item.am_terminal_id);
                    query.Parameters.AddWithValue("@am_wcp_sequence_id", item.am_wcp_sequence_id == null ? DBNull.Value : (object)item.am_wcp_sequence_id);
                    query.Parameters.AddWithValue("@am_wcp_transaction_id", item.am_wcp_transaction_id == null ? DBNull.Value : (object)item.am_wcp_transaction_id);
                    query.Parameters.AddWithValue("@am_datetime", item.am_datetime == null ? DBNull.Value : (object)item.am_datetime);
                    query.Parameters.AddWithValue("@am_type", item.am_type == null ? DBNull.Value : (object)item.am_type);
                    query.Parameters.AddWithValue("@am_initial_balance", item.am_initial_balance == null ? DBNull.Value : (object)item.am_initial_balance);
                    query.Parameters.AddWithValue("@am_sub_amount", item.am_sub_amount == null ? DBNull.Value : (object)item.am_sub_amount);
                    query.Parameters.AddWithValue("@am_add_amount", item.am_add_amount == null ? DBNull.Value : (object)item.am_add_amount);
                    query.Parameters.AddWithValue("@am_final_balance", item.am_final_balance == null ? DBNull.Value : (object)item.am_final_balance);
                    query.Parameters.AddWithValue("@am_cashier_id", item.am_cashier_id == null ? DBNull.Value : (object)item.am_cashier_id);
                    query.Parameters.AddWithValue("@am_cashier_name", item.am_cashier_name == null ? DBNull.Value : (object)item.am_cashier_name);
                    query.Parameters.AddWithValue("@am_terminal_name", item.am_terminal_name == null ? DBNull.Value : (object)item.am_terminal_name);
                    query.Parameters.AddWithValue("@am_operation_id", item.am_operation_id == null ? DBNull.Value : (object)item.am_operation_id);
                    query.Parameters.AddWithValue("@am_details", item.am_details == null ? DBNull.Value : (object)item.am_details);
                    query.Parameters.AddWithValue("@am_reasons", item.am_reasons == null ? DBNull.Value : (object)item.am_reasons);
                    query.Parameters.AddWithValue("@am_undo_status", item.am_undo_status == null ? DBNull.Value : (object)item.am_undo_status);
                    query.Parameters.AddWithValue("@am_track_data", item.am_track_data == null ? DBNull.Value : (object)item.am_track_data);
                    query.Parameters.AddWithValue("@am_modified_bucket_reason", item.am_modified_bucket_reason == null ? DBNull.Value : (object)item.am_modified_bucket_reason);
                    query.Parameters.AddWithValue("@am_data_before", item.am_data_before == null ? DBNull.Value : (object)item.am_data_before);
                    query.Parameters.AddWithValue("@am_data_after", item.am_data_after == null ? DBNull.Value : (object)item.am_data_after);
                    //IdInsertado = Convert.ToInt32(query.ExecuteScalar());
                    query.ExecuteNonQuery();
                    IdInsertado = item.am_movement_id;
                }
            }
            catch (Exception ex)
            {
                funciones.logueo($"Error metodo SaveAccountMovements - {ex.Message}");
                IdInsertado = 0;
            }
            return IdInsertado;
        }
        public long GetLastIdInserted()
        {
            long total = 0;

            string query = @"
            select top 1 am_movement_id as lastid from 
            [dbo].[account_movements]
            order by am_movement_id desc
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
                            total = (long)data["lastid"];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                funciones.logueo($"Error metodo GetLastIdInserted - {ex.Message}");
                total = -1;
            }

            return total;
        }
    }
}
