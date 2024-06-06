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
    public class account_operations_dal
    {
        private readonly string bd_username = string.Empty;
        private readonly string bd_password = string.Empty;
        private readonly string bd_datasource = string.Empty;
        private readonly string connectionString = string.Empty;
        public account_operations_dal(string database_name)
        {
            bd_username = ConfigurationManager.AppSettings["bd_username"];
            bd_password = ConfigurationManager.AppSettings["bd_password"];
            bd_datasource = ConfigurationManager.AppSettings["bd_datasource"];
            connectionString = $"Data Source={bd_datasource};Initial Catalog={database_name};Integrated Security=False;User ID={bd_username};Password={bd_password}";

        }
        public bool SaveAccountOperations(account_operations item)
        {
            //bool respuesta = false;
            long IdInsertado = 0;
            string consulta = @"
if not exists (select ao_operation_id from [dbo].[account_operations] where ao_operation_id = @ao_operation_id)
begin
INSERT INTO [dbo].[account_operations]
           ([ao_operation_id]
           ,[ao_datetime]
           ,[ao_code]
           ,[ao_account_id]
           ,[ao_cashier_session_id]
           ,[ao_mb_account_id]
           ,[ao_promo_id]
           ,[ao_amount]
           ,[ao_non_redeemable]
           ,[ao_won_lock]
           ,[ao_operation_data]
           ,[ao_automatically_printed]
           ,[ao_non_redeemable2]
           ,[ao_redeemable]
           ,[ao_promo_redeemable]
           ,[ao_promo_not_redeemable]
           ,[ao_user_id]
           ,[ao_undo_status]
           ,[ao_undo_operation_id]
           ,[ao_reason_id]
           ,[ao_comment]
           ,[ao_comment_handpay]
           ,[ao_venue_id]
           ,[ao_spent_used])
--output inserted.ao_operation_id
     VALUES
           (@ao_operation_id
           ,@ao_datetime
           ,@ao_code
           ,@ao_account_id
           ,@ao_cashier_session_id
           ,@ao_mb_account_id
           ,@ao_promo_id
           ,@ao_amount
           ,@ao_non_redeemable
           ,@ao_won_lock
           ,@ao_operation_data
           ,@ao_automatically_printed
           ,@ao_non_redeemable2
           ,@ao_redeemable
           ,@ao_promo_redeemable
           ,@ao_promo_not_redeemable
           ,@ao_user_id
           ,@ao_undo_status
           ,@ao_undo_operation_id
           ,@ao_reason_id
           ,@ao_comment
           ,@ao_comment_handpay
           ,@ao_venue_id
           ,@ao_spent_used)
end
                      ";
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@ao_operation_id", item.ao_operation_id == null ? DBNull.Value : (object)item.ao_operation_id);
                    query.Parameters.AddWithValue("@ao_datetime", item.ao_datetime == null ? SqlDateTime.Null : (object)item.ao_datetime);
                    query.Parameters.AddWithValue("@ao_code", item.ao_code == null ? DBNull.Value : (object)item.ao_code);
                    query.Parameters.AddWithValue("@ao_account_id", item.ao_account_id == null ? DBNull.Value : (object)item.ao_account_id);
                    query.Parameters.AddWithValue("@ao_cashier_session_id", item.ao_cashier_session_id == null ? DBNull.Value : (object)item.ao_cashier_session_id);
                    query.Parameters.AddWithValue("@ao_mb_account_id", item.ao_mb_account_id == null ? DBNull.Value : (object)item.ao_mb_account_id);
                    query.Parameters.AddWithValue("@ao_promo_id", item.ao_promo_id == null ? DBNull.Value : (object)item.ao_promo_id);
                    query.Parameters.AddWithValue("@ao_amount", item.ao_amount == null ? DBNull.Value : (object)item.ao_amount);
                    query.Parameters.AddWithValue("@ao_non_redeemable", item.ao_non_redeemable == null ? DBNull.Value : (object)item.ao_non_redeemable);
                    query.Parameters.AddWithValue("@ao_won_lock", item.ao_won_lock == null ? DBNull.Value : (object)item.ao_won_lock);
                    query.Parameters.AddWithValue("@ao_operation_data", item.ao_operation_data == null ? DBNull.Value : (object)item.ao_operation_data);
                    query.Parameters.AddWithValue("@ao_automatically_printed", item.ao_automatically_printed == null ? DBNull.Value : (object)item.ao_automatically_printed);
                    query.Parameters.AddWithValue("@ao_non_redeemable2", item.ao_non_redeemable2 == null ? DBNull.Value : (object)item.ao_non_redeemable2);
                    query.Parameters.AddWithValue("@ao_redeemable", item.ao_redeemable == null ? DBNull.Value : (object)item.ao_redeemable);
                    query.Parameters.AddWithValue("@ao_promo_redeemable", item.ao_promo_redeemable == null ? DBNull.Value : (object)item.ao_promo_redeemable);
                    query.Parameters.AddWithValue("@ao_promo_not_redeemable", item.ao_promo_not_redeemable == null ? DBNull.Value : (object)item.ao_promo_not_redeemable);
                    query.Parameters.AddWithValue("@ao_user_id", item.ao_user_id == null ? DBNull.Value : (object)item.ao_user_id);
                    query.Parameters.AddWithValue("@ao_undo_status", item.ao_undo_status == null ? DBNull.Value : (object)item.ao_undo_status);
                    query.Parameters.AddWithValue("@ao_undo_operation_id", item.ao_undo_operation_id == null ? DBNull.Value : (object)item.ao_undo_operation_id);
                    query.Parameters.AddWithValue("@ao_reason_id", item.ao_reason_id == null ? DBNull.Value : (object)item.ao_reason_id);
                    query.Parameters.AddWithValue("@ao_comment", item.ao_comment == null ? DBNull.Value : (object)item.ao_comment);
                    query.Parameters.AddWithValue("@ao_comment_handpay", item.ao_comment_handpay == null ? DBNull.Value : (object)item.ao_comment_handpay);
                    query.Parameters.AddWithValue("@ao_venue_id", item.ao_venue_id == null ? DBNull.Value : (object)item.ao_venue_id);
                    query.Parameters.AddWithValue("@ao_spent_used", item.ao_spent_used == null ? DBNull.Value : (object)item.ao_spent_used);
                    query.Parameters.AddWithValue("@ao_spent_remaining", item.ao_spent_remaining == null ? DBNull.Value : (object)item.ao_spent_remaining);
                    //IdInsertado = Convert.ToInt32(query.ExecuteScalar());
                    query.ExecuteNonQuery();
                    //IdInsertado = item.ao_operation_id;
                    return true;
                }
            }
            catch (Exception ex)
            {
                funciones.logueo($"Error metodo SaveAccountOperations - {ex.Message}");
            }
            return false;
        }
        public long GetLastIdInserted()
        {
            long total = 0;

            string query = @"
            select top 1 ao_operation_id as lastid from 
            [dbo].[account_operations]
            order by ao_operation_id desc
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
                            total = ManejoNulos.ManageNullInteger64(data["lastid"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                funciones.logueo($"Error metodo GetLastIdInserted account_operations_dal.cs- {ex.Message}", "Error");
                total = 0;
            }

            return total;
        }
    }
}