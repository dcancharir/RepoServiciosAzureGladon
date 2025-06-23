using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioAzureIAS.Clases.GestioProyectos {
    public class Empleado {
        public long Id { get; set; }
        public long IdBuk { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string NombreCompleto { get; set; }
        public long IdEmpresa { get; set; }
        public string Empresa { get; set; }
        public string Correo { get; set; }
        public string Celular { get; set; }
        public bool EstadoCese { get; set; }
        public long IdAreaBuk { get; set; }
        public string Area {  get; set; }

        public bool isDifferentEmpleadoApi(EmpleadosApi api) {

            return this.IdBuk == api.IdBuk &&
                       Normalizar(this.TipoDocumento) != Normalizar(api.TipoDocumento) ||
                       Normalizar(this.NumeroDocumento) != Normalizar(api.NumeroDocumento) ||
                       Normalizar(this.Nombres) != Normalizar(api.Nombres) ||
                       Normalizar(this.ApellidoPaterno) != Normalizar(api.ApellidoPaterno) ||
                       Normalizar(this.ApellidoMaterno) != Normalizar(api.ApellidoMaterno) ||
                       Normalizar(this.NombreCompleto) != Normalizar(api.NombreCompleto) ||
                       this.IdEmpresa != api.IdEmpresa ||
                       Normalizar(this.Empresa) != Normalizar(api.Empresa) ||
                       Normalizar(this.Correo) != Normalizar(api.Correo) ||
                       Normalizar(this.Celular) != Normalizar(api.Celular) ||
                       this.EstadoCese != api.EstadoCese ||
                       this.IdAreaBuk != api.IdAreaBuk ||
                       Normalizar(this.Area) != Normalizar(api.Area);
        }

        public bool isDifferentEmpleado(Empleado api) {

            return this.IdBuk == api.IdBuk &&
                       Normalizar(this.TipoDocumento) != Normalizar(api.TipoDocumento) ||
                       Normalizar(this.NumeroDocumento) != Normalizar(api.NumeroDocumento) ||
                       Normalizar(this.Nombres) != Normalizar(api.Nombres) ||
                       Normalizar(this.ApellidoPaterno) != Normalizar(api.ApellidoPaterno) ||
                       Normalizar(this.ApellidoMaterno) != Normalizar(api.ApellidoMaterno) ||
                       Normalizar(this.NombreCompleto) != Normalizar(api.NombreCompleto) ||
                       this.IdEmpresa != api.IdEmpresa ||
                       Normalizar(this.Empresa) != Normalizar(api.Empresa) ||
                       Normalizar(this.Correo) != Normalizar(api.Correo) ||
                       Normalizar(this.Celular) != Normalizar(api.Celular) ||
                       this.EstadoCese != api.EstadoCese ||
                       this.IdAreaBuk != api.IdAreaBuk ||
                       Normalizar(this.Area) != Normalizar(api.Area);
        }

        public override string ToString() {

            return $"EmpleadoId: {Id}\n" +
                                $"TipoDocumento: {TipoDocumento}\n" +
                                $"NumeroDocumento: {NumeroDocumento}\n" +
                                $"Nombres: {Nombres}\n" +
                                $"ApellidoPaterno: {ApellidoPaterno}\n" +
                                $"ApellidoMaterno: {ApellidoMaterno}\n" +
                                $"NombreCompleto: {NombreCompleto}\n" +
                                $"IdEmpresa: {IdEmpresa}\n" +
                                $"Empresa: {Empresa}\n" +
                                $"Correo: {Correo}\n" +
                                $"Celular: {Celular}\n" +
                                $"EstadoCese: {EstadoCese}\n" +
                                $"IdAreaBuk: {IdAreaBuk}\n" +
                                $"Area: {Area}\n";

        }

        public bool IsEqualsToApiEmpleado2(EmpleadosApi api) {
            if(this.IdBuk != api.IdBuk)
                return true;
            if(Normalizar(this.TipoDocumento) != Normalizar(api.TipoDocumento))
                return true;
            if(Normalizar(this.NumeroDocumento) != Normalizar(api.NumeroDocumento))
                return true;
            if(Normalizar(this.Nombres) != Normalizar(api.Nombres))
                return true;
            if(Normalizar(this.ApellidoPaterno) != Normalizar(api.ApellidoPaterno))
                return true;
            if(Normalizar(this.ApellidoMaterno) != Normalizar(api.ApellidoMaterno))
                return true;
            if(Normalizar(this.NombreCompleto) != Normalizar(api.NombreCompleto))
                return true;
            if(this.IdEmpresa != api.IdEmpresa)
                return true;
            if(Normalizar(this.Empresa) != Normalizar(api.Empresa))
                return true;
            if(Normalizar(this.Correo) != Normalizar(api.Correo))
                return true;
            if(Normalizar(this.Celular) != Normalizar(api.Celular))
                return true;
            if(this.EstadoCese != api.EstadoCese)
                return true;
            if(this.IdAreaBuk != api.IdAreaBuk)
                return true;
            if(Normalizar(this.Area) != Normalizar(api.Area))
                return true;

            return false;
        }

        private string Normalizar(string valor) {
            return string.IsNullOrWhiteSpace(valor) ? "" : valor.Trim();
        }

        //public List<string> DiferenciasConApiEmpleado(EmpleadosApi api) {
        //    var diferencias = new List<string>();
 

        //    if(this.IdBuk != api.IdBuk)
        //        diferencias.Add(nameof(IdBuk));
        //    if(!string.Equals(Normalizar(this.TipoDocumento), Normalizar(api.TipoDocumento), StringComparison.OrdinalIgnoreCase))
        //        diferencias.Add(nameof(TipoDocumento));
        //    if(!string.Equals(Normalizar(this.NumeroDocumento), Normalizar(api.NumeroDocumento), StringComparison.OrdinalIgnoreCase))
        //        diferencias.Add(nameof(NumeroDocumento));
        //    if(!string.Equals(Normalizar(this.Nombres), Normalizar(api.Nombres), StringComparison.OrdinalIgnoreCase))
        //        diferencias.Add(nameof(Nombres));
        //    if(!string.Equals(Normalizar(this.ApellidoPaterno), Normalizar(api.ApellidoPaterno), StringComparison.OrdinalIgnoreCase))
        //        diferencias.Add(nameof(ApellidoPaterno));
        //    if(!string.Equals(Normalizar(this.ApellidoMaterno), Normalizar(api.ApellidoMaterno), StringComparison.OrdinalIgnoreCase))
        //        diferencias.Add(nameof(ApellidoMaterno));
        //    if(!string.Equals(Normalizar(this.NombreCompleto), Normalizar(api.NombreCompleto), StringComparison.OrdinalIgnoreCase))
        //        diferencias.Add(nameof(NombreCompleto));
        //    if(this.IdEmpresa != api.IdEmpresa)
        //        diferencias.Add(nameof(IdEmpresa));
        //    if(!string.Equals(Normalizar(this.Empresa), Normalizar(api.Empresa), StringComparison.OrdinalIgnoreCase))
        //        diferencias.Add(nameof(Empresa));
        //    if(!string.Equals(Normalizar(this.Correo), Normalizar(api.Correo), StringComparison.OrdinalIgnoreCase))
        //        diferencias.Add(nameof(Correo));
        //    if(!string.Equals(Normalizar(this.Celular), Normalizar(api.Celular), StringComparison.OrdinalIgnoreCase))
        //        diferencias.Add(nameof(Celular));
        //    if(this.EstadoCese != api.EstadoCese)
        //        diferencias.Add(nameof(EstadoCese));
        //    if(this.IdAreaBuk != api.IdAreaBuk)
        //        diferencias.Add(nameof(IdAreaBuk));
        //    if(!string.Equals(Normalizar(this.Area), Normalizar(api.Area), StringComparison.OrdinalIgnoreCase))
        //        diferencias.Add(nameof(Area));

        //    return diferencias;
        //}
    }

    public class EmpleadosResponse {
        public bool Success { get; set; }
        public string DisplayMessage { get; set; } = string.Empty;
        public List<EmpleadosApi> Data { get; set; }
        public List<string> ErrorMessage { get; set; }
    }

    public class EmpleadosApi {
        public long IdBuk { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string NombreCompleto { get; set; }
        public long IdCargo { get; set; }
        public string Cargo { get; set; }
        public long IdEmpresa { get; set; }
        public string Empresa { get; set; }
        public DateTime? FechaCese { get; set; }
        public string Correo { get; set; }
        public string Celular { get; set; }
        public string Direccion { get; set; }
        public string Genero { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public bool EstadoCese { get; set; }
        public long IdUnidadBuk { get; set; }
        public string Unidad { get; set; }
        public long IdDepartamentoBuk { get; set; }
        public string Departamento { get; set; }
        public long IdAreaBuk { get; set; }
        public string Area { get; set; }
        public long IdGrupoOcupacionalBuk { get; set; }
        public string GrupoOcupacional { get; set; }
    }
}
