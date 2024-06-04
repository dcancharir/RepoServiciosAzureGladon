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
    public class gui_users_dal
    {
        private readonly string bd_username = string.Empty;
        private readonly string bd_password = string.Empty;
        private readonly string bd_datasource = string.Empty;
        private readonly string connectionString = string.Empty;
        public gui_users_dal(string database_name)
        {
            bd_username = ConfigurationManager.AppSettings["bd_username"];
            bd_password = ConfigurationManager.AppSettings["bd_password"];
            bd_datasource = ConfigurationManager.AppSettings["bd_datasource"];
            connectionString = $"Data Source={bd_datasource};Initial Catalog={database_name};Integrated Security=False;User ID={bd_username};Password={bd_password}";

        }
        public int SaveGuiUsers(gui_users item)
        {
            //bool respuesta = false;
            int IdInsertado = 0;
            string consulta = @"
INSERT INTO [dbo].[gui_users]
           ([gu_user_id]
           ,[gu_profile_id]
           ,[gu_username]
           ,[gu_enabled]
           ,[gu_password]
           ,[gu_not_valid_before]
           ,[gu_not_valid_after]
           ,[gu_last_changed]
           ,[gu_password_exp]
           ,[gu_pwd_chg_req]
           ,[gu_login_failures]
           ,[gu_password_h1]
           ,[gu_password_h2]
           ,[gu_password_h3]
           ,[gu_password_h4]
           ,[gu_password_h5]
           ,[gu_full_name]
           ,[gu_user_type]
           ,[gu_logged_in]
           ,[gu_logon_computer]
           ,[gu_last_activity]
           ,[gu_last_action]
           ,[gu_exit_code]
           ,[gu_sales_limit]
           ,[gu_mb_sales_limit]
           ,[gu_block_reason]
           ,[gu_master_id]
           ,[gu_master_sequence_id]
           ,[gu_employee_code]
           ,[gu_gui_last_login]
           ,[gu_cashier_last_login]
           ,[gu_intellia_roles]
           ,[gu_cage_vault_id])
--output inserted.gu_user_id
     VALUES
           (@gu_user_id
           ,@gu_profile_id
           ,@gu_username
           ,@gu_enabled
           ,@gu_password
           ,@gu_not_valid_before
           ,@gu_not_valid_after
           ,@gu_last_changed
           ,@gu_password_exp
           ,@gu_pwd_chg_req
           ,@gu_login_failures
           ,@gu_password_h1
           ,@gu_password_h2
           ,@gu_password_h3
           ,@gu_password_h4
           ,@gu_password_h5
           ,@gu_full_name
           ,@gu_user_type
           ,@gu_logged_in
           ,@gu_logon_computer
           ,@gu_last_activity
           ,@gu_last_action
           ,@gu_exit_code
           ,@gu_sales_limit
           ,@gu_mb_sales_limit
           ,@gu_block_reason
           ,@gu_master_id
           ,@gu_master_sequence_id
           ,@gu_employee_code
           ,@gu_gui_last_login
           ,@gu_cashier_last_login
           ,@gu_intellia_roles
           ,@gu_cage_vault_id)
                      ";
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@gu_user_id", ManejoNulos.ManageNullInteger(item.gu_user_id));
                    query.Parameters.AddWithValue("@gu_profile_id", ManejoNulos.ManageNullInteger(item.gu_profile_id));
                    query.Parameters.AddWithValue("@gu_username", ManejoNulos.ManageNullStr(item.gu_username));
                    query.Parameters.AddWithValue("@gu_enabled", ManejoNulos.ManegeNullBool(item.gu_enabled));
                    query.Parameters.AddWithValue("@gu_password", ManejoNulos.ManageNullByteArray(item.gu_password));
                    query.Parameters.AddWithValue("@gu_not_valid_before", ManejoNulos.ManageNullDate(item.gu_not_valid_before));
                    query.Parameters.AddWithValue("@gu_not_valid_after", ManejoNulos.ManageNullDate(item.gu_not_valid_after));
                    query.Parameters.AddWithValue("@gu_last_changed", ManejoNulos.ManageNullDate(item.gu_last_changed));
                    query.Parameters.AddWithValue("@gu_password_exp", ManejoNulos.ManageNullDate(item.gu_password_exp));
                    query.Parameters.AddWithValue("@gu_pwd_chg_req", ManejoNulos.ManegeNullBool(item.gu_pwd_chg_req));
                    query.Parameters.AddWithValue("@gu_login_failures", ManejoNulos.ManageNullInteger(item.gu_login_failures));
                    query.Parameters.AddWithValue("@gu_password_h1", ManejoNulos.ManageNullByteArray(item.gu_password_h1));
                    query.Parameters.AddWithValue("@gu_password_h2", ManejoNulos.ManageNullByteArray(item.gu_password_h2));
                    query.Parameters.AddWithValue("@gu_password_h3", ManejoNulos.ManageNullByteArray(item.gu_password_h3));
                    query.Parameters.AddWithValue("@gu_password_h4", ManejoNulos.ManageNullByteArray(item.gu_password_h4));
                    query.Parameters.AddWithValue("@gu_password_h5", ManejoNulos.ManageNullByteArray(item.gu_password_h5));
                    query.Parameters.AddWithValue("@gu_full_name", ManejoNulos.ManageNullStr(item.gu_full_name));
                    query.Parameters.AddWithValue("@gu_timestamp", ManejoNulos.ManageNullInteger64(item.gu_timestamp));
                    query.Parameters.AddWithValue("@gu_user_type", ManejoNulos.ManageNullShort(item.gu_user_type));
                    query.Parameters.AddWithValue("@gu_logged_in", ManejoNulos.ManageNullDate(item.gu_logged_in));
                    query.Parameters.AddWithValue("@gu_logon_computer", ManejoNulos.ManageNullStr(item.gu_logon_computer));
                    query.Parameters.AddWithValue("@gu_last_activity", ManejoNulos.ManageNullDate(item.gu_last_activity));
                    query.Parameters.AddWithValue("@gu_last_action", ManejoNulos.ManageNullStr(item.gu_last_action));
                    query.Parameters.AddWithValue("@gu_exit_code", ManejoNulos.ManageNullShort(item.gu_exit_code));
                    query.Parameters.AddWithValue("@gu_sales_limit", ManejoNulos.ManageNullDecimal(item.gu_sales_limit));
                    query.Parameters.AddWithValue("@gu_mb_sales_limit", ManejoNulos.ManageNullDecimal(item.gu_mb_sales_limit));
                    query.Parameters.AddWithValue("@gu_block_reason", ManejoNulos.ManageNullInteger(item.gu_block_reason));
                    query.Parameters.AddWithValue("@gu_master_id", ManejoNulos.ManageNullInteger(item.gu_master_id));
                    query.Parameters.AddWithValue("@gu_master_sequence_id", ManejoNulos.ManageNullInteger64(item.gu_master_sequence_id));
                    query.Parameters.AddWithValue("@gu_employee_code", ManejoNulos.ManageNullStr(item.gu_employee_code));
                    query.Parameters.AddWithValue("@gu_gui_last_login", ManejoNulos.ManageNullStr(item.gu_gui_last_login));
                    query.Parameters.AddWithValue("@gu_cashier_last_login", ManejoNulos.ManageNullStr(item.gu_cashier_last_login));
                    query.Parameters.AddWithValue("@gu_intellia_roles", ManejoNulos.ManageNullInteger(item.gu_intellia_roles));
                    query.Parameters.AddWithValue("@gu_cage_vault_id", ManejoNulos.ManageNullInteger(item.gu_cage_vault_id));
                    //IdInsertado = Convert.ToInt32(query.ExecuteScalar());
                    query.ExecuteNonQuery();
                    IdInsertado = item.gu_user_id;
                }
            }
            catch (Exception ex)
            {
                IdInsertado = 0;
            }
            return IdInsertado;
        }
        public int GetTotalGuiUsers()
        {
            int total = 0;

            string query = @"
            select count(*) as total from 
            [dbo].[gui_users]
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
