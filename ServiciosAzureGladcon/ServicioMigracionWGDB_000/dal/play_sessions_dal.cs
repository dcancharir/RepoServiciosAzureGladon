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
    public class play_sessions_dal
    {
        private readonly string _conexion = string.Empty;

        public play_sessions_dal()
        {
            _conexion = ConfigurationManager.ConnectionStrings["connection_wgdb_000"].ConnectionString;
        }
        public List<play_sessions> GetPlaySessionsPaginated(long lastid, int skip, int pageSize)
        {
            var result = new List<play_sessions>();
            try
            {
                string query = $@"
SELECT [ps_play_session_id]
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
      ,[ps_timestamp]
      ,[ps_locked]
      ,[ps_stand_alone]
      ,[ps_promo]
      ,[ps_wcp_transaction_id]
      ,[ps_total_played]
      ,[ps_total_won]
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
      ,[ps_spent_remaining]
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
      ,[ps_total_cash_in]
      ,[ps_paid_from_hopper]
      ,[ps_bills_dispensed]
      ,[ps_total_drop]
      ,[ps_total_cancelled]
      ,[ps_cash_in_coins]
      ,[ps_cash_in_bills]
      ,[ps_total_cash_out]
      ,[ps_original_account]
  FROM [dbo].[play_sessions]
  where ps_play_session_id > {lastid}
  order by ps_play_session_id asc
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
                                var item = new play_sessions()
                                {
                                    ps_play_session_id = ManejoNulos.ManageNullInteger64(dr["ps_play_session_id"]),
                                    ps_account_id = ManejoNulos.ManageNullInteger64(dr["ps_account_id"]),
                                    ps_terminal_id = ManejoNulos.ManageNullInteger(dr["ps_terminal_id"]),
                                    ps_type = ManejoNulos.ManageNullInteger(dr["ps_type"]),
                                    ps_type_data = ManejoNulos.ManageNullStr(dr["ps_type_data"]),
                                    ps_status = ManejoNulos.ManageNullInteger(dr["ps_status"]),
                                    ps_started = ManejoNulos.ManageNullDate(dr["ps_started"]),
                                    ps_initial_balance = ManejoNulos.ManageNullDecimal(dr["ps_initial_balance"]),
                                    ps_played_count = ManejoNulos.ManageNullInteger(dr["ps_played_count"]),
                                    ps_played_amount = ManejoNulos.ManageNullDecimal(dr["ps_played_amount"]),
                                    ps_won_count = ManejoNulos.ManageNullInteger(dr["ps_won_count"]),
                                    ps_won_amount = ManejoNulos.ManageNullDecimal(dr["ps_won_amount"]),
                                    ps_cash_in = ManejoNulos.ManageNullDecimal(dr["ps_cash_in"]),
                                    ps_cash_out = ManejoNulos.ManageNullDecimal(dr["ps_cash_out"]),
                                    ps_finished = ManejoNulos.ManageNullDate(dr["ps_finished"]),
                                    ps_final_balance = ManejoNulos.ManageNullDecimal(dr["ps_final_balance"]),
                                    ps_timestamp = ManejoNulos.ManageNullInteger64(dr["ps_timestamp"]),
                                    ps_locked = ManejoNulos.ManageNullDate(dr["ps_locked"]),
                                    ps_stand_alone = ManejoNulos.ManegeNullBool(dr["ps_stand_alone"]),
                                    ps_promo = ManejoNulos.ManegeNullBool(dr["ps_promo"]),
                                    ps_wcp_transaction_id = ManejoNulos.ManageNullInteger64(dr["ps_wcp_transaction_id"]),
                                    ps_total_played = ManejoNulos.ManageNullDecimal(dr["ps_total_played"]),
                                    ps_total_won = ManejoNulos.ManageNullDecimal(dr["ps_total_won"]),
                                    ps_redeemable_cash_in = ManejoNulos.ManageNullDecimal(dr["ps_redeemable_cash_in"]),
                                    ps_redeemable_cash_out = ManejoNulos.ManageNullDecimal(dr["ps_redeemable_cash_out"]),
                                    ps_redeemable_played = ManejoNulos.ManageNullDecimal(dr["ps_redeemable_played"]),
                                    ps_redeemable_won = ManejoNulos.ManageNullDecimal(dr["ps_redeemable_won"]),
                                    ps_non_redeemable_cash_in = ManejoNulos.ManageNullDecimal(dr["ps_non_redeemable_cash_in"]),
                                    ps_non_redeemable_cash_out = ManejoNulos.ManageNullDecimal(dr["ps_non_redeemable_cash_out"]),
                                    ps_non_redeemable_played = ManejoNulos.ManageNullDecimal(dr["ps_non_redeemable_played"]),
                                    ps_non_redeemable_won = ManejoNulos.ManageNullDecimal(dr["ps_non_redeemable_won"]),
                                    ps_balance_mismatch = ManejoNulos.ManegeNullBool(dr["ps_balance_mismatch"]),
                                    ps_spent_used = ManejoNulos.ManageNullDecimal(dr["ps_spent_used"]),
                                    ps_spent_remaining = ManejoNulos.ManageNullDecimal(dr["ps_spent_remaining"]),
                                    ps_cancellable_amount = ManejoNulos.ManageNullDecimal(dr["ps_cancellable_amount"]),
                                    ps_reported_balance_mismatch = ManejoNulos.ManageNullDecimal(dr["ps_reported_balance_mismatch"]),
                                    ps_re_cash_in = ManejoNulos.ManageNullDecimal(dr["ps_re_cash_in"]),
                                    ps_promo_re_cash_in = ManejoNulos.ManageNullDecimal(dr["ps_promo_re_cash_in"]),
                                    ps_re_cash_out = ManejoNulos.ManageNullDecimal(dr["ps_re_cash_out"]),
                                    ps_promo_re_cash_out = ManejoNulos.ManageNullDecimal(dr["ps_promo_re_cash_out"]),
                                    ps_computed_points = ManejoNulos.ManageNullDecimal(dr["ps_computed_points"]),
                                    ps_awarded_points = ManejoNulos.ManageNullDecimal(dr["ps_awarded_points"]),
                                    ps_awarded_points_status = ManejoNulos.ManageNullInteger(dr["ps_awarded_points_status"]),
                                    ps_re_ticket_in = ManejoNulos.ManageNullDecimal(dr["ps_re_ticket_in"]),
                                    ps_promo_re_ticket_in = ManejoNulos.ManageNullDecimal(dr["ps_promo_re_ticket_in"]),
                                    ps_promo_nr_ticket_in = ManejoNulos.ManageNullDecimal(dr["ps_promo_nr_ticket_in"]),
                                    ps_re_ticket_out = ManejoNulos.ManageNullDecimal(dr["ps_re_ticket_out"]),
                                    ps_promo_nr_ticket_out = ManejoNulos.ManageNullDecimal(dr["ps_promo_nr_ticket_out"]),
                                    ps_bills_in_amount = ManejoNulos.ManageNullDecimal(dr["ps_bills_in_amount"]),
                                    ps_redeemable_played_original = ManejoNulos.ManageNullDecimal(dr["ps_redeemable_played_original"]),
                                    ps_redeemable_won_original = ManejoNulos.ManageNullDecimal(dr["ps_redeemable_won_original"]),
                                    ps_non_redeemable_played_original = ManejoNulos.ManageNullDecimal(dr["ps_non_redeemable_played_original"]),
                                    ps_non_redeemable_won_original = ManejoNulos.ManageNullDecimal(dr["ps_non_redeemable_won_original"]),
                                    ps_played_count_original = ManejoNulos.ManageNullInteger(dr["ps_played_count_original"]),
                                    ps_won_count_original = ManejoNulos.ManageNullInteger(dr["ps_won_count_original"]),
                                    ps_aux_ft_re_cash_in = ManejoNulos.ManageNullDecimal(dr["ps_aux_ft_re_cash_in"]),
                                    ps_aux_ft_nr_cash_in = ManejoNulos.ManageNullDecimal(dr["ps_aux_ft_nr_cash_in"]),
                                    ps_re_found_in_egm = ManejoNulos.ManageNullDecimal(dr["ps_re_found_in_egm"]),
                                    ps_nr_found_in_egm = ManejoNulos.ManageNullDecimal(dr["ps_nr_found_in_egm"]),
                                    ps_re_remaining_in_egm = ManejoNulos.ManageNullDecimal(dr["ps_re_remaining_in_egm"]),
                                    ps_nr_remaining_in_egm = ManejoNulos.ManageNullDecimal(dr["ps_nr_remaining_in_egm"]),
                                    ps_handpays_amount = ManejoNulos.ManageNullDecimal(dr["ps_handpays_amount"]),
                                    ps_handpays_paid_amount = ManejoNulos.ManageNullDecimal(dr["ps_handpays_paid_amount"]),
                                    ps_sequence = ManejoNulos.ManageNullInteger64(dr["ps_sequence"]),
                                    ps_handpays_cc_amount = ManejoNulos.ManageNullDecimal(dr["ps_handpays_cc_amount"]),
                                    ps_handpays_jkp_amount = ManejoNulos.ManageNullDecimal(dr["ps_handpays_jkp_amount"]),
                                    ps_is_new_session = ManejoNulos.ManegeNullBool(dr["ps_is_new_session"]),
                                    ps_nr_promo_available = ManejoNulos.ManageNullDecimal(dr["ps_nr_promo_available"]),
                                    ps_theo_win = ManejoNulos.ManageNullDecimal(dr["ps_theo_win"]),
                                    ps_total_cash_in = ManejoNulos.ManageNullDecimal(dr["ps_total_cash_in"]),
                                    ps_paid_from_hopper = ManejoNulos.ManageNullDecimal(dr["ps_paid_from_hopper"]),
                                    ps_bills_dispensed = ManejoNulos.ManageNullDecimal(dr["ps_bills_dispensed"]),
                                    ps_total_drop = ManejoNulos.ManageNullDecimal(dr["ps_total_drop"]),
                                    ps_total_cancelled = ManejoNulos.ManageNullDecimal(dr["ps_total_cancelled"]),
                                    ps_cash_in_coins = ManejoNulos.ManageNullDecimal(dr["ps_cash_in_coins"]),
                                    ps_cash_in_bills = ManejoNulos.ManageNullDecimal(dr["ps_cash_in_bills"]),
                                    ps_total_cash_out = ManejoNulos.ManageNullDecimal(dr["ps_total_cash_out"]),
                                    ps_original_account = ManejoNulos.ManageNullInteger64(dr["ps_original_account"]),
                                };
                                result.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = new List<play_sessions>();
            }
            return result;
        }
        public int GetTotalPlaySessionsForMigration(long lastid)
        {
            int total = 0;

            string query = @"
            select count(*) as total from 
            [dbo].[play_sessions]
where ps_play_session_id > @lastid
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
                            total = ManejoNulos.ManageNullInteger(data["total"]);
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
        public int SavePlaySessions(play_sessions item)
        {
            //bool respuesta = false;
            int IdInsertado = 0;
            string consulta = @"
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
output inserted.ps_play_session_id
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
                      ";
            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@ps_play_session_id", ManejoNulos.ManageNullInteger64(item.ps_play_session_id));
                    query.Parameters.AddWithValue("@ps_account_id", ManejoNulos.ManageNullInteger64(item.ps_account_id));
                    query.Parameters.AddWithValue("@ps_terminal_id", ManejoNulos.ManageNullInteger(item.ps_terminal_id));
                    query.Parameters.AddWithValue("@ps_type", ManejoNulos.ManageNullInteger(item.ps_type));
                    query.Parameters.AddWithValue("@ps_type_data", ManejoNulos.ManageNullStr(item.ps_type_data));
                    query.Parameters.AddWithValue("@ps_status", ManejoNulos.ManageNullInteger(item.ps_status));
                    query.Parameters.AddWithValue("@ps_started", ManejoNulos.ManageNullDate(item.ps_started));
                    query.Parameters.AddWithValue("@ps_initial_balance", ManejoNulos.ManageNullDecimal(item.ps_initial_balance));
                    query.Parameters.AddWithValue("@ps_played_count", ManejoNulos.ManageNullInteger(item.ps_played_count));
                    query.Parameters.AddWithValue("@ps_played_amount", ManejoNulos.ManageNullDecimal(item.ps_played_amount));
                    query.Parameters.AddWithValue("@ps_won_count", ManejoNulos.ManageNullInteger(item.ps_won_count));
                    query.Parameters.AddWithValue("@ps_won_amount", ManejoNulos.ManageNullDecimal(item.ps_won_amount));
                    query.Parameters.AddWithValue("@ps_cash_in", ManejoNulos.ManageNullDecimal(item.ps_cash_in));
                    query.Parameters.AddWithValue("@ps_cash_out", ManejoNulos.ManageNullDecimal(item.ps_cash_out));
                    query.Parameters.AddWithValue("@ps_finished", ManejoNulos.ManageNullDate(item.ps_finished));
                    query.Parameters.AddWithValue("@ps_final_balance", ManejoNulos.ManageNullDecimal(item.ps_final_balance));
                    query.Parameters.AddWithValue("@ps_timestamp", ManejoNulos.ManageNullInteger64(item.ps_timestamp));
                    query.Parameters.AddWithValue("@ps_locked", ManejoNulos.ManageNullDate(item.ps_locked));
                    query.Parameters.AddWithValue("@ps_stand_alone", ManejoNulos.ManegeNullBool(item.ps_stand_alone));
                    query.Parameters.AddWithValue("@ps_promo", ManejoNulos.ManegeNullBool(item.ps_promo));
                    query.Parameters.AddWithValue("@ps_wcp_transaction_id", ManejoNulos.ManageNullInteger64(item.ps_wcp_transaction_id));
                    query.Parameters.AddWithValue("@ps_total_played", ManejoNulos.ManageNullDecimal(item.ps_total_played));
                    query.Parameters.AddWithValue("@ps_total_won", ManejoNulos.ManageNullDecimal(item.ps_total_won));
                    query.Parameters.AddWithValue("@ps_redeemable_cash_in", ManejoNulos.ManageNullDecimal(item.ps_redeemable_cash_in));
                    query.Parameters.AddWithValue("@ps_redeemable_cash_out", ManejoNulos.ManageNullDecimal(item.ps_redeemable_cash_out));
                    query.Parameters.AddWithValue("@ps_redeemable_played", ManejoNulos.ManageNullDecimal(item.ps_redeemable_played));
                    query.Parameters.AddWithValue("@ps_redeemable_won", ManejoNulos.ManageNullDecimal(item.ps_redeemable_won));
                    query.Parameters.AddWithValue("@ps_non_redeemable_cash_in", ManejoNulos.ManageNullDecimal(item.ps_non_redeemable_cash_in));
                    query.Parameters.AddWithValue("@ps_non_redeemable_cash_out", ManejoNulos.ManageNullDecimal(item.ps_non_redeemable_cash_out));
                    query.Parameters.AddWithValue("@ps_non_redeemable_played", ManejoNulos.ManageNullDecimal(item.ps_non_redeemable_played));
                    query.Parameters.AddWithValue("@ps_non_redeemable_won", ManejoNulos.ManageNullDecimal(item.ps_non_redeemable_won));
                    query.Parameters.AddWithValue("@ps_balance_mismatch", ManejoNulos.ManegeNullBool(item.ps_balance_mismatch));
                    query.Parameters.AddWithValue("@ps_spent_used", ManejoNulos.ManageNullDecimal(item.ps_spent_used));
                    query.Parameters.AddWithValue("@ps_spent_remaining", ManejoNulos.ManageNullDecimal(item.ps_spent_remaining));
                    query.Parameters.AddWithValue("@ps_cancellable_amount", ManejoNulos.ManageNullDecimal(item.ps_cancellable_amount));
                    query.Parameters.AddWithValue("@ps_reported_balance_mismatch", ManejoNulos.ManageNullDecimal(item.ps_reported_balance_mismatch));
                    query.Parameters.AddWithValue("@ps_re_cash_in", ManejoNulos.ManageNullDecimal(item.ps_re_cash_in));
                    query.Parameters.AddWithValue("@ps_promo_re_cash_in", ManejoNulos.ManageNullDecimal(item.ps_promo_re_cash_in));
                    query.Parameters.AddWithValue("@ps_re_cash_out", ManejoNulos.ManageNullDecimal(item.ps_re_cash_out));
                    query.Parameters.AddWithValue("@ps_promo_re_cash_out", ManejoNulos.ManageNullDecimal(item.ps_promo_re_cash_out));
                    query.Parameters.AddWithValue("@ps_computed_points", ManejoNulos.ManageNullDecimal(item.ps_computed_points));
                    query.Parameters.AddWithValue("@ps_awarded_points", ManejoNulos.ManageNullDecimal(item.ps_awarded_points));
                    query.Parameters.AddWithValue("@ps_awarded_points_status", ManejoNulos.ManageNullInteger(item.ps_awarded_points_status));
                    query.Parameters.AddWithValue("@ps_re_ticket_in", ManejoNulos.ManageNullDecimal(item.ps_re_ticket_in));
                    query.Parameters.AddWithValue("@ps_promo_re_ticket_in", ManejoNulos.ManageNullDecimal(item.ps_promo_re_ticket_in));
                    query.Parameters.AddWithValue("@ps_promo_nr_ticket_in", ManejoNulos.ManageNullDecimal(item.ps_promo_nr_ticket_in));
                    query.Parameters.AddWithValue("@ps_re_ticket_out", ManejoNulos.ManageNullDecimal(item.ps_re_ticket_out));
                    query.Parameters.AddWithValue("@ps_promo_nr_ticket_out", ManejoNulos.ManageNullDecimal(item.ps_promo_nr_ticket_out));
                    query.Parameters.AddWithValue("@ps_bills_in_amount", ManejoNulos.ManageNullDecimal(item.ps_bills_in_amount));
                    query.Parameters.AddWithValue("@ps_redeemable_played_original", ManejoNulos.ManageNullDecimal(item.ps_redeemable_played_original));
                    query.Parameters.AddWithValue("@ps_redeemable_won_original", ManejoNulos.ManageNullDecimal(item.ps_redeemable_won_original));
                    query.Parameters.AddWithValue("@ps_non_redeemable_played_original", ManejoNulos.ManageNullDecimal(item.ps_non_redeemable_played_original));
                    query.Parameters.AddWithValue("@ps_non_redeemable_won_original", ManejoNulos.ManageNullDecimal(item.ps_non_redeemable_won_original));
                    query.Parameters.AddWithValue("@ps_played_count_original", ManejoNulos.ManageNullInteger(item.ps_played_count_original));
                    query.Parameters.AddWithValue("@ps_won_count_original", ManejoNulos.ManageNullInteger(item.ps_won_count_original));
                    query.Parameters.AddWithValue("@ps_aux_ft_re_cash_in", ManejoNulos.ManageNullDecimal(item.ps_aux_ft_re_cash_in));
                    query.Parameters.AddWithValue("@ps_aux_ft_nr_cash_in", ManejoNulos.ManageNullDecimal(item.ps_aux_ft_nr_cash_in));
                    query.Parameters.AddWithValue("@ps_re_found_in_egm", ManejoNulos.ManageNullDecimal(item.ps_re_found_in_egm));
                    query.Parameters.AddWithValue("@ps_nr_found_in_egm", ManejoNulos.ManageNullDecimal(item.ps_nr_found_in_egm));
                    query.Parameters.AddWithValue("@ps_re_remaining_in_egm", ManejoNulos.ManageNullDecimal(item.ps_re_remaining_in_egm));
                    query.Parameters.AddWithValue("@ps_nr_remaining_in_egm", ManejoNulos.ManageNullDecimal(item.ps_nr_remaining_in_egm));
                    query.Parameters.AddWithValue("@ps_handpays_amount", ManejoNulos.ManageNullDecimal(item.ps_handpays_amount));
                    query.Parameters.AddWithValue("@ps_handpays_paid_amount", ManejoNulos.ManageNullDecimal(item.ps_handpays_paid_amount));
                    query.Parameters.AddWithValue("@ps_sequence", ManejoNulos.ManageNullInteger64(item.ps_sequence));
                    query.Parameters.AddWithValue("@ps_handpays_cc_amount", ManejoNulos.ManageNullDecimal(item.ps_handpays_cc_amount));
                    query.Parameters.AddWithValue("@ps_handpays_jkp_amount", ManejoNulos.ManageNullDecimal(item.ps_handpays_jkp_amount));
                    query.Parameters.AddWithValue("@ps_is_new_session", ManejoNulos.ManegeNullBool(item.ps_is_new_session));
                    query.Parameters.AddWithValue("@ps_nr_promo_available", ManejoNulos.ManageNullDecimal(item.ps_nr_promo_available));
                    query.Parameters.AddWithValue("@ps_theo_win", ManejoNulos.ManageNullDecimal(item.ps_theo_win));
                    query.Parameters.AddWithValue("@ps_total_cash_in", ManejoNulos.ManageNullDecimal(item.ps_total_cash_in));
                    query.Parameters.AddWithValue("@ps_paid_from_hopper", ManejoNulos.ManageNullDecimal(item.ps_paid_from_hopper));
                    query.Parameters.AddWithValue("@ps_bills_dispensed", ManejoNulos.ManageNullDecimal(item.ps_bills_dispensed));
                    query.Parameters.AddWithValue("@ps_total_drop", ManejoNulos.ManageNullDecimal(item.ps_total_drop));
                    query.Parameters.AddWithValue("@ps_total_cancelled", ManejoNulos.ManageNullDecimal(item.ps_total_cancelled));
                    query.Parameters.AddWithValue("@ps_cash_in_coins", ManejoNulos.ManageNullDecimal(item.ps_cash_in_coins));
                    query.Parameters.AddWithValue("@ps_cash_in_bills", ManejoNulos.ManageNullDecimal(item.ps_cash_in_bills));
                    query.Parameters.AddWithValue("@ps_total_cash_out", ManejoNulos.ManageNullDecimal(item.ps_total_cash_out));
                    query.Parameters.AddWithValue("@ps_original_account", ManejoNulos.ManageNullInteger64(item.ps_original_account));
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
