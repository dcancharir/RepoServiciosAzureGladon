using ServicioServidorVPN.utilitarios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServicioServidorVPN.WGDB_000.model;
using System.Configuration;
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
        public long SaveAccountOperations(account_operations item)
        {
            //bool respuesta = false;
            long IdInsertado = 0;
            string consulta = @"
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
                      ";
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@ao_operation_id", ManejoNulos.ManageNullInteger64(item.ao_operation_id));
                    query.Parameters.AddWithValue("@ao_datetime", ManejoNulos.ManageNullDate(item.ao_datetime));
                    query.Parameters.AddWithValue("@ao_code", ManejoNulos.ManageNullInteger(item.ao_code));
                    query.Parameters.AddWithValue("@ao_account_id", ManejoNulos.ManageNullInteger64(item.ao_account_id));
                    query.Parameters.AddWithValue("@ao_cashier_session_id", ManejoNulos.ManageNullInteger64(item.ao_cashier_session_id));
                    query.Parameters.AddWithValue("@ao_mb_account_id", ManejoNulos.ManageNullInteger64(item.ao_mb_account_id));
                    query.Parameters.AddWithValue("@ao_promo_id", ManejoNulos.ManageNullInteger64(item.ao_promo_id));
                    query.Parameters.AddWithValue("@ao_amount", ManejoNulos.ManageNullDecimal(item.ao_amount));
                    query.Parameters.AddWithValue("@ao_non_redeemable", ManejoNulos.ManageNullDecimal(item.ao_non_redeemable));
                    query.Parameters.AddWithValue("@ao_won_lock", ManejoNulos.ManageNullDecimal(item.ao_won_lock));
                    query.Parameters.AddWithValue("@ao_operation_data", ManejoNulos.ManageNullInteger64(item.ao_operation_data));
                    query.Parameters.AddWithValue("@ao_automatically_printed", ManejoNulos.ManegeNullBool(item.ao_automatically_printed));
                    query.Parameters.AddWithValue("@ao_non_redeemable2", ManejoNulos.ManageNullDecimal(item.ao_non_redeemable2));
                    query.Parameters.AddWithValue("@ao_redeemable", ManejoNulos.ManageNullDecimal(item.ao_redeemable));
                    query.Parameters.AddWithValue("@ao_promo_redeemable", ManejoNulos.ManageNullDecimal(item.ao_promo_redeemable));
                    query.Parameters.AddWithValue("@ao_promo_not_redeemable", ManejoNulos.ManageNullDecimal(item.ao_promo_not_redeemable));
                    query.Parameters.AddWithValue("@ao_user_id", ManejoNulos.ManageNullInteger64(item.ao_user_id));
                    query.Parameters.AddWithValue("@ao_undo_status", ManejoNulos.ManageNullInteger(item.ao_undo_status));
                    query.Parameters.AddWithValue("@ao_undo_operation_id", ManejoNulos.ManageNullInteger64(item.ao_undo_operation_id));
                    query.Parameters.AddWithValue("@ao_reason_id", ManejoNulos.ManageNullInteger64(item.ao_reason_id));
                    query.Parameters.AddWithValue("@ao_comment", ManejoNulos.ManageNullStr(item.ao_comment));
                    query.Parameters.AddWithValue("@ao_comment_handpay", ManejoNulos.ManageNullStr(item.ao_comment_handpay));
                    query.Parameters.AddWithValue("@ao_venue_id", ManejoNulos.ManageNullInteger(item.ao_venue_id));
                    query.Parameters.AddWithValue("@ao_spent_used", ManejoNulos.ManageNullDecimal(item.ao_spent_used));
                    query.Parameters.AddWithValue("@ao_spent_remaining", ManejoNulos.ManageNullDecimal(item.ao_spent_remaining));
                    //IdInsertado = Convert.ToInt32(query.ExecuteScalar());
                    query.ExecuteNonQuery();
                    IdInsertado = item.ao_operation_id;
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