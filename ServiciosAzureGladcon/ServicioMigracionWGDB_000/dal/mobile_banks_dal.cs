using ServicioMigracionWGDB_000.models;
using ServicioMigracionWGDB_000.utilitarios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioMigracionWGDB_000.dal
{
    public class mobile_banks_dal
    {
        private readonly string _conexion = string.Empty;

        public mobile_banks_dal()
        {
            _conexion = ConfigurationManager.ConnectionStrings["connection_wgdb_000"].ConnectionString;
        }
        public List<mobile_banks> GetMobileBanksPaginated(long lastid, int skip, int pageSize)
        {
            var result = new List<mobile_banks>();
            try
            {
                string query = $@"
SELECT [mb_account_id]
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
      ,[mb_timestamp]
      ,[mb_track_number]
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
      ,[mb_lock]
  FROM [dbo].[mobile_banks]
  where mb_account_id > {lastid}
  order by mb_account_id asc
  OFFSET {skip} ROWS -- Número de filas para omitir
  FETCH NEXT {pageSize} ROWS ONLY; -- Número de filas para devolver
    ";
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var command = new SqlCommand(query, con);
                    using (var dr = command.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                var item = new mobile_banks()
                                {
                                    mb_account_id = (long)dr["mb_account_id"],
                                    mb_account_type = (short)dr["mb_account_type"],
                                    mb_holder_name = dr["mb_holder_name"] == DBNull.Value ? null : (string)dr["mb_holder_name"],
                                    mb_blocked = (bool)dr["mb_blocked"],
                                    mb_track_data = dr["mb_track_data"] == DBNull.Value ? null : (string)dr["mb_track_data"],
                                    mb_balance = (decimal)dr["mb_balance"],
                                    mb_initial_limit = (decimal)dr["mb_initial_limit"],
                                    mb_cash_in = (decimal)dr["mb_cash_in"],
                                    mb_deposit = (decimal)dr["mb_deposit"],
                                    mb_extension = (decimal)dr["mb_extension"],
                                    mb_pending_cash = (decimal)dr["mb_pending_cash"],
                                    mb_pin = dr["mb_pin"] == DBNull.Value ? null : (string)dr["mb_pin"],
                                    mb_cashier_session_id = dr["mb_cashier_session_id"] == DBNull.Value ? null : (long?)dr["mb_cashier_session_id"],
                                    mb_failed_login_attempts = (short)dr["mb_failed_login_attempts"],
                                    mb_last_activity = dr["mb_last_activity"] == DBNull.Value ? null : (DateTime?)dr["mb_last_activity"],
                                    mb_last_terminal_id = dr["mb_last_terminal_id"] == DBNull.Value ? null : (int?)dr["mb_last_terminal_id"],
                                    mb_last_terminal_name = dr["mb_last_terminal_name"] == DBNull.Value ? null : (string)dr["mb_last_terminal_name"],
                                    mb_timestamp = dr["mb_timestamp"] == DBNull.Value ? null : (byte[])dr["mb_timestamp"],
                                    mb_track_number = dr["mb_track_number"] == DBNull.Value ? null : (long?)dr["mb_track_number"],
                                    mb_terminal_id = dr["mb_terminal_id"] == DBNull.Value ? null : (int?)dr["mb_terminal_id"],
                                    mb_over_cash = (decimal)dr["mb_over_cash"],
                                    mb_total_limit = dr["mb_total_limit"] == DBNull.Value ? null : (decimal?)dr["mb_total_limit"],
                                    mb_recharge_limit = dr["mb_recharge_limit"] == DBNull.Value ? null : (decimal?)dr["mb_recharge_limit"],
                                    mb_number_of_recharges_limit = dr["mb_number_of_recharges_limit"] == DBNull.Value ? null : (int?)dr["mb_number_of_recharges_limit"],
                                    mb_actual_number_of_recharges = dr["mb_actual_number_of_recharges"] == DBNull.Value ? null : (int?)dr["mb_actual_number_of_recharges"],
                                    mb_employee_code = dr["mb_employee_code"] == DBNull.Value ? null : (string)dr["mb_employee_code"],
                                    mb_shortfall_cash = (decimal)dr["mb_shortfall_cash"],
                                    mb_over_cash_session = (decimal)dr["mb_over_cash_session"],
                                    mb_shortfall_cash_session = (decimal)dr["mb_shortfall_cash_session"],
                                    mb_session_status = (short)dr["mb_session_status"],
                                    mb_user_id = dr["mb_user_id"] == DBNull.Value ? null : (int?)dr["mb_user_id"],
                                    mb_lock = (bool)dr["mb_lock"],
                                };
                                result.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = new List<mobile_banks>();
            }
            return result;
        }
        public long GetTotalMobileBanksForMigration(long lastid)
        {
            long total = 0;

            string query = @"
            select count(*) as total from 
            [dbo].[mobile_banks]
where mb_account_id > @lastid
";

            try
            {
                using (SqlConnection conecction = new SqlConnection(_conexion))
                {
                    conecction.Open();
                    SqlCommand command = new SqlCommand(query, conecction);
                    command.Parameters.AddWithValue("@lastid", lastid);
                    using (SqlDataReader data = command.ExecuteReader())
                    {
                        if (data.Read())
                        {
                            total = ManejoNulos.ManageNullInteger64(data["total"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                funciones.logueo($"Error metodo GetTotalMobileBanksForMigration - {ex.Message}", "Error");
                total = 0;
            }

            return total;
        }
        public int SaveMobileBanks(mobile_banks item)
        {
            //bool respuesta = false;
            int IdInsertado = 0;
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
output inserted.mb_account_id
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
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@mb_account_id", ManejoNulos.ManageNullInteger64(item.mb_account_id));
                    query.Parameters.AddWithValue("@mb_account_type", ManejoNulos.ManageNullShort(item.mb_account_type));
                    query.Parameters.AddWithValue("@mb_holder_name", ManejoNulos.ManageNullStr(item.mb_holder_name));
                    query.Parameters.AddWithValue("@mb_blocked", ManejoNulos.ManegeNullBool(item.mb_blocked));
                    query.Parameters.AddWithValue("@mb_track_data", ManejoNulos.ManageNullStr(item.mb_track_data));
                    query.Parameters.AddWithValue("@mb_balance", ManejoNulos.ManageNullDecimal(item.mb_balance));
                    query.Parameters.AddWithValue("@mb_initial_limit", ManejoNulos.ManageNullDecimal(item.mb_initial_limit));
                    query.Parameters.AddWithValue("@mb_cash_in", ManejoNulos.ManageNullDecimal(item.mb_cash_in));
                    query.Parameters.AddWithValue("@mb_deposit", ManejoNulos.ManageNullDecimal(item.mb_deposit));
                    query.Parameters.AddWithValue("@mb_extension", ManejoNulos.ManageNullDecimal(item.mb_extension));
                    query.Parameters.AddWithValue("@mb_pending_cash", ManejoNulos.ManageNullDecimal(item.mb_pending_cash));
                    query.Parameters.AddWithValue("@mb_pin", ManejoNulos.ManageNullStr(item.mb_pin));
                    query.Parameters.AddWithValue("@mb_cashier_session_id", ManejoNulos.ManageNullInteger64(item.mb_cashier_session_id));
                    query.Parameters.AddWithValue("@mb_failed_login_attempts", ManejoNulos.ManageNullShort(item.mb_failed_login_attempts));
                    query.Parameters.AddWithValue("@mb_last_activity", ManejoNulos.ManageNullDate(item.mb_last_activity));
                    query.Parameters.AddWithValue("@mb_last_terminal_id", ManejoNulos.ManageNullInteger(item.mb_last_terminal_id));
                    query.Parameters.AddWithValue("@mb_last_terminal_name", ManejoNulos.ManageNullStr(item.mb_last_terminal_name));
                    query.Parameters.AddWithValue("@mb_timestamp", ManejoNulos.ManageNullInteger64(item.mb_timestamp));
                    query.Parameters.AddWithValue("@mb_track_number", ManejoNulos.ManageNullInteger64(item.mb_track_number));
                    query.Parameters.AddWithValue("@mb_terminal_id", ManejoNulos.ManageNullInteger(item.mb_terminal_id));
                    query.Parameters.AddWithValue("@mb_over_cash", ManejoNulos.ManageNullDecimal(item.mb_over_cash));
                    query.Parameters.AddWithValue("@mb_total_limit", ManejoNulos.ManageNullDecimal(item.mb_total_limit));
                    query.Parameters.AddWithValue("@mb_recharge_limit", ManejoNulos.ManageNullDecimal(item.mb_recharge_limit));
                    query.Parameters.AddWithValue("@mb_number_of_recharges_limit", ManejoNulos.ManageNullInteger(item.mb_number_of_recharges_limit));
                    query.Parameters.AddWithValue("@mb_actual_number_of_recharges", ManejoNulos.ManageNullInteger(item.mb_actual_number_of_recharges));
                    query.Parameters.AddWithValue("@mb_employee_code", ManejoNulos.ManageNullStr(item.mb_employee_code));
                    query.Parameters.AddWithValue("@mb_shortfall_cash", ManejoNulos.ManageNullDecimal(item.mb_shortfall_cash));
                    query.Parameters.AddWithValue("@mb_over_cash_session", ManejoNulos.ManageNullDecimal(item.mb_over_cash_session));
                    query.Parameters.AddWithValue("@mb_shortfall_cash_session", ManejoNulos.ManageNullDecimal(item.mb_shortfall_cash_session));
                    query.Parameters.AddWithValue("@mb_session_status", ManejoNulos.ManageNullShort(item.mb_session_status));
                    query.Parameters.AddWithValue("@mb_user_id", ManejoNulos.ManageNullInteger(item.mb_user_id));
                    query.Parameters.AddWithValue("@mb_lock", ManejoNulos.ManegeNullBool(item.mb_lock));
                    IdInsertado = Convert.ToInt32(query.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                IdInsertado = 0;
            }
            return IdInsertado;
        }
    }
}
