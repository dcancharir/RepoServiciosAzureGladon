using ServicioAzureIAS.Clases.Mincetur;
using ServicioAzureIAS.utilitarios;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace ServicioAzureIAS.DAL.Mincetur {
    public class MinceturDAL {
        private readonly string _conexion = string.Empty;

        public MinceturDAL() {
            _conexion = ConfigurationManager.ConnectionStrings["conectionBdSeguridad"].ConnectionString;
        }

        public List<CredencialMincetur> ObtenerCredencialesMinceturActivas() {
            List<CredencialMincetur> lista = new List<CredencialMincetur>();
            string consulta = @"
                SELECT
                    * 
                FROM
                    CAL_CredencialMincetur
                WHERE
                    estado = 1
            ";

            try {
                using(var con = new SqlConnection(_conexion)) {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    using(var dr = query.ExecuteReader()) {
                        if(dr.HasRows) {
                            while(dr.Read()) {
                                CredencialMincetur credencial = new CredencialMincetur {
                                    idCredencialMincetur = ManejoNulos.ManageNullInteger(dr["idCredencialMincetur"]),
                                    codEmpresa = ManejoNulos.ManageNullInteger(dr["codEmpresa"]),
                                    token = ManejoNulos.ManageNullStr(dr["token"]),
                                    estado = ManejoNulos.ManegeNullBool(dr["estado"]),
                                };

                                lista.Add(credencial);
                            }
                        }
                    }
                }
            } catch {
                lista = new List<CredencialMincetur>();
            }
            return lista;
        }
    }
}
