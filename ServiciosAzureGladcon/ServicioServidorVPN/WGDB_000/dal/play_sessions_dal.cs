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
    public class play_sessions_dal
    {
        private readonly string bd_username = string.Empty;
        private readonly string bd_password = string.Empty;
        private readonly string bd_datasource = string.Empty;
        private readonly string connectionString = string.Empty;
        public play_sessions_dal(string database_name)
        {
            bd_username = ConfigurationManager.AppSettings["bd_username"];
            bd_password = ConfigurationManager.AppSettings["bd_password"];
            bd_datasource = ConfigurationManager.AppSettings["bd_datasource"];
            connectionString = $"Data Source={bd_datasource};Initial Catalog={database_name};Integrated Security=False;User ID={bd_username};Password={bd_password}";

        }
        public bool SavePlaySessions(play_sessions item)
        {
            //bool respuesta = false;
            long IdInsertado = 0;
            string consulta = @"
if not exists (select ps_play_session_id from [dbo].[play_sessions] where ps_play_session_id = @ps_play_session_id)
begin
INSERT INTO [dbo].[play_sessions]
           ([ps_play_session_id]
           ,[ps_account_id]
           ,[ps_terminal_id]
           ,[ps_type]
           ,[ps_type_data]
           ,[ps_status]
           ,[ps_started]
           ,[ps_initial_balance]
           ,[ps_played_count]
           ,[ps_played_amount]
           ,[ps_won_count]
           ,[ps_won_amount]
           ,[ps_cash_in]
           ,[ps_cash_out]
           ,[ps_finished]
           ,[ps_final_balance]
           ,[ps_locked]
           ,[ps_stand_alone]
           ,[ps_promo]
           ,[ps_wcp_transaction_id]
           ,[ps_redeemable_cash_in]
           ,[ps_redeemable_cash_out]
           ,[ps_redeemable_played]
           ,[ps_redeemable_won]
           ,[ps_non_redeemable_cash_in]
           ,[ps_non_redeemable_cash_out]
           ,[ps_non_redeemable_played]
           ,[ps_non_redeemable_won]
           ,[ps_balance_mismatch]
           ,[ps_spent_used]
           ,[ps_cancellable_amount]
           ,[ps_reported_balance_mismatch]
           ,[ps_re_cash_in]
           ,[ps_promo_re_cash_in]
           ,[ps_re_cash_out]
           ,[ps_promo_re_cash_out]
           ,[ps_computed_points]
           ,[ps_awarded_points]
           ,[ps_awarded_points_status]
           ,[ps_re_ticket_in]
           ,[ps_promo_re_ticket_in]
           ,[ps_promo_nr_ticket_in]
           ,[ps_re_ticket_out]
           ,[ps_promo_nr_ticket_out]
           ,[ps_bills_in_amount]
           ,[ps_redeemable_played_original]
           ,[ps_redeemable_won_original]
           ,[ps_non_redeemable_played_original]
           ,[ps_non_redeemable_won_original]
           ,[ps_played_count_original]
           ,[ps_won_count_original]
           ,[ps_aux_ft_re_cash_in]
           ,[ps_aux_ft_nr_cash_in]
           ,[ps_re_found_in_egm]
           ,[ps_nr_found_in_egm]
           ,[ps_re_remaining_in_egm]
           ,[ps_nr_remaining_in_egm]
           ,[ps_handpays_amount]
           ,[ps_handpays_paid_amount]
           ,[ps_sequence]
           ,[ps_handpays_cc_amount]
           ,[ps_handpays_jkp_amount]
           ,[ps_is_new_session]
           ,[ps_nr_promo_available]
           ,[ps_theo_win]
           ,[ps_paid_from_hopper]
           ,[ps_bills_dispensed]
           ,[ps_total_drop]
           ,[ps_total_cancelled]
           ,[ps_cash_in_coins]
           ,[ps_cash_in_bills]
           ,[ps_original_account])
--output inserted.ps_play_session_id
     VALUES
           (@ps_play_session_id
           ,@ps_account_id
           ,@ps_terminal_id
           ,@ps_type
           ,@ps_type_data
           ,@ps_status
           ,@ps_started
           ,@ps_initial_balance
           ,@ps_played_count
           ,@ps_played_amount
           ,@ps_won_count
           ,@ps_won_amount
           ,@ps_cash_in
           ,@ps_cash_out
           ,@ps_finished
           ,@ps_final_balance
           ,@ps_locked
           ,@ps_stand_alone
           ,@ps_promo
           ,@ps_wcp_transaction_id
           ,@ps_redeemable_cash_in
           ,@ps_redeemable_cash_out
           ,@ps_redeemable_played
           ,@ps_redeemable_won
           ,@ps_non_redeemable_cash_in
           ,@ps_non_redeemable_cash_out
           ,@ps_non_redeemable_played
           ,@ps_non_redeemable_won
           ,@ps_balance_mismatch
           ,@ps_spent_used
           ,@ps_cancellable_amount
           ,@ps_reported_balance_mismatch
           ,@ps_re_cash_in
           ,@ps_promo_re_cash_in
           ,@ps_re_cash_out
           ,@ps_promo_re_cash_out
           ,@ps_computed_points
           ,@ps_awarded_points
           ,@ps_awarded_points_status
           ,@ps_re_ticket_in
           ,@ps_promo_re_ticket_in
           ,@ps_promo_nr_ticket_in
           ,@ps_re_ticket_out
           ,@ps_promo_nr_ticket_out
           ,@ps_bills_in_amount
           ,@ps_redeemable_played_original
           ,@ps_redeemable_won_original
           ,@ps_non_redeemable_played_original
           ,@ps_non_redeemable_won_original
           ,@ps_played_count_original
           ,@ps_won_count_original
           ,@ps_aux_ft_re_cash_in
           ,@ps_aux_ft_nr_cash_in
           ,@ps_re_found_in_egm
           ,@ps_nr_found_in_egm
           ,@ps_re_remaining_in_egm
           ,@ps_nr_remaining_in_egm
           ,@ps_handpays_amount
           ,@ps_handpays_paid_amount
           ,@ps_sequence
           ,@ps_handpays_cc_amount
           ,@ps_handpays_jkp_amount
           ,@ps_is_new_session
           ,@ps_nr_promo_available
           ,@ps_theo_win
           ,@ps_paid_from_hopper
           ,@ps_bills_dispensed
           ,@ps_total_drop
           ,@ps_total_cancelled
           ,@ps_cash_in_coins
           ,@ps_cash_in_bills
           ,@ps_original_account)
end

                      ";
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@ps_play_session_id", item.ps_play_session_id == null ? DBNull.Value : (object)item.ps_play_session_id);
                    query.Parameters.AddWithValue("@ps_account_id", item.ps_account_id == null ? DBNull.Value : (object)item.ps_account_id);
                    query.Parameters.AddWithValue("@ps_terminal_id", item.ps_terminal_id == null ? DBNull.Value : (object)item.ps_terminal_id);
                    query.Parameters.AddWithValue("@ps_type", item.ps_type == null ? DBNull.Value : (object)item.ps_type);
                    query.Parameters.AddWithValue("@ps_type_data", item.ps_type_data == null ? DBNull.Value : (object)item.ps_type_data);
                    query.Parameters.AddWithValue("@ps_status", item.ps_status == null ? DBNull.Value : (object)item.ps_status);
                    query.Parameters.AddWithValue("@ps_started", item.ps_started == null ? DBNull.Value : (object)item.ps_started);
                    query.Parameters.AddWithValue("@ps_initial_balance", item.ps_initial_balance == null ? DBNull.Value : (object)item.ps_initial_balance);
                    query.Parameters.AddWithValue("@ps_played_count", item.ps_played_count == null ? DBNull.Value : (object)item.ps_played_count);
                    query.Parameters.AddWithValue("@ps_played_amount", item.ps_played_amount == null ? DBNull.Value : (object)item.ps_played_amount);
                    query.Parameters.AddWithValue("@ps_won_count", item.ps_won_count == null ? DBNull.Value : (object)item.ps_won_count);
                    query.Parameters.AddWithValue("@ps_won_amount", item.ps_won_amount == null ? DBNull.Value : (object)item.ps_won_amount);
                    query.Parameters.AddWithValue("@ps_cash_in", item.ps_cash_in == null ? DBNull.Value : (object)item.ps_cash_in);
                    query.Parameters.AddWithValue("@ps_cash_out", item.ps_cash_out == null ? DBNull.Value : (object)item.ps_cash_out);
                    query.Parameters.AddWithValue("@ps_finished", item.ps_finished == null ? DBNull.Value : (object)item.ps_finished);
                    query.Parameters.AddWithValue("@ps_final_balance", item.ps_final_balance == null ? DBNull.Value : (object)item.ps_final_balance);
                    query.Parameters.AddWithValue("@ps_locked", item.ps_locked == null ? DBNull.Value : (object)item.ps_locked);
                    query.Parameters.AddWithValue("@ps_stand_alone", item.ps_stand_alone == null ? DBNull.Value : (object)item.ps_stand_alone);
                    query.Parameters.AddWithValue("@ps_promo", item.ps_promo == null ? DBNull.Value : (object)item.ps_promo);
                    query.Parameters.AddWithValue("@ps_wcp_transaction_id", item.ps_wcp_transaction_id == null ? DBNull.Value : (object)item.ps_wcp_transaction_id);
                    query.Parameters.AddWithValue("@ps_total_played", item.ps_total_played == null ? DBNull.Value : (object)item.ps_total_played);
                    query.Parameters.AddWithValue("@ps_total_won", item.ps_total_won == null ? DBNull.Value : (object)item.ps_total_won);
                    query.Parameters.AddWithValue("@ps_redeemable_cash_in", item.ps_redeemable_cash_in == null ? DBNull.Value : (object)item.ps_redeemable_cash_in);
                    query.Parameters.AddWithValue("@ps_redeemable_cash_out", item.ps_redeemable_cash_out == null ? DBNull.Value : (object)item.ps_redeemable_cash_out);
                    query.Parameters.AddWithValue("@ps_redeemable_played", item.ps_redeemable_played == null ? DBNull.Value : (object)item.ps_redeemable_played);
                    query.Parameters.AddWithValue("@ps_redeemable_won", item.ps_redeemable_won == null ? DBNull.Value : (object)item.ps_redeemable_won);
                    query.Parameters.AddWithValue("@ps_non_redeemable_cash_in", item.ps_non_redeemable_cash_in == null ? DBNull.Value : (object)item.ps_non_redeemable_cash_in);
                    query.Parameters.AddWithValue("@ps_non_redeemable_cash_out", item.ps_non_redeemable_cash_out == null ? DBNull.Value : (object)item.ps_non_redeemable_cash_out);
                    query.Parameters.AddWithValue("@ps_non_redeemable_played", item.ps_non_redeemable_played == null ? DBNull.Value : (object)item.ps_non_redeemable_played);
                    query.Parameters.AddWithValue("@ps_non_redeemable_won", item.ps_non_redeemable_won == null ? DBNull.Value : (object)item.ps_non_redeemable_won);
                    query.Parameters.AddWithValue("@ps_balance_mismatch", item.ps_balance_mismatch == null ? DBNull.Value : (object)item.ps_balance_mismatch);
                    query.Parameters.AddWithValue("@ps_spent_used", item.ps_spent_used == null ? DBNull.Value : (object)item.ps_spent_used);
                    query.Parameters.AddWithValue("@ps_spent_remaining", item.ps_spent_remaining == null ? DBNull.Value : (object)item.ps_spent_remaining);
                    query.Parameters.AddWithValue("@ps_cancellable_amount", item.ps_cancellable_amount == null ? DBNull.Value : (object)item.ps_cancellable_amount);
                    query.Parameters.AddWithValue("@ps_reported_balance_mismatch", item.ps_reported_balance_mismatch == null ? DBNull.Value : (object)item.ps_reported_balance_mismatch);
                    query.Parameters.AddWithValue("@ps_re_cash_in", item.ps_re_cash_in == null ? DBNull.Value : (object)item.ps_re_cash_in);
                    query.Parameters.AddWithValue("@ps_promo_re_cash_in", item.ps_promo_re_cash_in == null ? DBNull.Value : (object)item.ps_promo_re_cash_in);
                    query.Parameters.AddWithValue("@ps_re_cash_out", item.ps_re_cash_out == null ? DBNull.Value : (object)item.ps_re_cash_out);
                    query.Parameters.AddWithValue("@ps_promo_re_cash_out", item.ps_promo_re_cash_out == null ? DBNull.Value : (object)item.ps_promo_re_cash_out);
                    query.Parameters.AddWithValue("@ps_computed_points", item.ps_computed_points == null ? DBNull.Value : (object)item.ps_computed_points);
                    query.Parameters.AddWithValue("@ps_awarded_points", item.ps_awarded_points == null ? DBNull.Value : (object)item.ps_awarded_points);
                    query.Parameters.AddWithValue("@ps_awarded_points_status", item.ps_awarded_points_status == null ? DBNull.Value : (object)item.ps_awarded_points_status);
                    query.Parameters.AddWithValue("@ps_re_ticket_in", item.ps_re_ticket_in == null ? DBNull.Value : (object)item.ps_re_ticket_in);
                    query.Parameters.AddWithValue("@ps_promo_re_ticket_in", item.ps_promo_re_ticket_in == null ? DBNull.Value : (object)item.ps_promo_re_ticket_in);
                    query.Parameters.AddWithValue("@ps_promo_nr_ticket_in", item.ps_promo_nr_ticket_in == null ? DBNull.Value : (object)item.ps_promo_nr_ticket_in);
                    query.Parameters.AddWithValue("@ps_re_ticket_out", item.ps_re_ticket_out == null ? DBNull.Value : (object)item.ps_re_ticket_out);
                    query.Parameters.AddWithValue("@ps_promo_nr_ticket_out", item.ps_promo_nr_ticket_out == null ? DBNull.Value : (object)item.ps_promo_nr_ticket_out);
                    query.Parameters.AddWithValue("@ps_bills_in_amount", item.ps_bills_in_amount == null ? DBNull.Value : (object)item.ps_bills_in_amount);
                    query.Parameters.AddWithValue("@ps_redeemable_played_original", item.ps_redeemable_played_original == null ? DBNull.Value : (object)item.ps_redeemable_played_original);
                    query.Parameters.AddWithValue("@ps_redeemable_won_original", item.ps_redeemable_won_original == null ? DBNull.Value : (object)item.ps_redeemable_won_original);
                    query.Parameters.AddWithValue("@ps_non_redeemable_played_original", item.ps_non_redeemable_played_original == null ? DBNull.Value : (object)item.ps_non_redeemable_played_original);
                    query.Parameters.AddWithValue("@ps_non_redeemable_won_original", item.ps_non_redeemable_won_original == null ? DBNull.Value : (object)item.ps_non_redeemable_won_original);
                    query.Parameters.AddWithValue("@ps_played_count_original", item.ps_played_count_original == null ? DBNull.Value : (object)item.ps_played_count_original);
                    query.Parameters.AddWithValue("@ps_won_count_original", item.ps_won_count_original == null ? DBNull.Value : (object)item.ps_won_count_original);
                    query.Parameters.AddWithValue("@ps_aux_ft_re_cash_in", item.ps_aux_ft_re_cash_in == null ? DBNull.Value : (object)item.ps_aux_ft_re_cash_in);
                    query.Parameters.AddWithValue("@ps_aux_ft_nr_cash_in", item.ps_aux_ft_nr_cash_in == null ? DBNull.Value : (object)item.ps_aux_ft_nr_cash_in);
                    query.Parameters.AddWithValue("@ps_re_found_in_egm", item.ps_re_found_in_egm == null ? DBNull.Value : (object)item.ps_re_found_in_egm);
                    query.Parameters.AddWithValue("@ps_nr_found_in_egm", item.ps_nr_found_in_egm == null ? DBNull.Value : (object)item.ps_nr_found_in_egm);
                    query.Parameters.AddWithValue("@ps_re_remaining_in_egm", item.ps_re_remaining_in_egm == null ? DBNull.Value : (object)item.ps_re_remaining_in_egm);
                    query.Parameters.AddWithValue("@ps_nr_remaining_in_egm", item.ps_nr_remaining_in_egm == null ? DBNull.Value : (object)item.ps_nr_remaining_in_egm);
                    query.Parameters.AddWithValue("@ps_handpays_amount", item.ps_handpays_amount == null ? DBNull.Value : (object)item.ps_handpays_amount);
                    query.Parameters.AddWithValue("@ps_handpays_paid_amount", item.ps_handpays_paid_amount == null ? DBNull.Value : (object)item.ps_handpays_paid_amount);
                    query.Parameters.AddWithValue("@ps_sequence", item.ps_sequence == null ? DBNull.Value : (object)item.ps_sequence);
                    query.Parameters.AddWithValue("@ps_handpays_cc_amount", item.ps_handpays_cc_amount == null ? DBNull.Value : (object)item.ps_handpays_cc_amount);
                    query.Parameters.AddWithValue("@ps_handpays_jkp_amount", item.ps_handpays_jkp_amount == null ? DBNull.Value : (object)item.ps_handpays_jkp_amount);
                    query.Parameters.AddWithValue("@ps_is_new_session", item.ps_is_new_session == null ? DBNull.Value : (object)item.ps_is_new_session);
                    query.Parameters.AddWithValue("@ps_nr_promo_available", item.ps_nr_promo_available == null ? DBNull.Value : (object)item.ps_nr_promo_available);
                    query.Parameters.AddWithValue("@ps_theo_win", item.ps_theo_win == null ? DBNull.Value : (object)item.ps_theo_win);
                    query.Parameters.AddWithValue("@ps_total_cash_in", item.ps_total_cash_in == null ? DBNull.Value : (object)item.ps_total_cash_in);
                    query.Parameters.AddWithValue("@ps_paid_from_hopper", item.ps_paid_from_hopper == null ? DBNull.Value : (object)item.ps_paid_from_hopper);
                    query.Parameters.AddWithValue("@ps_bills_dispensed", item.ps_bills_dispensed == null ? DBNull.Value : (object)item.ps_bills_dispensed);
                    query.Parameters.AddWithValue("@ps_total_drop", item.ps_total_drop == null ? DBNull.Value : (object)item.ps_total_drop);
                    query.Parameters.AddWithValue("@ps_total_cancelled", item.ps_total_cancelled == null ? DBNull.Value : (object)item.ps_total_cancelled);
                    query.Parameters.AddWithValue("@ps_cash_in_coins", item.ps_cash_in_coins == null ? DBNull.Value : (object)item.ps_cash_in_coins);
                    query.Parameters.AddWithValue("@ps_cash_in_bills", item.ps_cash_in_bills == null ? DBNull.Value : (object)item.ps_cash_in_bills);
                    query.Parameters.AddWithValue("@ps_total_cash_out", item.ps_total_cash_out == null ? DBNull.Value : (object)item.ps_total_cash_out);
                    query.Parameters.AddWithValue("@ps_original_account", item.ps_original_account == null ? DBNull.Value : (object)item.ps_original_account);
                    //IdInsertado = Convert.ToInt32(query.ExecuteScalar());
                    query.ExecuteNonQuery();
                    //IdInsertado = item.ps_play_session_id;
                    return true;
                }
            }
            catch (Exception ex)
            {
                funciones.logueo($"Error metodo SavePlaySessions - {ex.Message}");
            }
            return false;
        }
        public long GetLastIdInserted()
        {
            long total = 0;

            string query = @"
            select top 1 ps_play_session_id as lastid from 
            [dbo].[play_sessions]
            order by ps_play_session_id desc
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
                funciones.logueo($"Error metodo GetLastIdInserted play_sessions_dal.cs- {ex.Message}", "Error");
                total = 0;
            }

            return total;
        }
    }
}
