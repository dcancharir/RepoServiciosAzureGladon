using ServicioAzureIAS.utilitarios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace ServicioAzureIAS.Clases.Sala
{
    public class SalaDAL
    {
        private readonly string _conexion = string.Empty;

        public SalaDAL()
        {
            _conexion = ConfigurationManager.ConnectionStrings["conectionBdSeguridad"].ConnectionString;
        }

        public List<SalaEntidad> ListarSalaActivas()
        {
            List<SalaEntidad> lista = new List<SalaEntidad>();

            string query = @"
            SELECT
	            sala.CodSala,
	            sala.CodEmpresa,
	            sala.CodUbigeo,
	            sala.Nombre,
	            sala.UrlProgresivo,
	            sala.IpPublica,
	            sala.UrlCuadre,
	            sala.UrlPlayerTracking,
	            sala.UrlBoveda,
	            sala.UrlSalaOnline,
	            sala.PuertoSignalr,
	            sala.IpPrivada,
	            sala.PuertoServicioWebOnline,
	            sala.PuertoWebOnline,
	            sala.CarpetaOnline
            FROM dbo.Sala sala
            WHERE sala.Activo = 1 AND sala.Estado = 1
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(_conexion))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(query, connection);

                    using (SqlDataReader data = command.ExecuteReader())
                    {
                        while (data.Read())
                        {
                            SalaEntidad sala = new SalaEntidad
                            {
                                CodSala = ManejoNulos.ManageNullInteger(data["CodSala"]),
                                CodEmpresa = ManejoNulos.ManageNullInteger(data["CodEmpresa"]),
                                CodUbigeo = ManejoNulos.ManageNullInteger(data["CodUbigeo"]),
                                Nombre = ManejoNulos.ManageNullStr(data["Nombre"]),
                                UrlProgresivo = ManejoNulos.ManageNullStr(data["UrlProgresivo"]),
                                IpPublica = ManejoNulos.ManageNullStr(data["IpPublica"]),
                                UrlCuadre = ManejoNulos.ManageNullStr(data["UrlCuadre"]),
                                UrlPlayerTracking = ManejoNulos.ManageNullStr(data["UrlPlayerTracking"]),
                                UrlBoveda = ManejoNulos.ManageNullStr(data["UrlBoveda"]),
                                UrlSalaOnline = ManejoNulos.ManageNullStr(data["UrlSalaOnline"]),
                                PuertoSignalr = ManejoNulos.ManageNullInteger(data["PuertoSignalr"]),
                                IpPrivada = ManejoNulos.ManageNullStr(data["IpPrivada"]),
                                PuertoServicioWebOnline = ManejoNulos.ManageNullInteger(data["PuertoServicioWebOnline"]),
                                PuertoWebOnline = ManejoNulos.ManageNullInteger(data["PuertoWebOnline"]),
                                CarpetaOnline = ManejoNulos.ManageNullStr(data["CarpetaOnline"])
                            };

                            lista.Add(sala);
                        }
                    }
                }
            }
            catch (Exception)
            {
                lista = new List<SalaEntidad>();
            }

            return lista;
        }
    }
}
