using ServicioMigracionWGDB_000.models;
using ServicioMigracionWGDB_000.utilitarios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
namespace ServicioMigracionWGDB_000.dal
{
    public class account_promotions_dal
    {
        private readonly string _conexion = string.Empty;

        public account_promotions_dal()
        {
            _conexion = ConfigurationManager.ConnectionStrings["connection_wgdb_000"].ConnectionString;
        }
        public List<account_promotions> GetAccountPromotionsPaginated(long lastid, int skip, int pageSize)
        {
            var result = new List<account_promotions>();
            try
            {
                string query = $@"
SELECT [acp_unique_id]
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
      ,[acp_token_reward]
  FROM [dbo].[account_promotions]
  where acp_unique_id > {lastid}
  order by acp_unique_id asc
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
                                var item = new account_promotions()
                                {
                                    acp_unique_id = (long)dr["acp_unique_id"],
                                    acp_created = (DateTime)dr["acp_created"],
                                    acp_account_id = (long)dr["acp_account_id"],
                                    acp_account_level = (int)dr["acp_account_level"],
                                    acp_promo_type = (int)dr["acp_promo_type"],
                                    acp_promo_id = (long)dr["acp_promo_id"],
                                    acp_promo_name = dr["acp_promo_name"] == DBNull.Value ? null : (string)dr["acp_promo_name"],
                                    acp_promo_date = (DateTime)dr["acp_promo_date"],
                                    acp_operation_id = (long)dr["acp_operation_id"],
                                    acp_credit_type = (int)dr["acp_credit_type"],
                                    acp_activation = dr["acp_activation"] == DBNull.Value ? null : (DateTime?)dr["acp_activation"],
                                    acp_expiration = dr["acp_expiration"] == DBNull.Value ? null : (DateTime?)dr["acp_expiration"],
                                    acp_cash_in = dr["acp_cash_in"] == DBNull.Value ? null : (decimal?)dr["acp_cash_in"],
                                    acp_points = dr["acp_points"] == DBNull.Value ? null : (decimal?)dr["acp_points"],
                                    acp_ini_balance = (decimal)dr["acp_ini_balance"],
                                    acp_ini_withhold = (decimal)dr["acp_ini_withhold"],
                                    acp_ini_wonlock = dr["acp_ini_wonlock"] == DBNull.Value ? null : (decimal?)dr["acp_ini_wonlock"],
                                    acp_balance = (decimal)dr["acp_balance"],
                                    acp_withhold = (decimal)dr["acp_withhold"],
                                    acp_wonlock = dr["acp_wonlock"] == DBNull.Value ? null : (decimal?)dr["acp_wonlock"],
                                    acp_played = (decimal)dr["acp_played"],
                                    acp_won = (decimal)dr["acp_won"],
                                    acp_status = (int)dr["acp_status"],
                                    acp_updated = (DateTime)dr["acp_updated"],
                                    acp_play_session_id = dr["acp_play_session_id"] == DBNull.Value ? null : (long?)dr["acp_play_session_id"],
                                    acp_transaction_id = dr["acp_transaction_id"] == DBNull.Value ? null : (long?)dr["acp_transaction_id"],
                                    acp_recommended_account_id = dr["acp_recommended_account_id"] == DBNull.Value ? null : (long?)dr["acp_recommended_account_id"],
                                    acp_details = dr["acp_details"] == DBNull.Value ? null : (string)dr["acp_details"],
                                    acp_promo_category_id = (int)dr["acp_promo_category_id"],
                                    acp_redeemable_cost = dr["acp_redeemable_cost"] == DBNull.Value ? null : (decimal?)dr["acp_redeemable_cost"],
                                    acp_prize_type = dr["acp_prize_type"] == DBNull.Value ? null : (int?)dr["acp_prize_type"],
                                    acp_prize_gross = dr["acp_prize_gross"] == DBNull.Value ? null : (decimal?)dr["acp_prize_gross"],
                                    acp_prize_tax1 = dr["acp_prize_tax1"] == DBNull.Value ? null : (decimal?)dr["acp_prize_tax1"],
                                    acp_prize_tax2 = dr["acp_prize_tax2"] == DBNull.Value ? null : (decimal?)dr["acp_prize_tax2"],
                                    acp_prize_tax3 = dr["acp_prize_tax3"] == DBNull.Value ? null : (decimal?)dr["acp_prize_tax3"],
                                    acp_pyramid_prize = dr["acp_pyramid_prize"] == DBNull.Value ? null : (decimal?)dr["acp_pyramid_prize"],
                                    acp_draw_price = dr["acp_draw_price"] == DBNull.Value ? null : (decimal?)dr["acp_draw_price"],
                                    acp_total_prize = dr["acp_total_prize"] == DBNull.Value ? null : (decimal?)dr["acp_total_prize"],
                                    acp_prize_tax4 = dr["acp_prize_tax4"] == DBNull.Value ? null : (decimal?)dr["acp_prize_tax4"],
                                    acp_prize_tax5 = dr["acp_prize_tax5"] == DBNull.Value ? null : (decimal?)dr["acp_prize_tax5"],
                                    acp_promogame_return_price_pct = dr["acp_promogame_return_price_pct"] == DBNull.Value ? null : (decimal?)dr["acp_promogame_return_price_pct"],
                                    acp_ms_id = dr["acp_ms_id"] == DBNull.Value ? null : (long?)dr["acp_ms_id"],
                                    acp_ms_sequence_id = dr["acp_ms_sequence_id"] == DBNull.Value ? null : (long?)dr["acp_ms_sequence_id"],
                                    acp_site_redeemed = dr["acp_site_redeemed"] == DBNull.Value ? null : (int?)dr["acp_site_redeemed"],
                                    acp_import_filename = dr["acp_import_filename"] == DBNull.Value ? null : (string)dr["acp_import_filename"],
                                    acp_ms_promo_id = dr["acp_ms_promo_id"] == DBNull.Value ? null : (long?)dr["acp_ms_promo_id"],
                                    acp_lock_enabled = (int)dr["acp_lock_enabled"],
                                    acp_lock_balance_factor = dr["acp_lock_balance_factor"] == DBNull.Value ? null : (int?)dr["acp_lock_balance_factor"],
                                    acp_lock_balance_amount = dr["acp_lock_balance_amount"] == DBNull.Value ? null : (decimal?)dr["acp_lock_balance_amount"],
                                    acp_lock_coin_in_factor = dr["acp_lock_coin_in_factor"] == DBNull.Value ? null : (int?)dr["acp_lock_coin_in_factor"],
                                    acp_lock_coin_in_amount = dr["acp_lock_coin_in_amount"] == DBNull.Value ? null : (decimal?)dr["acp_lock_coin_in_amount"],
                                    acp_lock_average_bet = dr["acp_lock_average_bet"] == DBNull.Value ? null : (decimal?)dr["acp_lock_average_bet"],
                                    acp_lock_plays = dr["acp_lock_plays"] == DBNull.Value ? null : (int?)dr["acp_lock_plays"],
                                    acp_lock_max_payable_factor_enabled = (int)dr["acp_lock_max_payable_factor_enabled"],
                                    acp_lock_max_payable_factor = dr["acp_lock_max_payable_factor"] == DBNull.Value ? null : (decimal?)dr["acp_lock_max_payable_factor"],
                                    acp_lock_max_payable_amount_enabled = (int)dr["acp_lock_max_payable_amount_enabled"],
                                    acp_lock_max_payable_amount = dr["acp_lock_max_payable_amount"] == DBNull.Value ? null : (decimal?)dr["acp_lock_max_payable_amount"],
                                    acp_lock_min_payable_amount = dr["acp_lock_min_payable_amount"] == DBNull.Value ? null : (decimal?)dr["acp_lock_min_payable_amount"],
                                    acp_cash_in_max_reward = (decimal)dr["acp_cash_in_max_reward"],
                                    acp_weights_id = dr["acp_weights_id"] == DBNull.Value ? null : (int?)dr["acp_weights_id"],
                                    acp_total_played_weighted = (decimal)dr["acp_total_played_weighted"],
                                    acp_total_won_weighted = (decimal)dr["acp_total_won_weighted"],
                                    acp_total_plays = (int)dr["acp_total_plays"],
                                    acp_min_cash_in_reward = dr["acp_min_cash_in_reward"] == DBNull.Value ? null : (decimal?)dr["acp_min_cash_in_reward"],
                                    acp_redeemable_played = dr["acp_redeemable_played"] == DBNull.Value ? null : (decimal?)dr["acp_redeemable_played"],
                                    acp_awarded_promotion_discounted = (bool)dr["acp_awarded_promotion_discounted"],
                                    acp_converted_amount = dr["acp_converted_amount"] == DBNull.Value ? null : (decimal?)dr["acp_converted_amount"],
                                    acp_auto_conversion = (bool)dr["acp_auto_conversion"],
                                    acp_lock_percentage = (decimal)dr["acp_lock_percentage"],
                                    acp_include_recharge = (bool)dr["acp_include_recharge"],
                                    acp_min_cash_in = (decimal)dr["acp_min_cash_in"],
                                    acp_min_spent = dr["acp_min_spent"] == DBNull.Value ? null : (decimal?)dr["acp_min_spent"],
                                    acp_min_spent_reward = dr["acp_min_spent_reward"] == DBNull.Value ? null : (decimal?)dr["acp_min_spent_reward"],
                                    acp_num_tokens = dr["acp_num_tokens"] == DBNull.Value ? null : (int?)dr["acp_num_tokens"],
                                    acp_num_used_tokens = dr["acp_num_used_tokens"] == DBNull.Value ? null : (int?)dr["acp_num_used_tokens"],
                                    acp_token_reward = dr["acp_token_reward"] == DBNull.Value ? null : (decimal?)dr["acp_token_reward"],
                                };
                                result.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = new List<account_promotions>();
            }
            return result;
        }
        public int GetTotalAccountPromotionsForMigration(long lastid)
        {
            int total = 0;

            string query = @"
            select count(*) as total from 
            [dbo].[[account_promotions]]
where [acp_unique_id] > @lastid
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
        public int SaveAccountPromotions(account_promotions item)
        {
            //bool respuesta = false;
            int IdInsertado = 0;
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
output inserted.acp_unique_id
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
                using (var con = new SqlConnection(_conexion))
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
