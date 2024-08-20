using ServicioAzureIAS.Clases.Email;
using ServicioAzureIAS.utilitarios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace ServicioAzureIAS.DAL.Email {
    public class DestinatarioDAL {
        private readonly string _conexion = string.Empty;

        public DestinatarioDAL() {
            _conexion = ConfigurationManager.ConnectionStrings["conectionBdSeguridad"].ConnectionString;
        }

        public List<Destinatario> ObtenerDestinatariosPorTipo(int tipoEmail) {
            List<Destinatario> destinatarios = new List<Destinatario>();
            string consulta = @"
                SELECT 
                    Destinatario.EmailID,
                    Destinatario.Nombre,
                    Destinatario.Email,
                    Destinatario.Estado
                FROM
                    Destinatario
                LEFT JOIN
                    Destinatario_Detalle
                    ON Destinatario_Detalle.EmailID = Destinatario.EmailID
                WHERE
                    Destinatario_Detalle.TipoEmail = @tipoEmail;
            ";
            try {
                using(var con = new SqlConnection(_conexion)) {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@tipoEmail", tipoEmail);
                    using(var dr = query.ExecuteReader()) {
                        while(dr.Read()) {
                            Destinatario destinatario = new Destinatario {
                                EmailID = ManejoNulos.ManageNullInteger(dr["EmailID"]),
                                Nombre = ManejoNulos.ManageNullStr(dr["Nombre"]),
                                Email = ManejoNulos.ManageNullStr(dr["Email"]),
                                Estado = ManejoNulos.ManegeNullBool(dr["Estado"]),
                            };
                            destinatarios.Add(destinatario);
                        }
                    }
                }
            } catch(Exception ex) {
                Console.WriteLine(ex.Message);
            }
            return destinatarios;
        }
    }
}
