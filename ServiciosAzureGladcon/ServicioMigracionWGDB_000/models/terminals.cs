using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioMigracionWGDB_000.models
{
    public class terminals
    {
        public int te_terminal_id { get; set; }
        public int te_type { get; set; }
        public int? te_server_id { get; set; }
        public string te_external_id { get; set; }
        public bool te_blocked { get; set; }
        public long te_timestamp { get; set; }
        public bool te_active { get; set; }
        public string te_provider_id { get; set; }
        public short? te_client_id { get; set; }
        public short? te_build_id { get; set; }
        public short te_terminal_type { get; set; }
        public string te_vendor_id { get; set; }
        public string te_unique_external_id { get; set; }
        public int te_status { get; set; }
        public DateTime? te_retirement_date { get; set; }
        public DateTime? te_retirement_requested { get; set; }
        public decimal? te_denomination { get; set; }
        public string te_multi_denomination { get; set; }
        public string te_program { get; set; }
        public decimal? te_theoretical_payout { get; set; }
        public decimal? te_theoretical_hold { get; set; }
        public int? te_prov_id { get; set; }
        public int te_bank_id { get; set; }
        public string te_floor_id { get; set; }
        public int te_game_type { get; set; }
        public DateTime te_activation_date { get; set; }
        public long? te_current_account_id { get; set; }
        public long? te_current_play_session_id { get; set; }
        public string te_registration_code { get; set; }
        public int te_sas_flags { get; set; }
        public string te_serial_number { get; set; }
        public string te_cabinet_type { get; set; }
        public decimal? te_jackpot_contribution_pct { get; set; }
        public int te_contract_type { get; set; }
        public string te_contract_id { get; set; }
        public string te_order_number { get; set; }
        public bool te_wxp_reported { get; set; }
        public DateTime te_wxp_reported_status_datetime { get; set; }
        public long te_wxp_reported_status { get; set; }
        public long? te_sequence_id { get; set; }
        public int? te_validation_type { get; set; }
        public bool? te_allowed_cashable_emission { get; set; }
        public bool? te_allowed_promo_emission { get; set; }
        public bool? te_allowed_redemption { get; set; }
        public decimal? te_max_allowed_ti { get; set; }
        public decimal? te_max_allowed_to { get; set; }
        public string te_sas_version { get; set; }
        public string te_sas_machine_name { get; set; }
        public int? te_bonus_flags { get; set; }
        public int? te_features_bytes { get; set; }
        public long? te_virtual_account_id { get; set; }
        public int te_sas_flags_use_site_default { get; set; }
        public int? te_authentication_method { get; set; }
        public string te_authentication_seed { get; set; }
        public string te_authentication_signature { get; set; }
        public int? te_authentication_status { get; set; }
        public DateTime? te_authentication_last_checked { get; set; }
        public string te_machine_id { get; set; }
        public int? te_position { get; set; }
        public decimal? te_top_award { get; set; }
        public decimal? te_max_bet { get; set; }
        public string te_number_lines { get; set; }
        public string te_game_theme { get; set; }
        public long? te_account_promotion_id { get; set; }
        public long? te_machine_asset_number { get; set; }
        public long? te_asset_number { get; set; }
        public string te_machine_serial_number { get; set; }
        public long te_meter_delta_id { get; set; }
        public int te_master_id { get; set; }
        public int te_change_id { get; set; }
        public string te_base_name { get; set; }
        public string te_name { get; set; }
        public int te_transfer_status { get; set; }
        public int? te_smib2egm_comm_type { get; set; }
        public long? te_smib2egm_conf_id { get; set; }
        public int te_last_game_played_id { get; set; }
        public string te_brand_code { get; set; }
        public string te_model { get; set; }
        public int? te_manufacture_year { get; set; }
        public bool? te_met_homologated { get; set; }
        public string te_bet_code { get; set; }
        public bool te_coin_collection { get; set; }
        public int? te_terminal_currency_id { get; set; }
        public string te_iso_code { get; set; }
        public decimal? te_equity_percentage { get; set; }
        public decimal? te_sas_accounting_denom { get; set; }
        public int te_tito_host_id { get; set; }
        public decimal? te_min_allowed_ti { get; set; }
        public bool? te_allow_truncate { get; set; }
        public decimal? te_min_denomination { get; set; }
        public bool? te_chk_equity_percentage { get; set; }
        public long? te_reserve_account_id { get; set; }
        public DateTime? te_reservation_expires { get; set; }
        public int? te_multiseat_id { get; set; }
        public int? te_manufacture_month { get; set; }
        public int? te_manufacture_day { get; set; }
        public DateTime te_creation_date { get; set; }
        public DateTime? te_replacement_date { get; set; }
        public decimal? te_equity_percentage_2 { get; set; }
        public decimal? te_equity_fixed_amount { get; set; }
        public int te_sas_flags2 { get; set; }
        public int te_sas_flags2_use_site_default { get; set; }
        public string te_cashier_draw_device_ip { get; set; }
        public int? te_ven_id { get; set; }
        public long? te_last_play_session_id { get; set; }
        public string te_sas_version_edit { get; set; }
        public bool? te_sas_aft_enabled_edit { get; set; }
        public bool? te_collect_tax { get; set; }
        public long? te_current_draw_session_id { get; set; }
        public Guid? te_model_id { get; set; }
        public int? te_terminal_settings_id { get; set; }
        public short? TE_COLLECTION_TYPE { get; set; }
        public string te_external_request_id { get; set; }
        public bool? TE_ONLY_REDEEMABLE { get; set; }
    }
}
