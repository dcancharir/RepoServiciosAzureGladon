using ServicioAzureIAS.Clases.BUK;
using ServicioAzureIAS.utilitarios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioAzureIAS.DAL.BUK {
    public class BUK_EquivalenciaEmpresaDAL {
        private string _conexion = string.Empty;
        public BUK_EquivalenciaEmpresaDAL() {
            _conexion = ConfigurationManager.ConnectionStrings["conectionBdSeguridad"].ConnectionString;
        }
        public List<BUK_EquivalenciaEmpresa> ObtenerEmpresas() {
            string consulta = $@"SELECT [IdEquivalenciaEmpresa]
      ,[Nombre]
      ,[CodEmpresaOfisis]
      ,[IdEmpresaBuk]
  FROM [BUK_EquivalenciaEmpresa]";
            List<BUK_EquivalenciaEmpresa> result = new List<BUK_EquivalenciaEmpresa>();
            try {
                using(var con = new SqlConnection(_conexion)) {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    using(var dr = query.ExecuteReader()) {
                        while(dr.Read()) {
                            BUK_EquivalenciaEmpresa item = new BUK_EquivalenciaEmpresa() {
                                IdEquivalenciaEmpresa = ManejoNulos.ManageNullInteger(dr["IdEquivalenciaEmpresa"]),
                                Nombre = ManejoNulos.ManageNullStr(dr["Nombre"]),
                                CodEmpresaOfisis = ManejoNulos.ManageNullStr(dr["CodEmpresaOfisis"]),
                                IdEmpresaBuk = ManejoNulos.ManageNullInteger(dr["IdEmpresaBuk"]),
                            };
                            result.Add(item);
                        }
                    }
                }
            } catch(Exception ex) {
                return new List<BUK_EquivalenciaEmpresa>();
            }
            return result;
        }
    }
}
