using ServicioServidorVPN.utilitarios;
using ServicioServidorVPN.WGDB_000.model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioServidorVPN.WGDB_000.dal
{
    public class accounts_dal
    {
        private readonly string bd_username = string.Empty;
        private readonly string bd_password = string.Empty;
        private readonly string bd_datasource = string.Empty;
        private readonly string connectionString = string.Empty;
        public accounts_dal(string database_name)
        {
            bd_username = ConfigurationManager.AppSettings["bd_username"];
            bd_password = ConfigurationManager.AppSettings["bd_password"];
            bd_datasource = ConfigurationManager.AppSettings["bd_datasource"];
            connectionString = $"Data Source={bd_datasource};Initial Catalog={database_name};Integrated Security=False;User ID={bd_username};Password={bd_password}";

        }
        public long SaveAccounts(accounts item)
        {
            //bool respuesta = false;
            long IdInsertado = 0;
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
    --output inserted.ac_account_id
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
                using (var con = new SqlConnection(connectionString))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@ac_account_id", item.ac_account_id == null ? DBNull.Value : (object)item.ac_account_id);
                    query.Parameters.AddWithValue("@ac_type", item.ac_type == null ? DBNull.Value : (object)item.ac_type);
                    query.Parameters.AddWithValue("@ac_holder_name", item.ac_holder_name == null ? DBNull.Value: (object)item.ac_holder_name);
                    query.Parameters.AddWithValue("@ac_blocked", item.ac_blocked == null ? DBNull.Value : (object)item.ac_blocked);
                    query.Parameters.AddWithValue("@ac_not_valid_before", item.ac_not_valid_before == null ? DBNull.Value : (object)item.ac_not_valid_before);
                    query.Parameters.AddWithValue("@ac_not_valid_after", item.ac_not_valid_after == null ? DBNull.Value : (object)item.ac_not_valid_after);
                    query.Parameters.AddWithValue("@ac_balance", item.ac_balance == null ? DBNull.Value : (object)item.ac_balance);
                    query.Parameters.AddWithValue("@ac_cash_in", item.ac_cash_in == null ? DBNull.Value : (object)item.ac_cash_in);
                    query.Parameters.AddWithValue("@ac_cash_won", item.ac_cash_won == null ? DBNull.Value : (object)item.ac_cash_won);
                    query.Parameters.AddWithValue("@ac_not_redeemable", item.ac_not_redeemable == null ? DBNull.Value : (object)item.ac_not_redeemable);
                    query.Parameters.AddWithValue("@ac_track_data", item.ac_track_data == null ? DBNull.Value: (object)item.ac_track_data);
                    query.Parameters.AddWithValue("@ac_total_cash_in", item.ac_total_cash_in == null ? DBNull.Value : (object)item.ac_total_cash_in);
                    query.Parameters.AddWithValue("@ac_total_cash_out", item.ac_total_cash_out == null ? DBNull.Value : (object)item.ac_total_cash_out);
                    query.Parameters.AddWithValue("@ac_initial_cash_in", item.ac_initial_cash_in == null ? DBNull.Value : (object)item.ac_initial_cash_in);
                    query.Parameters.AddWithValue("@ac_activated", item.ac_activated == null ? DBNull.Value : (object)item.ac_activated);
                    query.Parameters.AddWithValue("@ac_deposit", item.ac_deposit == null ? DBNull.Value : (object)item.ac_deposit);
                    query.Parameters.AddWithValue("@ac_current_terminal_id", item.ac_current_terminal_id == null ? DBNull.Value : (object)item.ac_current_terminal_id);
                    query.Parameters.AddWithValue("@ac_current_terminal_name", item.ac_current_terminal_name == null ? DBNull.Value: (object)item.ac_current_terminal_name);
                    query.Parameters.AddWithValue("@ac_current_play_session_id", item.ac_current_play_session_id == null ? DBNull.Value : (object)item.ac_current_play_session_id);
                    query.Parameters.AddWithValue("@ac_last_terminal_id", item.ac_last_terminal_id == null ? DBNull.Value : (object)item.ac_last_terminal_id);
                    query.Parameters.AddWithValue("@ac_last_terminal_name", item.ac_last_terminal_name == null ? DBNull.Value: (object)item.ac_last_terminal_name);
                    query.Parameters.AddWithValue("@ac_last_play_session_id", item.ac_last_play_session_id == null ? DBNull.Value : (object)item.ac_last_play_session_id);
                    query.Parameters.AddWithValue("@ac_user_type", item.ac_user_type == null ? DBNull.Value : (object)item.ac_user_type);
                    query.Parameters.AddWithValue("@ac_points", item.ac_points == null ? DBNull.Value : (object)item.ac_points);
                    query.Parameters.AddWithValue("@ac_initial_not_redeemable", item.ac_initial_not_redeemable == null ? DBNull.Value : (object)item.ac_initial_not_redeemable);
                    query.Parameters.AddWithValue("@ac_created", item.ac_created == null ? DBNull.Value : (object)item.ac_created);
                    query.Parameters.AddWithValue("@ac_promo_balance", item.ac_promo_balance == null ? DBNull.Value : (object)item.ac_promo_balance);
                    query.Parameters.AddWithValue("@ac_promo_limit", item.ac_promo_limit == null ? DBNull.Value : (object)item.ac_promo_limit);
                    query.Parameters.AddWithValue("@ac_promo_creation", item.ac_promo_creation == null ? DBNull.Value : (object)item.ac_promo_creation);
                    query.Parameters.AddWithValue("@ac_promo_expiration", item.ac_promo_expiration == null ? DBNull.Value : (object)item.ac_promo_expiration);
                    query.Parameters.AddWithValue("@ac_last_activity", item.ac_last_activity == null ? DBNull.Value : (object)item.ac_last_activity);
                    query.Parameters.AddWithValue("@ac_holder_id", item.ac_holder_id == null ? DBNull.Value: (object)item.ac_holder_id);
                    query.Parameters.AddWithValue("@ac_holder_address_01", item.ac_holder_address_01 == null ? DBNull.Value: (object)item.ac_holder_address_01);
                    query.Parameters.AddWithValue("@ac_holder_address_02", item.ac_holder_address_02 == null ? DBNull.Value: (object)item.ac_holder_address_02);
                    query.Parameters.AddWithValue("@ac_holder_address_03", item.ac_holder_address_03 == null ? DBNull.Value: (object)item.ac_holder_address_03);
                    query.Parameters.AddWithValue("@ac_holder_city", item.ac_holder_city == null ? DBNull.Value: (object)item.ac_holder_city);
                    query.Parameters.AddWithValue("@ac_holder_zip", item.ac_holder_zip == null ? DBNull.Value: (object)item.ac_holder_zip);
                    query.Parameters.AddWithValue("@ac_holder_email_01", item.ac_holder_email_01 == null ? DBNull.Value: (object)item.ac_holder_email_01);
                    query.Parameters.AddWithValue("@ac_holder_email_02", item.ac_holder_email_02 == null ? DBNull.Value: (object)item.ac_holder_email_02);
                    query.Parameters.AddWithValue("@ac_holder_phone_number_01", item.ac_holder_phone_number_01 == null ? DBNull.Value: (object)item.ac_holder_phone_number_01);
                    query.Parameters.AddWithValue("@ac_holder_phone_number_02", item.ac_holder_phone_number_02 == null ? DBNull.Value: (object)item.ac_holder_phone_number_02);
                    query.Parameters.AddWithValue("@ac_holder_comments", item.ac_holder_comments == null ? DBNull.Value: (object)item.ac_holder_comments);
                    query.Parameters.AddWithValue("@ac_holder_gender", item.ac_holder_gender == null ? DBNull.Value : (object)item.ac_holder_gender);
                    query.Parameters.AddWithValue("@ac_holder_marital_status", item.ac_holder_marital_status == null ? DBNull.Value : (object)item.ac_holder_marital_status);
                    query.Parameters.AddWithValue("@ac_holder_birth_date", item.ac_holder_birth_date == null ? DBNull.Value : (object)item.ac_holder_birth_date);
                    query.Parameters.AddWithValue("@ac_draw_last_play_session_id", item.ac_draw_last_play_session_id == null ? DBNull.Value : (object)item.ac_draw_last_play_session_id);
                    query.Parameters.AddWithValue("@ac_draw_last_play_session_remainder", item.ac_draw_last_play_session_remainder == null ? DBNull.Value : (object)item.ac_draw_last_play_session_remainder);
                    query.Parameters.AddWithValue("@ac_nr_won_lock", item.ac_nr_won_lock == null ? DBNull.Value : (object)item.ac_nr_won_lock);
                    query.Parameters.AddWithValue("@ac_nr_expiration", item.ac_nr_expiration == null ? DBNull.Value : (object)item.ac_nr_expiration);
                    query.Parameters.AddWithValue("@ac_cashin_while_playing", item.ac_cashin_while_playing == null ? DBNull.Value : (object)item.ac_cashin_while_playing);
                    query.Parameters.AddWithValue("@ac_holder_level", item.ac_holder_level == null ? DBNull.Value : (object)item.ac_holder_level);
                    query.Parameters.AddWithValue("@ac_card_paid", item.ac_card_paid == null ? DBNull.Value : (object)item.ac_card_paid);
                    query.Parameters.AddWithValue("@ac_cancellable_operation_id", item.ac_cancellable_operation_id == null ? DBNull.Value : (object)item.ac_cancellable_operation_id);
                    query.Parameters.AddWithValue("@ac_current_promotion_id", item.ac_current_promotion_id == null ? DBNull.Value : (object)item.ac_current_promotion_id);
                    query.Parameters.AddWithValue("@ac_block_reason", item.ac_block_reason == null ? DBNull.Value : (object)item.ac_block_reason);
                    query.Parameters.AddWithValue("@ac_holder_level_expiration", item.ac_holder_level_expiration == null ? DBNull.Value : (object)item.ac_holder_level_expiration);
                    query.Parameters.AddWithValue("@ac_holder_level_entered", item.ac_holder_level_entered == null ? DBNull.Value : (object)item.ac_holder_level_entered);
                    query.Parameters.AddWithValue("@ac_holder_level_notify", item.ac_holder_level_notify == null ? DBNull.Value : (object)item.ac_holder_level_notify);
                    query.Parameters.AddWithValue("@ac_pin", item.ac_pin == null ? DBNull.Value: (object)item.ac_pin);
                    query.Parameters.AddWithValue("@ac_pin_failures", item.ac_pin_failures == null ? DBNull.Value : (object)item.ac_pin_failures);
                    query.Parameters.AddWithValue("@ac_holder_name1", item.ac_holder_name1 == null ? DBNull.Value: (object)item.ac_holder_name1);
                    query.Parameters.AddWithValue("@ac_holder_name2", item.ac_holder_name2 == null ? DBNull.Value: (object)item.ac_holder_name2);
                    query.Parameters.AddWithValue("@ac_holder_name3", item.ac_holder_name3 == null ? DBNull.Value: (object)item.ac_holder_name3);
                    query.Parameters.AddWithValue("@ac_holder_id1", item.ac_holder_id1 == null ? DBNull.Value: (object)item.ac_holder_id1);
                    query.Parameters.AddWithValue("@ac_holder_id2", item.ac_holder_id2 == null ? DBNull.Value: (object)item.ac_holder_id2);
                    query.Parameters.AddWithValue("@ac_holder_document_id1", item.ac_holder_document_id1 == null ? DBNull.Value : (object)item.ac_holder_document_id1);
                    query.Parameters.AddWithValue("@ac_holder_document_id2", item.ac_holder_document_id2 == null ? DBNull.Value : (object)item.ac_holder_document_id2);
                    query.Parameters.AddWithValue("@ac_nr2_expiration", item.ac_nr2_expiration == null ? DBNull.Value : (object)item.ac_nr2_expiration);
                    query.Parameters.AddWithValue("@ac_recommended_by", item.ac_recommended_by == null ? DBNull.Value : (object)item.ac_recommended_by);
                    query.Parameters.AddWithValue("@ac_re_balance", item.ac_re_balance == null ? DBNull.Value : (object)item.ac_re_balance);
                    query.Parameters.AddWithValue("@ac_promo_re_balance", item.ac_promo_re_balance == null ? DBNull.Value : (object)item.ac_promo_re_balance);
                    query.Parameters.AddWithValue("@ac_promo_nr_balance", item.ac_promo_nr_balance == null ? DBNull.Value : (object)item.ac_promo_nr_balance);
                    query.Parameters.AddWithValue("@ac_in_session_played", item.ac_in_session_played == null ? DBNull.Value : (object)item.ac_in_session_played);
                    query.Parameters.AddWithValue("@ac_in_session_won", item.ac_in_session_won == null ? DBNull.Value : (object)item.ac_in_session_won);
                    query.Parameters.AddWithValue("@ac_in_session_re_balance", item.ac_in_session_re_balance == null ? DBNull.Value : (object)item.ac_in_session_re_balance);
                    query.Parameters.AddWithValue("@ac_in_session_promo_re_balance", item.ac_in_session_promo_re_balance == null ? DBNull.Value : (object)item.ac_in_session_promo_re_balance);
                    query.Parameters.AddWithValue("@ac_in_session_promo_nr_balance", item.ac_in_session_promo_nr_balance == null ? DBNull.Value : (object)item.ac_in_session_promo_nr_balance);
                    query.Parameters.AddWithValue("@ac_in_session_re_to_gm", item.ac_in_session_re_to_gm == null ? DBNull.Value : (object)item.ac_in_session_re_to_gm);
                    query.Parameters.AddWithValue("@ac_in_session_promo_re_to_gm", item.ac_in_session_promo_re_to_gm == null ? DBNull.Value : (object)item.ac_in_session_promo_re_to_gm);
                    query.Parameters.AddWithValue("@ac_in_session_promo_nr_to_gm", item.ac_in_session_promo_nr_to_gm == null ? DBNull.Value : (object)item.ac_in_session_promo_nr_to_gm);
                    query.Parameters.AddWithValue("@ac_in_session_re_from_gm", item.ac_in_session_re_from_gm == null ? DBNull.Value : (object)item.ac_in_session_re_from_gm);
                    query.Parameters.AddWithValue("@ac_in_session_promo_re_from_gm", item.ac_in_session_promo_re_from_gm == null ? DBNull.Value : (object)item.ac_in_session_promo_re_from_gm);
                    query.Parameters.AddWithValue("@ac_in_session_promo_nr_from_gm", item.ac_in_session_promo_nr_from_gm == null ? DBNull.Value : (object)item.ac_in_session_promo_nr_from_gm);
                    query.Parameters.AddWithValue("@ac_in_session_re_played", item.ac_in_session_re_played == null ? DBNull.Value : (object)item.ac_in_session_re_played);
                    query.Parameters.AddWithValue("@ac_in_session_nr_played", item.ac_in_session_nr_played == null ? DBNull.Value : (object)item.ac_in_session_nr_played);
                    query.Parameters.AddWithValue("@ac_in_session_re_won", item.ac_in_session_re_won == null ? DBNull.Value : (object)item.ac_in_session_re_won);
                    query.Parameters.AddWithValue("@ac_in_session_nr_won", item.ac_in_session_nr_won == null ? DBNull.Value : (object)item.ac_in_session_nr_won);
                    query.Parameters.AddWithValue("@ac_in_session_re_cancellable", item.ac_in_session_re_cancellable == null ? DBNull.Value : (object)item.ac_in_session_re_cancellable);
                    query.Parameters.AddWithValue("@ac_in_session_promo_re_cancellable", item.ac_in_session_promo_re_cancellable == null ? DBNull.Value : (object)item.ac_in_session_promo_re_cancellable);
                    query.Parameters.AddWithValue("@ac_in_session_promo_nr_cancellable", item.ac_in_session_promo_nr_cancellable == null ? DBNull.Value : (object)item.ac_in_session_promo_nr_cancellable);
                    query.Parameters.AddWithValue("@ac_in_session_cancellable_transaction_id", item.ac_in_session_cancellable_transaction_id == null ? DBNull.Value : (object)item.ac_in_session_cancellable_transaction_id);
                    query.Parameters.AddWithValue("@ac_holder_id_type", item.ac_holder_id_type == null ? DBNull.Value : (object)item.ac_holder_id_type);
                    query.Parameters.AddWithValue("@ac_promo_ini_re_balance", item.ac_promo_ini_re_balance == null ? DBNull.Value : (object)item.ac_promo_ini_re_balance);
                    query.Parameters.AddWithValue("@ac_holder_is_vip", item.ac_holder_is_vip == null ? DBNull.Value : (object)item.ac_holder_is_vip);
                    query.Parameters.AddWithValue("@ac_holder_title", item.ac_holder_title == null ? DBNull.Value: (object)item.ac_holder_title);
                    query.Parameters.AddWithValue("@ac_holder_name4", item.ac_holder_name4 == null ? DBNull.Value: (object)item.ac_holder_name4);
                    query.Parameters.AddWithValue("@ac_holder_wedding_date", item.ac_holder_wedding_date == null ? DBNull.Value : (object)item.ac_holder_wedding_date);
                    query.Parameters.AddWithValue("@ac_holder_phone_type_01", item.ac_holder_phone_type_01 == null ? DBNull.Value : (object)item.ac_holder_phone_type_01);
                    query.Parameters.AddWithValue("@ac_holder_phone_type_02", item.ac_holder_phone_type_02 == null ? DBNull.Value : (object)item.ac_holder_phone_type_02);
                    query.Parameters.AddWithValue("@ac_holder_state", item.ac_holder_state == null ? DBNull.Value: (object)item.ac_holder_state);
                    query.Parameters.AddWithValue("@ac_holder_country", item.ac_holder_country == null ? DBNull.Value: (object)item.ac_holder_country);
                    query.Parameters.AddWithValue("@ac_holder_address_01_alt", item.ac_holder_address_01_alt == null ? DBNull.Value: (object)item.ac_holder_address_01_alt);
                    query.Parameters.AddWithValue("@ac_holder_address_02_alt", item.ac_holder_address_02_alt == null ? DBNull.Value: (object)item.ac_holder_address_02_alt);
                    query.Parameters.AddWithValue("@ac_holder_address_03_alt", item.ac_holder_address_03_alt == null ? DBNull.Value: (object)item.ac_holder_address_03_alt);
                    query.Parameters.AddWithValue("@ac_holder_city_alt", item.ac_holder_city_alt == null ? DBNull.Value: (object)item.ac_holder_city_alt);
                    query.Parameters.AddWithValue("@ac_holder_zip_alt", item.ac_holder_zip_alt == null ? DBNull.Value: (object)item.ac_holder_zip_alt);
                    query.Parameters.AddWithValue("@ac_holder_state_alt", item.ac_holder_state_alt == null ? DBNull.Value: (object)item.ac_holder_state_alt);
                    query.Parameters.AddWithValue("@ac_holder_country_alt", item.ac_holder_country_alt == null ? DBNull.Value: (object)item.ac_holder_country_alt);
                    query.Parameters.AddWithValue("@ac_holder_is_smoker", item.ac_holder_is_smoker == null ? DBNull.Value : (object)item.ac_holder_is_smoker);
                    query.Parameters.AddWithValue("@ac_holder_nickname", item.ac_holder_nickname == null ? DBNull.Value: (object)item.ac_holder_nickname);
                    query.Parameters.AddWithValue("@ac_holder_credit_limit", item.ac_holder_credit_limit == null ? DBNull.Value : (object)item.ac_holder_credit_limit);
                    query.Parameters.AddWithValue("@ac_holder_request_credit_limit", item.ac_holder_request_credit_limit == null ? DBNull.Value : (object)item.ac_holder_request_credit_limit);
                    query.Parameters.AddWithValue("@ac_pin_last_modified", item.ac_pin_last_modified == null ? DBNull.Value : (object)item.ac_pin_last_modified);
                    query.Parameters.AddWithValue("@ac_last_activity_site_id", item.ac_last_activity_site_id == null ? DBNull.Value: (object)item.ac_last_activity_site_id);
                    query.Parameters.AddWithValue("@ac_creation_site_id", item.ac_creation_site_id == null ? DBNull.Value: (object)item.ac_creation_site_id);
                    query.Parameters.AddWithValue("@ac_last_update_site_id", item.ac_last_update_site_id == null ? DBNull.Value: (object)item.ac_last_update_site_id);
                    query.Parameters.AddWithValue("@ac_external_reference", item.ac_external_reference == null ? DBNull.Value: (object)item.ac_external_reference);
                    query.Parameters.AddWithValue("@ac_points_status", item.ac_points_status == null ? DBNull.Value : (object)item.ac_points_status);
                    query.Parameters.AddWithValue("@ac_ms_has_local_changes", item.ac_ms_has_local_changes == null ? DBNull.Value : (object)item.ac_ms_has_local_changes);
                    query.Parameters.AddWithValue("@ac_ms_change_guid", item.ac_ms_change_guid == null ? DBNull.Value : (object)item.ac_ms_change_guid);
                    query.Parameters.AddWithValue("@ac_ms_created_on_site_id", item.ac_ms_created_on_site_id == null ? DBNull.Value : (object)item.ac_ms_created_on_site_id);
                    query.Parameters.AddWithValue("@ac_ms_modified_on_site_id", item.ac_ms_modified_on_site_id == null ? DBNull.Value : (object)item.ac_ms_modified_on_site_id);
                    query.Parameters.AddWithValue("@ac_ms_last_site_id", item.ac_ms_last_site_id == null ? DBNull.Value : (object)item.ac_ms_last_site_id);
                    query.Parameters.AddWithValue("@ac_ms_points_seq_id", item.ac_ms_points_seq_id == null ? DBNull.Value : (object)item.ac_ms_points_seq_id);
                    query.Parameters.AddWithValue("@ac_ms_points_synchronized", item.ac_ms_points_synchronized == null ? DBNull.Value : (object)item.ac_ms_points_synchronized);
                    query.Parameters.AddWithValue("@ac_ms_personal_info_seq_id", item.ac_ms_personal_info_seq_id == null ? DBNull.Value : (object)item.ac_ms_personal_info_seq_id);
                    //query.Parameters.AddWithValue("@ac_ms_hash", item.ac_ms_hash == null ? DBNull.Value : (object)item.ac_ms_hash);
                    query.Parameters.AddWithValue("@ac_holder_twitter_account", item.ac_holder_twitter_account == null ? DBNull.Value: (object)item.ac_holder_twitter_account);
                    query.Parameters.AddWithValue("@ac_block_description", item.ac_block_description == null ? DBNull.Value: (object)item.ac_block_description);
                    query.Parameters.AddWithValue("@ac_holder_occupation", item.ac_holder_occupation == null ? DBNull.Value: (object)item.ac_holder_occupation);
                    query.Parameters.AddWithValue("@ac_holder_ext_num", item.ac_holder_ext_num == null ? DBNull.Value: (object)item.ac_holder_ext_num);
                    query.Parameters.AddWithValue("@ac_holder_nationality", item.ac_holder_nationality == null ? DBNull.Value : (object)item.ac_holder_nationality);
                    query.Parameters.AddWithValue("@ac_holder_birth_country", item.ac_holder_birth_country == null ? DBNull.Value : (object)item.ac_holder_birth_country);
                    query.Parameters.AddWithValue("@ac_holder_fed_entity", item.ac_holder_fed_entity == null ? DBNull.Value : (object)item.ac_holder_fed_entity);
                    query.Parameters.AddWithValue("@ac_holder_id1_type", item.ac_holder_id1_type == null ? DBNull.Value : (object)item.ac_holder_id1_type);
                    query.Parameters.AddWithValue("@ac_holder_id2_type", item.ac_holder_id2_type == null ? DBNull.Value : (object)item.ac_holder_id2_type);
                    query.Parameters.AddWithValue("@ac_holder_id3_type", item.ac_holder_id3_type == null ? DBNull.Value: (object)item.ac_holder_id3_type);
                    query.Parameters.AddWithValue("@ac_holder_id3", item.ac_holder_id3 == null ? DBNull.Value: (object)item.ac_holder_id3);
                    query.Parameters.AddWithValue("@ac_beneficiary_name", item.ac_beneficiary_name == null ? DBNull.Value: (object)item.ac_beneficiary_name);
                    query.Parameters.AddWithValue("@ac_beneficiary_name1", item.ac_beneficiary_name1 == null ? DBNull.Value: (object)item.ac_beneficiary_name1);
                    query.Parameters.AddWithValue("@ac_beneficiary_name2", item.ac_beneficiary_name2 == null ? DBNull.Value: (object)item.ac_beneficiary_name2);
                    query.Parameters.AddWithValue("@ac_beneficiary_name3", item.ac_beneficiary_name3 == null ? DBNull.Value: (object)item.ac_beneficiary_name3);
                    query.Parameters.AddWithValue("@ac_beneficiary_birth_date", item.ac_beneficiary_birth_date == null ? DBNull.Value : (object)item.ac_beneficiary_birth_date);
                    query.Parameters.AddWithValue("@ac_beneficiary_gender", item.ac_beneficiary_gender == null ? DBNull.Value : (object)item.ac_beneficiary_gender);
                    query.Parameters.AddWithValue("@ac_beneficiary_occupation", item.ac_beneficiary_occupation == null ? DBNull.Value: (object)item.ac_beneficiary_occupation);
                    query.Parameters.AddWithValue("@ac_beneficiary_id1_type", item.ac_beneficiary_id1_type == null ? DBNull.Value : (object)item.ac_beneficiary_id1_type);
                    query.Parameters.AddWithValue("@ac_beneficiary_id1", item.ac_beneficiary_id1 == null ? DBNull.Value: (object)item.ac_beneficiary_id1);
                    query.Parameters.AddWithValue("@ac_beneficiary_id2_type", item.ac_beneficiary_id2_type == null ? DBNull.Value : (object)item.ac_beneficiary_id2_type);
                    query.Parameters.AddWithValue("@ac_beneficiary_id2", item.ac_beneficiary_id2 == null ? DBNull.Value: (object)item.ac_beneficiary_id2);
                    query.Parameters.AddWithValue("@ac_beneficiary_id3_type", item.ac_beneficiary_id3_type == null ? DBNull.Value: (object)item.ac_beneficiary_id3_type);
                    query.Parameters.AddWithValue("@ac_beneficiary_id3", item.ac_beneficiary_id3 == null ? DBNull.Value: (object)item.ac_beneficiary_id3);
                    query.Parameters.AddWithValue("@ac_holder_has_beneficiary", item.ac_holder_has_beneficiary == null ? DBNull.Value : (object)item.ac_holder_has_beneficiary);
                    query.Parameters.AddWithValue("@ac_holder_occupation_id", item.ac_holder_occupation_id == null ? DBNull.Value : (object)item.ac_holder_occupation_id);
                    query.Parameters.AddWithValue("@ac_beneficiary_occupation_id", item.ac_beneficiary_occupation_id == null ? DBNull.Value : (object)item.ac_beneficiary_occupation_id);
                    query.Parameters.AddWithValue("@ac_card_replacement_count", item.ac_card_replacement_count == null ? DBNull.Value : (object)item.ac_card_replacement_count);
                    query.Parameters.AddWithValue("@ac_show_comments_on_cashier", item.ac_show_comments_on_cashier == null ? DBNull.Value : (object)item.ac_show_comments_on_cashier);
                    query.Parameters.AddWithValue("@ac_external_aml_file_updated", item.ac_external_aml_file_updated == null ? DBNull.Value : (object)item.ac_external_aml_file_updated);
                    query.Parameters.AddWithValue("@ac_holder_address_country", item.ac_holder_address_country == null ? DBNull.Value : (object)item.ac_holder_address_country);
                    query.Parameters.AddWithValue("@ac_holder_address_validation", item.ac_holder_address_validation == null ? DBNull.Value : (object)item.ac_holder_address_validation);
                    query.Parameters.AddWithValue("@ac_re_reserved", item.ac_re_reserved == null ? DBNull.Value : (object)item.ac_re_reserved);
                    query.Parameters.AddWithValue("@ac_mode_reserved", item.ac_mode_reserved == null ? DBNull.Value : (object)item.ac_mode_reserved);
                    query.Parameters.AddWithValue("@ac_external_aml_file_sequence", item.ac_external_aml_file_sequence == null ? DBNull.Value : (object)item.ac_external_aml_file_sequence);
                    query.Parameters.AddWithValue("@ac_provision", item.ac_provision == null ? DBNull.Value : (object)item.ac_provision);
                    query.Parameters.AddWithValue("@ac_egm_reserved", item.ac_egm_reserved == null ? DBNull.Value : (object)item.ac_egm_reserved);
                    query.Parameters.AddWithValue("@ac_last_update_in_local_time", item.ac_last_update_in_local_time == null ? DBNull.Value : (object)item.ac_last_update_in_local_time);
                    query.Parameters.AddWithValue("@ac_last_update_in_utc_time", item.ac_last_update_in_utc_time == null ? DBNull.Value : (object)item.ac_last_update_in_utc_time);
                    query.Parameters.AddWithValue("@ac_sales_chips_rounding_amount", item.ac_sales_chips_rounding_amount == null ? DBNull.Value : (object)item.ac_sales_chips_rounding_amount);
                    query.Parameters.AddWithValue("@ac_show_holder_name", item.ac_show_holder_name == null ? DBNull.Value : (object)item.ac_show_holder_name);
                    query.Parameters.AddWithValue("@ac_constancy_printed", item.ac_constancy_printed == null ? DBNull.Value : (object)item.ac_constancy_printed);
                    query.Parameters.AddWithValue("@ac_constancy_signed", item.ac_constancy_signed == null ? DBNull.Value : (object)item.ac_constancy_signed);
                    query.Parameters.AddWithValue("@ac_holder_country_phone_code", item.ac_holder_country_phone_code == null ? DBNull.Value : (object)item.ac_holder_country_phone_code);
                    query.Parameters.AddWithValue("@ac_method_contact", item.ac_method_contact == null ? DBNull.Value : (object)item.ac_method_contact);
                    query.Parameters.AddWithValue("@ac_fiscal_address", item.ac_fiscal_address == null ? DBNull.Value : (object)item.ac_fiscal_address);
                    query.Parameters.AddWithValue("@ac_mailing_address", item.ac_mailing_address == null ? DBNull.Value : (object)item.ac_mailing_address);
                    query.Parameters.AddWithValue("@ac_tax_form_expiration_date", item.ac_tax_form_expiration_date == null ? DBNull.Value : (object)item.ac_tax_form_expiration_date);
                    query.Parameters.AddWithValue("@ac_holder_country_phone_code_02", item.ac_holder_country_phone_code_02 == null ? DBNull.Value : (object)item.ac_holder_country_phone_code_02);
                    query.Parameters.AddWithValue("@ac_host_assigned_id", item.ac_host_assigned_id == null ? DBNull.Value : (object)item.ac_host_assigned_id);
                    query.Parameters.AddWithValue("@ac_host_assigned_date", item.ac_host_assigned_date == null ? DBNull.Value : (object)item.ac_host_assigned_date);
                    query.Parameters.AddWithValue("@ac_holder_email_validation_status", item.ac_holder_email_validation_status == null ? DBNull.Value : (object)item.ac_holder_email_validation_status);
                    query.Parameters.AddWithValue("@ac_holder_email_last_send", item.ac_holder_email_last_send == null ? DBNull.Value : (object)item.ac_holder_email_last_send);
                    query.Parameters.AddWithValue("@ac_holder_ssn", item.ac_holder_ssn == null ? DBNull.Value: (object)item.ac_holder_ssn);
                    query.Parameters.AddWithValue("@ac_bad_address1", item.ac_bad_address1 == null ? DBNull.Value : (object)item.ac_bad_address1);
                    query.Parameters.AddWithValue("@ac_bad_address2", item.ac_bad_address2 == null ? DBNull.Value : (object)item.ac_bad_address2);
                    query.Parameters.AddWithValue("@ac_no_mail1", item.ac_no_mail1 == null ? DBNull.Value : (object)item.ac_no_mail1);
                    query.Parameters.AddWithValue("@ac_no_mail2", item.ac_no_mail2 == null ? DBNull.Value : (object)item.ac_no_mail2);
                    query.Parameters.AddWithValue("@ac_preferred_method", item.ac_preferred_method == null ? DBNull.Value : (object)item.ac_preferred_method);
                    query.Parameters.AddWithValue("@ac_holder_id_indexed", item.ac_holder_id_indexed == null ? DBNull.Value: (object)item.ac_holder_id_indexed);
                    query.Parameters.AddWithValue("@ac_holder_ext_num_alt", item.ac_holder_ext_num_alt == null ? DBNull.Value: (object)item.ac_holder_ext_num_alt);
                    query.Parameters.AddWithValue("@ac_holder_level_change_comments", item.ac_holder_level_change_comments == null ? DBNull.Value: (object)item.ac_holder_level_change_comments);
                    query.Parameters.AddWithValue("@ac_in_session_plays_count", item.ac_in_session_plays_count == null ? DBNull.Value : (object)item.ac_in_session_plays_count);
                    query.Parameters.AddWithValue("@ac_holder_address_01_tertiary", item.ac_holder_address_01_tertiary == null ? DBNull.Value: (object)item.ac_holder_address_01_tertiary);
                    query.Parameters.AddWithValue("@ac_holder_address_02_tertiary", item.ac_holder_address_02_tertiary == null ? DBNull.Value: (object)item.ac_holder_address_02_tertiary);
                    query.Parameters.AddWithValue("@ac_holder_address_03_tertiary", item.ac_holder_address_03_tertiary == null ? DBNull.Value: (object)item.ac_holder_address_03_tertiary);
                    query.Parameters.AddWithValue("@ac_holder_ext_num_tertiary", item.ac_holder_ext_num_tertiary == null ? DBNull.Value: (object)item.ac_holder_ext_num_tertiary);
                    query.Parameters.AddWithValue("@ac_holder_country_tertiary", item.ac_holder_country_tertiary == null ? DBNull.Value: (object)item.ac_holder_country_tertiary);
                    query.Parameters.AddWithValue("@ac_holder_state_tertiary", item.ac_holder_state_tertiary == null ? DBNull.Value: (object)item.ac_holder_state_tertiary);
                    query.Parameters.AddWithValue("@ac_holder_city_tertiary", item.ac_holder_city_tertiary == null ? DBNull.Value: (object)item.ac_holder_city_tertiary);
                    query.Parameters.AddWithValue("@ac_holder_zip_tertiary", item.ac_holder_zip_tertiary == null ? DBNull.Value: (object)item.ac_holder_zip_tertiary);
                    query.Parameters.AddWithValue("@ac_bad_address3", item.ac_bad_address3 == null ? DBNull.Value : (object)item.ac_bad_address3);
                    query.Parameters.AddWithValue("@ac_no_mail3", item.ac_no_mail3 == null ? DBNull.Value : (object)item.ac_no_mail3);
                    query.Parameters.AddWithValue("@ac_in_session_cash_in", item.ac_in_session_cash_in == null ? DBNull.Value : (object)item.ac_in_session_cash_in);
                    query.Parameters.AddWithValue("@ac_in_session_promo_ticket_re_in", item.ac_in_session_promo_ticket_re_in == null ? DBNull.Value : (object)item.ac_in_session_promo_ticket_re_in);
                    query.Parameters.AddWithValue("@ac_in_session_promo_ticket_nr_in", item.ac_in_session_promo_ticket_nr_in == null ? DBNull.Value : (object)item.ac_in_session_promo_ticket_nr_in);
                    //IdInsertado = Convert.ToInt32(query.ExecuteScalar());
                    query.ExecuteNonQuery();
                    IdInsertado = item.ac_account_id;
                }
            }
            catch (Exception ex)
            {
                IdInsertado = 0;
            }
            return IdInsertado;
        }
        public int GetTotalAccounts()
        {
            int total = 0;

            string query = @"
            select count(*) as total from 
            [dbo].[accounts]
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
                            total = (int)data["total"];
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
