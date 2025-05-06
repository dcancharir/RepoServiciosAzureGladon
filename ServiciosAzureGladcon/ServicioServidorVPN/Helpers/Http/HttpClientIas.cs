using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using TimeOut = System.Threading.Timeout;

namespace ServicioServidorVPN.Helpers.Http {
    public class HttpClientIas : HttpClient {
        public HttpClientIas() {
            BaseAddress = new Uri(ConfigurationManager.AppSettings["UrlIASAzure"]);
            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            MaxResponseContentBufferSize = int.MaxValue;
            Timeout = TimeOut.InfiniteTimeSpan;
        }
    }
}
