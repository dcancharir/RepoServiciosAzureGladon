using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ServicioAzureIAS
{
    public class ServicioController : ApiController
    {

        /// METODOS DEL SERVIDOR
        [HttpPost]
        public dynamic DevolverDatos()
        {
            return "datos";
        }

        [HttpPost]
        public dynamic DevolverEstado()
        {
            return true;
        }




    }
}
