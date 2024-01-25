namespace ServicioAzureIAS.Clases.Response {
    public class ResponseEntidad<T> {
        public bool success { get; set; }
        public string displayMessage { get; set; }
        public T data { get; set; }
        public string errorMessage { get; set; }
    }
}
