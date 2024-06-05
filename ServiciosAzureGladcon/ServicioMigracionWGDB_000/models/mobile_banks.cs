using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioMigracionWGDB_000.models
{
    public class mobile_banks
    {
        public long mb_account_id { get; set; }
        public short mb_account_type { get; set; }
        public string mb_holder_name { get; set; }
        public bool mb_blocked { get; set; }
        public string mb_track_data { get; set; }
        public decimal mb_balance { get; set; }
        public decimal mb_initial_limit { get; set; }
        public decimal mb_cash_in { get; set; }
        public decimal mb_deposit { get; set; }
        public decimal mb_extension { get; set; }
        public decimal mb_pending_cash { get; set; }
        public string mb_pin { get; set; } 
        public long? mb_cashier_session_id { get; set; }
        public short mb_failed_login_attempts { get; set; }
        public DateTime? mb_last_activity { get; set; }
        public int? mb_last_terminal_id { get; set; }
        public string mb_last_terminal_name { get; set; }
        public byte[] mb_timestamp { get; set; }
        public long? mb_track_number { get; set; }
        public int? mb_terminal_id { get; set; }
        public decimal mb_over_cash { get; set; }
        public decimal? mb_total_limit { get; set; }
        public decimal? mb_recharge_limit { get; set; }
        public int? mb_number_of_recharges_limit { get; set; }
        public int? mb_actual_number_of_recharges { get; set; }
        public string mb_employee_code { get; set; }
        public decimal mb_shortfall_cash { get; set; }
        public decimal mb_over_cash_session { get; set; }
        public decimal mb_shortfall_cash_session { get; set; }
        public short mb_session_status { get; set; }
        public int? mb_user_id { get; set; }
        public bool mb_lock { get; set; }
    }
}
