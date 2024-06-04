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
        public long SavePromotions(promotions item)
        {
            //bool respuesta = false;
            long IdInsertado = 0;
            string consulta = @"
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
                      ";
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@pm_promotion_id", ManejoNulos.ManageNullInteger64(item.pm_promotion_id));
                    query.Parameters.AddWithValue("@pm_name", ManejoNulos.ManageNullStr(item.pm_name));
                    query.Parameters.AddWithValue("@pm_enabled", ManejoNulos.ManegeNullBool(item.pm_enabled));
                    query.Parameters.AddWithValue("@pm_type", ManejoNulos.ManageNullInteger(item.pm_type));
                    query.Parameters.AddWithValue("@pm_date_start", ManejoNulos.ManageNullDate(item.pm_date_start));
                    query.Parameters.AddWithValue("@pm_date_finish", ManejoNulos.ManageNullDate(item.pm_date_finish));
                    query.Parameters.AddWithValue("@pm_schedule_weekday", ManejoNulos.ManageNullInteger(item.pm_schedule_weekday));
                    query.Parameters.AddWithValue("@pm_schedule1_time_from", ManejoNulos.ManageNullInteger(item.pm_schedule1_time_from));
                    query.Parameters.AddWithValue("@pm_schedule1_time_to", ManejoNulos.ManageNullInteger(item.pm_schedule1_time_to));
                    query.Parameters.AddWithValue("@pm_schedule2_enabled", ManejoNulos.ManegeNullBool(item.pm_schedule2_enabled));
                    query.Parameters.AddWithValue("@pm_schedule2_time_from", ManejoNulos.ManageNullInteger(item.pm_schedule2_time_from));
                    query.Parameters.AddWithValue("@pm_schedule2_time_to", ManejoNulos.ManageNullInteger(item.pm_schedule2_time_to));
                    query.Parameters.AddWithValue("@pm_gender_filter", ManejoNulos.ManageNullInteger(item.pm_gender_filter));
                    query.Parameters.AddWithValue("@pm_birthday_filter", ManejoNulos.ManageNullInteger(item.pm_birthday_filter));
                    query.Parameters.AddWithValue("@pm_expiration_type", ManejoNulos.ManageNullInteger(item.pm_expiration_type));
                    query.Parameters.AddWithValue("@pm_expiration_value", ManejoNulos.ManageNullInteger(item.pm_expiration_value));
                    query.Parameters.AddWithValue("@pm_min_cash_in", ManejoNulos.ManageNullDecimal(item.pm_min_cash_in));
                    query.Parameters.AddWithValue("@pm_min_cash_in_reward", ManejoNulos.ManageNullDecimal(item.pm_min_cash_in_reward));
                    query.Parameters.AddWithValue("@pm_cash_in", ManejoNulos.ManageNullDecimal(item.pm_cash_in));
                    query.Parameters.AddWithValue("@pm_cash_in_reward", ManejoNulos.ManageNullDecimal(item.pm_cash_in_reward));
                    query.Parameters.AddWithValue("@pm_won_lock", ManejoNulos.ManageNullDecimal(item.pm_won_lock));
                    query.Parameters.AddWithValue("@pm_num_tokens", ManejoNulos.ManageNullInteger(item.pm_num_tokens));
                    query.Parameters.AddWithValue("@pm_token_name", ManejoNulos.ManageNullStr(item.pm_token_name));
                    query.Parameters.AddWithValue("@pm_token_reward", ManejoNulos.ManageNullDecimal(item.pm_token_reward));
                    query.Parameters.AddWithValue("@pm_daily_limit", ManejoNulos.ManageNullDecimal(item.pm_daily_limit));
                    query.Parameters.AddWithValue("@pm_monthly_limit", ManejoNulos.ManageNullDecimal(item.pm_monthly_limit));
                    query.Parameters.AddWithValue("@pm_level_filter", ManejoNulos.ManageNullInteger(item.pm_level_filter));
                    query.Parameters.AddWithValue("@pm_permission", ManejoNulos.ManageNullInteger(item.pm_permission));
                    query.Parameters.AddWithValue("@pm_freq_filter_last_days", ManejoNulos.ManageNullInteger(item.pm_freq_filter_last_days));
                    query.Parameters.AddWithValue("@pm_freq_filter_min_days", ManejoNulos.ManageNullInteger(item.pm_freq_filter_min_days));
                    query.Parameters.AddWithValue("@pm_freq_filter_min_cash_in", ManejoNulos.ManageNullDecimal(item.pm_freq_filter_min_cash_in));
                    query.Parameters.AddWithValue("@pm_min_spent", ManejoNulos.ManageNullDecimal(item.pm_min_spent));
                    query.Parameters.AddWithValue("@pm_min_spent_reward", ManejoNulos.ManageNullDecimal(item.pm_min_spent_reward));
                    query.Parameters.AddWithValue("@pm_spent", ManejoNulos.ManageNullDecimal(item.pm_spent));
                    query.Parameters.AddWithValue("@pm_spent_reward", ManejoNulos.ManageNullDecimal(item.pm_spent_reward));
                    query.Parameters.AddWithValue("@pm_provider_list", ManejoNulos.ManageNullStr(item.pm_provider_list));
                    query.Parameters.AddWithValue("@pm_offer_list", ManejoNulos.ManageNullStr(item.pm_offer_list));
                    query.Parameters.AddWithValue("@pm_global_daily_limit", ManejoNulos.ManageNullDecimal(item.pm_global_daily_limit));
                    query.Parameters.AddWithValue("@pm_global_monthly_limit", ManejoNulos.ManageNullDecimal(item.pm_global_monthly_limit));
                    query.Parameters.AddWithValue("@pm_small_resource_id", ManejoNulos.ManageNullInteger64(item.pm_small_resource_id));
                    query.Parameters.AddWithValue("@pm_large_resource_id", ManejoNulos.ManageNullInteger64(item.pm_large_resource_id));
                    query.Parameters.AddWithValue("@pm_min_played", ManejoNulos.ManageNullDecimal(item.pm_min_played));
                    query.Parameters.AddWithValue("@pm_min_played_reward", ManejoNulos.ManageNullDecimal(item.pm_min_played_reward));
                    query.Parameters.AddWithValue("@pm_played", ManejoNulos.ManageNullDecimal(item.pm_played));
                    query.Parameters.AddWithValue("@pm_played_reward", ManejoNulos.ManageNullDecimal(item.pm_played_reward));
                    query.Parameters.AddWithValue("@pm_play_restricted_to_provider_list", ManejoNulos.ManegeNullBool(item.pm_play_restricted_to_provider_list));
                    query.Parameters.AddWithValue("@pm_last_executed", ManejoNulos.ManageNullDate(item.pm_last_executed));
                    query.Parameters.AddWithValue("@pm_next_execution", ManejoNulos.ManageNullDate(item.pm_next_execution));
                    query.Parameters.AddWithValue("@pm_global_limit", ManejoNulos.ManageNullDecimal(item.pm_global_limit));
                    query.Parameters.AddWithValue("@pm_credit_type", ManejoNulos.ManageNullInteger(item.pm_credit_type));
                    query.Parameters.AddWithValue("@pm_category_id", ManejoNulos.ManageNullInteger(item.pm_category_id));
                    query.Parameters.AddWithValue("@pm_ticket_footer", ManejoNulos.ManageNullStr(item.pm_ticket_footer));
                    query.Parameters.AddWithValue("@pm_visible_on_promobox", ManejoNulos.ManegeNullBool(item.pm_visible_on_promobox));
                    query.Parameters.AddWithValue("@pm_expiration_limit", ManejoNulos.ManageNullDate(item.pm_expiration_limit));
                    query.Parameters.AddWithValue("@pm_restricted_to_terminal_list", ManejoNulos.ManageNullStr(item.pm_restricted_to_terminal_list));
                    query.Parameters.AddWithValue("@pm_points_to_credits_id", ManejoNulos.ManageNullInteger64(item.pm_points_to_credits_id));
                    query.Parameters.AddWithValue("@pm_award_on_promobox", ManejoNulos.ManageNullInteger(item.pm_award_on_promobox));
                    query.Parameters.AddWithValue("@pm_text_on_promobox", ManejoNulos.ManageNullStr(item.pm_text_on_promobox));
                    query.Parameters.AddWithValue("@pm_ticket_quantity", ManejoNulos.ManageNullInteger(item.pm_ticket_quantity));
                    query.Parameters.AddWithValue("@pm_generated_tickets", ManejoNulos.ManageNullInteger(item.pm_generated_tickets));
                    query.Parameters.AddWithValue("@pm_vip", ManejoNulos.ManegeNullBool(item.pm_vip));
                    query.Parameters.AddWithValue("@pm_created_account_filter", ManejoNulos.ManageNullInteger(item.pm_created_account_filter));
                    query.Parameters.AddWithValue("@pm_apply_tax", ManejoNulos.ManegeNullBool(item.pm_apply_tax));
                    query.Parameters.AddWithValue("@pm_promogame_id", ManejoNulos.ManageNullInteger64(item.pm_promogame_id));
                    query.Parameters.AddWithValue("@pm_journey_limit", ManejoNulos.ManegeNullBool(item.pm_journey_limit));
                    query.Parameters.AddWithValue("@pm_pyramidal_dist", ManejoNulos.ManageNullStr(item.pm_pyramidal_dist));
                    query.Parameters.AddWithValue("@PM_PCT_BY_CHARGE", ManejoNulos.ManageNullDecimal(item.PM_PCT_BY_CHARGE));
                    query.Parameters.AddWithValue("@PM_ORDER", ManejoNulos.ManageNullInteger(item.PM_ORDER));
                    query.Parameters.AddWithValue("@pm_automatically_assign_sashost", ManejoNulos.ManegeNullBool(item.pm_automatically_assign_sashost));
                    query.Parameters.AddWithValue("@pm_automatically_assign_cashier", ManejoNulos.ManegeNullBool(item.pm_automatically_assign_cashier));
                    query.Parameters.AddWithValue("@pm_automatically_assign_promobox", ManejoNulos.ManegeNullBool(item.pm_automatically_assign_promobox));
                    query.Parameters.AddWithValue("@pm_automatically_promotion", ManejoNulos.ManegeNullBool(item.pm_automatically_promotion));
                    query.Parameters.AddWithValue("@pm_spent_flags", ManejoNulos.ManegeNullBool(item.pm_spent_flags));
                    query.Parameters.AddWithValue("@pm_status", ManejoNulos.ManageNullInteger(item.pm_status));
                    query.Parameters.AddWithValue("@pm_ms_sequence_id", ManejoNulos.ManageNullInteger64(item.pm_ms_sequence_id));
                    query.Parameters.AddWithValue("@pm_ms_id", ManejoNulos.ManageNullInteger64(item.pm_ms_id));
                    query.Parameters.AddWithValue("@pm_enabled_sites", ManejoNulos.ManageNullStr(item.pm_enabled_sites));
                    query.Parameters.AddWithValue("@pm_studyPeriod1_enabled", ManejoNulos.ManegeNullBool(item.pm_studyPeriod1_enabled));
                    query.Parameters.AddWithValue("@pm_studyPeriod1_time_from", ManejoNulos.ManageNullInteger(item.pm_studyPeriod1_time_from));
                    query.Parameters.AddWithValue("@pm_studyPeriod1_time_to", ManejoNulos.ManageNullInteger(item.pm_studyPeriod1_time_to));
                    query.Parameters.AddWithValue("@pm_studyPeriod2_enabled", ManejoNulos.ManegeNullBool(item.pm_studyPeriod2_enabled));
                    query.Parameters.AddWithValue("@pm_studyPeriod2_time_from", ManejoNulos.ManageNullInteger(item.pm_studyPeriod2_time_from));
                    query.Parameters.AddWithValue("@pm_studyPeriod2_time_to", ManejoNulos.ManageNullInteger(item.pm_studyPeriod2_time_to));
                    query.Parameters.AddWithValue("@pm_studyPeriodAccumulated_enabled", ManejoNulos.ManegeNullBool(item.pm_studyPeriodAccumulated_enabled));
                    query.Parameters.AddWithValue("@pm_gift_by_level", ManejoNulos.ManageNullStr(item.pm_gift_by_level));
                    query.Parameters.AddWithValue("@pm_weekly_limit", ManejoNulos.ManageNullDecimal(item.pm_weekly_limit));
                    query.Parameters.AddWithValue("@pm_global_weekly_limit", ManejoNulos.ManageNullDecimal(item.pm_global_weekly_limit));
                    query.Parameters.AddWithValue("@pm_decrease_spent_redeemable_gom", ManejoNulos.ManegeNullBool(item.pm_decrease_spent_redeemable_gom));
                    query.Parameters.AddWithValue("@pm_notification_gom", ManejoNulos.ManageNullStr(item.pm_notification_gom));
                    query.Parameters.AddWithValue("@pm_flag_require", ManejoNulos.ManegeNullBool(item.pm_flag_require));
                    query.Parameters.AddWithValue("@pm_all_accounts", ManejoNulos.ManegeNullBool(item.pm_all_accounts));
                    query.Parameters.AddWithValue("@pm_coin_in", ManejoNulos.ManageNullStr(item.pm_coin_in));
                    query.Parameters.AddWithValue("@pm_country_list", ManejoNulos.ManageNullStr(item.pm_country_list));
                    query.Parameters.AddWithValue("@pm_include_prize_tax_form", ManejoNulos.ManegeNullBool(item.pm_include_prize_tax_form));
                    query.Parameters.AddWithValue("@pm_lock_enabled", ManejoNulos.ManageNullInteger(item.pm_lock_enabled));
                    query.Parameters.AddWithValue("@pm_lock_balance_factor", ManejoNulos.ManageNullInteger(item.pm_lock_balance_factor));
                    query.Parameters.AddWithValue("@pm_lock_balance_amount", ManejoNulos.ManageNullDecimal(item.pm_lock_balance_amount));
                    query.Parameters.AddWithValue("@pm_lock_coin_in_factor", ManejoNulos.ManageNullInteger(item.pm_lock_coin_in_factor));
                    query.Parameters.AddWithValue("@pm_lock_coin_in_amount", ManejoNulos.ManageNullDecimal(item.pm_lock_coin_in_amount));
                    query.Parameters.AddWithValue("@pm_lock_average_bet", ManejoNulos.ManageNullDecimal(item.pm_lock_average_bet));
                    query.Parameters.AddWithValue("@pm_lock_plays", ManejoNulos.ManageNullInteger(item.pm_lock_plays));
                    query.Parameters.AddWithValue("@pm_lock_max_payable_factor_enabled", ManejoNulos.ManageNullInteger(item.pm_lock_max_payable_factor_enabled));
                    query.Parameters.AddWithValue("@pm_lock_max_payable_factor", ManejoNulos.ManageNullDecimal(item.pm_lock_max_payable_factor));
                    query.Parameters.AddWithValue("@pm_lock_max_payable_amount_enabled", ManejoNulos.ManageNullInteger(item.pm_lock_max_payable_amount_enabled));
                    query.Parameters.AddWithValue("@pm_lock_max_payable_amount", ManejoNulos.ManageNullDecimal(item.pm_lock_max_payable_amount));
                    query.Parameters.AddWithValue("@pm_patron_limit", ManejoNulos.ManageNullDecimal(item.pm_patron_limit));
                    query.Parameters.AddWithValue("@pm_times_daily", ManejoNulos.ManageNullInteger(item.pm_times_daily));
                    query.Parameters.AddWithValue("@pm_times_weekly", ManejoNulos.ManageNullInteger(item.pm_times_weekly));
                    query.Parameters.AddWithValue("@pm_times_monthly", ManejoNulos.ManageNullInteger(item.pm_times_monthly));
                    query.Parameters.AddWithValue("@pm_times_patron", ManejoNulos.ManageNullInteger(item.pm_times_patron));
                    query.Parameters.AddWithValue("@pm_global_times_day", ManejoNulos.ManageNullInteger(item.pm_global_times_day));
                    query.Parameters.AddWithValue("@pm_global_times_week", ManejoNulos.ManageNullInteger(item.pm_global_times_week));
                    query.Parameters.AddWithValue("@pm_global_times_month", ManejoNulos.ManageNullInteger(item.pm_global_times_month));
                    query.Parameters.AddWithValue("@pm_global_times_global", ManejoNulos.ManageNullInteger(item.pm_global_times_global));
                    query.Parameters.AddWithValue("@pm_cash_in_max_reward", ManejoNulos.ManageNullDecimal(item.pm_cash_in_max_reward));
                    query.Parameters.AddWithValue("@pm_weights_id", ManejoNulos.ManageNullInteger(item.pm_weights_id));
                    query.Parameters.AddWithValue("@pm_awarded_promotion_discounted", ManejoNulos.ManegeNullBool(item.pm_awarded_promotion_discounted));
                    query.Parameters.AddWithValue("@pm_lock_min_payable_amount", ManejoNulos.ManageNullDecimal(item.pm_lock_min_payable_amount));
                    query.Parameters.AddWithValue("@pm_auto_conversion", ManejoNulos.ManegeNullBool(item.pm_auto_conversion));
                    query.Parameters.AddWithValue("@pm_input_amount_type", ManejoNulos.ManageNullInteger(item.pm_input_amount_type));
                    query.Parameters.AddWithValue("@pm_is_automatic_transfer", ManejoNulos.ManegeNullBool(item.pm_is_automatic_transfer));
                    query.Parameters.AddWithValue("@pm_min_coinin", ManejoNulos.ManageNullDecimal(item.pm_min_coinin));
                    query.Parameters.AddWithValue("@pm_min_coinin_reward", ManejoNulos.ManageNullDecimal(item.pm_min_coinin_reward));
                    query.Parameters.AddWithValue("@pm_coinin", ManejoNulos.ManageNullDecimal(item.pm_coinin));
                    query.Parameters.AddWithValue("@pm_coinin_reward", ManejoNulos.ManageNullDecimal(item.pm_coinin_reward));
                    query.Parameters.AddWithValue("@pm_min_points", ManejoNulos.ManageNullDecimal(item.pm_min_points));
                    query.Parameters.AddWithValue("@pm_min_points_reward", ManejoNulos.ManageNullDecimal(item.pm_min_points_reward));
                    query.Parameters.AddWithValue("@pm_points", ManejoNulos.ManageNullDecimal(item.pm_points));
                    query.Parameters.AddWithValue("@pm_points_reward", ManejoNulos.ManageNullDecimal(item.pm_points_reward));
                    query.Parameters.AddWithValue("@pm_coinin_to_terminal_list", ManejoNulos.ManageNullStr(item.pm_coinin_to_terminal_list));
                    query.Parameters.AddWithValue("@pm_points_to_terminal_list", ManejoNulos.ManageNullStr(item.pm_points_to_terminal_list));
                    query.Parameters.AddWithValue("@pm_small_resource_id_55", ManejoNulos.ManageNullInteger64(item.pm_small_resource_id_55));
                    query.Parameters.AddWithValue("@pm_include_recharge", ManejoNulos.ManegeNullBool(item.pm_include_recharge));
                    query.Parameters.AddWithValue("@pm_parent_promotion_id", ManejoNulos.ManageNullInteger64(item.pm_parent_promotion_id));
                    query.Parameters.AddWithValue("@pm_has_schedule", ManejoNulos.ManegeNullBool(item.pm_has_schedule));
                    //IdInsertado = Convert.ToInt32(query.ExecuteScalar());
                    query.ExecuteNonQuery();
                    IdInsertado = item.pm_promotion_id;
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
