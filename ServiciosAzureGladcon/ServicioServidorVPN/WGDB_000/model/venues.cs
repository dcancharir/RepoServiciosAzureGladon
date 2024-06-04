using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioServidorVPN.WGDB_000.model
{
    public class venues
    {
        public int ve_venue_id { get; set; }
        public string ve_external_venue_id { get; set; }
        public int ve_venue_type_id { get; set; }
        public string ve_shortname { get; set; }
        public string ve_fullname { get; set; }
        public string ve_location { get; set; }
        public string ve_geolocation { get; set; }
        public string ve_zipcode { get; set; }
        public int ve_status_id { get; set; }
        public decimal? ve_surface_m2 { get; set; }
        public bool ve_enable { get; set; }
        public DateTime? ve_disabletime { get; set; }
        public string ve_reason { get; set; }
        public decimal? ve_threshold_amount { get; set; }
        public decimal? ve_netwin_pct { get; set; }
        public int? ve_vault_id { get; set; }
        public int? ve_operator_id { get; set; }
        public string ve_dbversion { get; set; }
        public string ve_db_description { get; set; }
        public DateTime? ve_db_update { get; set; }
    }
}
