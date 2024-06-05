using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioServidorVPN.WGDB_000.model
{
    public class account_promotions
    {
        public long acp_unique_id { get; set; }
        public DateTime acp_created { get; set; }
        public long acp_account_id { get; set; }
        public int acp_account_level { get; set; }
        public int acp_promo_type { get; set; }
        public long acp_promo_id { get; set; }
        public string acp_promo_name { get; set; }
        public DateTime acp_promo_date { get; set; }
        public long acp_operation_id { get; set; }
        public int acp_credit_type { get; set; }
        public DateTime? acp_activation { get; set; }
        public DateTime? acp_expiration { get; set; }
        public decimal? acp_cash_in { get; set; }
        public decimal? acp_points { get; set; }
        public decimal acp_ini_balance { get; set; }
        public decimal acp_ini_withhold { get; set; }
        public decimal? acp_ini_wonlock { get; set; }
        public decimal acp_balance { get; set; }
        public decimal acp_withhold { get; set; }
        public decimal? acp_wonlock { get; set; }
        public decimal acp_played { get; set; }
        public decimal acp_won { get; set; }
        public int acp_status { get; set; }
        public DateTime acp_updated { get; set; }
        public long? acp_play_session_id { get; set; }
        public long? acp_transaction_id { get; set; }
        public long? acp_recommended_account_id { get; set; }
        public string acp_details { get; set; }
        public int acp_promo_category_id { get; set; }
        public decimal? acp_redeemable_cost { get; set; }
        public int? acp_prize_type { get; set; }
        public decimal? acp_prize_gross { get; set; }
        public decimal? acp_prize_tax1 { get; set; }
        public decimal? acp_prize_tax2 { get; set; }
        public decimal? acp_prize_tax3 { get; set; }
        public decimal? acp_pyramid_prize { get; set; }
        public decimal? acp_draw_price { get; set; }
        public decimal? acp_total_prize { get; set; }
        public decimal? acp_prize_tax4 { get; set; }
        public decimal? acp_prize_tax5 { get; set; }
        public decimal? acp_promogame_return_price_pct { get; set; }
        public long? acp_ms_id { get; set; }
        public long? acp_ms_sequence_id { get; set; }
        public int? acp_site_redeemed { get; set; }
        public string acp_import_filename { get; set; }
        public long? acp_ms_promo_id { get; set; }
        public int acp_lock_enabled { get; set; }
        public int? acp_lock_balance_factor { get; set; }
        public decimal? acp_lock_balance_amount { get; set; }
        public int? acp_lock_coin_in_factor { get; set; }
        public decimal? acp_lock_coin_in_amount { get; set; }
        public decimal? acp_lock_average_bet { get; set; }
        public int? acp_lock_plays { get; set; }
        public int acp_lock_max_payable_factor_enabled { get; set; }
        public decimal? acp_lock_max_payable_factor { get; set; }
        public int acp_lock_max_payable_amount_enabled { get; set; }
        public decimal? acp_lock_max_payable_amount { get; set; }
        public decimal? acp_lock_min_payable_amount { get; set; }
        public decimal acp_cash_in_max_reward { get; set; }
        public int? acp_weights_id { get; set; }
        public decimal acp_total_played_weighted { get; set; }
        public decimal acp_total_won_weighted { get; set; }
        public int acp_total_plays { get; set; }
        public decimal? acp_min_cash_in_reward { get; set; }
        public decimal? acp_redeemable_played { get; set; }
        public bool acp_awarded_promotion_discounted { get; set; }
        public decimal? acp_converted_amount { get; set; }
        public bool acp_auto_conversion { get; set; }
        public decimal acp_lock_percentage { get; set; }
        public bool acp_include_recharge { get; set; }
        public decimal acp_min_cash_in { get; set; }
        public decimal? acp_min_spent { get; set; }
        public decimal? acp_min_spent_reward { get; set; }
        public int? acp_num_tokens { get; set; }
        public int? acp_num_used_tokens { get; set; }
        public decimal? acp_token_reward { get; set; }

    }
}
