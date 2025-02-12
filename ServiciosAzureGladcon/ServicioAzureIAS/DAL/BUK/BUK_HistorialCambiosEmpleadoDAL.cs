using ServicioAzureIAS.Clases.BUK;
using ServicioAzureIAS.utilitarios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioAzureIAS.DAL.BUK {
    public class BUK_HistorialCambiosEmpleadoDAL {
        private readonly string _conexion = string.Empty;
        public BUK_HistorialCambiosEmpleadoDAL() {
            _conexion = ConfigurationManager.ConnectionStrings["conectionBdSeguridad"].ConnectionString;
        }
        public int InsertarHistorialCambiosEmpleado(BUK_HistorialCambiosEmpleado item) {
            int idInsertado = -1;
            string consulta = @"
INSERT INTO [dbo].[BUK_Empleado]
           ([IdBuk],[TipoDocumento]
           ,[NumeroDocumento]
           ,[Nombres]
           ,[ApellidoPaterno]
           ,[ApellidoMaterno]
           ,[NombreCompleto]
           ,[IdCargo]
           ,[Cargo]
           ,[IdEmpresa]
           ,[Empresa]
           ,[FechaCese]
           ,[EstadoCese],[FechaRegistro],[ResumenCambios])
           output inserted.[IdHistorialCambiosEmpleado]
     VALUES
           (@IdBuk,@TipoDocumento
           ,@NumeroDocumento
           ,@Nombres
           ,@ApellidoPaterno
           ,@ApellidoMaterno
           ,@NombreCompleto
           ,@IdCargo
           ,@Cargo
           ,@IdEmpresa
           ,@Empresa
           ,@FechaCese
           ,@EstadoCese,getdate(),@ResumenCambios)
";
            try {
                using(var con = new SqlConnection(_conexion)) {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@IdBuk", ManejoNulos.ManageNullInteger(item.IdBuk));
                    query.Parameters.AddWithValue("@TipoDocumento", ManejoNulos.ManageNullStr(item.TipoDocumento));
                    query.Parameters.AddWithValue("@NumeroDocumento", ManejoNulos.ManageNullStr(item.NumeroDocumento));
                    query.Parameters.AddWithValue("@Nombres", ManejoNulos.ManageNullStr(item.Nombres));
                    query.Parameters.AddWithValue("@ApellidoPaterno", ManejoNulos.ManageNullStr(item.ApellidoPaterno));
                    query.Parameters.AddWithValue("@ApellidoMaterno", ManejoNulos.ManageNullStr(item.ApellidoMaterno));
                    query.Parameters.AddWithValue("@NombreCompleto", ManejoNulos.ManageNullStr(item.NombreCompleto));
                    query.Parameters.AddWithValue("@IdCargo", ManejoNulos.ManageNullInteger(item.IdCargo));
                    query.Parameters.AddWithValue("@Cargo", ManejoNulos.ManageNullStr(item.Cargo));
                    query.Parameters.AddWithValue("@IdEmpresa", ManejoNulos.ManageNullInteger(item.IdEmpresa));
                    query.Parameters.AddWithValue("@Empresa", ManejoNulos.ManageNullStr(item.Empresa));
                    query.Parameters.AddWithValue("@FechaCese", item.FechaCese == null ? SqlDateTime.Null : ManejoNulos.ManageNullDate(item.FechaCese));
                    query.Parameters.AddWithValue("@EstadoCese", ManejoNulos.ManageNullInteger(item.EstadoCese));
                    query.Parameters.AddWithValue("@ResumenCambios", ManejoNulos.ManageNullStr(item.ResumenCambios));

                    idInsertado = Convert.ToInt32(query.ExecuteScalar());

                }
            } catch(Exception ex) {
                idInsertado = -1;
            }

            return idInsertado;
        }
    }
}
