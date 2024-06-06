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
    public class terminals_dal
    {
        private readonly string bd_username = string.Empty;
        private readonly string bd_password = string.Empty;
        private readonly string bd_datasource = string.Empty;
        private readonly string connectionString = string.Empty;
        public terminals_dal(string database_name)
        {
            bd_username = ConfigurationManager.AppSettings["bd_username"];
            bd_password = ConfigurationManager.AppSettings["bd_password"];
            bd_datasource = ConfigurationManager.AppSettings["bd_datasource"];
            connectionString = $"Data Source={bd_datasource};Initial Catalog={database_name};Integrated Security=False;User ID={bd_username};Password={bd_password}";

        }
        public bool SaveTerminals(terminals item)
        {
            //bool respuesta = false;
            int IdInsertado = 0;
            string consulta = @"
if not exists (select te_terminal_id from [dbo].[terminals] where te_terminal_id = @te_terminal_id)
begin
INSERT INTO [dbo].[terminals]
           ([te_terminal_id]
           ,[te_type]
           ,[te_server_id]
           ,[te_external_id]
           ,[te_blocked]
           ,[te_active]
           ,[te_provider_id]
           ,[te_client_id]
           ,[te_build_id]
           ,[te_terminal_type]
           ,[te_vendor_id]
           ,[te_status]
           ,[te_retirement_date]
           ,[te_retirement_requested]
           ,[te_denomination]
           ,[te_multi_denomination]
           ,[te_program]
           ,[te_theoretical_payout]
           ,[te_prov_id]
           ,[te_bank_id]
           ,[te_floor_id]
           ,[te_game_type]
           ,[te_activation_date]
           ,[te_current_account_id]
           ,[te_current_play_session_id]
           ,[te_registration_code]
           ,[te_sas_flags]
           ,[te_serial_number]
           ,[te_cabinet_type]
           ,[te_jackpot_contribution_pct]
           ,[te_contract_type]
           ,[te_contract_id]
           ,[te_order_number]
           ,[te_wxp_reported]
           ,[te_wxp_reported_status_datetime]
           ,[te_wxp_reported_status]
           ,[te_sequence_id]
           ,[te_validation_type]
           ,[te_allowed_cashable_emission]
           ,[te_allowed_promo_emission]
           ,[te_allowed_redemption]
           ,[te_max_allowed_ti]
           ,[te_max_allowed_to]
           ,[te_sas_version]
           ,[te_sas_machine_name]
           ,[te_bonus_flags]
           ,[te_features_bytes]
           ,[te_virtual_account_id]
           ,[te_sas_flags_use_site_default]
           ,[te_authentication_method]
           ,[te_authentication_seed]
           ,[te_authentication_signature]
           ,[te_authentication_status]
           ,[te_authentication_last_checked]
           ,[te_machine_id]
           ,[te_position]
           ,[te_top_award]
           ,[te_max_bet]
           ,[te_number_lines]
           ,[te_game_theme]
           ,[te_account_promotion_id]
           ,[te_machine_asset_number]
           ,[te_asset_number]
           ,[te_machine_serial_number]
           ,[te_meter_delta_id]
           ,[te_master_id]
           ,[te_change_id]
           ,[te_base_name]
           ,[te_transfer_status]
           ,[te_smib2egm_comm_type]
           ,[te_smib2egm_conf_id]
           ,[te_last_game_played_id]
           ,[te_brand_code]
           ,[te_model]
           ,[te_manufacture_year]
           ,[te_met_homologated]
           ,[te_bet_code]
           ,[te_coin_collection]
           ,[te_terminal_currency_id]
           ,[te_iso_code]
           ,[te_equity_percentage]
           ,[te_sas_accounting_denom]
           ,[te_tito_host_id]
           ,[te_min_allowed_ti]
           ,[te_allow_truncate]
           ,[te_min_denomination]
           ,[te_chk_equity_percentage]
           ,[te_reserve_account_id]
           ,[te_reservation_expires]
           ,[te_multiseat_id]
           ,[te_manufacture_month]
           ,[te_manufacture_day]
           ,[te_creation_date]
           ,[te_replacement_date]
           ,[te_equity_percentage_2]
           ,[te_equity_fixed_amount]
           ,[te_sas_flags2]
           ,[te_sas_flags2_use_site_default]
           ,[te_cashier_draw_device_ip]
           ,[te_ven_id]
           ,[te_last_play_session_id]
           ,[te_sas_version_edit]
           ,[te_sas_aft_enabled_edit]
           ,[te_collect_tax]
           ,[te_current_draw_session_id]
           ,[te_model_id]
           ,[te_terminal_settings_id]
           ,[TE_COLLECTION_TYPE]
           ,[te_external_request_id]
           ,[TE_ONLY_REDEEMABLE])
--output inserted.te_terminal_id
     VALUES
           (@te_terminal_id
           ,@te_type
           ,@te_server_id
           ,@te_external_id
           ,@te_blocked
           ,@te_active
           ,@te_provider_id
           ,@te_client_id
           ,@te_build_id
           ,@te_terminal_type
           ,@te_vendor_id
           ,@te_status
           ,@te_retirement_date
           ,@te_retirement_requested
           ,@te_denomination
           ,@te_multi_denomination
           ,@te_program
           ,@te_theoretical_payout
           ,@te_prov_id
           ,@te_bank_id
           ,@te_floor_id
           ,@te_game_type
           ,@te_activation_date
           ,@te_current_account_id
           ,@te_current_play_session_id
           ,@te_registration_code
           ,@te_sas_flags
           ,@te_serial_number
           ,@te_cabinet_type
           ,@te_jackpot_contribution_pct
           ,@te_contract_type
           ,@te_contract_id
           ,@te_order_number
           ,@te_wxp_reported
           ,@te_wxp_reported_status_datetime
           ,@te_wxp_reported_status
           ,@te_sequence_id
           ,@te_validation_type
           ,@te_allowed_cashable_emission
           ,@te_allowed_promo_emission
           ,@te_allowed_redemption
           ,@te_max_allowed_ti
           ,@te_max_allowed_to
           ,@te_sas_version
           ,@te_sas_machine_name
           ,@te_bonus_flags
           ,@te_features_bytes
           ,@te_virtual_account_id
           ,@te_sas_flags_use_site_default
           ,@te_authentication_method
           ,@te_authentication_seed
           ,@te_authentication_signature
           ,@te_authentication_status
           ,@te_authentication_last_checked
           ,@te_machine_id
           ,@te_position
           ,@te_top_award
           ,@te_max_bet
           ,@te_number_lines
           ,@te_game_theme
           ,@te_account_promotion_id
           ,@te_machine_asset_number
           ,@te_asset_number
           ,@te_machine_serial_number
           ,@te_meter_delta_id
           ,@te_master_id
           ,@te_change_id
           ,@te_base_name
           ,@te_transfer_status
           ,@te_smib2egm_comm_type
           ,@te_smib2egm_conf_id
           ,@te_last_game_played_id
           ,@te_brand_code
           ,@te_model
           ,@te_manufacture_year
           ,@te_met_homologated
           ,@te_bet_code
           ,@te_coin_collection
           ,@te_terminal_currency_id
           ,@te_iso_code
           ,@te_equity_percentage
           ,@te_sas_accounting_denom
           ,@te_tito_host_id
           ,@te_min_allowed_ti
           ,@te_allow_truncate
           ,@te_min_denomination
           ,@te_chk_equity_percentage
           ,@te_reserve_account_id
           ,@te_reservation_expires
           ,@te_multiseat_id
           ,@te_manufacture_month
           ,@te_manufacture_day
           ,@te_creation_date
           ,@te_replacement_date
           ,@te_equity_percentage_2
           ,@te_equity_fixed_amount
           ,@te_sas_flags2
           ,@te_sas_flags2_use_site_default
           ,@te_cashier_draw_device_ip
           ,@te_ven_id
           ,@te_last_play_session_id
           ,@te_sas_version_edit
           ,@te_sas_aft_enabled_edit
           ,@te_collect_tax
           ,@te_current_draw_session_id
           ,@te_model_id
           ,@te_terminal_settings_id
           ,@te_COLLECTION_TYPE
           ,@te_external_request_id
           ,@te_ONLY_REDEEMABLE)
end

                      ";
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@te_terminal_id", item.te_terminal_id == null ? DBNull.Value : (object)item.te_terminal_id);
                    query.Parameters.AddWithValue("@te_type", item.te_type == null ? DBNull.Value : (object)item.te_type);
                    query.Parameters.AddWithValue("@te_server_id", item.te_server_id == null ? DBNull.Value : (object)item.te_server_id);
                    query.Parameters.AddWithValue("@te_external_id", item.te_external_id == null ? DBNull.Value : (object)item.te_external_id);
                    query.Parameters.AddWithValue("@te_blocked", item.te_blocked == null ? DBNull.Value : (object)item.te_blocked);
                    query.Parameters.AddWithValue("@te_active", item.te_active == null ? DBNull.Value : (object)item.te_active);
                    query.Parameters.AddWithValue("@te_provider_id", item.te_provider_id == null ? DBNull.Value : (object)item.te_provider_id);
                    query.Parameters.AddWithValue("@te_client_id", item.te_client_id == null ? DBNull.Value : (object)item.te_client_id);
                    query.Parameters.AddWithValue("@te_build_id", item.te_build_id == null ? DBNull.Value : (object)item.te_build_id);
                    query.Parameters.AddWithValue("@te_terminal_type", item.te_terminal_type == null ? DBNull.Value : (object)item.te_terminal_type);
                    query.Parameters.AddWithValue("@te_vendor_id", item.te_vendor_id == null ? DBNull.Value : (object)item.te_vendor_id);
                    query.Parameters.AddWithValue("@te_unique_external_id", item.te_unique_external_id == null ? DBNull.Value : (object)item.te_unique_external_id);
                    query.Parameters.AddWithValue("@te_status", item.te_status == null ? DBNull.Value : (object)item.te_status);
                    query.Parameters.AddWithValue("@te_retirement_date", item.te_retirement_date == null ? DBNull.Value : (object)item.te_retirement_date);
                    query.Parameters.AddWithValue("@te_retirement_requested", item.te_retirement_requested == null ? DBNull.Value : (object)item.te_retirement_requested);
                    query.Parameters.AddWithValue("@te_denomination", item.te_denomination == null ? DBNull.Value : (object)item.te_denomination);
                    query.Parameters.AddWithValue("@te_multi_denomination", item.te_multi_denomination == null ? DBNull.Value : (object)item.te_multi_denomination);
                    query.Parameters.AddWithValue("@te_program", item.te_program == null ? DBNull.Value : (object)item.te_program);
                    query.Parameters.AddWithValue("@te_theoretical_payout", item.te_theoretical_payout == null ? DBNull.Value : (object)item.te_theoretical_payout);
                    query.Parameters.AddWithValue("@te_theoretical_hold", item.te_theoretical_hold == null ? DBNull.Value : (object)item.te_theoretical_hold);
                    query.Parameters.AddWithValue("@te_prov_id", item.te_prov_id == null ? DBNull.Value : (object)item.te_prov_id);
                    query.Parameters.AddWithValue("@te_bank_id", item.te_bank_id == null ? DBNull.Value : (object)item.te_bank_id);
                    query.Parameters.AddWithValue("@te_floor_id", item.te_floor_id == null ? DBNull.Value : (object)item.te_floor_id);
                    query.Parameters.AddWithValue("@te_game_type", item.te_game_type == null ? DBNull.Value : (object)item.te_game_type);
                    query.Parameters.AddWithValue("@te_activation_date", item.te_activation_date == null ? DBNull.Value : (object)item.te_activation_date);
                    query.Parameters.AddWithValue("@te_current_account_id", item.te_current_account_id == null ? DBNull.Value : (object)item.te_current_account_id);
                    query.Parameters.AddWithValue("@te_current_play_session_id", item.te_current_play_session_id == null ? DBNull.Value : (object)item.te_current_play_session_id);
                    query.Parameters.AddWithValue("@te_registration_code", item.te_registration_code == null ? DBNull.Value : (object)item.te_registration_code);
                    query.Parameters.AddWithValue("@te_sas_flags", item.te_sas_flags == null ? DBNull.Value : (object)item.te_sas_flags);
                    query.Parameters.AddWithValue("@te_serial_number", item.te_serial_number == null ? DBNull.Value : (object)item.te_serial_number);
                    query.Parameters.AddWithValue("@te_cabinet_type", item.te_cabinet_type == null ? DBNull.Value : (object)item.te_cabinet_type);
                    query.Parameters.AddWithValue("@te_jackpot_contribution_pct", item.te_jackpot_contribution_pct == null ? DBNull.Value : (object)item.te_jackpot_contribution_pct);
                    query.Parameters.AddWithValue("@te_contract_type", item.te_contract_type == null ? DBNull.Value : (object)item.te_contract_type);
                    query.Parameters.AddWithValue("@te_contract_id", item.te_contract_id == null ? DBNull.Value : (object)item.te_contract_id);
                    query.Parameters.AddWithValue("@te_order_number", item.te_order_number == null ? DBNull.Value : (object)item.te_order_number);
                    query.Parameters.AddWithValue("@te_wxp_reported", item.te_wxp_reported == null ? DBNull.Value : (object)item.te_wxp_reported);
                    query.Parameters.AddWithValue("@te_wxp_reported_status_datetime", item.te_wxp_reported_status_datetime == null ? DBNull.Value : (object)item.te_wxp_reported_status_datetime);
                    query.Parameters.AddWithValue("@te_wxp_reported_status", item.te_wxp_reported_status == null ? DBNull.Value : (object)item.te_wxp_reported_status);
                    query.Parameters.AddWithValue("@te_sequence_id", item.te_sequence_id == null ? DBNull.Value : (object)item.te_sequence_id);
                    query.Parameters.AddWithValue("@te_validation_type", item.te_validation_type == null ? DBNull.Value : (object)item.te_validation_type);
                    query.Parameters.AddWithValue("@te_allowed_cashable_emission", item.te_allowed_cashable_emission == null ? DBNull.Value : (object)item.te_allowed_cashable_emission);
                    query.Parameters.AddWithValue("@te_allowed_promo_emission", item.te_allowed_promo_emission == null ? DBNull.Value : (object)item.te_allowed_promo_emission);
                    query.Parameters.AddWithValue("@te_allowed_redemption", item.te_allowed_redemption == null ? DBNull.Value : (object)item.te_allowed_redemption);
                    query.Parameters.AddWithValue("@te_max_allowed_ti", item.te_max_allowed_ti == null ? DBNull.Value : (object)item.te_max_allowed_ti);
                    query.Parameters.AddWithValue("@te_max_allowed_to", item.te_max_allowed_to == null ? DBNull.Value : (object)item.te_max_allowed_to);
                    query.Parameters.AddWithValue("@te_sas_version", item.te_sas_version == null ? DBNull.Value : (object)item.te_sas_version);
                    query.Parameters.AddWithValue("@te_sas_machine_name", item.te_sas_machine_name == null ? DBNull.Value : (object)item.te_sas_machine_name);
                    query.Parameters.AddWithValue("@te_bonus_flags", item.te_bonus_flags == null ? DBNull.Value : (object)item.te_bonus_flags);
                    query.Parameters.AddWithValue("@te_features_bytes", item.te_features_bytes == null ? DBNull.Value : (object)item.te_features_bytes);
                    query.Parameters.AddWithValue("@te_virtual_account_id", item.te_virtual_account_id == null ? DBNull.Value : (object)item.te_virtual_account_id);
                    query.Parameters.AddWithValue("@te_sas_flags_use_site_default", item.te_sas_flags_use_site_default == null ? DBNull.Value : (object)item.te_sas_flags_use_site_default);
                    query.Parameters.AddWithValue("@te_authentication_method", item.te_authentication_method == null ? DBNull.Value : (object)item.te_authentication_method);
                    query.Parameters.AddWithValue("@te_authentication_seed", item.te_authentication_seed == null ? DBNull.Value : (object)item.te_authentication_seed);
                    query.Parameters.AddWithValue("@te_authentication_signature", item.te_authentication_signature == null ? DBNull.Value : (object)item.te_authentication_signature);
                    query.Parameters.AddWithValue("@te_authentication_status", item.te_authentication_status == null ? DBNull.Value : (object)item.te_authentication_status);
                    query.Parameters.AddWithValue("@te_authentication_last_checked", item.te_authentication_last_checked == null ? DBNull.Value : (object)item.te_authentication_last_checked);
                    query.Parameters.AddWithValue("@te_machine_id", item.te_machine_id == null ? DBNull.Value : (object)item.te_machine_id);
                    query.Parameters.AddWithValue("@te_position", item.te_position == null ? DBNull.Value : (object)item.te_position);
                    query.Parameters.AddWithValue("@te_top_award", item.te_top_award == null ? DBNull.Value : (object)item.te_top_award);
                    query.Parameters.AddWithValue("@te_max_bet", item.te_max_bet == null ? DBNull.Value : (object)item.te_max_bet);
                    query.Parameters.AddWithValue("@te_number_lines", item.te_number_lines == null ? DBNull.Value : (object)item.te_number_lines);
                    query.Parameters.AddWithValue("@te_game_theme", item.te_game_theme == null ? DBNull.Value : (object)item.te_game_theme);
                    query.Parameters.AddWithValue("@te_account_promotion_id", item.te_account_promotion_id == null ? DBNull.Value : (object)item.te_account_promotion_id);
                    query.Parameters.AddWithValue("@te_machine_asset_number", item.te_machine_asset_number == null ? DBNull.Value : (object)item.te_machine_asset_number);
                    query.Parameters.AddWithValue("@te_asset_number", item.te_asset_number == null ? DBNull.Value : (object)item.te_asset_number);
                    query.Parameters.AddWithValue("@te_machine_serial_number", item.te_machine_serial_number == null ? DBNull.Value : (object)item.te_machine_serial_number);
                    query.Parameters.AddWithValue("@te_meter_delta_id", item.te_meter_delta_id == null ? DBNull.Value : (object)item.te_meter_delta_id);
                    query.Parameters.AddWithValue("@te_master_id", item.te_master_id == null ? DBNull.Value : (object)item.te_master_id);
                    query.Parameters.AddWithValue("@te_change_id", item.te_change_id == null ? DBNull.Value : (object)item.te_change_id);
                    query.Parameters.AddWithValue("@te_base_name", item.te_base_name == null ? DBNull.Value : (object)item.te_base_name);
                    query.Parameters.AddWithValue("@te_name", item.te_name == null ? DBNull.Value : (object)item.te_name);
                    query.Parameters.AddWithValue("@te_transfer_status", item.te_transfer_status == null ? DBNull.Value : (object)item.te_transfer_status);
                    query.Parameters.AddWithValue("@te_smib2egm_comm_type", item.te_smib2egm_comm_type == null ? DBNull.Value : (object)item.te_smib2egm_comm_type);
                    query.Parameters.AddWithValue("@te_smib2egm_conf_id", item.te_smib2egm_conf_id == null ? DBNull.Value : (object)item.te_smib2egm_conf_id);
                    query.Parameters.AddWithValue("@te_last_game_played_id", item.te_last_game_played_id == null ? DBNull.Value : (object)item.te_last_game_played_id);
                    query.Parameters.AddWithValue("@te_brand_code", item.te_brand_code == null ? DBNull.Value : (object)item.te_brand_code);
                    query.Parameters.AddWithValue("@te_model", item.te_model == null ? DBNull.Value : (object)item.te_model);
                    query.Parameters.AddWithValue("@te_manufacture_year", item.te_manufacture_year == null ? DBNull.Value : (object)item.te_manufacture_year);
                    query.Parameters.AddWithValue("@te_met_homologated", item.te_met_homologated == null ? DBNull.Value : (object)item.te_met_homologated);
                    query.Parameters.AddWithValue("@te_bet_code", item.te_bet_code == null ? DBNull.Value : (object)item.te_bet_code);
                    query.Parameters.AddWithValue("@te_coin_collection", item.te_coin_collection == null ? DBNull.Value : (object)item.te_coin_collection);
                    query.Parameters.AddWithValue("@te_terminal_currency_id", item.te_terminal_currency_id == null ? DBNull.Value : (object)item.te_terminal_currency_id);
                    query.Parameters.AddWithValue("@te_iso_code", item.te_iso_code == null ? DBNull.Value : (object)item.te_iso_code);
                    query.Parameters.AddWithValue("@te_equity_percentage", item.te_equity_percentage == null ? DBNull.Value : (object)item.te_equity_percentage);
                    query.Parameters.AddWithValue("@te_sas_accounting_denom", item.te_sas_accounting_denom == null ? DBNull.Value : (object)item.te_sas_accounting_denom);
                    query.Parameters.AddWithValue("@te_tito_host_id", item.te_tito_host_id == null ? DBNull.Value : (object)item.te_tito_host_id);
                    query.Parameters.AddWithValue("@te_min_allowed_ti", item.te_min_allowed_ti == null ? DBNull.Value : (object)item.te_min_allowed_ti);
                    query.Parameters.AddWithValue("@te_allow_truncate", item.te_allow_truncate == null ? DBNull.Value : (object)item.te_allow_truncate);
                    query.Parameters.AddWithValue("@te_min_denomination", item.te_min_denomination == null ? DBNull.Value : (object)item.te_min_denomination);
                    query.Parameters.AddWithValue("@te_chk_equity_percentage", item.te_chk_equity_percentage == null ? DBNull.Value : (object)item.te_chk_equity_percentage);
                    query.Parameters.AddWithValue("@te_reserve_account_id", item.te_reserve_account_id == null ? DBNull.Value : (object)item.te_reserve_account_id);
                    query.Parameters.AddWithValue("@te_reservation_expires", item.te_reservation_expires == null ? DBNull.Value : (object)item.te_reservation_expires);
                    query.Parameters.AddWithValue("@te_multiseat_id", item.te_multiseat_id == null ? DBNull.Value : (object)item.te_multiseat_id);
                    query.Parameters.AddWithValue("@te_manufacture_month", item.te_manufacture_month == null ? DBNull.Value : (object)item.te_manufacture_month);
                    query.Parameters.AddWithValue("@te_manufacture_day", item.te_manufacture_day == null ? DBNull.Value : (object)item.te_manufacture_day);
                    query.Parameters.AddWithValue("@te_creation_date", item.te_creation_date == null ? DBNull.Value : (object)item.te_creation_date);
                    query.Parameters.AddWithValue("@te_replacement_date", item.te_replacement_date == null ? DBNull.Value : (object)item.te_replacement_date);
                    query.Parameters.AddWithValue("@te_equity_percentage_2", item.te_equity_percentage_2 == null ? DBNull.Value : (object)item.te_equity_percentage_2);
                    query.Parameters.AddWithValue("@te_equity_fixed_amount", item.te_equity_fixed_amount == null ? DBNull.Value : (object)item.te_equity_fixed_amount);
                    query.Parameters.AddWithValue("@te_sas_flags2", item.te_sas_flags2 == null ? DBNull.Value : (object)item.te_sas_flags2);
                    query.Parameters.AddWithValue("@te_sas_flags2_use_site_default", item.te_sas_flags2_use_site_default == null ? DBNull.Value : (object)item.te_sas_flags2_use_site_default);
                    query.Parameters.AddWithValue("@te_cashier_draw_device_ip", item.te_cashier_draw_device_ip == null ? DBNull.Value : (object)item.te_cashier_draw_device_ip);
                    query.Parameters.AddWithValue("@te_ven_id", item.te_ven_id == null ? DBNull.Value : (object)item.te_ven_id);
                    query.Parameters.AddWithValue("@te_last_play_session_id", item.te_last_play_session_id == null ? DBNull.Value : (object)item.te_last_play_session_id);
                    query.Parameters.AddWithValue("@te_sas_version_edit", item.te_sas_version_edit == null ? DBNull.Value : (object)item.te_sas_version_edit);
                    query.Parameters.AddWithValue("@te_sas_aft_enabled_edit", item.te_sas_aft_enabled_edit == null ? DBNull.Value : (object)item.te_sas_aft_enabled_edit);
                    query.Parameters.AddWithValue("@te_collect_tax", item.te_collect_tax == null ? DBNull.Value : (object)item.te_collect_tax);
                    query.Parameters.AddWithValue("@te_current_draw_session_id", item.te_current_draw_session_id == null ? DBNull.Value : (object)item.te_current_draw_session_id);
                    query.Parameters.AddWithValue("@te_model_id", item.te_model_id == null ? DBNull.Value : (object)item.te_model_id);
                    query.Parameters.AddWithValue("@te_terminal_settings_id", item.te_terminal_settings_id == null ? DBNull.Value : (object)item.te_terminal_settings_id);
                    query.Parameters.AddWithValue("@TE_COLLECTION_TYPE", item.TE_COLLECTION_TYPE == null ? DBNull.Value : (object)item.TE_COLLECTION_TYPE);
                    query.Parameters.AddWithValue("@te_external_request_id", item.te_external_request_id == null ? DBNull.Value : (object)item.te_external_request_id);
                    query.Parameters.AddWithValue("@TE_ONLY_REDEEMABLE", item.TE_ONLY_REDEEMABLE == null ? DBNull.Value : (object)item.TE_ONLY_REDEEMABLE);
                    //IdInsertado = Convert.ToInt32(query.ExecuteScalar());
                    query.ExecuteNonQuery();
                    //IdInsertado = item.te_terminal_id;
                    return true;
                }
            }
            catch (Exception ex)
            {
                funciones.logueo($"Error metodo SaveTerminals - {ex.Message}");
            }
            return false;
        }
        public long GetLastIdInserted()
        {
            long total = 0;

            string query = @"
            select top 1 te_terminal_id as lastid from 
            [dbo].[terminals]
            order by te_terminal_id desc
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
                funciones.logueo($"Error metodo GetLastIdInserted terminals_dal.cs - {ex.Message}", "Error");
                total = 0;
            }

            return total;
        }
    }
}
