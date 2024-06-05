using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioServidorVPN.WGDB_000.model
{
    public class areas
    {
        public int ar_area_id { get; set; }
        public string ar_name { get; set; } 
        public bool ar_smoking { get; set; }
        public byte[] ar_timestamp { get; set; }
        public int ar_venue_id { get; set; }
        public string ar_external_id { get; set; }
    }
}
