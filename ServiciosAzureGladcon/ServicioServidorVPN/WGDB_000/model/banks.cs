using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioServidorVPN.WGDB_000.model
{
    public class banks
    {
        public int bk_bank_id { get; set; }
        public int bk_area_id { get; set; }
        public string bk_name { get; set; }
        public byte[] bk_timestamp { get; set; }
        public bool bk_multiposition { get; set; }
        public string bk_external_id { get; set; }
        public string bk_shape_code { get; set; }
        public int? bk_shape_w { get; set; }
        public int? bk_shape_h { get; set; }
        public int? bk_play_safe_distance { get; set; }
    }
}
