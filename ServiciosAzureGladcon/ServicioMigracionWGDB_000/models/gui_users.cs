using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioMigracionWGDB_000.models
{
    public class gui_users
    {
        public int gu_user_id { get; set; }
        public int gu_profile_id { get; set; }
        public string gu_username { get; set; }
        public bool gu_enabled { get; set; }
        public byte[] gu_password { get; set; }
        public DateTime gu_not_valid_before { get; set; }
        public DateTime? gu_not_valid_after { get; set; }
        public DateTime? gu_last_changed { get; set; }
        public DateTime? gu_password_exp { get; set; }
        public bool gu_pwd_chg_req { get; set; }
        public int? gu_login_failures { get; set; }
        public byte[] gu_password_h1 { get; set; }
        public byte[] gu_password_h2 { get; set; }
        public byte[] gu_password_h3 { get; set; }
        public byte[] gu_password_h4 { get; set; }
        public byte[] gu_password_h5 { get; set; }
        public string gu_full_name { get; set; }
        public long gu_timestamp { get; set; }
        public short gu_user_type { get; set; }
        public DateTime? gu_logged_in { get; set; }
        public string gu_logon_computer { get; set; }
        public DateTime? gu_last_activity { get; set; }
        public string gu_last_action { get; set; }
        public short? gu_exit_code { get; set; }
        public decimal? gu_sales_limit { get; set; }
        public decimal? gu_mb_sales_limit { get; set; }
        public int gu_block_reason { get; set; }
        public int? gu_master_id { get; set; }
        public long? gu_master_sequence_id { get; set; }
        public string gu_employee_code { get; set; }
        public string gu_gui_last_login { get; set; }
        public string gu_cashier_last_login { get; set; }
        public int? gu_intellia_roles { get; set; }
        public int? gu_cage_vault_id { get; set; }
    }
}
