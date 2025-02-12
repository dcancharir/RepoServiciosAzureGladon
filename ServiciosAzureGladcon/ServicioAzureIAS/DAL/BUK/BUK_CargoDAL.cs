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
    public class BUK_CargoDAL {
        private string _conexion = string.Empty;
        public BUK_CargoDAL() {
            _conexion = ConfigurationManager.ConnectionStrings["conectionBdSeguridad"].ConnectionString;
        }
        public int InsertarCargo(BUK_Cargo obj) {
            int idInsertado = -1;
            string consulta = @"
IF NOT EXISTS (SELECT * FROM BUK_Cargo 
                   WHERE CodCargo=@CodCargo)
   BEGIN

INSERT INTO [BUK_Cargo]
           ([CodEmpresa]
           ,[CodCargo]
           ,[nombre])
     Output Inserted.CodCargo
     VALUES
           (@CodEmpresa
           ,@CodCargo
           ,@Nombre)
    END
";
            try {
                using(var con = new SqlConnection(_conexion)) {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@CodEmpresa", ManejoNulos.ManageNullStr(obj.codempresa));
                    query.Parameters.AddWithValue("@CodCargo", ManejoNulos.ManageNullStr(obj.codcargo));
                    query.Parameters.AddWithValue("@Nombre", ManejoNulos.ManageNullStr(obj.nombre));
                    idInsertado = Convert.ToInt32(query.ExecuteScalar());

                }
            } catch(Exception ex) {
                idInsertado = -1;
            }

            return idInsertado;
        }
    }
}
