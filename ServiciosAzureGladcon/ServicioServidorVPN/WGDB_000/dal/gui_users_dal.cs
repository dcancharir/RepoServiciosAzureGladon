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
        public bool SaveGuiUsers(gui_users item)
        {
            //bool respuesta = false;
            int IdInsertado = 0;
            string consulta = @"
if not exists (select gu_user_id from [dbo].[gui_users] where gu_user_id = @gu_user_id)
begin
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
end

                      ";
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@gu_user_id", item.gu_user_id == null ? DBNull.Value : (object)item.gu_user_id);
                    query.Parameters.AddWithValue("@gu_profile_id", item.gu_profile_id == null ? DBNull.Value : (object)item.gu_profile_id);
                    query.Parameters.AddWithValue("@gu_username", item.gu_username == null ? DBNull.Value : (object)item.gu_username);
                    query.Parameters.AddWithValue("@gu_enabled", item.gu_enabled == null ? DBNull.Value : (object)item.gu_enabled);
                    query.Parameters.AddWithValue("@gu_password", item.gu_password == null ? DBNull.Value : (object)new byte[0]);
                    query.Parameters.AddWithValue("@gu_not_valid_before", item.gu_not_valid_before == null ? DBNull.Value : (object)item.gu_not_valid_before);
                    query.Parameters.AddWithValue("@gu_not_valid_after", item.gu_not_valid_after == null ? DBNull.Value : (object)item.gu_not_valid_after);
                    query.Parameters.AddWithValue("@gu_last_changed", item.gu_last_changed == null ? DBNull.Value : (object)item.gu_last_changed);
                    query.Parameters.AddWithValue("@gu_password_exp", item.gu_password_exp == null ? DBNull.Value : (object)item.gu_password_exp);
                    query.Parameters.AddWithValue("@gu_pwd_chg_req", item.gu_pwd_chg_req == null ? DBNull.Value : (object)item.gu_pwd_chg_req);
                    query.Parameters.AddWithValue("@gu_login_failures", item.gu_login_failures == null ? DBNull.Value : (object)item.gu_login_failures);
                    query.Parameters.AddWithValue("@gu_full_name", item.gu_full_name == null ? DBNull.Value : (object)item.gu_full_name);
                    query.Parameters.AddWithValue("@gu_user_type", item.gu_user_type == null ? DBNull.Value : (object)item.gu_user_type);
                    query.Parameters.AddWithValue("@gu_logged_in", item.gu_logged_in == null ? DBNull.Value : (object)item.gu_logged_in);
                    query.Parameters.AddWithValue("@gu_logon_computer", item.gu_logon_computer == null ? DBNull.Value : (object)item.gu_logon_computer);
                    query.Parameters.AddWithValue("@gu_last_activity", item.gu_last_activity == null ? DBNull.Value : (object)item.gu_last_activity);
                    query.Parameters.AddWithValue("@gu_last_action", item.gu_last_action == null ? DBNull.Value : (object)item.gu_last_action);
                    query.Parameters.AddWithValue("@gu_exit_code", item.gu_exit_code == null ? DBNull.Value : (object)item.gu_exit_code);
                    query.Parameters.AddWithValue("@gu_sales_limit", item.gu_sales_limit == null ? DBNull.Value : (object)item.gu_sales_limit);
                    query.Parameters.AddWithValue("@gu_mb_sales_limit", item.gu_mb_sales_limit == null ? DBNull.Value : (object)item.gu_mb_sales_limit);
                    query.Parameters.AddWithValue("@gu_block_reason", item.gu_block_reason == null ? DBNull.Value : (object)item.gu_block_reason);
                    query.Parameters.AddWithValue("@gu_master_id", item.gu_master_id == null ? DBNull.Value : (object)item.gu_master_id);
                    query.Parameters.AddWithValue("@gu_master_sequence_id", item.gu_master_sequence_id == null ? DBNull.Value : (object)item.gu_master_sequence_id);
                    query.Parameters.AddWithValue("@gu_employee_code", item.gu_employee_code == null ? DBNull.Value : (object)item.gu_employee_code);
                    query.Parameters.AddWithValue("@gu_gui_last_login", item.gu_gui_last_login == null ? DBNull.Value : (object)item.gu_gui_last_login);
                    query.Parameters.AddWithValue("@gu_cashier_last_login", item.gu_cashier_last_login == null ? DBNull.Value : (object)item.gu_cashier_last_login);
                    query.Parameters.AddWithValue("@gu_intellia_roles", item.gu_intellia_roles == null ? DBNull.Value : (object)item.gu_intellia_roles);
                    query.Parameters.AddWithValue("@gu_cage_vault_id", item.gu_cage_vault_id == null ? DBNull.Value : (object)item.gu_cage_vault_id);
                    //IdInsertado = Convert.ToInt32(query.ExecuteScalar());
                    query.ExecuteNonQuery();
                    //IdInsertado = item.gu_user_id;
                    return true;
                }
            }
            catch (Exception ex)
            {
                funciones.logueo($"Error metodo SaveGuiUsers - {ex.Message}");
            }
            return false;
        }
        public long GetTotalGuiUsers()
        {
            long total = 0;

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
                            total = ManejoNulos.ManageNullInteger64(data["total"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                funciones.logueo($"Error metodo GetLastIdInserted gui_users_dal.cs- {ex.Message}", "Error");
                total = 0;
            }

            return total;
        }
    }
}
