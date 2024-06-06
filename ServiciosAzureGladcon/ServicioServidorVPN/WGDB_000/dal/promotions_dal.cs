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
    public class promotions_dal
    {
        private readonly string bd_username = string.Empty;
        private readonly string bd_password = string.Empty;
        private readonly string bd_datasource = string.Empty;
        private readonly string connectionString = string.Empty;
        public promotions_dal(string database_name)
        {
            bd_username = ConfigurationManager.AppSettings["bd_username"];
            bd_password = ConfigurationManager.AppSettings["bd_password"];
            bd_datasource = ConfigurationManager.AppSettings["bd_datasource"];
            connectionString = $"Data Source={bd_datasource};Initial Catalog={database_name};Integrated Security=False;User ID={bd_username};Password={bd_password}";

        }
        public bool SavePromotions(promotions item)
        {
            //bool respuesta = false;
            long IdInsertado = 0;
            string consulta = @"
if not exists (select pm_promotion_id from [dbo].[promotions] where pm_promotion_id = @pm_promotion_id)
begin
INSERT INTO [dbo].[promotions]
           ([pm_promotion_id]
           ,[pm_name]
           ,[pm_enabled]
           ,[pm_type]
           ,[pm_date_start]
           ,[pm_date_finish]
           ,[pm_schedule_weekday]
           ,[pm_schedule1_time_from]
           ,[pm_schedule1_time_to]
           ,[pm_schedule2_enabled]
           ,[pm_schedule2_time_from]
           ,[pm_schedule2_time_to]
           ,[pm_gender_filter]
           ,[pm_birthday_filter]
           ,[pm_expiration_type]
           ,[pm_expiration_value]
           ,[pm_min_cash_in]
           ,[pm_min_cash_in_reward]
           ,[pm_cash_in]
           ,[pm_cash_in_reward]
           ,[pm_won_lock]
           ,[pm_num_tokens]
           ,[pm_token_name]
           ,[pm_token_reward]
           ,[pm_daily_limit]
           ,[pm_monthly_limit]
           ,[pm_level_filter]
           ,[pm_permission]
           ,[pm_freq_filter_last_days]
           ,[pm_freq_filter_min_days]
           ,[pm_freq_filter_min_cash_in]
           ,[pm_min_spent]
           ,[pm_min_spent_reward]
           ,[pm_spent]
           ,[pm_spent_reward]
           ,[pm_provider_list]
           ,[pm_offer_list]
           ,[pm_global_daily_limit]
           ,[pm_global_monthly_limit]
           ,[pm_small_resource_id]
           ,[pm_large_resource_id]
           ,[pm_min_played]
           ,[pm_min_played_reward]
           ,[pm_played]
           ,[pm_played_reward]
           ,[pm_play_restricted_to_provider_list]
           ,[pm_last_executed]
           ,[pm_next_execution]
           ,[pm_global_limit]
           ,[pm_credit_type]
           ,[pm_category_id]
           ,[pm_ticket_footer]
           ,[pm_visible_on_promobox]
           ,[pm_expiration_limit]
           ,[pm_restricted_to_terminal_list]
           ,[pm_points_to_credits_id]
           ,[pm_award_on_promobox]
           ,[pm_text_on_promobox]
           ,[pm_ticket_quantity]
           ,[pm_generated_tickets]
           ,[pm_vip]
           ,[pm_created_account_filter]
           ,[pm_apply_tax]
           ,[pm_promogame_id]
           ,[pm_journey_limit]
           ,[pm_pyramidal_dist]
           ,[PM_PCT_BY_CHARGE]
           ,[PM_ORDER]
           ,[pm_automatically_assign_sashost]
           ,[pm_automatically_assign_cashier]
           ,[pm_automatically_assign_promobox]
           ,[pm_automatically_promotion]
           ,[pm_spent_flags]
           ,[pm_status]
           ,[pm_ms_sequence_id]
           ,[pm_ms_id]
           ,[pm_enabled_sites]
           ,[pm_studyPeriod1_enabled]
           ,[pm_studyPeriod1_time_from]
           ,[pm_studyPeriod1_time_to]
           ,[pm_studyPeriod2_enabled]
           ,[pm_studyPeriod2_time_from]
           ,[pm_studyPeriod2_time_to]
           ,[pm_studyPeriodAccumulated_enabled]
           ,[pm_gift_by_level]
           ,[pm_weekly_limit]
           ,[pm_global_weekly_limit]
           ,[pm_decrease_spent_redeemable_gom]
           ,[pm_notification_gom]
           ,[pm_flag_require]
           ,[pm_all_accounts]
           ,[pm_coin_in]
           ,[pm_country_list]
           ,[pm_include_prize_tax_form]
           ,[pm_lock_enabled]
           ,[pm_lock_balance_factor]
           ,[pm_lock_balance_amount]
           ,[pm_lock_coin_in_factor]
           ,[pm_lock_coin_in_amount]
           ,[pm_lock_average_bet]
           ,[pm_lock_plays]
           ,[pm_lock_max_payable_factor_enabled]
           ,[pm_lock_max_payable_factor]
           ,[pm_lock_max_payable_amount_enabled]
           ,[pm_lock_max_payable_amount]
           ,[pm_patron_limit]
           ,[pm_times_daily]
           ,[pm_times_weekly]
           ,[pm_times_monthly]
           ,[pm_times_patron]
           ,[pm_global_times_day]
           ,[pm_global_times_week]
           ,[pm_global_times_month]
           ,[pm_global_times_global]
           ,[pm_cash_in_max_reward]
           ,[pm_weights_id]
           ,[pm_awarded_promotion_discounted]
           ,[pm_lock_min_payable_amount]
           ,[pm_auto_conversion]
           ,[pm_input_amount_type]
           ,[pm_is_automatic_transfer]
           ,[pm_min_coinin]
           ,[pm_min_coinin_reward]
           ,[pm_coinin]
           ,[pm_coinin_reward]
           ,[pm_min_points]
           ,[pm_min_points_reward]
           ,[pm_points]
           ,[pm_points_reward]
           ,[pm_coinin_to_terminal_list]
           ,[pm_points_to_terminal_list]
           ,[pm_small_resource_id_55]
           ,[pm_include_recharge]
           ,[pm_parent_promotion_id]
           ,[pm_has_schedule])
--output inserted.pm_promotion_id
     VALUES
           (@pm_promotion_id
           ,@pm_name
           ,@pm_enabled
           ,@pm_type
           ,@pm_date_start
           ,@pm_date_finish
           ,@pm_schedule_weekday
           ,@pm_schedule1_time_from
           ,@pm_schedule1_time_to
           ,@pm_schedule2_enabled
           ,@pm_schedule2_time_from
           ,@pm_schedule2_time_to
           ,@pm_gender_filter
           ,@pm_birthday_filter
           ,@pm_expiration_type
           ,@pm_expiration_value
           ,@pm_min_cash_in
           ,@pm_min_cash_in_reward
           ,@pm_cash_in
           ,@pm_cash_in_reward
           ,@pm_won_lock
           ,@pm_num_tokens
           ,@pm_token_name
           ,@pm_token_reward
           ,@pm_daily_limit
           ,@pm_monthly_limit
           ,@pm_level_filter
           ,@pm_permission
           ,@pm_freq_filter_last_days
           ,@pm_freq_filter_min_days
           ,@pm_freq_filter_min_cash_in
           ,@pm_min_spent
           ,@pm_min_spent_reward
           ,@pm_spent
           ,@pm_spent_reward
           ,@pm_provider_list
           ,@pm_offer_list
           ,@pm_global_daily_limit
           ,@pm_global_monthly_limit
           ,@pm_small_resource_id
           ,@pm_large_resource_id
           ,@pm_min_played
           ,@pm_min_played_reward
           ,@pm_played
           ,@pm_played_reward
           ,@pm_play_restricted_to_provider_list
           ,@pm_last_executed
           ,@pm_next_execution
           ,@pm_global_limit
           ,@pm_credit_type
           ,@pm_category_id
           ,@pm_ticket_footer
           ,@pm_visible_on_promobox
           ,@pm_expiration_limit
           ,@pm_restricted_to_terminal_list
           ,@pm_points_to_credits_id
           ,@pm_award_on_promobox
           ,@pm_text_on_promobox
           ,@pm_ticket_quantity
           ,@pm_generated_tickets
           ,@pm_vip
           ,@pm_created_account_filter
           ,@pm_apply_tax
           ,@pm_promogame_id
           ,@pm_journey_limit
           ,@pm_pyramidal_dist
           ,@pm_PCT_BY_CHARGE
           ,@pm_ORDER
           ,@pm_automatically_assign_sashost
           ,@pm_automatically_assign_cashier
           ,@pm_automatically_assign_promobox
           ,@pm_automatically_promotion
           ,@pm_spent_flags
           ,@pm_status
           ,@pm_ms_sequence_id
           ,@pm_ms_id
           ,@pm_enabled_sites
           ,@pm_studyPeriod1_enabled
           ,@pm_studyPeriod1_time_from
           ,@pm_studyPeriod1_time_to
           ,@pm_studyPeriod2_enabled
           ,@pm_studyPeriod2_time_from
           ,@pm_studyPeriod2_time_to
           ,@pm_studyPeriodAccumulated_enabled
           ,@pm_gift_by_level
           ,@pm_weekly_limit
           ,@pm_global_weekly_limit
           ,@pm_decrease_spent_redeemable_gom
           ,@pm_notification_gom
           ,@pm_flag_require
           ,@pm_all_accounts
           ,@pm_coin_in
           ,@pm_country_list
           ,@pm_include_prize_tax_form
           ,@pm_lock_enabled
           ,@pm_lock_balance_factor
           ,@pm_lock_balance_amount
           ,@pm_lock_coin_in_factor
           ,@pm_lock_coin_in_amount
           ,@pm_lock_average_bet
           ,@pm_lock_plays
           ,@pm_lock_max_payable_factor_enabled
           ,@pm_lock_max_payable_factor
           ,@pm_lock_max_payable_amount_enabled
           ,@pm_lock_max_payable_amount
           ,@pm_patron_limit
           ,@pm_times_daily
           ,@pm_times_weekly
           ,@pm_times_monthly
           ,@pm_times_patron
           ,@pm_global_times_day
           ,@pm_global_times_week
           ,@pm_global_times_month
           ,@pm_global_times_global
           ,@pm_cash_in_max_reward
           ,@pm_weights_id
           ,@pm_awarded_promotion_discounted
           ,@pm_lock_min_payable_amount
           ,@pm_auto_conversion
           ,@pm_input_amount_type
           ,@pm_is_automatic_transfer
           ,@pm_min_coinin
           ,@pm_min_coinin_reward
           ,@pm_coinin
           ,@pm_coinin_reward
           ,@pm_min_points
           ,@pm_min_points_reward
           ,@pm_points
           ,@pm_points_reward
           ,@pm_coinin_to_terminal_list
           ,@pm_points_to_terminal_list
           ,@pm_small_resource_id_55
           ,@pm_include_recharge
           ,@pm_parent_promotion_id
           ,@pm_has_schedule)
end

                      ";
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@pm_promotion_id", item.pm_promotion_id == null ? DBNull.Value : (object)item.pm_promotion_id);
                    query.Parameters.AddWithValue("@pm_name", item.pm_name == null ? DBNull.Value : (object)item.pm_name);
                    query.Parameters.AddWithValue("@pm_enabled", item.pm_enabled == null ? DBNull.Value : (object)item.pm_enabled);
                    query.Parameters.AddWithValue("@pm_type", item.pm_type == null ? DBNull.Value : (object)item.pm_type);
                    query.Parameters.AddWithValue("@pm_date_start", item.pm_date_start == null ? DBNull.Value : (object)item.pm_date_start);
                    query.Parameters.AddWithValue("@pm_date_finish", item.pm_date_finish == null ? DBNull.Value : (object)item.pm_date_finish);
                    query.Parameters.AddWithValue("@pm_schedule_weekday", item.pm_schedule_weekday == null ? DBNull.Value : (object)item.pm_schedule_weekday);
                    query.Parameters.AddWithValue("@pm_schedule1_time_from", item.pm_schedule1_time_from == null ? DBNull.Value : (object)item.pm_schedule1_time_from);
                    query.Parameters.AddWithValue("@pm_schedule1_time_to", item.pm_schedule1_time_to == null ? DBNull.Value : (object)item.pm_schedule1_time_to);
                    query.Parameters.AddWithValue("@pm_schedule2_enabled", item.pm_schedule2_enabled == null ? DBNull.Value : (object)item.pm_schedule2_enabled);
                    query.Parameters.AddWithValue("@pm_schedule2_time_from", item.pm_schedule2_time_from == null ? DBNull.Value : (object)item.pm_schedule2_time_from);
                    query.Parameters.AddWithValue("@pm_schedule2_time_to", item.pm_schedule2_time_to == null ? DBNull.Value : (object)item.pm_schedule2_time_to);
                    query.Parameters.AddWithValue("@pm_gender_filter", item.pm_gender_filter == null ? DBNull.Value : (object)item.pm_gender_filter);
                    query.Parameters.AddWithValue("@pm_birthday_filter", item.pm_birthday_filter == null ? DBNull.Value : (object)item.pm_birthday_filter);
                    query.Parameters.AddWithValue("@pm_expiration_type", item.pm_expiration_type == null ? DBNull.Value : (object)item.pm_expiration_type);
                    query.Parameters.AddWithValue("@pm_expiration_value", item.pm_expiration_value == null ? DBNull.Value : (object)item.pm_expiration_value);
                    query.Parameters.AddWithValue("@pm_min_cash_in", item.pm_min_cash_in == null ? DBNull.Value : (object)item.pm_min_cash_in);
                    query.Parameters.AddWithValue("@pm_min_cash_in_reward", item.pm_min_cash_in_reward == null ? DBNull.Value : (object)item.pm_min_cash_in_reward);
                    query.Parameters.AddWithValue("@pm_cash_in", item.pm_cash_in == null ? DBNull.Value : (object)item.pm_cash_in);
                    query.Parameters.AddWithValue("@pm_cash_in_reward", item.pm_cash_in_reward == null ? DBNull.Value : (object)item.pm_cash_in_reward);
                    query.Parameters.AddWithValue("@pm_won_lock", item.pm_won_lock == null ? DBNull.Value : (object)item.pm_won_lock);
                    query.Parameters.AddWithValue("@pm_num_tokens", item.pm_num_tokens == null ? DBNull.Value : (object)item.pm_num_tokens);
                    query.Parameters.AddWithValue("@pm_token_name", item.pm_token_name == null ? DBNull.Value : (object)item.pm_token_name);
                    query.Parameters.AddWithValue("@pm_token_reward", item.pm_token_reward == null ? DBNull.Value : (object)item.pm_token_reward);
                    query.Parameters.AddWithValue("@pm_daily_limit", item.pm_daily_limit == null ? DBNull.Value : (object)item.pm_daily_limit);
                    query.Parameters.AddWithValue("@pm_monthly_limit", item.pm_monthly_limit == null ? DBNull.Value : (object)item.pm_monthly_limit);
                    query.Parameters.AddWithValue("@pm_level_filter", item.pm_level_filter == null ? DBNull.Value : (object)item.pm_level_filter);
                    query.Parameters.AddWithValue("@pm_permission", item.pm_permission == null ? DBNull.Value : (object)item.pm_permission);
                    query.Parameters.AddWithValue("@pm_freq_filter_last_days", item.pm_freq_filter_last_days == null ? DBNull.Value : (object)item.pm_freq_filter_last_days);
                    query.Parameters.AddWithValue("@pm_freq_filter_min_days", item.pm_freq_filter_min_days == null ? DBNull.Value : (object)item.pm_freq_filter_min_days);
                    query.Parameters.AddWithValue("@pm_freq_filter_min_cash_in", item.pm_freq_filter_min_cash_in == null ? DBNull.Value : (object)item.pm_freq_filter_min_cash_in);
                    query.Parameters.AddWithValue("@pm_min_spent", item.pm_min_spent == null ? DBNull.Value : (object)item.pm_min_spent);
                    query.Parameters.AddWithValue("@pm_min_spent_reward", item.pm_min_spent_reward == null ? DBNull.Value : (object)item.pm_min_spent_reward);
                    query.Parameters.AddWithValue("@pm_spent", item.pm_spent == null ? DBNull.Value : (object)item.pm_spent);
                    query.Parameters.AddWithValue("@pm_spent_reward", item.pm_spent_reward == null ? DBNull.Value : (object)item.pm_spent_reward);
                    query.Parameters.AddWithValue("@pm_provider_list", item.pm_provider_list == null ? DBNull.Value : (object)item.pm_provider_list);
                    query.Parameters.AddWithValue("@pm_offer_list", item.pm_offer_list == null ? DBNull.Value : (object)item.pm_offer_list);
                    query.Parameters.AddWithValue("@pm_global_daily_limit", item.pm_global_daily_limit == null ? DBNull.Value : (object)item.pm_global_daily_limit);
                    query.Parameters.AddWithValue("@pm_global_monthly_limit", item.pm_global_monthly_limit == null ? DBNull.Value : (object)item.pm_global_monthly_limit);
                    query.Parameters.AddWithValue("@pm_small_resource_id", item.pm_small_resource_id == null ? DBNull.Value : (object)item.pm_small_resource_id);
                    query.Parameters.AddWithValue("@pm_large_resource_id", item.pm_large_resource_id == null ? DBNull.Value : (object)item.pm_large_resource_id);
                    query.Parameters.AddWithValue("@pm_min_played", item.pm_min_played == null ? DBNull.Value : (object)item.pm_min_played);
                    query.Parameters.AddWithValue("@pm_min_played_reward", item.pm_min_played_reward == null ? DBNull.Value : (object)item.pm_min_played_reward);
                    query.Parameters.AddWithValue("@pm_played", item.pm_played == null ? DBNull.Value : (object)item.pm_played);
                    query.Parameters.AddWithValue("@pm_played_reward", item.pm_played_reward == null ? DBNull.Value : (object)item.pm_played_reward);
                    query.Parameters.AddWithValue("@pm_play_restricted_to_provider_list", item.pm_play_restricted_to_provider_list == null ? DBNull.Value : (object)item.pm_play_restricted_to_provider_list);
                    query.Parameters.AddWithValue("@pm_last_executed", item.pm_last_executed == null ? DBNull.Value : (object)item.pm_last_executed);
                    query.Parameters.AddWithValue("@pm_next_execution", item.pm_next_execution == null ? DBNull.Value : (object)item.pm_next_execution);
                    query.Parameters.AddWithValue("@pm_global_limit", item.pm_global_limit == null ? DBNull.Value : (object)item.pm_global_limit);
                    query.Parameters.AddWithValue("@pm_credit_type", item.pm_credit_type == null ? DBNull.Value : (object)item.pm_credit_type);
                    query.Parameters.AddWithValue("@pm_category_id", item.pm_category_id == null ? DBNull.Value : (object)item.pm_category_id);
                    query.Parameters.AddWithValue("@pm_ticket_footer", item.pm_ticket_footer == null ? DBNull.Value : (object)item.pm_ticket_footer);
                    query.Parameters.AddWithValue("@pm_visible_on_promobox", item.pm_visible_on_promobox == null ? DBNull.Value : (object)item.pm_visible_on_promobox);
                    query.Parameters.AddWithValue("@pm_expiration_limit", item.pm_expiration_limit == null ? DBNull.Value : (object)item.pm_expiration_limit);
                    query.Parameters.AddWithValue("@pm_restricted_to_terminal_list", item.pm_restricted_to_terminal_list == null ? DBNull.Value : (object)item.pm_restricted_to_terminal_list);
                    query.Parameters.AddWithValue("@pm_points_to_credits_id", item.pm_points_to_credits_id == null ? DBNull.Value : (object)item.pm_points_to_credits_id);
                    query.Parameters.AddWithValue("@pm_award_on_promobox", item.pm_award_on_promobox == null ? DBNull.Value : (object)item.pm_award_on_promobox);
                    query.Parameters.AddWithValue("@pm_text_on_promobox", item.pm_text_on_promobox == null ? DBNull.Value : (object)item.pm_text_on_promobox);
                    query.Parameters.AddWithValue("@pm_ticket_quantity", item.pm_ticket_quantity == null ? DBNull.Value : (object)item.pm_ticket_quantity);
                    query.Parameters.AddWithValue("@pm_generated_tickets", item.pm_generated_tickets == null ? DBNull.Value : (object)item.pm_generated_tickets);
                    query.Parameters.AddWithValue("@pm_vip", item.pm_vip == null ? DBNull.Value : (object)item.pm_vip);
                    query.Parameters.AddWithValue("@pm_created_account_filter", item.pm_created_account_filter == null ? DBNull.Value : (object)item.pm_created_account_filter);
                    query.Parameters.AddWithValue("@pm_apply_tax", item.pm_apply_tax == null ? DBNull.Value : (object)item.pm_apply_tax);
                    query.Parameters.AddWithValue("@pm_promogame_id", item.pm_promogame_id == null ? DBNull.Value : (object)item.pm_promogame_id);
                    query.Parameters.AddWithValue("@pm_journey_limit", item.pm_journey_limit == null ? DBNull.Value : (object)item.pm_journey_limit);
                    query.Parameters.AddWithValue("@pm_pyramidal_dist", item.pm_pyramidal_dist == null ? DBNull.Value : (object)item.pm_pyramidal_dist);
                    query.Parameters.AddWithValue("@PM_PCT_BY_CHARGE", item.PM_PCT_BY_CHARGE == null ? DBNull.Value : (object)item.PM_PCT_BY_CHARGE);
                    query.Parameters.AddWithValue("@PM_ORDER", item.PM_ORDER == null ? DBNull.Value : (object)item.PM_ORDER);
                    query.Parameters.AddWithValue("@pm_automatically_assign_sashost", item.pm_automatically_assign_sashost == null ? DBNull.Value : (object)item.pm_automatically_assign_sashost);
                    query.Parameters.AddWithValue("@pm_automatically_assign_cashier", item.pm_automatically_assign_cashier == null ? DBNull.Value : (object)item.pm_automatically_assign_cashier);
                    query.Parameters.AddWithValue("@pm_automatically_assign_promobox", item.pm_automatically_assign_promobox == null ? DBNull.Value : (object)item.pm_automatically_assign_promobox);
                    query.Parameters.AddWithValue("@pm_automatically_promotion", item.pm_automatically_promotion == null ? DBNull.Value : (object)item.pm_automatically_promotion);
                    query.Parameters.AddWithValue("@pm_spent_flags", item.pm_spent_flags == null ? DBNull.Value : (object)item.pm_spent_flags);
                    query.Parameters.AddWithValue("@pm_status", item.pm_status == null ? DBNull.Value : (object)item.pm_status);
                    query.Parameters.AddWithValue("@pm_ms_sequence_id", item.pm_ms_sequence_id == null ? DBNull.Value : (object)item.pm_ms_sequence_id);
                    query.Parameters.AddWithValue("@pm_ms_id", item.pm_ms_id == null ? DBNull.Value : (object)item.pm_ms_id);
                    query.Parameters.AddWithValue("@pm_enabled_sites", item.pm_enabled_sites == null ? DBNull.Value : (object)item.pm_enabled_sites);
                    query.Parameters.AddWithValue("@pm_studyPeriod1_enabled", item.pm_studyPeriod1_enabled == null ? DBNull.Value : (object)item.pm_studyPeriod1_enabled);
                    query.Parameters.AddWithValue("@pm_studyPeriod1_time_from", item.pm_studyPeriod1_time_from == null ? DBNull.Value : (object)item.pm_studyPeriod1_time_from);
                    query.Parameters.AddWithValue("@pm_studyPeriod1_time_to", item.pm_studyPeriod1_time_to == null ? DBNull.Value : (object)item.pm_studyPeriod1_time_to);
                    query.Parameters.AddWithValue("@pm_studyPeriod2_enabled", item.pm_studyPeriod2_enabled == null ? DBNull.Value : (object)item.pm_studyPeriod2_enabled);
                    query.Parameters.AddWithValue("@pm_studyPeriod2_time_from", item.pm_studyPeriod2_time_from == null ? DBNull.Value : (object)item.pm_studyPeriod2_time_from);
                    query.Parameters.AddWithValue("@pm_studyPeriod2_time_to", item.pm_studyPeriod2_time_to == null ? DBNull.Value : (object)item.pm_studyPeriod2_time_to);
                    query.Parameters.AddWithValue("@pm_studyPeriodAccumulated_enabled", item.pm_studyPeriodAccumulated_enabled == null ? DBNull.Value : (object)item.pm_studyPeriodAccumulated_enabled);
                    query.Parameters.AddWithValue("@pm_gift_by_level", item.pm_gift_by_level == null ? DBNull.Value : (object)item.pm_gift_by_level);
                    query.Parameters.AddWithValue("@pm_weekly_limit", item.pm_weekly_limit == null ? DBNull.Value : (object)item.pm_weekly_limit);
                    query.Parameters.AddWithValue("@pm_global_weekly_limit", item.pm_global_weekly_limit == null ? DBNull.Value : (object)item.pm_global_weekly_limit);
                    query.Parameters.AddWithValue("@pm_decrease_spent_redeemable_gom", item.pm_decrease_spent_redeemable_gom == null ? DBNull.Value : (object)item.pm_decrease_spent_redeemable_gom);
                    query.Parameters.AddWithValue("@pm_notification_gom", item.pm_notification_gom == null ? DBNull.Value : (object)item.pm_notification_gom);
                    query.Parameters.AddWithValue("@pm_flag_require", item.pm_flag_require == null ? DBNull.Value : (object)item.pm_flag_require);
                    query.Parameters.AddWithValue("@pm_all_accounts", item.pm_all_accounts == null ? DBNull.Value : (object)item.pm_all_accounts);
                    query.Parameters.AddWithValue("@pm_coin_in", item.pm_coin_in == null ? DBNull.Value : (object)item.pm_coin_in);
                    query.Parameters.AddWithValue("@pm_country_list", item.pm_country_list == null ? DBNull.Value : (object)item.pm_country_list);
                    query.Parameters.AddWithValue("@pm_include_prize_tax_form", item.pm_include_prize_tax_form == null ? DBNull.Value : (object)item.pm_include_prize_tax_form);
                    query.Parameters.AddWithValue("@pm_lock_enabled", item.pm_lock_enabled == null ? DBNull.Value : (object)item.pm_lock_enabled);
                    query.Parameters.AddWithValue("@pm_lock_balance_factor", item.pm_lock_balance_factor == null ? DBNull.Value : (object)item.pm_lock_balance_factor);
                    query.Parameters.AddWithValue("@pm_lock_balance_amount", item.pm_lock_balance_amount == null ? DBNull.Value : (object)item.pm_lock_balance_amount);
                    query.Parameters.AddWithValue("@pm_lock_coin_in_factor", item.pm_lock_coin_in_factor == null ? DBNull.Value : (object)item.pm_lock_coin_in_factor);
                    query.Parameters.AddWithValue("@pm_lock_coin_in_amount", item.pm_lock_coin_in_amount == null ? DBNull.Value : (object)item.pm_lock_coin_in_amount);
                    query.Parameters.AddWithValue("@pm_lock_average_bet", item.pm_lock_average_bet == null ? DBNull.Value : (object)item.pm_lock_average_bet);
                    query.Parameters.AddWithValue("@pm_lock_plays", item.pm_lock_plays == null ? DBNull.Value : (object)item.pm_lock_plays);
                    query.Parameters.AddWithValue("@pm_lock_max_payable_factor_enabled", item.pm_lock_max_payable_factor_enabled == null ? DBNull.Value : (object)item.pm_lock_max_payable_factor_enabled);
                    query.Parameters.AddWithValue("@pm_lock_max_payable_factor", item.pm_lock_max_payable_factor == null ? DBNull.Value : (object)item.pm_lock_max_payable_factor);
                    query.Parameters.AddWithValue("@pm_lock_max_payable_amount_enabled", item.pm_lock_max_payable_amount_enabled == null ? DBNull.Value : (object)item.pm_lock_max_payable_amount_enabled);
                    query.Parameters.AddWithValue("@pm_lock_max_payable_amount", item.pm_lock_max_payable_amount == null ? DBNull.Value : (object)item.pm_lock_max_payable_amount);
                    query.Parameters.AddWithValue("@pm_patron_limit", item.pm_patron_limit == null ? DBNull.Value : (object)item.pm_patron_limit);
                    query.Parameters.AddWithValue("@pm_times_daily", item.pm_times_daily == null ? DBNull.Value : (object)item.pm_times_daily);
                    query.Parameters.AddWithValue("@pm_times_weekly", item.pm_times_weekly == null ? DBNull.Value : (object)item.pm_times_weekly);
                    query.Parameters.AddWithValue("@pm_times_monthly", item.pm_times_monthly == null ? DBNull.Value : (object)item.pm_times_monthly);
                    query.Parameters.AddWithValue("@pm_times_patron", item.pm_times_patron == null ? DBNull.Value : (object)item.pm_times_patron);
                    query.Parameters.AddWithValue("@pm_global_times_day", item.pm_global_times_day == null ? DBNull.Value : (object)item.pm_global_times_day);
                    query.Parameters.AddWithValue("@pm_global_times_week", item.pm_global_times_week == null ? DBNull.Value : (object)item.pm_global_times_week);
                    query.Parameters.AddWithValue("@pm_global_times_month", item.pm_global_times_month == null ? DBNull.Value : (object)item.pm_global_times_month);
                    query.Parameters.AddWithValue("@pm_global_times_global", item.pm_global_times_global == null ? DBNull.Value : (object)item.pm_global_times_global);
                    query.Parameters.AddWithValue("@pm_cash_in_max_reward", item.pm_cash_in_max_reward == null ? DBNull.Value : (object)item.pm_cash_in_max_reward);
                    query.Parameters.AddWithValue("@pm_weights_id", item.pm_weights_id == null ? DBNull.Value : (object)item.pm_weights_id);
                    query.Parameters.AddWithValue("@pm_awarded_promotion_discounted", item.pm_awarded_promotion_discounted == null ? DBNull.Value : (object)item.pm_awarded_promotion_discounted);
                    query.Parameters.AddWithValue("@pm_lock_min_payable_amount", item.pm_lock_min_payable_amount == null ? DBNull.Value : (object)item.pm_lock_min_payable_amount);
                    query.Parameters.AddWithValue("@pm_auto_conversion", item.pm_auto_conversion == null ? DBNull.Value : (object)item.pm_auto_conversion);
                    query.Parameters.AddWithValue("@pm_input_amount_type", item.pm_input_amount_type == null ? DBNull.Value : (object)item.pm_input_amount_type);
                    query.Parameters.AddWithValue("@pm_is_automatic_transfer", item.pm_is_automatic_transfer == null ? DBNull.Value : (object)item.pm_is_automatic_transfer);
                    query.Parameters.AddWithValue("@pm_min_coinin", item.pm_min_coinin == null ? DBNull.Value : (object)item.pm_min_coinin);
                    query.Parameters.AddWithValue("@pm_min_coinin_reward", item.pm_min_coinin_reward == null ? DBNull.Value : (object)item.pm_min_coinin_reward);
                    query.Parameters.AddWithValue("@pm_coinin", item.pm_coinin == null ? DBNull.Value : (object)item.pm_coinin);
                    query.Parameters.AddWithValue("@pm_coinin_reward", item.pm_coinin_reward == null ? DBNull.Value : (object)item.pm_coinin_reward);
                    query.Parameters.AddWithValue("@pm_min_points", item.pm_min_points == null ? DBNull.Value : (object)item.pm_min_points);
                    query.Parameters.AddWithValue("@pm_min_points_reward", item.pm_min_points_reward == null ? DBNull.Value : (object)item.pm_min_points_reward);
                    query.Parameters.AddWithValue("@pm_points", item.pm_points == null ? DBNull.Value : (object)item.pm_points);
                    query.Parameters.AddWithValue("@pm_points_reward", item.pm_points_reward == null ? DBNull.Value : (object)item.pm_points_reward);
                    query.Parameters.AddWithValue("@pm_coinin_to_terminal_list", item.pm_coinin_to_terminal_list == null ? DBNull.Value : (object)item.pm_coinin_to_terminal_list);
                    query.Parameters.AddWithValue("@pm_points_to_terminal_list", item.pm_points_to_terminal_list == null ? DBNull.Value : (object)item.pm_points_to_terminal_list);
                    query.Parameters.AddWithValue("@pm_small_resource_id_55", item.pm_small_resource_id_55 == null ? DBNull.Value : (object)item.pm_small_resource_id_55);
                    query.Parameters.AddWithValue("@pm_include_recharge", item.pm_include_recharge == null ? DBNull.Value : (object)item.pm_include_recharge);
                    query.Parameters.AddWithValue("@pm_parent_promotion_id", item.pm_parent_promotion_id == null ? DBNull.Value : (object)item.pm_parent_promotion_id);
                    query.Parameters.AddWithValue("@pm_has_schedule", item.pm_has_schedule == null ? DBNull.Value : (object)item.pm_has_schedule);
                    //IdInsertado = Convert.ToInt32(query.ExecuteScalar());
                    query.ExecuteNonQuery();
                    //IdInsertado = item.pm_promotion_id;
                    return true;
                }
            }
            catch (Exception ex)
            {
                funciones.logueo($"Error metodo SavePromotions - {ex.Message}");
            }
            return false;
        }
        public long GetLastIdInserted()
        {
            long total = 0;

            string query = @"
            select top 1 pm_promotion_id as lastid from 
            [dbo].[promotions]
            order by pm_promotion_id desc
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
                funciones.logueo($"Error metodo GetLastIdInserted promotions_dal.cs- {ex.Message}", "Error");
                total = 0;
            }

            return total;
        }
    }
}
