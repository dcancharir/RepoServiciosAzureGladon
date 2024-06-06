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
    public class promotions_dal
    {
        private readonly string _conexion = string.Empty;

        public promotions_dal()
        {
            _conexion = ConfigurationManager.ConnectionStrings["connection_wgdb_000"].ConnectionString;
        }
        public List<promotions> GetPromotionsPaginated(long lastid, int skip, int pageSize)
        {
            var result = new List<promotions>();
            try
            {
                string query = $@"
SELECT [pm_promotion_id]
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
      ,[pm_has_schedule]
  FROM [dbo].[promotions]
  where pm_promotion_id > {lastid}
  order by pm_promotion_id asc
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
                                var item = new promotions()
                                {
                                    pm_promotion_id = (long)dr["pm_promotion_id"],
                                    pm_name = dr["pm_name"] == DBNull.Value ? null : (string)dr["pm_name"],
                                    pm_enabled = (bool)dr["pm_enabled"],
                                    pm_type = (int)dr["pm_type"],
                                    pm_date_start = (DateTime)dr["pm_date_start"],
                                    pm_date_finish = (DateTime)dr["pm_date_finish"],
                                    pm_schedule_weekday = (int)dr["pm_schedule_weekday"],
                                    pm_schedule1_time_from = (int)dr["pm_schedule1_time_from"],
                                    pm_schedule1_time_to = (int)dr["pm_schedule1_time_to"],
                                    pm_schedule2_enabled = (bool)dr["pm_schedule2_enabled"],
                                    pm_schedule2_time_from = dr["pm_schedule2_time_from"] == DBNull.Value ? null : (int?)dr["pm_schedule2_time_from"],
                                    pm_schedule2_time_to = dr["pm_schedule2_time_to"] == DBNull.Value ? null : (int?)dr["pm_schedule2_time_to"],
                                    pm_gender_filter = (int)dr["pm_gender_filter"],
                                    pm_birthday_filter = (int)dr["pm_birthday_filter"],
                                    pm_expiration_type = (int)dr["pm_expiration_type"],
                                    pm_expiration_value = (int)dr["pm_expiration_value"],
                                    pm_min_cash_in = (decimal)dr["pm_min_cash_in"],
                                    pm_min_cash_in_reward = (decimal)dr["pm_min_cash_in_reward"],
                                    pm_cash_in = (decimal)dr["pm_cash_in"],
                                    pm_cash_in_reward = (decimal)dr["pm_cash_in_reward"],
                                    pm_won_lock = dr["pm_won_lock"] == DBNull.Value ? null : (decimal?)dr["pm_won_lock"],
                                    pm_num_tokens = (int)dr["pm_num_tokens"],
                                    pm_token_name = dr["pm_token_name"] == DBNull.Value ? null : (string)dr["pm_token_name"],
                                    pm_token_reward = (decimal)dr["pm_token_reward"],
                                    pm_daily_limit = dr["pm_daily_limit"] == DBNull.Value ? null : (decimal?)dr["pm_daily_limit"],
                                    pm_monthly_limit = dr["pm_monthly_limit"] == DBNull.Value ? null : (decimal?)dr["pm_monthly_limit"],
                                    pm_level_filter = (int)dr["pm_level_filter"],
                                    pm_permission = (int)dr["pm_permission"],
                                    pm_freq_filter_last_days = dr["pm_freq_filter_last_days"] == DBNull.Value ? null : (int?)dr["pm_freq_filter_last_days"],
                                    pm_freq_filter_min_days = dr["pm_freq_filter_min_days"] == DBNull.Value ? null : (int?)dr["pm_freq_filter_min_days"],
                                    pm_freq_filter_min_cash_in = dr["pm_freq_filter_min_cash_in"] == DBNull.Value ? null : (decimal?)dr["pm_freq_filter_min_cash_in"],
                                    pm_min_spent = (decimal)dr["pm_min_spent"],
                                    pm_min_spent_reward = (decimal)dr["pm_min_spent_reward"],
                                    pm_spent = (decimal)dr["pm_spent"],
                                    pm_spent_reward = (decimal)dr["pm_spent_reward"],
                                    pm_provider_list = dr["pm_provider_list"] == DBNull.Value ? null : (string)dr["pm_provider_list"],
                                    pm_offer_list = dr["pm_offer_list"] == DBNull.Value ? null : (string)dr["pm_offer_list"],
                                    pm_global_daily_limit = dr["pm_global_daily_limit"] == DBNull.Value ? null : (decimal?)dr["pm_global_daily_limit"],
                                    pm_global_monthly_limit = dr["pm_global_monthly_limit"] == DBNull.Value ? null : (decimal?)dr["pm_global_monthly_limit"],
                                    pm_small_resource_id = dr["pm_small_resource_id"] == DBNull.Value ? null : (long?)dr["pm_small_resource_id"],
                                    pm_large_resource_id = dr["pm_large_resource_id"] == DBNull.Value ? null : (long?)dr["pm_large_resource_id"],
                                    pm_min_played = (decimal)dr["pm_min_played"],
                                    pm_min_played_reward = (decimal)dr["pm_min_played_reward"],
                                    pm_played = (decimal)dr["pm_played"],
                                    pm_played_reward = (decimal)dr["pm_played_reward"],
                                    pm_play_restricted_to_provider_list = (bool)dr["pm_play_restricted_to_provider_list"],
                                    pm_last_executed = dr["pm_last_executed"] == DBNull.Value ? null : (DateTime?)dr["pm_last_executed"],
                                    pm_next_execution = dr["pm_next_execution"] == DBNull.Value ? null : (DateTime?)dr["pm_next_execution"],
                                    pm_global_limit = dr["pm_global_limit"] == DBNull.Value ? null : (decimal?)dr["pm_global_limit"],
                                    pm_credit_type = (int)dr["pm_credit_type"],
                                    pm_category_id = (int)dr["pm_category_id"],
                                    pm_ticket_footer = dr["pm_ticket_footer"] == DBNull.Value ? null : (string)dr["pm_ticket_footer"],
                                    pm_visible_on_promobox = (bool)dr["pm_visible_on_promobox"],
                                    pm_expiration_limit = dr["pm_expiration_limit"] == DBNull.Value ? null : (DateTime?)dr["pm_expiration_limit"],
                                    pm_restricted_to_terminal_list = dr["pm_restricted_to_terminal_list"] == DBNull.Value ? null : (string)dr["pm_restricted_to_terminal_list"],
                                    pm_points_to_credits_id = dr["pm_points_to_credits_id"] == DBNull.Value ? null : (long?)dr["pm_points_to_credits_id"],
                                    pm_award_on_promobox = dr["pm_award_on_promobox"] == DBNull.Value ? null : (int?)dr["pm_award_on_promobox"],
                                    pm_text_on_promobox = dr["pm_text_on_promobox"] == DBNull.Value ? null : (string)dr["pm_text_on_promobox"],
                                    pm_ticket_quantity = dr["pm_ticket_quantity"] == DBNull.Value ? null : (int?)dr["pm_ticket_quantity"],
                                    pm_generated_tickets = dr["pm_generated_tickets"] == DBNull.Value ? null : (int?)dr["pm_generated_tickets"],
                                    pm_vip = dr["pm_vip"] == DBNull.Value ? null : (bool?)dr["pm_vip"],
                                    pm_created_account_filter = dr["pm_created_account_filter"] == DBNull.Value ? null : (int?)dr["pm_created_account_filter"],
                                    pm_apply_tax = dr["pm_apply_tax"] == DBNull.Value ? null : (bool?)dr["pm_apply_tax"],
                                    pm_promogame_id = dr["pm_promogame_id"] == DBNull.Value ? null : (long?)dr["pm_promogame_id"],
                                    pm_journey_limit = (bool)dr["pm_journey_limit"],
                                    pm_pyramidal_dist = dr["pm_pyramidal_dist"] == DBNull.Value ? null : (string)dr["pm_pyramidal_dist"],
                                    PM_PCT_BY_CHARGE = dr["PM_PCT_BY_CHARGE"] == DBNull.Value ? null : (decimal?)dr["PM_PCT_BY_CHARGE"],
                                    PM_ORDER = (int)dr["PM_ORDER"],
                                    pm_automatically_assign_sashost = (bool)dr["pm_automatically_assign_sashost"],
                                    pm_automatically_assign_cashier = (bool)dr["pm_automatically_assign_cashier"],
                                    pm_automatically_assign_promobox = (bool)dr["pm_automatically_assign_promobox"],
                                    pm_automatically_promotion = (bool)dr["pm_automatically_promotion"],
                                    pm_spent_flags = (bool)dr["pm_spent_flags"],
                                    pm_status = dr["pm_status"] == DBNull.Value ? null : (int?)dr["pm_status"],
                                    pm_ms_sequence_id = dr["pm_ms_sequence_id"] == DBNull.Value ? null : (long?)dr["pm_ms_sequence_id"],
                                    pm_ms_id = dr["pm_ms_id"] == DBNull.Value ? null : (long?)dr["pm_ms_id"],
                                    pm_enabled_sites = dr["pm_enabled_sites"] == DBNull.Value ? null : (string)dr["pm_enabled_sites"],
                                    pm_studyPeriod1_enabled = dr["pm_studyPeriod1_enabled"] == DBNull.Value ? null : (bool?)dr["pm_studyPeriod1_enabled"],
                                    pm_studyPeriod1_time_from = dr["pm_studyPeriod1_time_from"] == DBNull.Value ? null : (int?)dr["pm_studyPeriod1_time_from"],
                                    pm_studyPeriod1_time_to = dr["pm_studyPeriod1_time_to"] == DBNull.Value ? null : (int?)dr["pm_studyPeriod1_time_to"],
                                    pm_studyPeriod2_enabled = dr["pm_studyPeriod2_enabled"] == DBNull.Value ? null : (bool?)dr["pm_studyPeriod2_enabled"],
                                    pm_studyPeriod2_time_from = dr["pm_studyPeriod2_time_from"] == DBNull.Value ? null : (int?)dr["pm_studyPeriod2_time_from"],
                                    pm_studyPeriod2_time_to = dr["pm_studyPeriod2_time_to"] == DBNull.Value ? null : (int?)dr["pm_studyPeriod2_time_to"],
                                    pm_studyPeriodAccumulated_enabled = dr["pm_studyPeriodAccumulated_enabled"] == DBNull.Value ? null : (bool?)dr["pm_studyPeriodAccumulated_enabled"],
                                    pm_gift_by_level = dr["pm_gift_by_level"] == DBNull.Value ? null : (string)dr["pm_gift_by_level"],
                                    pm_weekly_limit = dr["pm_weekly_limit"] == DBNull.Value ? null : (decimal?)dr["pm_weekly_limit"],
                                    pm_global_weekly_limit = dr["pm_global_weekly_limit"] == DBNull.Value ? null : (decimal?)dr["pm_global_weekly_limit"],
                                    pm_decrease_spent_redeemable_gom = dr["pm_decrease_spent_redeemable_gom"] == DBNull.Value ? null : (bool?)dr["pm_decrease_spent_redeemable_gom"],
                                    pm_notification_gom = dr["pm_notification_gom"] == DBNull.Value ? null : (string)dr["pm_notification_gom"],
                                    pm_flag_require = (bool)dr["pm_flag_require"],
                                    pm_all_accounts = (bool)dr["pm_all_accounts"],
                                    pm_coin_in = dr["pm_coin_in"] == DBNull.Value ? null : (string)dr["pm_coin_in"],
                                    pm_country_list = dr["pm_country_list"] == DBNull.Value ? null : (string)dr["pm_country_list"],
                                    pm_include_prize_tax_form = dr["pm_include_prize_tax_form"] == DBNull.Value ? null : (bool?)dr["pm_include_prize_tax_form"],
                                    pm_lock_enabled = (int)dr["pm_lock_enabled"],
                                    pm_lock_balance_factor = dr["pm_lock_balance_factor"] == DBNull.Value ? null : (int?)dr["pm_lock_balance_factor"],
                                    pm_lock_balance_amount = dr["pm_lock_balance_amount"] == DBNull.Value ? null : (decimal?)dr["pm_lock_balance_amount"],
                                    pm_lock_coin_in_factor = dr["pm_lock_coin_in_factor"] == DBNull.Value ? null : (int?)dr["pm_lock_coin_in_factor"],
                                    pm_lock_coin_in_amount = dr["pm_lock_coin_in_amount"] == DBNull.Value ? null : (decimal?)dr["pm_lock_coin_in_amount"],
                                    pm_lock_average_bet = dr["pm_lock_average_bet"] == DBNull.Value ? null : (decimal?)dr["pm_lock_average_bet"],
                                    pm_lock_plays = dr["pm_lock_plays"] == DBNull.Value ? null : (int?)dr["pm_lock_plays"],
                                    pm_lock_max_payable_factor_enabled = (int)dr["pm_lock_max_payable_factor_enabled"],
                                    pm_lock_max_payable_factor = dr["pm_lock_max_payable_factor"] == DBNull.Value ? null : (decimal?)dr["pm_lock_max_payable_factor"],
                                    pm_lock_max_payable_amount_enabled = (int)dr["pm_lock_max_payable_amount_enabled"],
                                    pm_lock_max_payable_amount = dr["pm_lock_max_payable_amount"] == DBNull.Value ? null : (decimal?)dr["pm_lock_max_payable_amount"],
                                    pm_patron_limit = dr["pm_patron_limit"] == DBNull.Value ? null : (decimal?)dr["pm_patron_limit"],
                                    pm_times_daily = dr["pm_times_daily"] == DBNull.Value ? null : (int?)dr["pm_times_daily"],
                                    pm_times_weekly = dr["pm_times_weekly"] == DBNull.Value ? null : (int?)dr["pm_times_weekly"],
                                    pm_times_monthly = dr["pm_times_monthly"] == DBNull.Value ? null : (int?)dr["pm_times_monthly"],
                                    pm_times_patron = dr["pm_times_patron"] == DBNull.Value ? null : (int?)dr["pm_times_patron"],
                                    pm_global_times_day = dr["pm_global_times_day"] == DBNull.Value ? null : (int?)dr["pm_global_times_day"],
                                    pm_global_times_week = dr["pm_global_times_week"] == DBNull.Value ? null : (int?)dr["pm_global_times_week"],
                                    pm_global_times_month = dr["pm_global_times_month"] == DBNull.Value ? null : (int?)dr["pm_global_times_month"],
                                    pm_global_times_global = dr["pm_global_times_global"] == DBNull.Value ? null : (int?)dr["pm_global_times_global"],
                                    pm_cash_in_max_reward = (decimal)dr["pm_cash_in_max_reward"],
                                    pm_weights_id = dr["pm_weights_id"] == DBNull.Value ? null : (int?)dr["pm_weights_id"],
                                    pm_awarded_promotion_discounted = (bool)dr["pm_awarded_promotion_discounted"],
                                    pm_lock_min_payable_amount = dr["pm_lock_min_payable_amount"] == DBNull.Value ? null : (decimal?)dr["pm_lock_min_payable_amount"],
                                    pm_auto_conversion = (bool)dr["pm_auto_conversion"],
                                    pm_input_amount_type = dr["pm_input_amount_type"] == DBNull.Value ? null : (int?)dr["pm_input_amount_type"],
                                    pm_is_automatic_transfer = dr["pm_is_automatic_transfer"] == DBNull.Value ? null : (bool?)dr["pm_is_automatic_transfer"],
                                    pm_min_coinin = (decimal)dr["pm_min_coinin"],
                                    pm_min_coinin_reward = (decimal)dr["pm_min_coinin_reward"],
                                    pm_coinin = (decimal)dr["pm_coinin"],
                                    pm_coinin_reward = (decimal)dr["pm_coinin_reward"],
                                    pm_min_points = (decimal)dr["pm_min_points"],
                                    pm_min_points_reward = (decimal)dr["pm_min_points_reward"],
                                    pm_points = (decimal)dr["pm_points"],
                                    pm_points_reward = (decimal)dr["pm_points_reward"],
                                    pm_coinin_to_terminal_list = dr["pm_coinin_to_terminal_list"] == DBNull.Value ? null : (string)dr["pm_coinin_to_terminal_list"],
                                    pm_points_to_terminal_list = dr["pm_points_to_terminal_list"] == DBNull.Value ? null : (string)dr["pm_points_to_terminal_list"],
                                    pm_small_resource_id_55 = dr["pm_small_resource_id_55"] == DBNull.Value ? null : (long?)dr["pm_small_resource_id_55"],
                                    pm_include_recharge = (bool)dr["pm_include_recharge"],
                                    pm_parent_promotion_id = dr["pm_parent_promotion_id"] == DBNull.Value ? null : (long?)dr["pm_parent_promotion_id"],
                                    pm_has_schedule = (bool)dr["pm_has_schedule"],
                                };
                                result.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = new List<promotions>();
            }
            return result;
        }
        public int GetTotalPromotionsForMigration(long lastid)
        {
            int total = 0;

            string query = @"
            select count(*) as total from 
            [dbo].[promotions]
where pm_promotion_id > @lastid
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
        public int SavePromotions(promotions item)
        {
            //bool respuesta = false;
            int IdInsertado = 0;
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
output inserted.pm_promotion_id
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
                using (var con = new SqlConnection(_conexion))
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
