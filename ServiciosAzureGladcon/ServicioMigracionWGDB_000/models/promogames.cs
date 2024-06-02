using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioMigracionWGDB_000.models
{
    public class promogames
    {
        public long pg_id { get; set; }
        public string pg_name { get; set; }
        public long pg_type { get; set; }
        public decimal pg_price { get; set; }
        public int pg_price_units { get; set; }
        public bool pg_return_price { get; set; }
        public int pg_return_units { get; set; }
        public decimal pg_percentatge_cost_return { get; set; }
        public string pg_game_url { get; set; }
        public bool pg_mandatory_ic { get; set; }
        public bool pg_show_buy_dialog { get; set; }
        public bool pg_player_can_cancel { get; set; }
        public int pg_autocancel { get; set; }
        public bool pg_transfer_screen { get; set; }
        public int pg_transfer_timeout { get; set; }
        public string pg_pay_table { get; set; }
        public DateTime? pg_last_update { get; set; }
        public bool pg_request_pin { get; set; }
        public bool pg_use_personalized_image { get; set; }
        public long? pg_personalized_image_closed { get; set; }
        public bool pg_personalized_image_closed_shared { get; set; }
        public long? pg_personalized_image_closed_2 { get; set; }
        public long? pg_personalized_image_closed_3 { get; set; }
        public long? pg_personalized_image_opened { get; set; }
        public bool pg_personalized_image_opened_shared { get; set; }
        public long? pg_personalized_image_opened_2 { get; set; }
        public long? pg_personalized_image_opened_3 { get; set; }
    }
}
