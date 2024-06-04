using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioServidorVPN.WGDB_000.model
{
    public partial class account_movements
    {
        public long am_movement_id { get; set; }
        public long? am_play_session_id { get; set; }
        public long am_account_id { get; set; }
        public int? am_terminal_id { get; set; }
        public long? am_wcp_sequence_id { get; set; }
        public long? am_wcp_transaction_id { get; set; }
        public DateTime am_datetime { get; set; }
        public int am_type { get; set; }
        public decimal am_initial_balance { get; set; }
        public decimal am_sub_amount { get; set; }
        public decimal am_add_amount { get; set; }
        public decimal am_final_balance { get; set; }
        public int? am_cashier_id { get; set; }
        public string am_cashier_name { get; set; }
        public string am_terminal_name { get; set; }
        public long? am_operation_id { get; set; }
        public string am_details { get; set; }
        public string am_reasons { get; set; }
        public int? am_undo_status { get; set; }
        public string am_track_data { get; set; }
        public int? am_modified_bucket_reason { get; set; }
        public string am_data_before { get; set; }
        public string am_data_after { get; set; }
    }
}
