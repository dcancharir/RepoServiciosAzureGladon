using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioAzureIAS.Clases.GestioProyectos {
    public class Usuario {
        public long UsuarioId { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string NombreUsuario { get; set; }
        public string ContraseniaUsuario { get; set; }
        public bool Estado { get; set; }
        public DateTime Fecharegistro { get; set; }
        public long? UsuarioSeguridadId { get; set; }
        public string Telefono { get; set; }
        public long? EmpresaId { get; set; }
        public long? AreaId { get; set; }
        public long? EmpleadoId { get; set; }

        public bool isDifferentUsuario(Usuario api) {
            return Normalizar(Nombres) != Normalizar(api.Nombres) ||
                   Normalizar(Apellidos) != Normalizar(api.Apellidos) ||
                   Normalizar(Correo) != Normalizar(api.Correo) ||
                   Estado != api.Estado ||
                   Normalizar(Telefono) != Normalizar(api.Telefono) ||
                   EmpresaId != api.EmpresaId ||
                   AreaId != api.AreaId ||
                   EmpleadoId != api.EmpleadoId;
        }

        private string Normalizar(string valor) {
            return string.IsNullOrWhiteSpace(valor) ? "" : valor.Trim();
        }

        public override string ToString() {
            return $"UsuarioId: {UsuarioId}\n" +
                   $"Nombres: {Nombres}\n" +
                   $"Apellidos: {Apellidos}\n" +
                   $"Correo: {Correo}\n" +
                   $"NombreUsuario: {NombreUsuario}\n" +
                   $"Estado: {Estado}\n" +
                   $"Fecharegistro: {Fecharegistro}\n" +
                   $"UsuarioSeguridadId: {UsuarioSeguridadId}\n" +
                   $"Telefono: {Telefono}\n" +
                   $"EmpresaId: {EmpresaId}\n" +
                   $"AreaId: {AreaId}\n" +
                   $"EmpleadoId: {EmpleadoId}\n";
        }
    }

}
