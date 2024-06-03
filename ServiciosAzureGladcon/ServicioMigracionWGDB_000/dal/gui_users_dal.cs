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
    public class gui_users_dal
    {
        private readonly string _conexion = string.Empty;

        public gui_users_dal()
        {
            _conexion = ConfigurationManager.ConnectionStrings["connection_wgdb_000"].ConnectionString;
        }
        public List<gui_users> GetGuiUsersPaginated(long lastid, int skip, int pageSize)
        {
            var result = new List<gui_users>();
            try
            {
                string query = $@"
SELECT [gu_user_id]
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
      ,[gu_timestamp]
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
      ,[gu_cage_vault_id]
  FROM [dbo].[gui_users]
  where gu_user_id > {lastid}
  order by gu_user_id asc
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
                                var item = new gui_users()
                                {
                                    gu_user_id = ManejoNulos.ManageNullInteger(dr["gu_user_id"]),
                                    gu_profile_id = ManejoNulos.ManageNullInteger(dr["gu_profile_id"]),
                                    gu_username = ManejoNulos.ManageNullStr(dr["gu_username"]),
                                    gu_enabled = ManejoNulos.ManegeNullBool(dr["gu_enabled"]),
                                    gu_password = ManejoNulos.ManageNullByteArray(dr["gu_password"]),
                                    gu_not_valid_before = ManejoNulos.ManageNullDate(dr["gu_not_valid_before"]),
                                    gu_not_valid_after = ManejoNulos.ManageNullDate(dr["gu_not_valid_after"]),
                                    gu_last_changed = ManejoNulos.ManageNullDate(dr["gu_last_changed"]),
                                    gu_password_exp = ManejoNulos.ManageNullDate(dr["gu_password_exp"]),
                                    gu_pwd_chg_req = ManejoNulos.ManegeNullBool(dr["gu_pwd_chg_req"]),
                                    gu_login_failures = ManejoNulos.ManageNullInteger(dr["gu_login_failures"]),
                                    gu_password_h1 = ManejoNulos.ManageNullByteArray(dr["gu_password_h1"]),
                                    gu_password_h2 = ManejoNulos.ManageNullByteArray(dr["gu_password_h2"]),
                                    gu_password_h3 = ManejoNulos.ManageNullByteArray(dr["gu_password_h3"]),
                                    gu_password_h4 = ManejoNulos.ManageNullByteArray(dr["gu_password_h4"]),
                                    gu_password_h5 = ManejoNulos.ManageNullByteArray(dr["gu_password_h5"]),
                                    gu_full_name = ManejoNulos.ManageNullStr(dr["gu_full_name"]),
                                    gu_timestamp = ManejoNulos.ManageNullInteger64(dr["gu_timestamp"]),
                                    gu_user_type = ManejoNulos.ManageNullShort(dr["gu_user_type"]),
                                    gu_logged_in = ManejoNulos.ManageNullDate(dr["gu_logged_in"]),
                                    gu_logon_computer = ManejoNulos.ManageNullStr(dr["gu_logon_computer"]),
                                    gu_last_activity = ManejoNulos.ManageNullDate(dr["gu_last_activity"]),
                                    gu_last_action = ManejoNulos.ManageNullStr(dr["gu_last_action"]),
                                    gu_exit_code = ManejoNulos.ManageNullShort(dr["gu_exit_code"]),
                                    gu_sales_limit = ManejoNulos.ManageNullDecimal(dr["gu_sales_limit"]),
                                    gu_mb_sales_limit = ManejoNulos.ManageNullDecimal(dr["gu_mb_sales_limit"]),
                                    gu_block_reason = ManejoNulos.ManageNullInteger(dr["gu_block_reason"]),
                                    gu_master_id = ManejoNulos.ManageNullInteger(dr["gu_master_id"]),
                                    gu_master_sequence_id = ManejoNulos.ManageNullInteger64(dr["gu_master_sequence_id"]),
                                    gu_employee_code = ManejoNulos.ManageNullStr(dr["gu_employee_code"]),
                                    gu_gui_last_login = ManejoNulos.ManageNullStr(dr["gu_gui_last_login"]),
                                    gu_cashier_last_login = ManejoNulos.ManageNullStr(dr["gu_cashier_last_login"]),
                                    gu_intellia_roles = ManejoNulos.ManageNullInteger(dr["gu_intellia_roles"]),
                                    gu_cage_vault_id = ManejoNulos.ManageNullInteger(dr["gu_cage_vault_id"]),
                                };
                                result.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = new List<gui_users>();
            }
            return result;
        }
        public int GetTotalGuiUsersForMigration(long lastid)
        {
            int total = 0;

            string query = @"
            select count(*) as total from 
            [dbo].[gui_users]
where gu_user_id > @lastid
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
output inserted.gu_user_id
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
                using (var con = new SqlConnection(_conexion))
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
