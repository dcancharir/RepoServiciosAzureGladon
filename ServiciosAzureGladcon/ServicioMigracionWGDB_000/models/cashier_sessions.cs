using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioMigracionWGDB_000.models
{
    public class cashier_sessions
    {
        public long cs_session_id { get; set; }
        public string cs_name { get; set; }
        public int cs_cashier_id { get; set; }
        public int cs_user_id { get; set; }
        public DateTime cs_opening_date { get; set; }
        public DateTime? cs_closing_date { get; set; }
        public int cs_status { get; set; }
        public decimal cs_balance { get; set; }
        public decimal cs_other_balance_1 { get; set; }
        public decimal cs_other_balance_2 { get; set; }
        public decimal? cs_tax_a_pct { get; set; }
        public decimal? cs_tax_b_pct { get; set; }
        public decimal? cs_collected_amount { get; set; }
        public decimal? cs_sales_limit { get; set; }
        public decimal? cs_total_sold { get; set; }
        public decimal? cs_mb_sales_limit { get; set; }
        public decimal? cs_mb_total_sold { get; set; }
        public bool cs_session_by_terminal { get; set; }
        public bool cs_history { get; set; }
        public DateTime? cs_gaming_day { get; set; }
        public bool cs_short_over_history { get; set; }
        public long? cs_has_pinpad_operations { get; set; }
        public int? cs_venue_id { get; set; }
        public bool? cs_is_session_collector { get; set; }
    }
}
