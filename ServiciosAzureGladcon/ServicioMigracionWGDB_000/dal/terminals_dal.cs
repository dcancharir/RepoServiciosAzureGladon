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
    public class terminals_dal
    {
        private readonly string _conexion = string.Empty;

        public terminals_dal()
        {
            _conexion = ConfigurationManager.ConnectionStrings["connection_wgdb_000"].ConnectionString;
        }
        public List<terminals> GetTerminalsPaginated(long lastid, int skip, int pageSize)
        {
            var result = new List<terminals>();
            try
            {
                string query = $@"
SELECT [te_terminal_id]
      ,[te_type]
      ,[te_server_id]
      ,[te_external_id]
      ,[te_blocked]
      ,[te_timestamp]
      ,[te_active]
      ,[te_provider_id]
      ,[te_client_id]
      ,[te_build_id]
      ,[te_terminal_type]
      ,[te_vendor_id]
      ,[te_unique_external_id]
      ,[te_status]
      ,[te_retirement_date]
      ,[te_retirement_requested]
      ,[te_denomination]
      ,[te_multi_denomination]
      ,[te_program]
      ,[te_theoretical_payout]
      ,[te_theoretical_hold]
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
      ,[te_name]
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
      ,[TE_ONLY_REDEEMABLE]
  FROM [dbo].[terminals]
  where te_terminal_id > {lastid}
  order by te_terminal_id asc
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
                                var item = new terminals()
                                {
                                    te_terminal_id = (int)dr["te_terminal_id"],
                                    te_type = (int)dr["te_type"],
                                    te_server_id = dr["te_server_id"] == DBNull.Value ? null : (int?)dr["te_server_id"],
                                    te_external_id = dr["te_external_id"] == DBNull.Value ? null : (string)dr["te_external_id"],
                                    te_blocked = (bool)dr["te_blocked"],
                                    te_timestamp = dr["te_timestamp"] == DBNull.Value ? null : (byte[])dr["te_timestamp"],
                                    te_active = (bool)dr["te_active"],
                                    te_provider_id = dr["te_provider_id"] == DBNull.Value ? null : (string)dr["te_provider_id"],
                                    te_client_id = dr["te_client_id"] == DBNull.Value ? null : (short?)dr["te_client_id"],
                                    te_build_id = dr["te_build_id"] == DBNull.Value ? null : (short?)dr["te_build_id"],
                                    te_terminal_type = (short)dr["te_terminal_type"],
                                    te_vendor_id = dr["te_vendor_id"] == DBNull.Value ? null : (string)dr["te_vendor_id"],
                                    te_unique_external_id = dr["te_unique_external_id"] == DBNull.Value ? null : (string)dr["te_unique_external_id"],
                                    te_status = (int)dr["te_status"],
                                    te_retirement_date = dr["te_retirement_date"] == DBNull.Value ? null : (DateTime?)dr["te_retirement_date"],
                                    te_retirement_requested = dr["te_retirement_requested"] == DBNull.Value ? null : (DateTime?)dr["te_retirement_requested"],
                                    te_denomination = dr["te_denomination"] == DBNull.Value ? null : (decimal?)dr["te_denomination"],
                                    te_multi_denomination = dr["te_multi_denomination"] == DBNull.Value ? null : (string)dr["te_multi_denomination"],
                                    te_program = dr["te_program"] == DBNull.Value ? null : (string)dr["te_program"],
                                    te_theoretical_payout = dr["te_theoretical_payout"] == DBNull.Value ? null : (decimal?)dr["te_theoretical_payout"],
                                    te_theoretical_hold = dr["te_theoretical_hold"] == DBNull.Value ? null : (decimal?)dr["te_theoretical_hold"],
                                    te_prov_id = dr["te_prov_id"] == DBNull.Value ? null : (int?)dr["te_prov_id"],
                                    te_bank_id = (int)dr["te_bank_id"],
                                    te_floor_id = dr["te_floor_id"] == DBNull.Value ? null : (string)dr["te_floor_id"],
                                    te_game_type = (int)dr["te_game_type"],
                                    te_activation_date = (DateTime)dr["te_activation_date"],
                                    te_current_account_id = dr["te_current_account_id"] == DBNull.Value ? null : (long?)dr["te_current_account_id"],
                                    te_current_play_session_id = dr["te_current_play_session_id"] == DBNull.Value ? null : (long?)dr["te_current_play_session_id"],
                                    te_registration_code = dr["te_registration_code"] == DBNull.Value ? null : (string)dr["te_registration_code"],
                                    te_sas_flags = (int)dr["te_sas_flags"],
                                    te_serial_number = dr["te_serial_number"] == DBNull.Value ? null : (string)dr["te_serial_number"],
                                    te_cabinet_type = dr["te_cabinet_type"] == DBNull.Value ? null : (string)dr["te_cabinet_type"],
                                    te_jackpot_contribution_pct = dr["te_jackpot_contribution_pct"] == DBNull.Value ? null : (decimal?)dr["te_jackpot_contribution_pct"],
                                    te_contract_type = (int)dr["te_contract_type"],
                                    te_contract_id = dr["te_contract_id"] == DBNull.Value ? null : (string)dr["te_contract_id"],
                                    te_order_number = dr["te_order_number"] == DBNull.Value ? null : (string)dr["te_order_number"],
                                    te_wxp_reported = (bool)dr["te_wxp_reported"],
                                    te_wxp_reported_status_datetime = (DateTime)dr["te_wxp_reported_status_datetime"],
                                    te_wxp_reported_status = (long)dr["te_wxp_reported_status"],
                                    te_sequence_id = dr["te_sequence_id"] == DBNull.Value ? null : (long?)dr["te_sequence_id"],
                                    te_validation_type = dr["te_validation_type"] == DBNull.Value ? null : (int?)dr["te_validation_type"],
                                    te_allowed_cashable_emission = dr["te_allowed_cashable_emission"] == DBNull.Value ? null : (bool?)dr["te_allowed_cashable_emission"],
                                    te_allowed_promo_emission = dr["te_allowed_promo_emission"] == DBNull.Value ? null : (bool?)dr["te_allowed_promo_emission"],
                                    te_allowed_redemption = dr["te_allowed_redemption"] == DBNull.Value ? null : (bool?)dr["te_allowed_redemption"],
                                    te_max_allowed_ti = dr["te_max_allowed_ti"] == DBNull.Value ? null : (decimal?)dr["te_max_allowed_ti"],
                                    te_max_allowed_to = dr["te_max_allowed_to"] == DBNull.Value ? null : (decimal?)dr["te_max_allowed_to"],
                                    te_sas_version = dr["te_sas_version"] == DBNull.Value ? null : (string)dr["te_sas_version"],
                                    te_sas_machine_name = dr["te_sas_machine_name"] == DBNull.Value ? null : (string)dr["te_sas_machine_name"],
                                    te_bonus_flags = dr["te_bonus_flags"] == DBNull.Value ? null : (int?)dr["te_bonus_flags"],
                                    te_features_bytes = dr["te_features_bytes"] == DBNull.Value ? null : (int?)dr["te_features_bytes"],
                                    te_virtual_account_id = dr["te_virtual_account_id"] == DBNull.Value ? null : (long?)dr["te_virtual_account_id"],
                                    te_sas_flags_use_site_default = (int)dr["te_sas_flags_use_site_default"],
                                    te_authentication_method = dr["te_authentication_method"] == DBNull.Value ? null : (int?)dr["te_authentication_method"],
                                    te_authentication_seed = dr["te_authentication_seed"] == DBNull.Value ? null : (string)dr["te_authentication_seed"],
                                    te_authentication_signature = dr["te_authentication_signature"] == DBNull.Value ? null : (string)dr["te_authentication_signature"],
                                    te_authentication_status = dr["te_authentication_status"] == DBNull.Value ? null : (int?)dr["te_authentication_status"],
                                    te_authentication_last_checked = dr["te_authentication_last_checked"] == DBNull.Value ? null : (DateTime?)dr["te_authentication_last_checked"],
                                    te_machine_id = dr["te_machine_id"] == DBNull.Value ? null : (string)dr["te_machine_id"],
                                    te_position = dr["te_position"] == DBNull.Value ? null : (int?)dr["te_position"],
                                    te_top_award = dr["te_top_award"] == DBNull.Value ? null : (decimal?)dr["te_top_award"],
                                    te_max_bet = dr["te_max_bet"] == DBNull.Value ? null : (decimal?)dr["te_max_bet"],
                                    te_number_lines = dr["te_number_lines"] == DBNull.Value ? null : (string)dr["te_number_lines"],
                                    te_game_theme = dr["te_game_theme"] == DBNull.Value ? null : (string)dr["te_game_theme"],
                                    te_account_promotion_id = dr["te_account_promotion_id"] == DBNull.Value ? null : (long?)dr["te_account_promotion_id"],
                                    te_machine_asset_number = dr["te_machine_asset_number"] == DBNull.Value ? null : (long?)dr["te_machine_asset_number"],
                                    te_asset_number = dr["te_asset_number"] == DBNull.Value ? null : (long?)dr["te_asset_number"],
                                    te_machine_serial_number = dr["te_machine_serial_number"] == DBNull.Value ? null : (string)dr["te_machine_serial_number"],
                                    te_meter_delta_id = (long)dr["te_meter_delta_id"],
                                    te_master_id = (int)dr["te_master_id"],
                                    te_change_id = (int)dr["te_change_id"],
                                    te_base_name = dr["te_base_name"] == DBNull.Value ? null : (string)dr["te_base_name"],
                                    te_name = dr["te_name"] == DBNull.Value ? null : (string)dr["te_name"],
                                    te_transfer_status = (int)dr["te_transfer_status"],
                                    te_smib2egm_comm_type = dr["te_smib2egm_comm_type"] == DBNull.Value ? null : (int?)dr["te_smib2egm_comm_type"],
                                    te_smib2egm_conf_id = dr["te_smib2egm_conf_id"] == DBNull.Value ? null : (long?)dr["te_smib2egm_conf_id"],
                                    te_last_game_played_id = (int)dr["te_last_game_played_id"],
                                    te_brand_code = dr["te_brand_code"] == DBNull.Value ? null : (string)dr["te_brand_code"],
                                    te_model = dr["te_model"] == DBNull.Value ? null : (string)dr["te_model"],
                                    te_manufacture_year = dr["te_manufacture_year"] == DBNull.Value ? null : (int?)dr["te_manufacture_year"],
                                    te_met_homologated = dr["te_met_homologated"] == DBNull.Value ? null : (bool?)dr["te_met_homologated"],
                                    te_bet_code = dr["te_bet_code"] == DBNull.Value ? null : (string)dr["te_bet_code"],
                                    te_coin_collection = (bool)dr["te_coin_collection"],
                                    te_terminal_currency_id = dr["te_terminal_currency_id"] == DBNull.Value ? null : (int?)dr["te_terminal_currency_id"],
                                    te_iso_code = dr["te_iso_code"] == DBNull.Value ? null : (string)dr["te_iso_code"],
                                    te_equity_percentage = dr["te_equity_percentage"] == DBNull.Value ? null : (decimal?)dr["te_equity_percentage"],
                                    te_sas_accounting_denom = dr["te_sas_accounting_denom"] == DBNull.Value ? null : (decimal?)dr["te_sas_accounting_denom"],
                                    te_tito_host_id = (int)dr["te_tito_host_id"],
                                    te_min_allowed_ti = dr["te_min_allowed_ti"] == DBNull.Value ? null : (decimal?)dr["te_min_allowed_ti"],
                                    te_allow_truncate = dr["te_allow_truncate"] == DBNull.Value ? null : (bool?)dr["te_allow_truncate"],
                                    te_min_denomination = dr["te_min_denomination"] == DBNull.Value ? null : (decimal?)dr["te_min_denomination"],
                                    te_chk_equity_percentage = dr["te_chk_equity_percentage"] == DBNull.Value ? null : (bool?)dr["te_chk_equity_percentage"],
                                    te_reserve_account_id = dr["te_reserve_account_id"] == DBNull.Value ? null : (long?)dr["te_reserve_account_id"],
                                    te_reservation_expires = dr["te_reservation_expires"] == DBNull.Value ? null : (DateTime?)dr["te_reservation_expires"],
                                    te_multiseat_id = dr["te_multiseat_id"] == DBNull.Value ? null : (int?)dr["te_multiseat_id"],
                                    te_manufacture_month = dr["te_manufacture_month"] == DBNull.Value ? null : (int?)dr["te_manufacture_month"],
                                    te_manufacture_day = dr["te_manufacture_day"] == DBNull.Value ? null : (int?)dr["te_manufacture_day"],
                                    te_creation_date = (DateTime)dr["te_creation_date"],
                                    te_replacement_date = dr["te_replacement_date"] == DBNull.Value ? null : (DateTime?)dr["te_replacement_date"],
                                    te_equity_percentage_2 = dr["te_equity_percentage_2"] == DBNull.Value ? null : (decimal?)dr["te_equity_percentage_2"],
                                    te_equity_fixed_amount = dr["te_equity_fixed_amount"] == DBNull.Value ? null : (decimal?)dr["te_equity_fixed_amount"],
                                    te_sas_flags2 = (int)dr["te_sas_flags2"],
                                    te_sas_flags2_use_site_default = (int)dr["te_sas_flags2_use_site_default"],
                                    te_cashier_draw_device_ip = dr["te_cashier_draw_device_ip"] == DBNull.Value ? null : (string)dr["te_cashier_draw_device_ip"],
                                    te_ven_id = dr["te_ven_id"] == DBNull.Value ? null : (int?)dr["te_ven_id"],
                                    te_last_play_session_id = dr["te_last_play_session_id"] == DBNull.Value ? null : (long?)dr["te_last_play_session_id"],
                                    te_sas_version_edit = dr["te_sas_version_edit"] == DBNull.Value ? null : (string)dr["te_sas_version_edit"],
                                    te_sas_aft_enabled_edit = dr["te_sas_aft_enabled_edit"] == DBNull.Value ? null : (bool?)dr["te_sas_aft_enabled_edit"],
                                    te_collect_tax = dr["te_collect_tax"] == DBNull.Value ? null : (bool?)dr["te_collect_tax"],
                                    te_current_draw_session_id = dr["te_current_draw_session_id"] == DBNull.Value ? null : (long?)dr["te_current_draw_session_id"],
                                    te_model_id = dr["te_model_id"] == DBNull.Value ? null : (Guid?)dr["te_model_id"],
                                    te_terminal_settings_id = dr["te_terminal_settings_id"] == DBNull.Value ? null : (int?)dr["te_terminal_settings_id"],
                                    TE_COLLECTION_TYPE = dr["TE_COLLECTION_TYPE"] == DBNull.Value ? null : (short?)dr["TE_COLLECTION_TYPE"],
                                    te_external_request_id = dr["te_external_request_id"] == DBNull.Value ? null : (string)dr["te_external_request_id"],
                                    TE_ONLY_REDEEMABLE = dr["TE_ONLY_REDEEMABLE"] == DBNull.Value ? null : (bool?)dr["TE_ONLY_REDEEMABLE"],
                                };
                                result.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = new List<terminals>();
            }
            return result;
        }
        public int GetTotalTerminalsForMigration(long lastid)
        {
            int total = 0;

            string query = @"
            select count(*) as total from 
            [dbo].[terminals]
where te_terminal_id > @lastid
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
        public int SaveTerminals(terminals item)
        {
            //bool respuesta = false;
            int IdInsertado = 0;
            string consulta = @"
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
output inserted.te_terminal_id
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
                      ";
            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@te_terminal_id", ManejoNulos.ManageNullInteger(item.te_terminal_id));
                    query.Parameters.AddWithValue("@te_type", ManejoNulos.ManageNullInteger(item.te_type));
                    query.Parameters.AddWithValue("@te_server_id", ManejoNulos.ManageNullInteger(item.te_server_id));
                    query.Parameters.AddWithValue("@te_external_id", ManejoNulos.ManageNullStr(item.te_external_id));
                    query.Parameters.AddWithValue("@te_blocked", ManejoNulos.ManegeNullBool(item.te_blocked));
                    query.Parameters.AddWithValue("@te_timestamp", ManejoNulos.ManageNullInteger64(item.te_timestamp));
                    query.Parameters.AddWithValue("@te_active", ManejoNulos.ManegeNullBool(item.te_active));
                    query.Parameters.AddWithValue("@te_provider_id", ManejoNulos.ManageNullStr(item.te_provider_id));
                    query.Parameters.AddWithValue("@te_client_id", ManejoNulos.ManageNullShort(item.te_client_id));
                    query.Parameters.AddWithValue("@te_build_id", ManejoNulos.ManageNullShort(item.te_build_id));
                    query.Parameters.AddWithValue("@te_terminal_type", ManejoNulos.ManageNullShort(item.te_terminal_type));
                    query.Parameters.AddWithValue("@te_vendor_id", ManejoNulos.ManageNullStr(item.te_vendor_id));
                    query.Parameters.AddWithValue("@te_unique_external_id", ManejoNulos.ManageNullStr(item.te_unique_external_id));
                    query.Parameters.AddWithValue("@te_status", ManejoNulos.ManageNullInteger(item.te_status));
                    query.Parameters.AddWithValue("@te_retirement_date", ManejoNulos.ManageNullDate(item.te_retirement_date));
                    query.Parameters.AddWithValue("@te_retirement_requested", ManejoNulos.ManageNullDate(item.te_retirement_requested));
                    query.Parameters.AddWithValue("@te_denomination", ManejoNulos.ManageNullDecimal(item.te_denomination));
                    query.Parameters.AddWithValue("@te_multi_denomination", ManejoNulos.ManageNullStr(item.te_multi_denomination));
                    query.Parameters.AddWithValue("@te_program", ManejoNulos.ManageNullStr(item.te_program));
                    query.Parameters.AddWithValue("@te_theoretical_payout", ManejoNulos.ManageNullDecimal(item.te_theoretical_payout));
                    query.Parameters.AddWithValue("@te_theoretical_hold", ManejoNulos.ManageNullDecimal(item.te_theoretical_hold));
                    query.Parameters.AddWithValue("@te_prov_id", ManejoNulos.ManageNullInteger(item.te_prov_id));
                    query.Parameters.AddWithValue("@te_bank_id", ManejoNulos.ManageNullInteger(item.te_bank_id));
                    query.Parameters.AddWithValue("@te_floor_id", ManejoNulos.ManageNullStr(item.te_floor_id));
                    query.Parameters.AddWithValue("@te_game_type", ManejoNulos.ManageNullInteger(item.te_game_type));
                    query.Parameters.AddWithValue("@te_activation_date", ManejoNulos.ManageNullDate(item.te_activation_date));
                    query.Parameters.AddWithValue("@te_current_account_id", ManejoNulos.ManageNullInteger64(item.te_current_account_id));
                    query.Parameters.AddWithValue("@te_current_play_session_id", ManejoNulos.ManageNullInteger64(item.te_current_play_session_id));
                    query.Parameters.AddWithValue("@te_registration_code", ManejoNulos.ManageNullStr(item.te_registration_code));
                    query.Parameters.AddWithValue("@te_sas_flags", ManejoNulos.ManageNullInteger(item.te_sas_flags));
                    query.Parameters.AddWithValue("@te_serial_number", ManejoNulos.ManageNullStr(item.te_serial_number));
                    query.Parameters.AddWithValue("@te_cabinet_type", ManejoNulos.ManageNullStr(item.te_cabinet_type));
                    query.Parameters.AddWithValue("@te_jackpot_contribution_pct", ManejoNulos.ManageNullDecimal(item.te_jackpot_contribution_pct));
                    query.Parameters.AddWithValue("@te_contract_type", ManejoNulos.ManageNullInteger(item.te_contract_type));
                    query.Parameters.AddWithValue("@te_contract_id", ManejoNulos.ManageNullStr(item.te_contract_id));
                    query.Parameters.AddWithValue("@te_order_number", ManejoNulos.ManageNullStr(item.te_order_number));
                    query.Parameters.AddWithValue("@te_wxp_reported", ManejoNulos.ManegeNullBool(item.te_wxp_reported));
                    query.Parameters.AddWithValue("@te_wxp_reported_status_datetime", ManejoNulos.ManageNullDate(item.te_wxp_reported_status_datetime));
                    query.Parameters.AddWithValue("@te_wxp_reported_status", ManejoNulos.ManageNullInteger64(item.te_wxp_reported_status));
                    query.Parameters.AddWithValue("@te_sequence_id", ManejoNulos.ManageNullInteger64(item.te_sequence_id));
                    query.Parameters.AddWithValue("@te_validation_type", ManejoNulos.ManageNullInteger(item.te_validation_type));
                    query.Parameters.AddWithValue("@te_allowed_cashable_emission", ManejoNulos.ManegeNullBool(item.te_allowed_cashable_emission));
                    query.Parameters.AddWithValue("@te_allowed_promo_emission", ManejoNulos.ManegeNullBool(item.te_allowed_promo_emission));
                    query.Parameters.AddWithValue("@te_allowed_redemption", ManejoNulos.ManegeNullBool(item.te_allowed_redemption));
                    query.Parameters.AddWithValue("@te_max_allowed_ti", ManejoNulos.ManageNullDecimal(item.te_max_allowed_ti));
                    query.Parameters.AddWithValue("@te_max_allowed_to", ManejoNulos.ManageNullDecimal(item.te_max_allowed_to));
                    query.Parameters.AddWithValue("@te_sas_version", ManejoNulos.ManageNullStr(item.te_sas_version));
                    query.Parameters.AddWithValue("@te_sas_machine_name", ManejoNulos.ManageNullStr(item.te_sas_machine_name));
                    query.Parameters.AddWithValue("@te_bonus_flags", ManejoNulos.ManageNullInteger(item.te_bonus_flags));
                    query.Parameters.AddWithValue("@te_features_bytes", ManejoNulos.ManageNullInteger(item.te_features_bytes));
                    query.Parameters.AddWithValue("@te_virtual_account_id", ManejoNulos.ManageNullInteger64(item.te_virtual_account_id));
                    query.Parameters.AddWithValue("@te_sas_flags_use_site_default", ManejoNulos.ManageNullInteger(item.te_sas_flags_use_site_default));
                    query.Parameters.AddWithValue("@te_authentication_method", ManejoNulos.ManageNullInteger(item.te_authentication_method));
                    query.Parameters.AddWithValue("@te_authentication_seed", ManejoNulos.ManageNullStr(item.te_authentication_seed));
                    query.Parameters.AddWithValue("@te_authentication_signature", ManejoNulos.ManageNullStr(item.te_authentication_signature));
                    query.Parameters.AddWithValue("@te_authentication_status", ManejoNulos.ManageNullInteger(item.te_authentication_status));
                    query.Parameters.AddWithValue("@te_authentication_last_checked", ManejoNulos.ManageNullDate(item.te_authentication_last_checked));
                    query.Parameters.AddWithValue("@te_machine_id", ManejoNulos.ManageNullStr(item.te_machine_id));
                    query.Parameters.AddWithValue("@te_position", ManejoNulos.ManageNullInteger(item.te_position));
                    query.Parameters.AddWithValue("@te_top_award", ManejoNulos.ManageNullDecimal(item.te_top_award));
                    query.Parameters.AddWithValue("@te_max_bet", ManejoNulos.ManageNullDecimal(item.te_max_bet));
                    query.Parameters.AddWithValue("@te_number_lines", ManejoNulos.ManageNullStr(item.te_number_lines));
                    query.Parameters.AddWithValue("@te_game_theme", ManejoNulos.ManageNullStr(item.te_game_theme));
                    query.Parameters.AddWithValue("@te_account_promotion_id", ManejoNulos.ManageNullInteger64(item.te_account_promotion_id));
                    query.Parameters.AddWithValue("@te_machine_asset_number", ManejoNulos.ManageNullInteger64(item.te_machine_asset_number));
                    query.Parameters.AddWithValue("@te_asset_number", ManejoNulos.ManageNullInteger64(item.te_asset_number));
                    query.Parameters.AddWithValue("@te_machine_serial_number", ManejoNulos.ManageNullStr(item.te_machine_serial_number));
                    query.Parameters.AddWithValue("@te_meter_delta_id", ManejoNulos.ManageNullInteger64(item.te_meter_delta_id));
                    query.Parameters.AddWithValue("@te_master_id", ManejoNulos.ManageNullInteger(item.te_master_id));
                    query.Parameters.AddWithValue("@te_change_id", ManejoNulos.ManageNullInteger(item.te_change_id));
                    query.Parameters.AddWithValue("@te_base_name", ManejoNulos.ManageNullStr(item.te_base_name));
                    query.Parameters.AddWithValue("@te_name", ManejoNulos.ManageNullStr(item.te_name));
                    query.Parameters.AddWithValue("@te_transfer_status", ManejoNulos.ManageNullInteger(item.te_transfer_status));
                    query.Parameters.AddWithValue("@te_smib2egm_comm_type", ManejoNulos.ManageNullInteger(item.te_smib2egm_comm_type));
                    query.Parameters.AddWithValue("@te_smib2egm_conf_id", ManejoNulos.ManageNullInteger64(item.te_smib2egm_conf_id));
                    query.Parameters.AddWithValue("@te_last_game_played_id", ManejoNulos.ManageNullInteger(item.te_last_game_played_id));
                    query.Parameters.AddWithValue("@te_brand_code", ManejoNulos.ManageNullStr(item.te_brand_code));
                    query.Parameters.AddWithValue("@te_model", ManejoNulos.ManageNullStr(item.te_model));
                    query.Parameters.AddWithValue("@te_manufacture_year", ManejoNulos.ManageNullInteger(item.te_manufacture_year));
                    query.Parameters.AddWithValue("@te_met_homologated", ManejoNulos.ManegeNullBool(item.te_met_homologated));
                    query.Parameters.AddWithValue("@te_bet_code", ManejoNulos.ManageNullStr(item.te_bet_code));
                    query.Parameters.AddWithValue("@te_coin_collection", ManejoNulos.ManegeNullBool(item.te_coin_collection));
                    query.Parameters.AddWithValue("@te_terminal_currency_id", ManejoNulos.ManageNullInteger(item.te_terminal_currency_id));
                    query.Parameters.AddWithValue("@te_iso_code", ManejoNulos.ManageNullStr(item.te_iso_code));
                    query.Parameters.AddWithValue("@te_equity_percentage", ManejoNulos.ManageNullDecimal(item.te_equity_percentage));
                    query.Parameters.AddWithValue("@te_sas_accounting_denom", ManejoNulos.ManageNullDecimal(item.te_sas_accounting_denom));
                    query.Parameters.AddWithValue("@te_tito_host_id", ManejoNulos.ManageNullInteger(item.te_tito_host_id));
                    query.Parameters.AddWithValue("@te_min_allowed_ti", ManejoNulos.ManageNullDecimal(item.te_min_allowed_ti));
                    query.Parameters.AddWithValue("@te_allow_truncate", ManejoNulos.ManegeNullBool(item.te_allow_truncate));
                    query.Parameters.AddWithValue("@te_min_denomination", ManejoNulos.ManageNullDecimal(item.te_min_denomination));
                    query.Parameters.AddWithValue("@te_chk_equity_percentage", ManejoNulos.ManegeNullBool(item.te_chk_equity_percentage));
                    query.Parameters.AddWithValue("@te_reserve_account_id", ManejoNulos.ManageNullInteger64(item.te_reserve_account_id));
                    query.Parameters.AddWithValue("@te_reservation_expires", ManejoNulos.ManageNullDate(item.te_reservation_expires));
                    query.Parameters.AddWithValue("@te_multiseat_id", ManejoNulos.ManageNullInteger(item.te_multiseat_id));
                    query.Parameters.AddWithValue("@te_manufacture_month", ManejoNulos.ManageNullInteger(item.te_manufacture_month));
                    query.Parameters.AddWithValue("@te_manufacture_day", ManejoNulos.ManageNullInteger(item.te_manufacture_day));
                    query.Parameters.AddWithValue("@te_creation_date", ManejoNulos.ManageNullDate(item.te_creation_date));
                    query.Parameters.AddWithValue("@te_replacement_date", ManejoNulos.ManageNullDate(item.te_replacement_date));
                    query.Parameters.AddWithValue("@te_equity_percentage_2", ManejoNulos.ManageNullDecimal(item.te_equity_percentage_2));
                    query.Parameters.AddWithValue("@te_equity_fixed_amount", ManejoNulos.ManageNullDecimal(item.te_equity_fixed_amount));
                    query.Parameters.AddWithValue("@te_sas_flags2", ManejoNulos.ManageNullInteger(item.te_sas_flags2));
                    query.Parameters.AddWithValue("@te_sas_flags2_use_site_default", ManejoNulos.ManageNullInteger(item.te_sas_flags2_use_site_default));
                    query.Parameters.AddWithValue("@te_cashier_draw_device_ip", ManejoNulos.ManageNullStr(item.te_cashier_draw_device_ip));
                    query.Parameters.AddWithValue("@te_ven_id", ManejoNulos.ManageNullInteger(item.te_ven_id));
                    query.Parameters.AddWithValue("@te_last_play_session_id", ManejoNulos.ManageNullInteger64(item.te_last_play_session_id));
                    query.Parameters.AddWithValue("@te_sas_version_edit", ManejoNulos.ManageNullStr(item.te_sas_version_edit));
                    query.Parameters.AddWithValue("@te_sas_aft_enabled_edit", ManejoNulos.ManegeNullBool(item.te_sas_aft_enabled_edit));
                    query.Parameters.AddWithValue("@te_collect_tax", ManejoNulos.ManegeNullBool(item.te_collect_tax));
                    query.Parameters.AddWithValue("@te_current_draw_session_id", ManejoNulos.ManageNullInteger64(item.te_current_draw_session_id));
                    query.Parameters.AddWithValue("@te_model_id", ManejoNulos.ManageNullGuid(item.te_model_id));
                    query.Parameters.AddWithValue("@te_terminal_settings_id", ManejoNulos.ManageNullInteger(item.te_terminal_settings_id));
                    query.Parameters.AddWithValue("@TE_COLLECTION_TYPE", ManejoNulos.ManageNullShort(item.TE_COLLECTION_TYPE));
                    query.Parameters.AddWithValue("@te_external_request_id", ManejoNulos.ManageNullStr(item.te_external_request_id));
                    query.Parameters.AddWithValue("@TE_ONLY_REDEEMABLE", ManejoNulos.ManegeNullBool(item.TE_ONLY_REDEEMABLE));
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
