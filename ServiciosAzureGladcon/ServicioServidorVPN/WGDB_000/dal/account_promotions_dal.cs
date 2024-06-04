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
    public class account_promotions_dal
    {
        private readonly string bd_username = string.Empty;
        private readonly string bd_password = string.Empty;
        private readonly string bd_datasource = string.Empty;
        private readonly string connectionString = string.Empty;
        public account_promotions_dal(string database_name)
        {
            bd_username = ConfigurationManager.AppSettings["bd_username"];
            bd_password = ConfigurationManager.AppSettings["bd_password"];
            bd_datasource = ConfigurationManager.AppSettings["bd_datasource"];
            connectionString = $"Data Source={bd_datasource};Initial Catalog={database_name};Integrated Security=False;User ID={bd_username};Password={bd_password}";

        }
        public long SaveAccountPromotions(account_promotions item)
        {
            //bool respuesta = false;
            long IdInsertado = 0;
            string consulta = @"
INSERT INTO [dbo].[account_promotions]
           ([acp_unique_id]
           ,[acp_created]
           ,[acp_account_id]
           ,[acp_account_level]
           ,[acp_promo_type]
           ,[acp_promo_id]
           ,[acp_promo_name]
           ,[acp_promo_date]
           ,[acp_operation_id]
           ,[acp_credit_type]
           ,[acp_activation]
           ,[acp_expiration]
           ,[acp_cash_in]
           ,[acp_points]
           ,[acp_ini_balance]
           ,[acp_ini_withhold]
           ,[acp_ini_wonlock]
           ,[acp_balance]
           ,[acp_withhold]
           ,[acp_wonlock]
           ,[acp_played]
           ,[acp_won]
           ,[acp_status]
           ,[acp_updated]
           ,[acp_play_session_id]
           ,[acp_transaction_id]
           ,[acp_recommended_account_id]
           ,[acp_details]
           ,[acp_promo_category_id]
           ,[acp_redeemable_cost]
           ,[acp_prize_type]
           ,[acp_prize_gross]
           ,[acp_prize_tax1]
           ,[acp_prize_tax2]
           ,[acp_prize_tax3]
           ,[acp_pyramid_prize]
           ,[acp_draw_price]
           ,[acp_total_prize]
           ,[acp_prize_tax4]
           ,[acp_prize_tax5]
           ,[acp_promogame_return_price_pct]
           ,[acp_ms_id]
           ,[acp_ms_sequence_id]
           ,[acp_site_redeemed]
           ,[acp_import_filename]
           ,[acp_ms_promo_id]
           ,[acp_lock_enabled]
           ,[acp_lock_balance_factor]
           ,[acp_lock_balance_amount]
           ,[acp_lock_coin_in_factor]
           ,[acp_lock_coin_in_amount]
           ,[acp_lock_average_bet]
           ,[acp_lock_plays]
           ,[acp_lock_max_payable_factor_enabled]
           ,[acp_lock_max_payable_factor]
           ,[acp_lock_max_payable_amount_enabled]
           ,[acp_lock_max_payable_amount]
           ,[acp_lock_min_payable_amount]
           ,[acp_cash_in_max_reward]
           ,[acp_weights_id]
           ,[acp_total_played_weighted]
           ,[acp_total_won_weighted]
           ,[acp_total_plays]
           ,[acp_min_cash_in_reward]
           ,[acp_redeemable_played]
           ,[acp_awarded_promotion_discounted]
           ,[acp_converted_amount]
           ,[acp_auto_conversion]
           ,[acp_lock_percentage]
           ,[acp_include_recharge]
           ,[acp_min_cash_in]
           ,[acp_min_spent]
           ,[acp_min_spent_reward]
           ,[acp_num_tokens]
           ,[acp_num_used_tokens]
           ,[acp_token_reward])
--output inserted.acp_unique_id
     VALUES
           (@acp_unique_id
           ,@acp_created
           ,@acp_account_id
           ,@acp_account_level
           ,@acp_promo_type
           ,@acp_promo_id
           ,@acp_promo_name
           ,@acp_promo_date
           ,@acp_operation_id
           ,@acp_credit_type
           ,@acp_activation
           ,@acp_expiration
           ,@acp_cash_in
           ,@acp_points
           ,@acp_ini_balance
           ,@acp_ini_withhold
           ,@acp_ini_wonlock
           ,@acp_balance
           ,@acp_withhold
           ,@acp_wonlock
           ,@acp_played
           ,@acp_won
           ,@acp_status
           ,@acp_updated
           ,@acp_play_session_id
           ,@acp_transaction_id
           ,@acp_recommended_account_id
           ,@acp_details
           ,@acp_promo_category_id
           ,@acp_redeemable_cost
           ,@acp_prize_type
           ,@acp_prize_gross
           ,@acp_prize_tax1
           ,@acp_prize_tax2
           ,@acp_prize_tax3
           ,@acp_pyramid_prize
           ,@acp_draw_price
           ,@acp_total_prize
           ,@acp_prize_tax4
           ,@acp_prize_tax5
           ,@acp_promogame_return_price_pct
           ,@acp_ms_id
           ,@acp_ms_sequence_id
           ,@acp_site_redeemed
           ,@acp_import_filename
           ,@acp_ms_promo_id
           ,@acp_lock_enabled
           ,@acp_lock_balance_factor
           ,@acp_lock_balance_amount
           ,@acp_lock_coin_in_factor
           ,@acp_lock_coin_in_amount
           ,@acp_lock_average_bet
           ,@acp_lock_plays
           ,@acp_lock_max_payable_factor_enabled
           ,@acp_lock_max_payable_factor
           ,@acp_lock_max_payable_amount_enabled
           ,@acp_lock_max_payable_amount
           ,@acp_lock_min_payable_amount
           ,@acp_cash_in_max_reward
           ,@acp_weights_id
           ,@acp_total_played_weighted
           ,@acp_total_won_weighted
           ,@acp_total_plays
           ,@acp_min_cash_in_reward
           ,@acp_redeemable_played
           ,@acp_awarded_promotion_discounted
           ,@acp_converted_amount
           ,@acp_auto_conversion
           ,@acp_lock_percentage
           ,@acp_include_recharge
           ,@acp_min_cash_in
           ,@acp_min_spent
           ,@acp_min_spent_reward
           ,@acp_num_tokens
           ,@acp_num_used_tokens
           ,@acp_token_reward)
                      ";
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@acp_unique_id", ManejoNulos.ManageNullInteger64(item.acp_unique_id));
                    query.Parameters.AddWithValue("@acp_created", ManejoNulos.ManageNullDate(item.acp_created));
                    query.Parameters.AddWithValue("@acp_account_id", ManejoNulos.ManageNullInteger64(item.acp_account_id));
                    query.Parameters.AddWithValue("@acp_account_level", ManejoNulos.ManageNullInteger(item.acp_account_level));
                    query.Parameters.AddWithValue("@acp_promo_type", ManejoNulos.ManageNullInteger(item.acp_promo_type));
                    query.Parameters.AddWithValue("@acp_promo_id", ManejoNulos.ManageNullInteger64(item.acp_promo_id));
                    query.Parameters.AddWithValue("@acp_promo_name", ManejoNulos.ManageNullStr(item.acp_promo_name));
                    query.Parameters.AddWithValue("@acp_promo_date", ManejoNulos.ManageNullDate(item.acp_promo_date));
                    query.Parameters.AddWithValue("@acp_operation_id", ManejoNulos.ManageNullInteger64(item.acp_operation_id));
                    query.Parameters.AddWithValue("@acp_credit_type", ManejoNulos.ManageNullInteger(item.acp_credit_type));
                    query.Parameters.AddWithValue("@acp_activation", ManejoNulos.ManageNullDate(item.acp_activation));
                    query.Parameters.AddWithValue("@acp_expiration", ManejoNulos.ManageNullDate(item.acp_expiration));
                    query.Parameters.AddWithValue("@acp_cash_in", ManejoNulos.ManageNullDecimal(item.acp_cash_in));
                    query.Parameters.AddWithValue("@acp_points", ManejoNulos.ManageNullDecimal(item.acp_points));
                    query.Parameters.AddWithValue("@acp_ini_balance", ManejoNulos.ManageNullDecimal(item.acp_ini_balance));
                    query.Parameters.AddWithValue("@acp_ini_withhold", ManejoNulos.ManageNullDecimal(item.acp_ini_withhold));
                    query.Parameters.AddWithValue("@acp_ini_wonlock", ManejoNulos.ManageNullDecimal(item.acp_ini_wonlock));
                    query.Parameters.AddWithValue("@acp_balance", ManejoNulos.ManageNullDecimal(item.acp_balance));
                    query.Parameters.AddWithValue("@acp_withhold", ManejoNulos.ManageNullDecimal(item.acp_withhold));
                    query.Parameters.AddWithValue("@acp_wonlock", ManejoNulos.ManageNullDecimal(item.acp_wonlock));
                    query.Parameters.AddWithValue("@acp_played", ManejoNulos.ManageNullDecimal(item.acp_played));
                    query.Parameters.AddWithValue("@acp_won", ManejoNulos.ManageNullDecimal(item.acp_won));
                    query.Parameters.AddWithValue("@acp_status", ManejoNulos.ManageNullInteger(item.acp_status));
                    query.Parameters.AddWithValue("@acp_updated", ManejoNulos.ManageNullDate(item.acp_updated));
                    query.Parameters.AddWithValue("@acp_play_session_id", ManejoNulos.ManageNullInteger64(item.acp_play_session_id));
                    query.Parameters.AddWithValue("@acp_transaction_id", ManejoNulos.ManageNullInteger64(item.acp_transaction_id));
                    query.Parameters.AddWithValue("@acp_recommended_account_id", ManejoNulos.ManageNullInteger64(item.acp_recommended_account_id));
                    query.Parameters.AddWithValue("@acp_details", ManejoNulos.ManageNullStr(item.acp_details));
                    query.Parameters.AddWithValue("@acp_promo_category_id", ManejoNulos.ManageNullInteger(item.acp_promo_category_id));
                    query.Parameters.AddWithValue("@acp_redeemable_cost", ManejoNulos.ManageNullDecimal(item.acp_redeemable_cost));
                    query.Parameters.AddWithValue("@acp_prize_type", ManejoNulos.ManageNullInteger(item.acp_prize_type));
                    query.Parameters.AddWithValue("@acp_prize_gross", ManejoNulos.ManageNullDecimal(item.acp_prize_gross));
                    query.Parameters.AddWithValue("@acp_prize_tax1", ManejoNulos.ManageNullDecimal(item.acp_prize_tax1));
                    query.Parameters.AddWithValue("@acp_prize_tax2", ManejoNulos.ManageNullDecimal(item.acp_prize_tax2));
                    query.Parameters.AddWithValue("@acp_prize_tax3", ManejoNulos.ManageNullDecimal(item.acp_prize_tax3));
                    query.Parameters.AddWithValue("@acp_pyramid_prize", ManejoNulos.ManageNullDecimal(item.acp_pyramid_prize));
                    query.Parameters.AddWithValue("@acp_draw_price", ManejoNulos.ManageNullDecimal(item.acp_draw_price));
                    query.Parameters.AddWithValue("@acp_total_prize", ManejoNulos.ManageNullDecimal(item.acp_total_prize));
                    query.Parameters.AddWithValue("@acp_prize_tax4", ManejoNulos.ManageNullDecimal(item.acp_prize_tax4));
                    query.Parameters.AddWithValue("@acp_prize_tax5", ManejoNulos.ManageNullDecimal(item.acp_prize_tax5));
                    query.Parameters.AddWithValue("@acp_promogame_return_price_pct", ManejoNulos.ManageNullDecimal(item.acp_promogame_return_price_pct));
                    query.Parameters.AddWithValue("@acp_ms_id", ManejoNulos.ManageNullInteger64(item.acp_ms_id));
                    query.Parameters.AddWithValue("@acp_ms_sequence_id", ManejoNulos.ManageNullInteger64(item.acp_ms_sequence_id));
                    query.Parameters.AddWithValue("@acp_site_redeemed", ManejoNulos.ManageNullInteger(item.acp_site_redeemed));
                    query.Parameters.AddWithValue("@acp_import_filename", ManejoNulos.ManageNullStr(item.acp_import_filename));
                    query.Parameters.AddWithValue("@acp_ms_promo_id", ManejoNulos.ManageNullInteger64(item.acp_ms_promo_id));
                    query.Parameters.AddWithValue("@acp_lock_enabled", ManejoNulos.ManageNullInteger(item.acp_lock_enabled));
                    query.Parameters.AddWithValue("@acp_lock_balance_factor", ManejoNulos.ManageNullInteger(item.acp_lock_balance_factor));
                    query.Parameters.AddWithValue("@acp_lock_balance_amount", ManejoNulos.ManageNullDecimal(item.acp_lock_balance_amount));
                    query.Parameters.AddWithValue("@acp_lock_coin_in_factor", ManejoNulos.ManageNullInteger(item.acp_lock_coin_in_factor));
                    query.Parameters.AddWithValue("@acp_lock_coin_in_amount", ManejoNulos.ManageNullDecimal(item.acp_lock_coin_in_amount));
                    query.Parameters.AddWithValue("@acp_lock_average_bet", ManejoNulos.ManageNullDecimal(item.acp_lock_average_bet));
                    query.Parameters.AddWithValue("@acp_lock_plays", ManejoNulos.ManageNullInteger(item.acp_lock_plays));
                    query.Parameters.AddWithValue("@acp_lock_max_payable_factor_enabled", ManejoNulos.ManageNullInteger(item.acp_lock_max_payable_factor_enabled));
                    query.Parameters.AddWithValue("@acp_lock_max_payable_factor", ManejoNulos.ManageNullDecimal(item.acp_lock_max_payable_factor));
                    query.Parameters.AddWithValue("@acp_lock_max_payable_amount_enabled", ManejoNulos.ManageNullInteger(item.acp_lock_max_payable_amount_enabled));
                    query.Parameters.AddWithValue("@acp_lock_max_payable_amount", ManejoNulos.ManageNullDecimal(item.acp_lock_max_payable_amount));
                    query.Parameters.AddWithValue("@acp_lock_min_payable_amount", ManejoNulos.ManageNullDecimal(item.acp_lock_min_payable_amount));
                    query.Parameters.AddWithValue("@acp_cash_in_max_reward", ManejoNulos.ManageNullDecimal(item.acp_cash_in_max_reward));
                    query.Parameters.AddWithValue("@acp_weights_id", ManejoNulos.ManageNullInteger(item.acp_weights_id));
                    query.Parameters.AddWithValue("@acp_total_played_weighted", ManejoNulos.ManageNullDecimal(item.acp_total_played_weighted));
                    query.Parameters.AddWithValue("@acp_total_won_weighted", ManejoNulos.ManageNullDecimal(item.acp_total_won_weighted));
                    query.Parameters.AddWithValue("@acp_total_plays", ManejoNulos.ManageNullInteger(item.acp_total_plays));
                    query.Parameters.AddWithValue("@acp_min_cash_in_reward", ManejoNulos.ManageNullDecimal(item.acp_min_cash_in_reward));
                    query.Parameters.AddWithValue("@acp_redeemable_played", ManejoNulos.ManageNullDecimal(item.acp_redeemable_played));
                    query.Parameters.AddWithValue("@acp_awarded_promotion_discounted", ManejoNulos.ManegeNullBool(item.acp_awarded_promotion_discounted));
                    query.Parameters.AddWithValue("@acp_converted_amount", ManejoNulos.ManageNullDecimal(item.acp_converted_amount));
                    query.Parameters.AddWithValue("@acp_auto_conversion", ManejoNulos.ManegeNullBool(item.acp_auto_conversion));
                    query.Parameters.AddWithValue("@acp_lock_percentage", ManejoNulos.ManageNullDecimal(item.acp_lock_percentage));
                    query.Parameters.AddWithValue("@acp_include_recharge", ManejoNulos.ManegeNullBool(item.acp_include_recharge));
                    query.Parameters.AddWithValue("@acp_min_cash_in", ManejoNulos.ManageNullDecimal(item.acp_min_cash_in));
                    query.Parameters.AddWithValue("@acp_min_spent", ManejoNulos.ManageNullDecimal(item.acp_min_spent));
                    query.Parameters.AddWithValue("@acp_min_spent_reward", ManejoNulos.ManageNullDecimal(item.acp_min_spent_reward));
                    query.Parameters.AddWithValue("@acp_num_tokens", ManejoNulos.ManageNullInteger(item.acp_num_tokens));
                    query.Parameters.AddWithValue("@acp_num_used_tokens", ManejoNulos.ManageNullInteger(item.acp_num_used_tokens));
                    query.Parameters.AddWithValue("@acp_token_reward", ManejoNulos.ManageNullDecimal(item.acp_token_reward));
                    //IdInsertado = Convert.ToInt32(query.ExecuteScalar());
                    query.ExecuteNonQuery();
                    IdInsertado = item.acp_unique_id;
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
            select top 1 acp_unique_id as lastid from 
            [dbo].[account_promotions]
            order by acp_unique_id desc
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
