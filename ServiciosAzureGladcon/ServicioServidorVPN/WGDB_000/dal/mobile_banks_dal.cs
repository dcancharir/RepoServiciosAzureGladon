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
    public class mobile_banks_dal
    {
        private readonly string bd_username = string.Empty;
        private readonly string bd_password = string.Empty;
        private readonly string bd_datasource = string.Empty;
        private readonly string connectionString = string.Empty;
        public mobile_banks_dal(string database_name)
        {
            bd_username = ConfigurationManager.AppSettings["bd_username"];
            bd_password = ConfigurationManager.AppSettings["bd_password"];
            bd_datasource = ConfigurationManager.AppSettings["bd_datasource"];
            connectionString = $"Data Source={bd_datasource};Initial Catalog={database_name};Integrated Security=False;User ID={bd_username};Password={bd_password}";

        }
        public long SaveMobileBanks(mobile_banks item)
        {
            //bool respuesta = false;
            long IdInsertado = 0;
            string consulta = @"
INSERT INTO [dbo].[mobile_banks]
           ([mb_account_id]
           ,[mb_account_type]
           ,[mb_holder_name]
           ,[mb_blocked]
           ,[mb_track_data]
           ,[mb_balance]
           ,[mb_initial_limit]
           ,[mb_cash_in]
           ,[mb_deposit]
           ,[mb_extension]
           ,[mb_pending_cash]
           ,[mb_pin]
           ,[mb_cashier_session_id]
           ,[mb_failed_login_attempts]
           ,[mb_last_activity]
           ,[mb_last_terminal_id]
           ,[mb_last_terminal_name]
           ,[mb_terminal_id]
           ,[mb_over_cash]
           ,[mb_total_limit]
           ,[mb_recharge_limit]
           ,[mb_number_of_recharges_limit]
           ,[mb_actual_number_of_recharges]
           ,[mb_employee_code]
           ,[mb_shortfall_cash]
           ,[mb_over_cash_session]
           ,[mb_shortfall_cash_session]
           ,[mb_session_status]
           ,[mb_user_id]
           ,[mb_lock])
--output inserted.mb_account_id
     VALUES
           (@mb_account_id
           ,@mb_account_type
           ,@mb_holder_name
           ,@mb_blocked
           ,@mb_track_data
           ,@mb_balance
           ,@mb_initial_limit
           ,@mb_cash_in
           ,@mb_deposit
           ,@mb_extension
           ,@mb_pending_cash
           ,@mb_pin
           ,@mb_cashier_session_id
           ,@mb_failed_login_attempts
           ,@mb_last_activity
           ,@mb_last_terminal_id
           ,@mb_last_terminal_name
           ,@mb_terminal_id
           ,@mb_over_cash
           ,@mb_total_limit
           ,@mb_recharge_limit
           ,@mb_number_of_recharges_limit
           ,@mb_actual_number_of_recharges
           ,@mb_employee_code
           ,@mb_shortfall_cash
           ,@mb_over_cash_session
           ,@mb_shortfall_cash_session
           ,@mb_session_status
           ,@mb_user_id
           ,@mb_lock)
                      ";
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@mb_account_id", item.mb_account_id == null ? DBNull.Value : (object)item.mb_account_id);
                    query.Parameters.AddWithValue("@mb_account_type", item.mb_account_type == null ? DBNull.Value : (object)item.mb_account_type);
                    query.Parameters.AddWithValue("@mb_holder_name", item.mb_holder_name == null ? DBNull.Value : (object)item.mb_holder_name);
                    query.Parameters.AddWithValue("@mb_blocked", item.mb_blocked == null ? DBNull.Value : (object)item.mb_blocked);
                    query.Parameters.AddWithValue("@mb_track_data", item.mb_track_data == null ? DBNull.Value : (object)item.mb_track_data);
                    query.Parameters.AddWithValue("@mb_balance", item.mb_balance == null ? DBNull.Value : (object)item.mb_balance);
                    query.Parameters.AddWithValue("@mb_initial_limit", item.mb_initial_limit == null ? DBNull.Value : (object)item.mb_initial_limit);
                    query.Parameters.AddWithValue("@mb_cash_in", item.mb_cash_in == null ? DBNull.Value : (object)item.mb_cash_in);
                    query.Parameters.AddWithValue("@mb_deposit", item.mb_deposit == null ? DBNull.Value : (object)item.mb_deposit);
                    query.Parameters.AddWithValue("@mb_extension", item.mb_extension == null ? DBNull.Value : (object)item.mb_extension);
                    query.Parameters.AddWithValue("@mb_pending_cash", item.mb_pending_cash == null ? DBNull.Value : (object)item.mb_pending_cash);
                    query.Parameters.AddWithValue("@mb_pin", item.mb_pin == null ? DBNull.Value : (object)item.mb_pin);
                    query.Parameters.AddWithValue("@mb_cashier_session_id", item.mb_cashier_session_id == null ? DBNull.Value : (object)item.mb_cashier_session_id);
                    query.Parameters.AddWithValue("@mb_failed_login_attempts", item.mb_failed_login_attempts == null ? DBNull.Value : (object)item.mb_failed_login_attempts);
                    query.Parameters.AddWithValue("@mb_last_activity", item.mb_last_activity == null ? DBNull.Value : (object)item.mb_last_activity);
                    query.Parameters.AddWithValue("@mb_last_terminal_id", item.mb_last_terminal_id == null ? DBNull.Value : (object)item.mb_last_terminal_id);
                    query.Parameters.AddWithValue("@mb_last_terminal_name", item.mb_last_terminal_name == null ? DBNull.Value : (object)item.mb_last_terminal_name);
                    query.Parameters.AddWithValue("@mb_track_number", item.mb_track_number == null ? DBNull.Value : (object)item.mb_track_number);
                    query.Parameters.AddWithValue("@mb_terminal_id", item.mb_terminal_id == null ? DBNull.Value : (object)item.mb_terminal_id);
                    query.Parameters.AddWithValue("@mb_over_cash", item.mb_over_cash == null ? DBNull.Value : (object)item.mb_over_cash);
                    query.Parameters.AddWithValue("@mb_total_limit", item.mb_total_limit == null ? DBNull.Value : (object)item.mb_total_limit);
                    query.Parameters.AddWithValue("@mb_recharge_limit", item.mb_recharge_limit == null ? DBNull.Value : (object)item.mb_recharge_limit);
                    query.Parameters.AddWithValue("@mb_number_of_recharges_limit", item.mb_number_of_recharges_limit == null ? DBNull.Value : (object)item.mb_number_of_recharges_limit);
                    query.Parameters.AddWithValue("@mb_actual_number_of_recharges", item.mb_actual_number_of_recharges == null ? DBNull.Value : (object)item.mb_actual_number_of_recharges);
                    query.Parameters.AddWithValue("@mb_employee_code", item.mb_employee_code == null ? DBNull.Value : (object)item.mb_employee_code);
                    query.Parameters.AddWithValue("@mb_shortfall_cash", item.mb_shortfall_cash == null ? DBNull.Value : (object)item.mb_shortfall_cash);
                    query.Parameters.AddWithValue("@mb_over_cash_session", item.mb_over_cash_session == null ? DBNull.Value : (object)item.mb_over_cash_session);
                    query.Parameters.AddWithValue("@mb_shortfall_cash_session", item.mb_shortfall_cash_session == null ? DBNull.Value : (object)item.mb_shortfall_cash_session);
                    query.Parameters.AddWithValue("@mb_session_status", item.mb_session_status == null ? DBNull.Value : (object)item.mb_session_status);
                    query.Parameters.AddWithValue("@mb_user_id", item.mb_user_id == null ? DBNull.Value : (object)item.mb_user_id);
                    query.Parameters.AddWithValue("@mb_lock", item.mb_lock == null ? DBNull.Value : (object)item.mb_lock);
                    //IdInsertado = Convert.ToInt32(query.ExecuteScalar());
                    query.ExecuteNonQuery();
                    IdInsertado = item.mb_account_id;
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
            select top 1 mb_account_id as lastid from 
            [dbo].[mobile_banks]
            order by mb_account_id desc
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
