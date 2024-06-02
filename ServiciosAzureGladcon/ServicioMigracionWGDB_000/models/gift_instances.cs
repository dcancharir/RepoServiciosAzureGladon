using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioMigracionWGDB_000.models
{
    public class gift_instances
    {
        public long gin_gift_instance_id { get; set; }
        public long gin_oper_request_id { get; set; }
        public long? gin_oper_delivery_id { get; set; }
        public long gin_account_id { get; set; }
        public long gin_gift_id { get; set; }
        public string gin_gift_name { get; set; }
        public int gin_gift_type { get; set; }
        public decimal gin_points { get; set; }
        public decimal? gin_conversion_to_nrc { get; set; }
        public decimal gin_spent_points { get; set; }
        public DateTime gin_requested { get; set; }
        public DateTime? gin_delivered { get; set; }
        public DateTime gin_expiration { get; set; }
        public int gin_request_status { get; set; }
        public int gin_num_items { get; set; }
        public long? gin_data_01 { get; set; }
        public int? gin_notification { get; set; }
        public long gin_row_version { get; set; }
    }
}
