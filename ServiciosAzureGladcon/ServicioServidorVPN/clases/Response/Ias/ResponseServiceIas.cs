namespace ServicioServidorVPN.clases.Response.Ias {
    public class ResponseServiceIas {
        public bool Success { get; set; }
        public string DisplayMessage { get; set; } = string.Empty;
    }

    public class ResponseServiceIas<T> : ResponseServiceIas where T : new() {
        public T Data { get; set; } = new T();
    }
}
