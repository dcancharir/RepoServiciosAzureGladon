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
    public class accounts_dal
    {
        private readonly string _conexion_wgdb_000 = string.Empty;
        private readonly string _conexion_wgdb_000_migration = string.Empty;

        public accounts_dal()
        {
            _conexion_wgdb_000 = ConfigurationManager.ConnectionStrings["connection_wgdb_000"].ConnectionString;
            _conexion_wgdb_000_migration = ConfigurationManager.ConnectionStrings["connection_wgdb_000_migration"].ConnectionString;
        }
        public List<accounts> GetAllAccounts() {
            var result = new List<accounts>();
            try
            {
                string query = $@"
SELECT [ac_account_id]
      ,[ac_type]
      ,[ac_holder_name]
      ,[ac_blocked]
      ,[ac_not_valid_before]
      ,[ac_not_valid_after]
      ,[ac_balance]
      ,[ac_cash_in]
      ,[ac_cash_won]
      ,[ac_not_redeemable]
      ,[ac_timestamp]
      ,[ac_track_data]
      ,[ac_total_cash_in]
      ,[ac_total_cash_out]
      ,[ac_initial_cash_in]
      ,[ac_activated]
      ,[ac_deposit]
      ,[ac_current_terminal_id]
      ,[ac_current_terminal_name]
      ,[ac_current_play_session_id]
      ,[ac_last_terminal_id]
      ,[ac_last_terminal_name]
      ,[ac_last_play_session_id]
      ,[ac_user_type]
      ,[ac_points]
      ,[ac_initial_not_redeemable]
      ,[ac_created]
      ,[ac_promo_balance]
      ,[ac_promo_limit]
      ,[ac_promo_creation]
      ,[ac_promo_expiration]
      ,[ac_last_activity]
      ,[ac_holder_id]
      ,[ac_holder_address_01]
      ,[ac_holder_address_02]
      ,[ac_holder_address_03]
      ,[ac_holder_city]
      ,[ac_holder_zip]
      ,[ac_holder_email_01]
      ,[ac_holder_email_02]
      ,[ac_holder_phone_number_01]
      ,[ac_holder_phone_number_02]
      ,[ac_holder_comments]
      ,[ac_holder_gender]
      ,[ac_holder_marital_status]
      ,[ac_holder_birth_date]
      ,[ac_draw_last_play_session_id]
      ,[ac_draw_last_play_session_remainder]
      ,[ac_nr_won_lock]
      ,[ac_nr_expiration]
      ,[ac_cashin_while_playing]
      ,[ac_holder_level]
      ,[ac_card_paid]
      ,[ac_cancellable_operation_id]
      ,[ac_current_promotion_id]
      ,[ac_block_reason]
      ,[ac_holder_level_expiration]
      ,[ac_holder_level_entered]
      ,[ac_holder_level_notify]
      ,[ac_pin]
      ,[ac_pin_failures]
      ,[ac_holder_name1]
      ,[ac_holder_name2]
      ,[ac_holder_name3]
      ,[ac_holder_id1]
      ,[ac_holder_id2]
      ,[ac_holder_document_id1]
      ,[ac_holder_document_id2]
      ,[ac_nr2_expiration]
      ,[ac_recommended_by]
      ,[ac_re_balance]
      ,[ac_promo_re_balance]
      ,[ac_promo_nr_balance]
      ,[ac_in_session_played]
      ,[ac_in_session_won]
      ,[ac_in_session_re_balance]
      ,[ac_in_session_promo_re_balance]
      ,[ac_in_session_promo_nr_balance]
      ,[ac_in_session_re_to_gm]
      ,[ac_in_session_promo_re_to_gm]
      ,[ac_in_session_promo_nr_to_gm]
      ,[ac_in_session_re_from_gm]
      ,[ac_in_session_promo_re_from_gm]
      ,[ac_in_session_promo_nr_from_gm]
      ,[ac_in_session_re_played]
      ,[ac_in_session_nr_played]
      ,[ac_in_session_re_won]
      ,[ac_in_session_nr_won]
      ,[ac_in_session_re_cancellable]
      ,[ac_in_session_promo_re_cancellable]
      ,[ac_in_session_promo_nr_cancellable]
      ,[ac_in_session_cancellable_transaction_id]
      ,[ac_holder_id_type]
      ,[ac_promo_ini_re_balance]
      ,[ac_holder_is_vip]
      ,[ac_holder_title]
      ,[ac_holder_name4]
      ,[ac_holder_wedding_date]
      ,[ac_holder_phone_type_01]
      ,[ac_holder_phone_type_02]
      ,[ac_holder_state]
      ,[ac_holder_country]
      ,[ac_holder_address_01_alt]
      ,[ac_holder_address_02_alt]
      ,[ac_holder_address_03_alt]
      ,[ac_holder_city_alt]
      ,[ac_holder_zip_alt]
      ,[ac_holder_state_alt]
      ,[ac_holder_country_alt]
      ,[ac_holder_is_smoker]
      ,[ac_holder_nickname]
      ,[ac_holder_credit_limit]
      ,[ac_holder_request_credit_limit]
      ,[ac_pin_last_modified]
      ,[ac_last_activity_site_id]
      ,[ac_creation_site_id]
      ,[ac_last_update_site_id]
      ,[ac_external_reference]
      ,[ac_points_status]
      ,[ac_ms_has_local_changes]
      ,[ac_ms_change_guid]
      ,[ac_ms_created_on_site_id]
      ,[ac_ms_modified_on_site_id]
      ,[ac_ms_last_site_id]
      ,[ac_ms_points_seq_id]
      ,[ac_ms_points_synchronized]
      ,[ac_ms_personal_info_seq_id]
      ,[ac_ms_hash]
      ,[ac_holder_twitter_account]
      ,[ac_block_description]
      ,[ac_holder_occupation]
      ,[ac_holder_ext_num]
      ,[ac_holder_nationality]
      ,[ac_holder_birth_country]
      ,[ac_holder_fed_entity]
      ,[ac_holder_id1_type]
      ,[ac_holder_id2_type]
      ,[ac_holder_id3_type]
      ,[ac_holder_id3]
      ,[ac_beneficiary_name]
      ,[ac_beneficiary_name1]
      ,[ac_beneficiary_name2]
      ,[ac_beneficiary_name3]
      ,[ac_beneficiary_birth_date]
      ,[ac_beneficiary_gender]
      ,[ac_beneficiary_occupation]
      ,[ac_beneficiary_id1_type]
      ,[ac_beneficiary_id1]
      ,[ac_beneficiary_id2_type]
      ,[ac_beneficiary_id2]
      ,[ac_beneficiary_id3_type]
      ,[ac_beneficiary_id3]
      ,[ac_holder_has_beneficiary]
      ,[ac_holder_occupation_id]
      ,[ac_beneficiary_occupation_id]
      ,[ac_card_replacement_count]
      ,[ac_show_comments_on_cashier]
      ,[ac_external_aml_file_updated]
      ,[ac_holder_address_country]
      ,[ac_holder_address_validation]
      ,[ac_re_reserved]
      ,[ac_mode_reserved]
      ,[ac_external_aml_file_sequence]
      ,[ac_provision]
      ,[ac_egm_reserved]
      ,[ac_last_update_in_local_time]
      ,[ac_last_update_in_utc_time]
      ,[ac_sales_chips_rounding_amount]
      ,[ac_show_holder_name]
      ,[ac_constancy_printed]
      ,[ac_constancy_signed]
      ,[ac_holder_country_phone_code]
      ,[ac_method_contact]
      ,[ac_fiscal_address]
      ,[ac_mailing_address]
      ,[ac_tax_form_expiration_date]
      ,[ac_holder_country_phone_code_02]
      ,[ac_host_assigned_id]
      ,[ac_host_assigned_date]
      ,[ac_holder_email_validation_status]
      ,[ac_holder_email_last_send]
      ,[ac_holder_ssn]
      ,[ac_bad_address1]
      ,[ac_bad_address2]
      ,[ac_no_mail1]
      ,[ac_no_mail2]
      ,[ac_preferred_method]
      ,[ac_holder_id_indexed]
      ,[ac_holder_ext_num_alt]
      ,[ac_holder_level_change_comments]
      ,[ac_in_session_plays_count]
      ,[ac_holder_address_01_tertiary]
      ,[ac_holder_address_02_tertiary]
      ,[ac_holder_address_03_tertiary]
      ,[ac_holder_ext_num_tertiary]
      ,[ac_holder_country_tertiary]
      ,[ac_holder_state_tertiary]
      ,[ac_holder_city_tertiary]
      ,[ac_holder_zip_tertiary]
      ,[ac_bad_address3]
      ,[ac_no_mail3]
      ,[ac_in_session_cash_in]
      ,[ac_in_session_promo_ticket_re_in]
      ,[ac_in_session_promo_ticket_nr_in]
  FROM [dbo].[accounts]
    ";
                using (var con = new SqlConnection(_conexion_wgdb_000)) { 
                    con.Open();
                    var command = new SqlCommand(query, con);
                    using (var dr = command.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                var item = new accounts() {
                                    ac_account_id = (long)dr["ac_account_id"],
                                    ac_type = (int)dr["ac_type"],
                                    ac_holder_name = dr["ac_holder_name"] == DBNull.Value ? null : (string)dr["ac_holder_name"],
                                    ac_blocked = (bool)dr["ac_blocked"],
                                    ac_not_valid_before = dr["ac_not_valid_before"] == DBNull.Value ? null : (DateTime?)dr["ac_not_valid_before"],
                                    ac_not_valid_after = dr["ac_not_valid_after"] == DBNull.Value ? null : (DateTime?)dr["ac_not_valid_after"],
                                    ac_balance = (decimal)dr["ac_balance"],
                                    ac_cash_in = (decimal)dr["ac_cash_in"],
                                    ac_cash_won = (decimal)dr["ac_cash_won"],
                                    ac_not_redeemable = (decimal)dr["ac_not_redeemable"],
                                    ac_timestamp = dr["ac_timestamp"] == DBNull.Value ? null : (byte[])dr["ac_timestamp"],
                                    ac_track_data = dr["ac_track_data"] == DBNull.Value ? null : (string)dr["ac_track_data"],
                                    ac_total_cash_in = (decimal)dr["ac_total_cash_in"],
                                    ac_total_cash_out = (decimal)dr["ac_total_cash_out"],
                                    ac_initial_cash_in = (decimal)dr["ac_initial_cash_in"],
                                    ac_activated = (bool)dr["ac_activated"],
                                    ac_deposit = (decimal)dr["ac_deposit"],
                                    ac_current_terminal_id = dr["ac_current_terminal_id"] == DBNull.Value ? null : (int?)dr["ac_current_terminal_id"],
                                    ac_current_terminal_name = dr["ac_current_terminal_name"] == DBNull.Value ? null : (string)dr["ac_current_terminal_name"],
                                    ac_current_play_session_id = dr["ac_current_play_session_id"] == DBNull.Value ? null : (long?)dr["ac_current_play_session_id"],
                                    ac_last_terminal_id = dr["ac_last_terminal_id"] == DBNull.Value ? null : (int?)dr["ac_last_terminal_id"],
                                    ac_last_terminal_name = dr["ac_last_terminal_name"] == DBNull.Value ? null : (string)dr["ac_last_terminal_name"],
                                    ac_last_play_session_id = dr["ac_last_play_session_id"] == DBNull.Value ? null : (long?)dr["ac_last_play_session_id"],
                                    ac_user_type = dr["ac_user_type"] == DBNull.Value ? null : (int?)dr["ac_user_type"],
                                    ac_points = dr["ac_points"] == DBNull.Value ? null : (decimal?)dr["ac_points"],
                                    ac_initial_not_redeemable = (decimal)dr["ac_initial_not_redeemable"],
                                    ac_created = (DateTime)dr["ac_created"],
                                    ac_promo_balance = (decimal)dr["ac_promo_balance"],
                                    ac_promo_limit = (decimal)dr["ac_promo_limit"],
                                    ac_promo_creation = (DateTime)dr["ac_promo_creation"],
                                    ac_promo_expiration = (DateTime)dr["ac_promo_expiration"],
                                    ac_last_activity = (DateTime)dr["ac_last_activity"],
                                    ac_holder_id = dr["ac_holder_id"] == DBNull.Value ? null : (string)dr["ac_holder_id"],
                                    ac_holder_address_01 = dr["ac_holder_address_01"] == DBNull.Value ? null : (string)dr["ac_holder_address_01"],
                                    ac_holder_address_02 = dr["ac_holder_address_02"] == DBNull.Value ? null : (string)dr["ac_holder_address_02"],
                                    ac_holder_address_03 = dr["ac_holder_address_03"] == DBNull.Value ? null : (string)dr["ac_holder_address_03"],
                                    ac_holder_city = dr["ac_holder_city"] == DBNull.Value ? null : (string)dr["ac_holder_city"],
                                    ac_holder_zip = dr["ac_holder_zip"] == DBNull.Value ? null : (string)dr["ac_holder_zip"],
                                    ac_holder_email_01 = dr["ac_holder_email_01"] == DBNull.Value ? null : (string)dr["ac_holder_email_01"],
                                    ac_holder_email_02 = dr["ac_holder_email_02"] == DBNull.Value ? null : (string)dr["ac_holder_email_02"],
                                    ac_holder_phone_number_01 = dr["ac_holder_phone_number_01"] == DBNull.Value ? null : (string)dr["ac_holder_phone_number_01"],
                                    ac_holder_phone_number_02 = dr["ac_holder_phone_number_02"] == DBNull.Value ? null : (string)dr["ac_holder_phone_number_02"],
                                    ac_holder_comments = dr["ac_holder_comments"] == DBNull.Value ? null : (string)dr["ac_holder_comments"],
                                    ac_holder_gender = dr["ac_holder_gender"] == DBNull.Value ? null : (int?)dr["ac_holder_gender"],
                                    ac_holder_marital_status = dr["ac_holder_marital_status"] == DBNull.Value ? null : (int?)dr["ac_holder_marital_status"],
                                    ac_holder_birth_date = dr["ac_holder_birth_date"] == DBNull.Value ? null : (DateTime?)dr["ac_holder_birth_date"],
                                    ac_draw_last_play_session_id = (long)dr["ac_draw_last_play_session_id"],
                                    ac_draw_last_play_session_remainder = (decimal)dr["ac_draw_last_play_session_remainder"],
                                    ac_nr_won_lock = dr["ac_nr_won_lock"] == DBNull.Value ? null : (decimal?)dr["ac_nr_won_lock"],
                                    ac_nr_expiration = dr["ac_nr_expiration"] == DBNull.Value ? null : (DateTime?)dr["ac_nr_expiration"],
                                    ac_cashin_while_playing = dr["ac_cashin_while_playing"] == DBNull.Value ? null : (decimal?)dr["ac_cashin_while_playing"],
                                    ac_holder_level = (int)dr["ac_holder_level"],
                                    ac_card_paid = (bool)dr["ac_card_paid"],
                                    ac_cancellable_operation_id = dr["ac_cancellable_operation_id"] == DBNull.Value ? null : (long?)dr["ac_cancellable_operation_id"],
                                    ac_current_promotion_id = dr["ac_current_promotion_id"] == DBNull.Value ? null : (long?)dr["ac_current_promotion_id"],
                                    ac_block_reason = (int)dr["ac_block_reason"],
                                    ac_holder_level_expiration = dr["ac_holder_level_expiration"] == DBNull.Value ? null : (DateTime?)dr["ac_holder_level_expiration"],
                                    ac_holder_level_entered = dr["ac_holder_level_entered"] == DBNull.Value ? null : (DateTime?)dr["ac_holder_level_entered"],
                                    ac_holder_level_notify = dr["ac_holder_level_notify"] == DBNull.Value ? null : (int?)dr["ac_holder_level_notify"],
                                    ac_pin = dr["ac_pin"] == DBNull.Value ? null : (string)dr["ac_pin"],
                                    ac_pin_failures = dr["ac_pin_failures"] == DBNull.Value ? null : (int?)dr["ac_pin_failures"],
                                    ac_holder_name1 = dr["ac_holder_name1"] == DBNull.Value ? null : (string)dr["ac_holder_name1"],
                                    ac_holder_name2 = dr["ac_holder_name2"] == DBNull.Value ? null : (string)dr["ac_holder_name2"],
                                    ac_holder_name3 = dr["ac_holder_name3"] == DBNull.Value ? null : (string)dr["ac_holder_name3"],
                                    ac_holder_id1 = dr["ac_holder_id1"] == DBNull.Value ? null : (string)dr["ac_holder_id1"],
                                    ac_holder_id2 = dr["ac_holder_id2"] == DBNull.Value ? null : (string)dr["ac_holder_id2"],
                                    ac_holder_document_id1 = dr["ac_holder_document_id1"] == DBNull.Value ? null : (long?)dr["ac_holder_document_id1"],
                                    ac_holder_document_id2 = dr["ac_holder_document_id2"] == DBNull.Value ? null : (long?)dr["ac_holder_document_id2"],
                                    ac_nr2_expiration = dr["ac_nr2_expiration"] == DBNull.Value ? null : (DateTime?)dr["ac_nr2_expiration"],
                                    ac_recommended_by = dr["ac_recommended_by"] == DBNull.Value ? null : (long?)dr["ac_recommended_by"],
                                    ac_re_balance = (decimal)dr["ac_re_balance"],
                                    ac_promo_re_balance = (decimal)dr["ac_promo_re_balance"],
                                    ac_promo_nr_balance = (decimal)dr["ac_promo_nr_balance"],
                                    ac_in_session_played = (decimal)dr["ac_in_session_played"],
                                    ac_in_session_won = (decimal)dr["ac_in_session_won"],
                                    ac_in_session_re_balance = (decimal)dr["ac_in_session_re_balance"],
                                    ac_in_session_promo_re_balance = (decimal)dr["ac_in_session_promo_re_balance"],
                                    ac_in_session_promo_nr_balance = (decimal)dr["ac_in_session_promo_nr_balance"],
                                    ac_in_session_re_to_gm = (decimal)dr["ac_in_session_re_to_gm"],
                                    ac_in_session_promo_re_to_gm = (decimal)dr["ac_in_session_promo_re_to_gm"],
                                    ac_in_session_promo_nr_to_gm = (decimal)dr["ac_in_session_promo_nr_to_gm"],
                                    ac_in_session_re_from_gm = (decimal)dr["ac_in_session_re_from_gm"],
                                    ac_in_session_promo_re_from_gm = (decimal)dr["ac_in_session_promo_re_from_gm"],
                                    ac_in_session_promo_nr_from_gm = (decimal)dr["ac_in_session_promo_nr_from_gm"],
                                    ac_in_session_re_played = (decimal)dr["ac_in_session_re_played"],
                                    ac_in_session_nr_played = (decimal)dr["ac_in_session_nr_played"],
                                    ac_in_session_re_won = (decimal)dr["ac_in_session_re_won"],
                                    ac_in_session_nr_won = (decimal)dr["ac_in_session_nr_won"],
                                    ac_in_session_re_cancellable = (decimal)dr["ac_in_session_re_cancellable"],
                                    ac_in_session_promo_re_cancellable = (decimal)dr["ac_in_session_promo_re_cancellable"],
                                    ac_in_session_promo_nr_cancellable = (decimal)dr["ac_in_session_promo_nr_cancellable"],
                                    ac_in_session_cancellable_transaction_id = (long)dr["ac_in_session_cancellable_transaction_id"],
                                    ac_holder_id_type = (int)dr["ac_holder_id_type"],
                                    ac_promo_ini_re_balance = dr["ac_promo_ini_re_balance"] == DBNull.Value ? null : (decimal?)dr["ac_promo_ini_re_balance"],
                                    ac_holder_is_vip = dr["ac_holder_is_vip"] == DBNull.Value ? null : (bool?)dr["ac_holder_is_vip"],
                                    ac_holder_title = dr["ac_holder_title"] == DBNull.Value ? null : (string)dr["ac_holder_title"],
                                    ac_holder_name4 = dr["ac_holder_name4"] == DBNull.Value ? null : (string)dr["ac_holder_name4"],
                                    ac_holder_wedding_date = dr["ac_holder_wedding_date"] == DBNull.Value ? null : (DateTime?)dr["ac_holder_wedding_date"],
                                    ac_holder_phone_type_01 = dr["ac_holder_phone_type_01"] == DBNull.Value ? null : (int?)dr["ac_holder_phone_type_01"],
                                    ac_holder_phone_type_02 = dr["ac_holder_phone_type_02"] == DBNull.Value ? null : (int?)dr["ac_holder_phone_type_02"],
                                    ac_holder_state = dr["ac_holder_state"] == DBNull.Value ? null : (string)dr["ac_holder_state"],
                                    ac_holder_country = dr["ac_holder_country"] == DBNull.Value ? null : (string)dr["ac_holder_country"],
                                    ac_holder_address_01_alt = dr["ac_holder_address_01_alt"] == DBNull.Value ? null : (string)dr["ac_holder_address_01_alt"],
                                    ac_holder_address_02_alt = dr["ac_holder_address_02_alt"] == DBNull.Value ? null : (string)dr["ac_holder_address_02_alt"],
                                    ac_holder_address_03_alt = dr["ac_holder_address_03_alt"] == DBNull.Value ? null : (string)dr["ac_holder_address_03_alt"],
                                    ac_holder_city_alt = dr["ac_holder_city_alt"] == DBNull.Value ? null : (string)dr["ac_holder_city_alt"],
                                    ac_holder_zip_alt = dr["ac_holder_zip_alt"] == DBNull.Value ? null : (string)dr["ac_holder_zip_alt"],
                                    ac_holder_state_alt = dr["ac_holder_state_alt"] == DBNull.Value ? null : (string)dr["ac_holder_state_alt"],
                                    ac_holder_country_alt = dr["ac_holder_country_alt"] == DBNull.Value ? null : (string)dr["ac_holder_country_alt"],
                                    ac_holder_is_smoker = dr["ac_holder_is_smoker"] == DBNull.Value ? null : (bool?)dr["ac_holder_is_smoker"],
                                    ac_holder_nickname = dr["ac_holder_nickname"] == DBNull.Value ? null : (string)dr["ac_holder_nickname"],
                                    ac_holder_credit_limit = dr["ac_holder_credit_limit"] == DBNull.Value ? null : (decimal?)dr["ac_holder_credit_limit"],
                                    ac_holder_request_credit_limit = dr["ac_holder_request_credit_limit"] == DBNull.Value ? null : (decimal?)dr["ac_holder_request_credit_limit"],
                                    ac_pin_last_modified = dr["ac_pin_last_modified"] == DBNull.Value ? null : (DateTime?)dr["ac_pin_last_modified"],
                                    ac_last_activity_site_id = dr["ac_last_activity_site_id"] == DBNull.Value ? null : (string)dr["ac_last_activity_site_id"],
                                    ac_creation_site_id = dr["ac_creation_site_id"] == DBNull.Value ? null : (string)dr["ac_creation_site_id"],
                                    ac_last_update_site_id = dr["ac_last_update_site_id"] == DBNull.Value ? null : (string)dr["ac_last_update_site_id"],
                                    ac_external_reference = dr["ac_external_reference"] == DBNull.Value ? null : (string)dr["ac_external_reference"],
                                    ac_points_status = dr["ac_points_status"] == DBNull.Value ? null : (int?)dr["ac_points_status"],
                                    ac_ms_has_local_changes = dr["ac_ms_has_local_changes"] == DBNull.Value ? null : (bool?)dr["ac_ms_has_local_changes"],
                                    ac_ms_change_guid = dr["ac_ms_change_guid"] == DBNull.Value ? null : (Guid?)dr["ac_ms_change_guid"],
                                    ac_ms_created_on_site_id = dr["ac_ms_created_on_site_id"] == DBNull.Value ? null : (int?)dr["ac_ms_created_on_site_id"],
                                    ac_ms_modified_on_site_id = dr["ac_ms_modified_on_site_id"] == DBNull.Value ? null : (int?)dr["ac_ms_modified_on_site_id"],
                                    ac_ms_last_site_id = dr["ac_ms_last_site_id"] == DBNull.Value ? null : (int?)dr["ac_ms_last_site_id"],
                                    ac_ms_points_seq_id = dr["ac_ms_points_seq_id"] == DBNull.Value ? null : (long?)dr["ac_ms_points_seq_id"],
                                    ac_ms_points_synchronized = dr["ac_ms_points_synchronized"] == DBNull.Value ? null : (DateTime?)dr["ac_ms_points_synchronized"],
                                    ac_ms_personal_info_seq_id = dr["ac_ms_personal_info_seq_id"] == DBNull.Value ? null : (long?)dr["ac_ms_personal_info_seq_id"],
                                    ac_ms_hash = dr["ac_ms_hash"] == DBNull.Value ? null : (byte[])dr["ac_ms_hash"],
                                    ac_holder_twitter_account = dr["ac_holder_twitter_account"] == DBNull.Value ? null : (string)dr["ac_holder_twitter_account"],
                                    ac_block_description = dr["ac_block_description"] == DBNull.Value ? null : (string)dr["ac_block_description"],
                                    ac_holder_occupation = dr["ac_holder_occupation"] == DBNull.Value ? null : (string)dr["ac_holder_occupation"],
                                    ac_holder_ext_num = dr["ac_holder_ext_num"] == DBNull.Value ? null : (string)dr["ac_holder_ext_num"],
                                    ac_holder_nationality = dr["ac_holder_nationality"] == DBNull.Value ? null : (int?)dr["ac_holder_nationality"],
                                    ac_holder_birth_country = dr["ac_holder_birth_country"] == DBNull.Value ? null : (int?)dr["ac_holder_birth_country"],
                                    ac_holder_fed_entity = dr["ac_holder_fed_entity"] == DBNull.Value ? null : (int?)dr["ac_holder_fed_entity"],
                                    ac_holder_id1_type = dr["ac_holder_id1_type"] == DBNull.Value ? null : (int?)dr["ac_holder_id1_type"],
                                    ac_holder_id2_type = dr["ac_holder_id2_type"] == DBNull.Value ? null : (int?)dr["ac_holder_id2_type"],
                                    ac_holder_id3_type = dr["ac_holder_id3_type"] == DBNull.Value ? null : (string)dr["ac_holder_id3_type"],
                                    ac_holder_id3 = dr["ac_holder_id3"] == DBNull.Value ? null : (string)dr["ac_holder_id3"],
                                    ac_beneficiary_name = dr["ac_beneficiary_name"] == DBNull.Value ? null : (string)dr["ac_beneficiary_name"],
                                    ac_beneficiary_name1 = dr["ac_beneficiary_name1"] == DBNull.Value ? null : (string)dr["ac_beneficiary_name1"],
                                    ac_beneficiary_name2 = dr["ac_beneficiary_name2"] == DBNull.Value ? null : (string)dr["ac_beneficiary_name2"],
                                    ac_beneficiary_name3 = dr["ac_beneficiary_name3"] == DBNull.Value ? null : (string)dr["ac_beneficiary_name3"],
                                    ac_beneficiary_birth_date = dr["ac_beneficiary_birth_date"] == DBNull.Value ? null : (DateTime?)dr["ac_beneficiary_birth_date"],
                                    ac_beneficiary_gender = dr["ac_beneficiary_gender"] == DBNull.Value ? null : (int?)dr["ac_beneficiary_gender"],
                                    ac_beneficiary_occupation = dr["ac_beneficiary_occupation"] == DBNull.Value ? null : (string)dr["ac_beneficiary_occupation"],
                                    ac_beneficiary_id1_type = dr["ac_beneficiary_id1_type"] == DBNull.Value ? null : (int?)dr["ac_beneficiary_id1_type"],
                                    ac_beneficiary_id1 = dr["ac_beneficiary_id1"] == DBNull.Value ? null : (string)dr["ac_beneficiary_id1"],
                                    ac_beneficiary_id2_type = dr["ac_beneficiary_id2_type"] == DBNull.Value ? null : (int?)dr["ac_beneficiary_id2_type"],
                                    ac_beneficiary_id2 = dr["ac_beneficiary_id2"] == DBNull.Value ? null : (string)dr["ac_beneficiary_id2"],
                                    ac_beneficiary_id3_type = dr["ac_beneficiary_id3_type"] == DBNull.Value ? null : (string)dr["ac_beneficiary_id3_type"],
                                    ac_beneficiary_id3 = dr["ac_beneficiary_id3"] == DBNull.Value ? null : (string)dr["ac_beneficiary_id3"],
                                    ac_holder_has_beneficiary = (bool)dr["ac_holder_has_beneficiary"],
                                    ac_holder_occupation_id = dr["ac_holder_occupation_id"] == DBNull.Value ? null : (int?)dr["ac_holder_occupation_id"],
                                    ac_beneficiary_occupation_id = dr["ac_beneficiary_occupation_id"] == DBNull.Value ? null : (int?)dr["ac_beneficiary_occupation_id"],
                                    ac_card_replacement_count = dr["ac_card_replacement_count"] == DBNull.Value ? null : (int?)dr["ac_card_replacement_count"],
                                    ac_show_comments_on_cashier = dr["ac_show_comments_on_cashier"] == DBNull.Value ? null : (bool?)dr["ac_show_comments_on_cashier"],
                                    ac_external_aml_file_updated = dr["ac_external_aml_file_updated"] == DBNull.Value ? null : (DateTime?)dr["ac_external_aml_file_updated"],
                                    ac_holder_address_country = dr["ac_holder_address_country"] == DBNull.Value ? null : (int?)dr["ac_holder_address_country"],
                                    ac_holder_address_validation = (int)dr["ac_holder_address_validation"],
                                    ac_re_reserved = (decimal)dr["ac_re_reserved"],
                                    ac_mode_reserved = (bool)dr["ac_mode_reserved"],
                                    ac_external_aml_file_sequence = dr["ac_external_aml_file_sequence"] == DBNull.Value ? null : (Guid?)dr["ac_external_aml_file_sequence"],
                                    ac_provision = (decimal)dr["ac_provision"],
                                    ac_egm_reserved = dr["ac_egm_reserved"] == DBNull.Value ? null : (int?)dr["ac_egm_reserved"],
                                    ac_last_update_in_local_time = (DateTime)dr["ac_last_update_in_local_time"],
                                    ac_last_update_in_utc_time = (DateTime)dr["ac_last_update_in_utc_time"],
                                    ac_sales_chips_rounding_amount = (decimal)dr["ac_sales_chips_rounding_amount"],
                                    ac_show_holder_name = (bool)dr["ac_show_holder_name"],
                                    ac_constancy_printed = dr["ac_constancy_printed"] == DBNull.Value ? null : (DateTime?)dr["ac_constancy_printed"],
                                    ac_constancy_signed = dr["ac_constancy_signed"] == DBNull.Value ? null : (DateTime?)dr["ac_constancy_signed"],
                                    ac_holder_country_phone_code = dr["ac_holder_country_phone_code"] == DBNull.Value ? null : (int?)dr["ac_holder_country_phone_code"],
                                    ac_method_contact = dr["ac_method_contact"] == DBNull.Value ? null : (int?)dr["ac_method_contact"],
                                    ac_fiscal_address = (int)dr["ac_fiscal_address"],
                                    ac_mailing_address = (int)dr["ac_mailing_address"],
                                    ac_tax_form_expiration_date = (DateTime)dr["ac_tax_form_expiration_date"],
                                    ac_holder_country_phone_code_02 = dr["ac_holder_country_phone_code_02"] == DBNull.Value ? null : (int?)dr["ac_holder_country_phone_code_02"],
                                    ac_host_assigned_id = dr["ac_host_assigned_id"] == DBNull.Value ? null : (int?)dr["ac_host_assigned_id"],
                                    ac_host_assigned_date = dr["ac_host_assigned_date"] == DBNull.Value ? null : (DateTime?)dr["ac_host_assigned_date"],
                                    ac_holder_email_validation_status = (int)dr["ac_holder_email_validation_status"],
                                    ac_holder_email_last_send = dr["ac_holder_email_last_send"] == DBNull.Value ? null : (DateTime?)dr["ac_holder_email_last_send"],
                                    ac_holder_ssn = dr["ac_holder_ssn"] == DBNull.Value ? null : (string)dr["ac_holder_ssn"],
                                    ac_bad_address1 = dr["ac_bad_address1"] == DBNull.Value ? null : (int?)dr["ac_bad_address1"],
                                    ac_bad_address2 = dr["ac_bad_address2"] == DBNull.Value ? null : (int?)dr["ac_bad_address2"],
                                    ac_no_mail1 = dr["ac_no_mail1"] == DBNull.Value ? null : (int?)dr["ac_no_mail1"],
                                    ac_no_mail2 = dr["ac_no_mail2"] == DBNull.Value ? null : (int?)dr["ac_no_mail2"],
                                    ac_preferred_method = dr["ac_preferred_method"] == DBNull.Value ? null : (int?)dr["ac_preferred_method"],
                                    ac_holder_id_indexed = dr["ac_holder_id_indexed"] == DBNull.Value ? null : (string)dr["ac_holder_id_indexed"],
                                    ac_holder_ext_num_alt = dr["ac_holder_ext_num_alt"] == DBNull.Value ? null : (string)dr["ac_holder_ext_num_alt"],
                                    ac_holder_level_change_comments = dr["ac_holder_level_change_comments"] == DBNull.Value ? null : (string)dr["ac_holder_level_change_comments"],
                                    ac_in_session_plays_count = (int)dr["ac_in_session_plays_count"],
                                    ac_holder_address_01_tertiary = dr["ac_holder_address_01_tertiary"] == DBNull.Value ? null : (string)dr["ac_holder_address_01_tertiary"],
                                    ac_holder_address_02_tertiary = dr["ac_holder_address_02_tertiary"] == DBNull.Value ? null : (string)dr["ac_holder_address_02_tertiary"],
                                    ac_holder_address_03_tertiary = dr["ac_holder_address_03_tertiary"] == DBNull.Value ? null : (string)dr["ac_holder_address_03_tertiary"],
                                    ac_holder_ext_num_tertiary = dr["ac_holder_ext_num_tertiary"] == DBNull.Value ? null : (string)dr["ac_holder_ext_num_tertiary"],
                                    ac_holder_country_tertiary = dr["ac_holder_country_tertiary"] == DBNull.Value ? null : (string)dr["ac_holder_country_tertiary"],
                                    ac_holder_state_tertiary = dr["ac_holder_state_tertiary"] == DBNull.Value ? null : (string)dr["ac_holder_state_tertiary"],
                                    ac_holder_city_tertiary = dr["ac_holder_city_tertiary"] == DBNull.Value ? null : (string)dr["ac_holder_city_tertiary"],
                                    ac_holder_zip_tertiary = dr["ac_holder_zip_tertiary"] == DBNull.Value ? null : (string)dr["ac_holder_zip_tertiary"],
                                    ac_bad_address3 = dr["ac_bad_address3"] == DBNull.Value ? null : (int?)dr["ac_bad_address3"],
                                    ac_no_mail3 = dr["ac_no_mail3"] == DBNull.Value ? null : (int?)dr["ac_no_mail3"],
                                    ac_in_session_cash_in = dr["ac_in_session_cash_in"] == DBNull.Value ? null : (decimal?)dr["ac_in_session_cash_in"],
                                    ac_in_session_promo_ticket_re_in = dr["ac_in_session_promo_ticket_re_in"] == DBNull.Value ? null : (decimal?)dr["ac_in_session_promo_ticket_re_in"],
                                    ac_in_session_promo_ticket_nr_in = dr["ac_in_session_promo_ticket_nr_in"] == DBNull.Value ? null : (decimal?)dr["ac_in_session_promo_ticket_nr_in"],
                                };
                                result.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = new List<accounts>();
            }
            return result;
        }
        public long GetTotalAccountsForMigration()
        {
            long total = 0;

            string query = @"
            select count(*) as total from 
            [dbo].[accounts]
";

            try
            {
                using (SqlConnection conecction = new SqlConnection(_conexion_wgdb_000))
                {
                    conecction.Open();
                    SqlCommand command = new SqlCommand(query, conecction);
                    using (SqlDataReader data = command.ExecuteReader())
                    {
                        if (data.Read())
                        {
                            total = ManejoNulos.ManageNullInteger64(data["total"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                funciones.logueo($"Error metodo GetTotalAccountsForMigration - {ex.Message}", "Error");
                total = 0;
            }

            return total;
        }
        public int SaveAccounts(accounts item)
        {
            //bool respuesta = false;
            int IdInsertado = 0;
            string consulta = @"
INSERT INTO [dbo].[accounts]
           ([ac_account_id]
           ,[ac_type]
           ,[ac_holder_name]
           ,[ac_blocked]
           ,[ac_not_valid_before]
           ,[ac_not_valid_after]
           ,[ac_balance]
           ,[ac_cash_in]
           ,[ac_cash_won]
           ,[ac_not_redeemable]
           ,[ac_track_data]
           ,[ac_total_cash_in]
           ,[ac_total_cash_out]
           ,[ac_initial_cash_in]
           ,[ac_activated]
           ,[ac_deposit]
           ,[ac_current_terminal_id]
           ,[ac_current_terminal_name]
           ,[ac_current_play_session_id]
           ,[ac_last_terminal_id]
           ,[ac_last_terminal_name]
           ,[ac_last_play_session_id]
           ,[ac_user_type]
           ,[ac_points]
           ,[ac_initial_not_redeemable]
           ,[ac_created]
           ,[ac_promo_balance]
           ,[ac_promo_limit]
           ,[ac_promo_creation]
           ,[ac_promo_expiration]
           ,[ac_last_activity]
           ,[ac_holder_id]
           ,[ac_holder_address_01]
           ,[ac_holder_address_02]
           ,[ac_holder_address_03]
           ,[ac_holder_city]
           ,[ac_holder_zip]
           ,[ac_holder_email_01]
           ,[ac_holder_email_02]
           ,[ac_holder_phone_number_01]
           ,[ac_holder_phone_number_02]
           ,[ac_holder_comments]
           ,[ac_holder_gender]
           ,[ac_holder_marital_status]
           ,[ac_holder_birth_date]
           ,[ac_draw_last_play_session_id]
           ,[ac_draw_last_play_session_remainder]
           ,[ac_nr_won_lock]
           ,[ac_nr_expiration]
           ,[ac_cashin_while_playing]
           ,[ac_holder_level]
           ,[ac_card_paid]
           ,[ac_cancellable_operation_id]
           ,[ac_current_promotion_id]
           ,[ac_block_reason]
           ,[ac_holder_level_expiration]
           ,[ac_holder_level_entered]
           ,[ac_holder_level_notify]
           ,[ac_pin]
           ,[ac_pin_failures]
           ,[ac_holder_name1]
           ,[ac_holder_name2]
           ,[ac_holder_name3]
           ,[ac_holder_id1]
           ,[ac_holder_id2]
           ,[ac_holder_document_id1]
           ,[ac_holder_document_id2]
           ,[ac_nr2_expiration]
           ,[ac_recommended_by]
           ,[ac_re_balance]
           ,[ac_promo_re_balance]
           ,[ac_promo_nr_balance]
           ,[ac_in_session_played]
           ,[ac_in_session_won]
           ,[ac_in_session_re_balance]
           ,[ac_in_session_promo_re_balance]
           ,[ac_in_session_promo_nr_balance]
           ,[ac_in_session_re_to_gm]
           ,[ac_in_session_promo_re_to_gm]
           ,[ac_in_session_promo_nr_to_gm]
           ,[ac_in_session_re_from_gm]
           ,[ac_in_session_promo_re_from_gm]
           ,[ac_in_session_promo_nr_from_gm]
           ,[ac_in_session_re_played]
           ,[ac_in_session_nr_played]
           ,[ac_in_session_re_won]
           ,[ac_in_session_nr_won]
           ,[ac_in_session_re_cancellable]
           ,[ac_in_session_promo_re_cancellable]
           ,[ac_in_session_promo_nr_cancellable]
           ,[ac_in_session_cancellable_transaction_id]
           ,[ac_holder_id_type]
           ,[ac_promo_ini_re_balance]
           ,[ac_holder_is_vip]
           ,[ac_holder_title]
           ,[ac_holder_name4]
           ,[ac_holder_wedding_date]
           ,[ac_holder_phone_type_01]
           ,[ac_holder_phone_type_02]
           ,[ac_holder_state]
           ,[ac_holder_country]
           ,[ac_holder_address_01_alt]
           ,[ac_holder_address_02_alt]
           ,[ac_holder_address_03_alt]
           ,[ac_holder_city_alt]
           ,[ac_holder_zip_alt]
           ,[ac_holder_state_alt]
           ,[ac_holder_country_alt]
           ,[ac_holder_is_smoker]
           ,[ac_holder_nickname]
           ,[ac_holder_credit_limit]
           ,[ac_holder_request_credit_limit]
           ,[ac_pin_last_modified]
           ,[ac_last_activity_site_id]
           ,[ac_creation_site_id]
           ,[ac_last_update_site_id]
           ,[ac_external_reference]
           ,[ac_points_status]
           ,[ac_ms_has_local_changes]
           ,[ac_ms_change_guid]
           ,[ac_ms_created_on_site_id]
           ,[ac_ms_modified_on_site_id]
           ,[ac_ms_last_site_id]
           ,[ac_ms_points_seq_id]
           ,[ac_ms_points_synchronized]
           ,[ac_ms_personal_info_seq_id]
           ,[ac_ms_hash]
           ,[ac_holder_twitter_account]
           ,[ac_block_description]
           ,[ac_holder_occupation]
           ,[ac_holder_ext_num]
           ,[ac_holder_nationality]
           ,[ac_holder_birth_country]
           ,[ac_holder_fed_entity]
           ,[ac_holder_id1_type]
           ,[ac_holder_id2_type]
           ,[ac_holder_id3_type]
           ,[ac_holder_id3]
           ,[ac_beneficiary_name]
           ,[ac_beneficiary_name1]
           ,[ac_beneficiary_name2]
           ,[ac_beneficiary_name3]
           ,[ac_beneficiary_birth_date]
           ,[ac_beneficiary_gender]
           ,[ac_beneficiary_occupation]
           ,[ac_beneficiary_id1_type]
           ,[ac_beneficiary_id1]
           ,[ac_beneficiary_id2_type]
           ,[ac_beneficiary_id2]
           ,[ac_beneficiary_id3_type]
           ,[ac_beneficiary_id3]
           ,[ac_holder_has_beneficiary]
           ,[ac_holder_occupation_id]
           ,[ac_beneficiary_occupation_id]
           ,[ac_card_replacement_count]
           ,[ac_show_comments_on_cashier]
           ,[ac_external_aml_file_updated]
           ,[ac_holder_address_country]
           ,[ac_holder_address_validation]
           ,[ac_re_reserved]
           ,[ac_mode_reserved]
           ,[ac_external_aml_file_sequence]
           ,[ac_provision]
           ,[ac_egm_reserved]
           ,[ac_last_update_in_local_time]
           ,[ac_last_update_in_utc_time]
           ,[ac_sales_chips_rounding_amount]
           ,[ac_show_holder_name]
           ,[ac_constancy_printed]
           ,[ac_constancy_signed]
           ,[ac_holder_country_phone_code]
           ,[ac_method_contact]
           ,[ac_fiscal_address]
           ,[ac_mailing_address]
           ,[ac_tax_form_expiration_date]
           ,[ac_holder_country_phone_code_02]
           ,[ac_host_assigned_id]
           ,[ac_host_assigned_date]
           ,[ac_holder_email_validation_status]
           ,[ac_holder_email_last_send]
           ,[ac_holder_ssn]
           ,[ac_bad_address1]
           ,[ac_bad_address2]
           ,[ac_no_mail1]
           ,[ac_no_mail2]
           ,[ac_preferred_method]
           ,[ac_holder_ext_num_alt]
           ,[ac_holder_level_change_comments]
           ,[ac_in_session_plays_count]
           ,[ac_holder_address_01_tertiary]
           ,[ac_holder_address_02_tertiary]
           ,[ac_holder_address_03_tertiary]
           ,[ac_holder_ext_num_tertiary]
           ,[ac_holder_country_tertiary]
           ,[ac_holder_state_tertiary]
           ,[ac_holder_city_tertiary]
           ,[ac_holder_zip_tertiary]
           ,[ac_bad_address3]
           ,[ac_no_mail3]
           ,[ac_in_session_cash_in]
           ,[ac_in_session_promo_ticket_re_in]
           ,[ac_in_session_promo_ticket_nr_in])
    output inserted.ac_account_id
     VALUES
           (@ac_account_id
           ,@ac_type
           ,@ac_holder_name
           ,@ac_blocked
           ,@ac_not_valid_before
           ,@ac_not_valid_after
           ,@ac_balance
           ,@ac_cash_in
           ,@ac_cash_won
           ,@ac_not_redeemable
           ,@ac_track_data
           ,@ac_total_cash_in
           ,@ac_total_cash_out
           ,@ac_initial_cash_in
           ,@ac_activated
           ,@ac_deposit
           ,@ac_current_terminal_id
           ,@ac_current_terminal_name
           ,@ac_current_play_session_id
           ,@ac_last_terminal_id
           ,@ac_last_terminal_name
           ,@ac_last_play_session_id
           ,@ac_user_type
           ,@ac_points
           ,@ac_initial_not_redeemable
           ,@ac_created
           ,@ac_promo_balance
           ,@ac_promo_limit
           ,@ac_promo_creation
           ,@ac_promo_expiration
           ,@ac_last_activity
           ,@ac_holder_id
           ,@ac_holder_address_01
           ,@ac_holder_address_02
           ,@ac_holder_address_03
           ,@ac_holder_city
           ,@ac_holder_zip
           ,@ac_holder_email_01
           ,@ac_holder_email_02
           ,@ac_holder_phone_number_01
           ,@ac_holder_phone_number_02
           ,@ac_holder_comments
           ,@ac_holder_gender
           ,@ac_holder_marital_status
           ,@ac_holder_birth_date
           ,@ac_draw_last_play_session_id
           ,@ac_draw_last_play_session_remainder
           ,@ac_nr_won_lock
           ,@ac_nr_expiration
           ,@ac_cashin_while_playing
           ,@ac_holder_level
           ,@ac_card_paid
           ,@ac_cancellable_operation_id
           ,@ac_current_promotion_id
           ,@ac_block_reason
           ,@ac_holder_level_expiration
           ,@ac_holder_level_entered
           ,@ac_holder_level_notify
           ,@ac_pin
           ,@ac_pin_failures
           ,@ac_holder_name1
           ,@ac_holder_name2
           ,@ac_holder_name3
           ,@ac_holder_id1
           ,@ac_holder_id2
           ,@ac_holder_document_id1
           ,@ac_holder_document_id2
           ,@ac_nr2_expiration
           ,@ac_recommended_by
           ,@ac_re_balance
           ,@ac_promo_re_balance
           ,@ac_promo_nr_balance
           ,@ac_in_session_played
           ,@ac_in_session_won
           ,@ac_in_session_re_balance
           ,@ac_in_session_promo_re_balance
           ,@ac_in_session_promo_nr_balance
           ,@ac_in_session_re_to_gm
           ,@ac_in_session_promo_re_to_gm
           ,@ac_in_session_promo_nr_to_gm
           ,@ac_in_session_re_from_gm
           ,@ac_in_session_promo_re_from_gm
           ,@ac_in_session_promo_nr_from_gm
           ,@ac_in_session_re_played
           ,@ac_in_session_nr_played
           ,@ac_in_session_re_won
           ,@ac_in_session_nr_won
           ,@ac_in_session_re_cancellable
           ,@ac_in_session_promo_re_cancellable
           ,@ac_in_session_promo_nr_cancellable
           ,@ac_in_session_cancellable_transaction_id
           ,@ac_holder_id_type
           ,@ac_promo_ini_re_balance
           ,@ac_holder_is_vip
           ,@ac_holder_title
           ,@ac_holder_name4
           ,@ac_holder_wedding_date
           ,@ac_holder_phone_type_01
           ,@ac_holder_phone_type_02
           ,@ac_holder_state
           ,@ac_holder_country
           ,@ac_holder_address_01_alt
           ,@ac_holder_address_02_alt
           ,@ac_holder_address_03_alt
           ,@ac_holder_city_alt
           ,@ac_holder_zip_alt
           ,@ac_holder_state_alt
           ,@ac_holder_country_alt
           ,@ac_holder_is_smoker
           ,@ac_holder_nickname
           ,@ac_holder_credit_limit
           ,@ac_holder_request_credit_limit
           ,@ac_pin_last_modified
           ,@ac_last_activity_site_id
           ,@ac_creation_site_id
           ,@ac_last_update_site_id
           ,@ac_external_reference
           ,@ac_points_status
           ,@ac_ms_has_local_changes
           ,@ac_ms_change_guid
           ,@ac_ms_created_on_site_id
           ,@ac_ms_modified_on_site_id
           ,@ac_ms_last_site_id
           ,@ac_ms_points_seq_id
           ,@ac_ms_points_synchronized
           ,@ac_ms_personal_info_seq_id
           ,@ac_ms_hash
           ,@ac_holder_twitter_account
           ,@ac_block_description
           ,@ac_holder_occupation
           ,@ac_holder_ext_num
           ,@ac_holder_nationality
           ,@ac_holder_birth_country
           ,@ac_holder_fed_entity
           ,@ac_holder_id1_type
           ,@ac_holder_id2_type
           ,@ac_holder_id3_type
           ,@ac_holder_id3
           ,@ac_beneficiary_name
           ,@ac_beneficiary_name1
           ,@ac_beneficiary_name2
           ,@ac_beneficiary_name3
           ,@ac_beneficiary_birth_date
           ,@ac_beneficiary_gender
           ,@ac_beneficiary_occupation
           ,@ac_beneficiary_id1_type
           ,@ac_beneficiary_id1
           ,@ac_beneficiary_id2_type
           ,@ac_beneficiary_id2
           ,@ac_beneficiary_id3_type
           ,@ac_beneficiary_id3
           ,@ac_holder_has_beneficiary
           ,@ac_holder_occupation_id
           ,@ac_beneficiary_occupation_id
           ,@ac_card_replacement_count
           ,@ac_show_comments_on_cashier
           ,@ac_external_aml_file_updated
           ,@ac_holder_address_country
           ,@ac_holder_address_validation
           ,@ac_re_reserved
           ,@ac_mode_reserved
           ,@ac_external_aml_file_sequence
           ,@ac_provision
           ,@ac_egm_reserved
           ,@ac_last_update_in_local_time
           ,@ac_last_update_in_utc_time
           ,@ac_sales_chips_rounding_amount
           ,@ac_show_holder_name
           ,@ac_constancy_printed
           ,@ac_constancy_signed
           ,@ac_holder_country_phone_code
           ,@ac_method_contact
           ,@ac_fiscal_address
           ,@ac_mailing_address
           ,@ac_tax_form_expiration_date
           ,@ac_holder_country_phone_code_02
           ,@ac_host_assigned_id
           ,@ac_host_assigned_date
           ,@ac_holder_email_validation_status
           ,@ac_holder_email_last_send
           ,@ac_holder_ssn
           ,@ac_bad_address1
           ,@ac_bad_address2
           ,@ac_no_mail1
           ,@ac_no_mail2
           ,@ac_preferred_method
           ,@ac_holder_ext_num_alt
           ,@ac_holder_level_change_comments
           ,@ac_in_session_plays_count
           ,@ac_holder_address_01_tertiary
           ,@ac_holder_address_02_tertiary
           ,@ac_holder_address_03_tertiary
           ,@ac_holder_ext_num_tertiary
           ,@ac_holder_country_tertiary
           ,@ac_holder_state_tertiary
           ,@ac_holder_city_tertiary
           ,@ac_holder_zip_tertiary
           ,@ac_bad_address3
           ,@ac_no_mail3
           ,@ac_in_session_cash_in
           ,@ac_in_session_promo_ticket_re_in
           ,@ac_in_session_promo_ticket_nr_in)

                      ";
            try
            {
                using (var con = new SqlConnection(_conexion_wgdb_000_migration))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@ac_account_id", ManejoNulos.ManageNullInteger64(item.ac_account_id));
                    query.Parameters.AddWithValue("@ac_type", ManejoNulos.ManageNullInteger(item.ac_type));
                    query.Parameters.AddWithValue("@ac_holder_name", ManejoNulos.ManageNullStr(item.ac_holder_name));
                    query.Parameters.AddWithValue("@ac_blocked", ManejoNulos.ManegeNullBool(item.ac_blocked));
                    query.Parameters.AddWithValue("@ac_not_valid_before", ManejoNulos.ManageNullDate(item.ac_not_valid_before));
                    query.Parameters.AddWithValue("@ac_not_valid_after", ManejoNulos.ManageNullDate(item.ac_not_valid_after));
                    query.Parameters.AddWithValue("@ac_balance", ManejoNulos.ManageNullDecimal(item.ac_balance));
                    query.Parameters.AddWithValue("@ac_cash_in", ManejoNulos.ManageNullDecimal(item.ac_cash_in));
                    query.Parameters.AddWithValue("@ac_cash_won", ManejoNulos.ManageNullDecimal(item.ac_cash_won));
                    query.Parameters.AddWithValue("@ac_not_redeemable", ManejoNulos.ManageNullDecimal(item.ac_not_redeemable));
                    query.Parameters.AddWithValue("@ac_timestamp", ManejoNulos.ManageNullByteArray(item.ac_timestamp));
                    query.Parameters.AddWithValue("@ac_track_data", ManejoNulos.ManageNullStr(item.ac_track_data));
                    query.Parameters.AddWithValue("@ac_total_cash_in", ManejoNulos.ManageNullDecimal(item.ac_total_cash_in));
                    query.Parameters.AddWithValue("@ac_total_cash_out", ManejoNulos.ManageNullDecimal(item.ac_total_cash_out));
                    query.Parameters.AddWithValue("@ac_initial_cash_in", ManejoNulos.ManageNullDecimal(item.ac_initial_cash_in));
                    query.Parameters.AddWithValue("@ac_activated", ManejoNulos.ManegeNullBool(item.ac_activated));
                    query.Parameters.AddWithValue("@ac_deposit", ManejoNulos.ManageNullDecimal(item.ac_deposit));
                    query.Parameters.AddWithValue("@ac_current_terminal_id", ManejoNulos.ManageNullInteger(item.ac_current_terminal_id));
                    query.Parameters.AddWithValue("@ac_current_terminal_name", ManejoNulos.ManageNullStr(item.ac_current_terminal_name));
                    query.Parameters.AddWithValue("@ac_current_play_session_id", ManejoNulos.ManageNullInteger64(item.ac_current_play_session_id));
                    query.Parameters.AddWithValue("@ac_last_terminal_id", ManejoNulos.ManageNullInteger(item.ac_last_terminal_id));
                    query.Parameters.AddWithValue("@ac_last_terminal_name", ManejoNulos.ManageNullStr(item.ac_last_terminal_name));
                    query.Parameters.AddWithValue("@ac_last_play_session_id", ManejoNulos.ManageNullInteger64(item.ac_last_play_session_id));
                    query.Parameters.AddWithValue("@ac_user_type", ManejoNulos.ManageNullInteger(item.ac_user_type));
                    query.Parameters.AddWithValue("@ac_points", ManejoNulos.ManageNullDecimal(item.ac_points));
                    query.Parameters.AddWithValue("@ac_initial_not_redeemable", ManejoNulos.ManageNullDecimal(item.ac_initial_not_redeemable));
                    query.Parameters.AddWithValue("@ac_created", ManejoNulos.ManageNullDate(item.ac_created));
                    query.Parameters.AddWithValue("@ac_promo_balance", ManejoNulos.ManageNullDecimal(item.ac_promo_balance));
                    query.Parameters.AddWithValue("@ac_promo_limit", ManejoNulos.ManageNullDecimal(item.ac_promo_limit));
                    query.Parameters.AddWithValue("@ac_promo_creation", ManejoNulos.ManageNullDate(item.ac_promo_creation));
                    query.Parameters.AddWithValue("@ac_promo_expiration", ManejoNulos.ManageNullDate(item.ac_promo_expiration));
                    query.Parameters.AddWithValue("@ac_last_activity", ManejoNulos.ManageNullDate(item.ac_last_activity));
                    query.Parameters.AddWithValue("@ac_holder_id", ManejoNulos.ManageNullStr(item.ac_holder_id));
                    query.Parameters.AddWithValue("@ac_holder_address_01", ManejoNulos.ManageNullStr(item.ac_holder_address_01));
                    query.Parameters.AddWithValue("@ac_holder_address_02", ManejoNulos.ManageNullStr(item.ac_holder_address_02));
                    query.Parameters.AddWithValue("@ac_holder_address_03", ManejoNulos.ManageNullStr(item.ac_holder_address_03));
                    query.Parameters.AddWithValue("@ac_holder_city", ManejoNulos.ManageNullStr(item.ac_holder_city));
                    query.Parameters.AddWithValue("@ac_holder_zip", ManejoNulos.ManageNullStr(item.ac_holder_zip));
                    query.Parameters.AddWithValue("@ac_holder_email_01", ManejoNulos.ManageNullStr(item.ac_holder_email_01));
                    query.Parameters.AddWithValue("@ac_holder_email_02", ManejoNulos.ManageNullStr(item.ac_holder_email_02));
                    query.Parameters.AddWithValue("@ac_holder_phone_number_01", ManejoNulos.ManageNullStr(item.ac_holder_phone_number_01));
                    query.Parameters.AddWithValue("@ac_holder_phone_number_02", ManejoNulos.ManageNullStr(item.ac_holder_phone_number_02));
                    query.Parameters.AddWithValue("@ac_holder_comments", ManejoNulos.ManageNullStr(item.ac_holder_comments));
                    query.Parameters.AddWithValue("@ac_holder_gender", ManejoNulos.ManageNullInteger(item.ac_holder_gender));
                    query.Parameters.AddWithValue("@ac_holder_marital_status", ManejoNulos.ManageNullInteger(item.ac_holder_marital_status));
                    query.Parameters.AddWithValue("@ac_holder_birth_date", ManejoNulos.ManageNullDate(item.ac_holder_birth_date));
                    query.Parameters.AddWithValue("@ac_draw_last_play_session_id", ManejoNulos.ManageNullInteger64(item.ac_draw_last_play_session_id));
                    query.Parameters.AddWithValue("@ac_draw_last_play_session_remainder", ManejoNulos.ManageNullDecimal(item.ac_draw_last_play_session_remainder));
                    query.Parameters.AddWithValue("@ac_nr_won_lock", ManejoNulos.ManageNullDecimal(item.ac_nr_won_lock));
                    query.Parameters.AddWithValue("@ac_nr_expiration", ManejoNulos.ManageNullDate(item.ac_nr_expiration));
                    query.Parameters.AddWithValue("@ac_cashin_while_playing", ManejoNulos.ManageNullDecimal(item.ac_cashin_while_playing));
                    query.Parameters.AddWithValue("@ac_holder_level", ManejoNulos.ManageNullInteger(item.ac_holder_level));
                    query.Parameters.AddWithValue("@ac_card_paid", ManejoNulos.ManegeNullBool(item.ac_card_paid));
                    query.Parameters.AddWithValue("@ac_cancellable_operation_id", ManejoNulos.ManageNullInteger64(item.ac_cancellable_operation_id));
                    query.Parameters.AddWithValue("@ac_current_promotion_id", ManejoNulos.ManageNullInteger64(item.ac_current_promotion_id));
                    query.Parameters.AddWithValue("@ac_block_reason", ManejoNulos.ManageNullInteger(item.ac_block_reason));
                    query.Parameters.AddWithValue("@ac_holder_level_expiration", ManejoNulos.ManageNullDate(item.ac_holder_level_expiration));
                    query.Parameters.AddWithValue("@ac_holder_level_entered", ManejoNulos.ManageNullDate(item.ac_holder_level_entered));
                    query.Parameters.AddWithValue("@ac_holder_level_notify", ManejoNulos.ManageNullInteger(item.ac_holder_level_notify));
                    query.Parameters.AddWithValue("@ac_pin", ManejoNulos.ManageNullStr(item.ac_pin));
                    query.Parameters.AddWithValue("@ac_pin_failures", ManejoNulos.ManageNullInteger(item.ac_pin_failures));
                    query.Parameters.AddWithValue("@ac_holder_name1", ManejoNulos.ManageNullStr(item.ac_holder_name1));
                    query.Parameters.AddWithValue("@ac_holder_name2", ManejoNulos.ManageNullStr(item.ac_holder_name2));
                    query.Parameters.AddWithValue("@ac_holder_name3", ManejoNulos.ManageNullStr(item.ac_holder_name3));
                    query.Parameters.AddWithValue("@ac_holder_id1", ManejoNulos.ManageNullStr(item.ac_holder_id1));
                    query.Parameters.AddWithValue("@ac_holder_id2", ManejoNulos.ManageNullStr(item.ac_holder_id2));
                    query.Parameters.AddWithValue("@ac_holder_document_id1", ManejoNulos.ManageNullInteger64(item.ac_holder_document_id1));
                    query.Parameters.AddWithValue("@ac_holder_document_id2", ManejoNulos.ManageNullInteger64(item.ac_holder_document_id2));
                    query.Parameters.AddWithValue("@ac_nr2_expiration", ManejoNulos.ManageNullDate(item.ac_nr2_expiration));
                    query.Parameters.AddWithValue("@ac_recommended_by", ManejoNulos.ManageNullInteger64(item.ac_recommended_by));
                    query.Parameters.AddWithValue("@ac_re_balance", ManejoNulos.ManageNullDecimal(item.ac_re_balance));
                    query.Parameters.AddWithValue("@ac_promo_re_balance", ManejoNulos.ManageNullDecimal(item.ac_promo_re_balance));
                    query.Parameters.AddWithValue("@ac_promo_nr_balance", ManejoNulos.ManageNullDecimal(item.ac_promo_nr_balance));
                    query.Parameters.AddWithValue("@ac_in_session_played", ManejoNulos.ManageNullDecimal(item.ac_in_session_played));
                    query.Parameters.AddWithValue("@ac_in_session_won", ManejoNulos.ManageNullDecimal(item.ac_in_session_won));
                    query.Parameters.AddWithValue("@ac_in_session_re_balance", ManejoNulos.ManageNullDecimal(item.ac_in_session_re_balance));
                    query.Parameters.AddWithValue("@ac_in_session_promo_re_balance", ManejoNulos.ManageNullDecimal(item.ac_in_session_promo_re_balance));
                    query.Parameters.AddWithValue("@ac_in_session_promo_nr_balance", ManejoNulos.ManageNullDecimal(item.ac_in_session_promo_nr_balance));
                    query.Parameters.AddWithValue("@ac_in_session_re_to_gm", ManejoNulos.ManageNullDecimal(item.ac_in_session_re_to_gm));
                    query.Parameters.AddWithValue("@ac_in_session_promo_re_to_gm", ManejoNulos.ManageNullDecimal(item.ac_in_session_promo_re_to_gm));
                    query.Parameters.AddWithValue("@ac_in_session_promo_nr_to_gm", ManejoNulos.ManageNullDecimal(item.ac_in_session_promo_nr_to_gm));
                    query.Parameters.AddWithValue("@ac_in_session_re_from_gm", ManejoNulos.ManageNullDecimal(item.ac_in_session_re_from_gm));
                    query.Parameters.AddWithValue("@ac_in_session_promo_re_from_gm", ManejoNulos.ManageNullDecimal(item.ac_in_session_promo_re_from_gm));
                    query.Parameters.AddWithValue("@ac_in_session_promo_nr_from_gm", ManejoNulos.ManageNullDecimal(item.ac_in_session_promo_nr_from_gm));
                    query.Parameters.AddWithValue("@ac_in_session_re_played", ManejoNulos.ManageNullDecimal(item.ac_in_session_re_played));
                    query.Parameters.AddWithValue("@ac_in_session_nr_played", ManejoNulos.ManageNullDecimal(item.ac_in_session_nr_played));
                    query.Parameters.AddWithValue("@ac_in_session_re_won", ManejoNulos.ManageNullDecimal(item.ac_in_session_re_won));
                    query.Parameters.AddWithValue("@ac_in_session_nr_won", ManejoNulos.ManageNullDecimal(item.ac_in_session_nr_won));
                    query.Parameters.AddWithValue("@ac_in_session_re_cancellable", ManejoNulos.ManageNullDecimal(item.ac_in_session_re_cancellable));
                    query.Parameters.AddWithValue("@ac_in_session_promo_re_cancellable", ManejoNulos.ManageNullDecimal(item.ac_in_session_promo_re_cancellable));
                    query.Parameters.AddWithValue("@ac_in_session_promo_nr_cancellable", ManejoNulos.ManageNullDecimal(item.ac_in_session_promo_nr_cancellable));
                    query.Parameters.AddWithValue("@ac_in_session_cancellable_transaction_id", ManejoNulos.ManageNullInteger64(item.ac_in_session_cancellable_transaction_id));
                    query.Parameters.AddWithValue("@ac_holder_id_type", ManejoNulos.ManageNullInteger(item.ac_holder_id_type));
                    query.Parameters.AddWithValue("@ac_promo_ini_re_balance", ManejoNulos.ManageNullDecimal(item.ac_promo_ini_re_balance));
                    query.Parameters.AddWithValue("@ac_holder_is_vip", ManejoNulos.ManegeNullBool(item.ac_holder_is_vip));
                    query.Parameters.AddWithValue("@ac_holder_title", ManejoNulos.ManageNullStr(item.ac_holder_title));
                    query.Parameters.AddWithValue("@ac_holder_name4", ManejoNulos.ManageNullStr(item.ac_holder_name4));
                    query.Parameters.AddWithValue("@ac_holder_wedding_date", ManejoNulos.ManageNullDate(item.ac_holder_wedding_date));
                    query.Parameters.AddWithValue("@ac_holder_phone_type_01", ManejoNulos.ManageNullInteger(item.ac_holder_phone_type_01));
                    query.Parameters.AddWithValue("@ac_holder_phone_type_02", ManejoNulos.ManageNullInteger(item.ac_holder_phone_type_02));
                    query.Parameters.AddWithValue("@ac_holder_state", ManejoNulos.ManageNullStr(item.ac_holder_state));
                    query.Parameters.AddWithValue("@ac_holder_country", ManejoNulos.ManageNullStr(item.ac_holder_country));
                    query.Parameters.AddWithValue("@ac_holder_address_01_alt", ManejoNulos.ManageNullStr(item.ac_holder_address_01_alt));
                    query.Parameters.AddWithValue("@ac_holder_address_02_alt", ManejoNulos.ManageNullStr(item.ac_holder_address_02_alt));
                    query.Parameters.AddWithValue("@ac_holder_address_03_alt", ManejoNulos.ManageNullStr(item.ac_holder_address_03_alt));
                    query.Parameters.AddWithValue("@ac_holder_city_alt", ManejoNulos.ManageNullStr(item.ac_holder_city_alt));
                    query.Parameters.AddWithValue("@ac_holder_zip_alt", ManejoNulos.ManageNullStr(item.ac_holder_zip_alt));
                    query.Parameters.AddWithValue("@ac_holder_state_alt", ManejoNulos.ManageNullStr(item.ac_holder_state_alt));
                    query.Parameters.AddWithValue("@ac_holder_country_alt", ManejoNulos.ManageNullStr(item.ac_holder_country_alt));
                    query.Parameters.AddWithValue("@ac_holder_is_smoker", ManejoNulos.ManegeNullBool(item.ac_holder_is_smoker));
                    query.Parameters.AddWithValue("@ac_holder_nickname", ManejoNulos.ManageNullStr(item.ac_holder_nickname));
                    query.Parameters.AddWithValue("@ac_holder_credit_limit", ManejoNulos.ManageNullDecimal(item.ac_holder_credit_limit));
                    query.Parameters.AddWithValue("@ac_holder_request_credit_limit", ManejoNulos.ManageNullDecimal(item.ac_holder_request_credit_limit));
                    query.Parameters.AddWithValue("@ac_pin_last_modified", ManejoNulos.ManageNullDate(item.ac_pin_last_modified));
                    query.Parameters.AddWithValue("@ac_last_activity_site_id", ManejoNulos.ManageNullStr(item.ac_last_activity_site_id));
                    query.Parameters.AddWithValue("@ac_creation_site_id", ManejoNulos.ManageNullStr(item.ac_creation_site_id));
                    query.Parameters.AddWithValue("@ac_last_update_site_id", ManejoNulos.ManageNullStr(item.ac_last_update_site_id));
                    query.Parameters.AddWithValue("@ac_external_reference", ManejoNulos.ManageNullStr(item.ac_external_reference));
                    query.Parameters.AddWithValue("@ac_points_status", ManejoNulos.ManageNullInteger(item.ac_points_status));
                    query.Parameters.AddWithValue("@ac_ms_has_local_changes", ManejoNulos.ManegeNullBool(item.ac_ms_has_local_changes));
                    query.Parameters.AddWithValue("@ac_ms_change_guid", ManejoNulos.ManageNullGuid(item.ac_ms_change_guid));
                    query.Parameters.AddWithValue("@ac_ms_created_on_site_id", ManejoNulos.ManageNullInteger(item.ac_ms_created_on_site_id));
                    query.Parameters.AddWithValue("@ac_ms_modified_on_site_id", ManejoNulos.ManageNullInteger(item.ac_ms_modified_on_site_id));
                    query.Parameters.AddWithValue("@ac_ms_last_site_id", ManejoNulos.ManageNullInteger(item.ac_ms_last_site_id));
                    query.Parameters.AddWithValue("@ac_ms_points_seq_id", ManejoNulos.ManageNullInteger64(item.ac_ms_points_seq_id));
                    query.Parameters.AddWithValue("@ac_ms_points_synchronized", ManejoNulos.ManageNullDate(item.ac_ms_points_synchronized));
                    query.Parameters.AddWithValue("@ac_ms_personal_info_seq_id", ManejoNulos.ManageNullInteger64(item.ac_ms_personal_info_seq_id));
                    query.Parameters.AddWithValue("@ac_ms_hash", ManejoNulos.ManageNullByteArray(item.ac_ms_hash));
                    query.Parameters.AddWithValue("@ac_holder_twitter_account", ManejoNulos.ManageNullStr(item.ac_holder_twitter_account));
                    query.Parameters.AddWithValue("@ac_block_description", ManejoNulos.ManageNullStr(item.ac_block_description));
                    query.Parameters.AddWithValue("@ac_holder_occupation", ManejoNulos.ManageNullStr(item.ac_holder_occupation));
                    query.Parameters.AddWithValue("@ac_holder_ext_num", ManejoNulos.ManageNullStr(item.ac_holder_ext_num));
                    query.Parameters.AddWithValue("@ac_holder_nationality", ManejoNulos.ManageNullInteger(item.ac_holder_nationality));
                    query.Parameters.AddWithValue("@ac_holder_birth_country", ManejoNulos.ManageNullInteger(item.ac_holder_birth_country));
                    query.Parameters.AddWithValue("@ac_holder_fed_entity", ManejoNulos.ManageNullInteger(item.ac_holder_fed_entity));
                    query.Parameters.AddWithValue("@ac_holder_id1_type", ManejoNulos.ManageNullInteger(item.ac_holder_id1_type));
                    query.Parameters.AddWithValue("@ac_holder_id2_type", ManejoNulos.ManageNullInteger(item.ac_holder_id2_type));
                    query.Parameters.AddWithValue("@ac_holder_id3_type", ManejoNulos.ManageNullStr(item.ac_holder_id3_type));
                    query.Parameters.AddWithValue("@ac_holder_id3", ManejoNulos.ManageNullStr(item.ac_holder_id3));
                    query.Parameters.AddWithValue("@ac_beneficiary_name", ManejoNulos.ManageNullStr(item.ac_beneficiary_name));
                    query.Parameters.AddWithValue("@ac_beneficiary_name1", ManejoNulos.ManageNullStr(item.ac_beneficiary_name1));
                    query.Parameters.AddWithValue("@ac_beneficiary_name2", ManejoNulos.ManageNullStr(item.ac_beneficiary_name2));
                    query.Parameters.AddWithValue("@ac_beneficiary_name3", ManejoNulos.ManageNullStr(item.ac_beneficiary_name3));
                    query.Parameters.AddWithValue("@ac_beneficiary_birth_date", ManejoNulos.ManageNullDate(item.ac_beneficiary_birth_date));
                    query.Parameters.AddWithValue("@ac_beneficiary_gender", ManejoNulos.ManageNullInteger(item.ac_beneficiary_gender));
                    query.Parameters.AddWithValue("@ac_beneficiary_occupation", ManejoNulos.ManageNullStr(item.ac_beneficiary_occupation));
                    query.Parameters.AddWithValue("@ac_beneficiary_id1_type", ManejoNulos.ManageNullInteger(item.ac_beneficiary_id1_type));
                    query.Parameters.AddWithValue("@ac_beneficiary_id1", ManejoNulos.ManageNullStr(item.ac_beneficiary_id1));
                    query.Parameters.AddWithValue("@ac_beneficiary_id2_type", ManejoNulos.ManageNullInteger(item.ac_beneficiary_id2_type));
                    query.Parameters.AddWithValue("@ac_beneficiary_id2", ManejoNulos.ManageNullStr(item.ac_beneficiary_id2));
                    query.Parameters.AddWithValue("@ac_beneficiary_id3_type", ManejoNulos.ManageNullStr(item.ac_beneficiary_id3_type));
                    query.Parameters.AddWithValue("@ac_beneficiary_id3", ManejoNulos.ManageNullStr(item.ac_beneficiary_id3));
                    query.Parameters.AddWithValue("@ac_holder_has_beneficiary", ManejoNulos.ManegeNullBool(item.ac_holder_has_beneficiary));
                    query.Parameters.AddWithValue("@ac_holder_occupation_id", ManejoNulos.ManageNullInteger(item.ac_holder_occupation_id));
                    query.Parameters.AddWithValue("@ac_beneficiary_occupation_id", ManejoNulos.ManageNullInteger(item.ac_beneficiary_occupation_id));
                    query.Parameters.AddWithValue("@ac_card_replacement_count", ManejoNulos.ManageNullInteger(item.ac_card_replacement_count));
                    query.Parameters.AddWithValue("@ac_show_comments_on_cashier", ManejoNulos.ManegeNullBool(item.ac_show_comments_on_cashier));
                    query.Parameters.AddWithValue("@ac_external_aml_file_updated", ManejoNulos.ManageNullDate(item.ac_external_aml_file_updated));
                    query.Parameters.AddWithValue("@ac_holder_address_country", ManejoNulos.ManageNullInteger(item.ac_holder_address_country));
                    query.Parameters.AddWithValue("@ac_holder_address_validation", ManejoNulos.ManageNullInteger(item.ac_holder_address_validation));
                    query.Parameters.AddWithValue("@ac_re_reserved", ManejoNulos.ManageNullDecimal(item.ac_re_reserved));
                    query.Parameters.AddWithValue("@ac_mode_reserved", ManejoNulos.ManegeNullBool(item.ac_mode_reserved));
                    query.Parameters.AddWithValue("@ac_external_aml_file_sequence", ManejoNulos.ManageNullGuid(item.ac_external_aml_file_sequence));
                    query.Parameters.AddWithValue("@ac_provision", ManejoNulos.ManageNullDecimal(item.ac_provision));
                    query.Parameters.AddWithValue("@ac_egm_reserved", ManejoNulos.ManageNullInteger(item.ac_egm_reserved));
                    query.Parameters.AddWithValue("@ac_last_update_in_local_time", ManejoNulos.ManageNullDate(item.ac_last_update_in_local_time));
                    query.Parameters.AddWithValue("@ac_last_update_in_utc_time", ManejoNulos.ManageNullDate(item.ac_last_update_in_utc_time));
                    query.Parameters.AddWithValue("@ac_sales_chips_rounding_amount", ManejoNulos.ManageNullDecimal(item.ac_sales_chips_rounding_amount));
                    query.Parameters.AddWithValue("@ac_show_holder_name", ManejoNulos.ManegeNullBool(item.ac_show_holder_name));
                    query.Parameters.AddWithValue("@ac_constancy_printed", ManejoNulos.ManageNullDate(item.ac_constancy_printed));
                    query.Parameters.AddWithValue("@ac_constancy_signed", ManejoNulos.ManageNullDate(item.ac_constancy_signed));
                    query.Parameters.AddWithValue("@ac_holder_country_phone_code", ManejoNulos.ManageNullInteger(item.ac_holder_country_phone_code));
                    query.Parameters.AddWithValue("@ac_method_contact", ManejoNulos.ManageNullInteger(item.ac_method_contact));
                    query.Parameters.AddWithValue("@ac_fiscal_address", ManejoNulos.ManageNullInteger(item.ac_fiscal_address));
                    query.Parameters.AddWithValue("@ac_mailing_address", ManejoNulos.ManageNullInteger(item.ac_mailing_address));
                    query.Parameters.AddWithValue("@ac_tax_form_expiration_date", ManejoNulos.ManageNullDate(item.ac_tax_form_expiration_date));
                    query.Parameters.AddWithValue("@ac_holder_country_phone_code_02", ManejoNulos.ManageNullInteger(item.ac_holder_country_phone_code_02));
                    query.Parameters.AddWithValue("@ac_host_assigned_id", ManejoNulos.ManageNullInteger(item.ac_host_assigned_id));
                    query.Parameters.AddWithValue("@ac_host_assigned_date", ManejoNulos.ManageNullDate(item.ac_host_assigned_date));
                    query.Parameters.AddWithValue("@ac_holder_email_validation_status", ManejoNulos.ManageNullInteger(item.ac_holder_email_validation_status));
                    query.Parameters.AddWithValue("@ac_holder_email_last_send", ManejoNulos.ManageNullDate(item.ac_holder_email_last_send));
                    query.Parameters.AddWithValue("@ac_holder_ssn", ManejoNulos.ManageNullStr(item.ac_holder_ssn));
                    query.Parameters.AddWithValue("@ac_bad_address1", ManejoNulos.ManageNullInteger(item.ac_bad_address1));
                    query.Parameters.AddWithValue("@ac_bad_address2", ManejoNulos.ManageNullInteger(item.ac_bad_address2));
                    query.Parameters.AddWithValue("@ac_no_mail1", ManejoNulos.ManageNullInteger(item.ac_no_mail1));
                    query.Parameters.AddWithValue("@ac_no_mail2", ManejoNulos.ManageNullInteger(item.ac_no_mail2));
                    query.Parameters.AddWithValue("@ac_preferred_method", ManejoNulos.ManageNullInteger(item.ac_preferred_method));
                    query.Parameters.AddWithValue("@ac_holder_id_indexed", ManejoNulos.ManageNullStr(item.ac_holder_id_indexed));
                    query.Parameters.AddWithValue("@ac_holder_ext_num_alt", ManejoNulos.ManageNullStr(item.ac_holder_ext_num_alt));
                    query.Parameters.AddWithValue("@ac_holder_level_change_comments", ManejoNulos.ManageNullStr(item.ac_holder_level_change_comments));
                    query.Parameters.AddWithValue("@ac_in_session_plays_count", ManejoNulos.ManageNullInteger(item.ac_in_session_plays_count));
                    query.Parameters.AddWithValue("@ac_holder_address_01_tertiary", ManejoNulos.ManageNullStr(item.ac_holder_address_01_tertiary));
                    query.Parameters.AddWithValue("@ac_holder_address_02_tertiary", ManejoNulos.ManageNullStr(item.ac_holder_address_02_tertiary));
                    query.Parameters.AddWithValue("@ac_holder_address_03_tertiary", ManejoNulos.ManageNullStr(item.ac_holder_address_03_tertiary));
                    query.Parameters.AddWithValue("@ac_holder_ext_num_tertiary", ManejoNulos.ManageNullStr(item.ac_holder_ext_num_tertiary));
                    query.Parameters.AddWithValue("@ac_holder_country_tertiary", ManejoNulos.ManageNullStr(item.ac_holder_country_tertiary));
                    query.Parameters.AddWithValue("@ac_holder_state_tertiary", ManejoNulos.ManageNullStr(item.ac_holder_state_tertiary));
                    query.Parameters.AddWithValue("@ac_holder_city_tertiary", ManejoNulos.ManageNullStr(item.ac_holder_city_tertiary));
                    query.Parameters.AddWithValue("@ac_holder_zip_tertiary", ManejoNulos.ManageNullStr(item.ac_holder_zip_tertiary));
                    query.Parameters.AddWithValue("@ac_bad_address3", ManejoNulos.ManageNullInteger(item.ac_bad_address3));
                    query.Parameters.AddWithValue("@ac_no_mail3", ManejoNulos.ManageNullInteger(item.ac_no_mail3));
                    query.Parameters.AddWithValue("@ac_in_session_cash_in", ManejoNulos.ManageNullDecimal(item.ac_in_session_cash_in));
                    query.Parameters.AddWithValue("@ac_in_session_promo_ticket_re_in", ManejoNulos.ManageNullDecimal(item.ac_in_session_promo_ticket_re_in));
                    query.Parameters.AddWithValue("@ac_in_session_promo_ticket_nr_in", ManejoNulos.ManageNullDecimal(item.ac_in_session_promo_ticket_nr_in));
                    IdInsertado = Convert.ToInt32(query.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                IdInsertado = 0;
            }
            return IdInsertado;
        }
        public int GetLastIdInserted() {
            int total = 0;

            string query = @"
            select top 1 ac_account_id as lastid from 
            [dbo].[accounts]
            order by ac_account_id desc
";

            try
            {
                using (SqlConnection conecction = new SqlConnection(_conexion_wgdb_000_migration))
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
