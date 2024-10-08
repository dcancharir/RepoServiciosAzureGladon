﻿using System;

namespace ServicioAzureIAS.Clases.CampaniaCliente {
    public class CMP_ClienteEntidad {
        public Int64 id { get; set; }
        public Int64 campania_id { get; set; }
        public Int64 cliente_id { get; set; }
        //---------------------------------------
        public string ApelPat { get; set; }
        public string ApelMat { get; set; }
        public string Nombre { get; set; }
        public string NombreCompleto { get; set; }
        public string NroDoc { get; set; }
        public int TipoDocumentoId { get; set; }
        public string TipoDocumento { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int CodSala { get; set; }
        public string NombreSala { get; set; }
        public Int64 CMPcliente_id { get; set; }
        public string Mail { get; set; }
        public string CodigoPais { get; set; }
        public string NumeroCelular { get; set; }
        //------------------------------------------
        public string CampaniaNombre { get; set; }
        public DateTime fecha_reg { get; set; }
        public string Codigo { get; set; }
        public bool CodigoCanjeado { get; set; }
        public DateTime FechaGeneracionCodigo { get; set; }
        public DateTime FechaExpiracionCodigo { get; set; }
        public DateTime FechaCanjeoCodigo { get; set; }
        public bool CodigoExpirado { get; set; }
        //para campaña
        public bool CodigoSeReactiva { get; set; }
        public string MensajeWhatsAppReactivacion { get; set; }
        public int DuracionReactivacionCodigoDias { get; set; }
        public int DuracionReactivacionCodigoHoras { get; set; }
        public int DuracionCodigoDias { get; set; }
        public int DuracionCodigoHoras { get; set; }

        public bool EsPosibleEnviarMensajeWhatsApp() {
            return
                !string.IsNullOrEmpty(CodigoPais) &&
                !string.IsNullOrEmpty(NumeroCelular) &&
                CodigoPais.Length >= 1 &&
                NumeroCelular.Length >= 9;
        }

        public string ObtenerMensajeFormateadoParaEnvio(string mensajePlantilla, CMP_ClienteEntidad campaniaCliente) {
            string mensaje = string.Empty;
            mensaje = string.Format(
                mensajePlantilla,
                campaniaCliente.Codigo,
                campaniaCliente.NombreSala,
                campaniaCliente.FechaExpiracionCodigo.ToString("dd/MM/yyyy HH:mm"),
                !string.IsNullOrEmpty(campaniaCliente.Nombre) ? campaniaCliente.Nombre : campaniaCliente.NombreCompleto,
                campaniaCliente.NombreCompleto,
                campaniaCliente.DuracionCodigoDias,
                campaniaCliente.DuracionCodigoHoras,
                campaniaCliente.DuracionReactivacionCodigoDias,
                campaniaCliente.DuracionReactivacionCodigoHoras,
                campaniaCliente.FechaExpiracionCodigo.AddDays(campaniaCliente.DuracionReactivacionCodigoDias).AddHours(campaniaCliente.DuracionReactivacionCodigoHoras).ToString("dd/MM/yyyy HH:mm")
            );
            return mensaje;
        }
    }
}
