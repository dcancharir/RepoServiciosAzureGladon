using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioAzureIAS.utilitarios {
    public class BUK_Response<T>{
        public bool success {  get; set; }
        public T data { get; set; }
        public string displayMessage { get; set; }
        public string[] errorMessage { get; set; }
    }
}
