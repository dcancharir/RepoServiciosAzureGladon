using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioServidorVPN.WGDB_000.model
{
    public class account_operations
    {
        public long ao_operation_id { get; set; }
        public DateTime ao_datetime { get; set; }
        public int ao_code { get; set; }
        public long ao_account_id { get; set; }
        public long? ao_cashier_session_id { get; set; }
        public long? ao_mb_account_id { get; set; }
        public long? ao_promo_id { get; set; }
        public decimal? ao_amount { get; set; }
        public decimal? ao_non_redeemable { get; set; }
        public decimal? ao_won_lock { get; set; }
        public long? ao_operation_data { get; set; }
        public bool ao_automatically_printed { get; set; }
        public decimal? ao_non_redeemable2 { get; set; }
        public decimal ao_redeemable { get; set; }
        public decimal ao_promo_redeemable { get; set; }
        public decimal ao_promo_not_redeemable { get; set; }
        public long? ao_user_id { get; set; }
        public int? ao_undo_status { get; set; }
        public long? ao_undo_operation_id { get; set; }
        public long? ao_reason_id { get; set; }
        public string ao_comment { get; set; }
        public string ao_comment_handpay { get; set; }
        public int? ao_venue_id { get; set; }
        public decimal? ao_spent_used { get; set; }
        public decimal? ao_spent_remaining { get; set; }
    }
}
