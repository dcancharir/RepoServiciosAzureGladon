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
    public class BUK_EmpleadoDAL {
        private string _conexion = string.Empty;
        public BUK_EmpleadoDAL() {
            _conexion = ConfigurationManager.ConnectionStrings["conectionBdSeguridad"].ConnectionString;
        }
        public int InsertarEmpleado(BUK_Empleado item) {
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
           ,[EstadoCese],[FechaRegistro])
           output inserted.[IdBuk]
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
           ,@EstadoCese,getdate())
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

                    idInsertado = Convert.ToInt32(query.ExecuteScalar());

                }
            } catch(Exception ex) {
                idInsertado = -1;
            }

            return idInsertado;
        }
        public List<BUK_Empleado> ListarEmpleadosPorEmpresaBuk(int idEmpresaBuk) {
            string consulta = $@"SELECT [IdBuk]
      ,[TipoDocumento]
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
      ,[EstadoCese]
  FROM [dbo].[BUK_Empleado] where IdEmpresa = @IdEmpresa";
            List<BUK_Empleado> result = new List<BUK_Empleado>();
            try {
                using(var con = new SqlConnection(_conexion)) {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@IdEmpresa", idEmpresaBuk);
                    using(var dr = query.ExecuteReader()) {
                        while(dr.Read()) {
                            BUK_Empleado item = new BUK_Empleado() {
                                IdBuk = ManejoNulos.ManageNullInteger(dr["IdBuk"]),
                                TipoDocumento = ManejoNulos.ManageNullStr(dr["TipoDocumento"]),
                                NumeroDocumento = ManejoNulos.ManageNullStr(dr["NumeroDocumento"]),
                                Nombres = ManejoNulos.ManageNullStr(dr["Nombres"]),
                                ApellidoPaterno = ManejoNulos.ManageNullStr(dr["ApellidoPaterno"]),
                                ApellidoMaterno = ManejoNulos.ManageNullStr(dr["ApellidoMaterno"]),
                                NombreCompleto = ManejoNulos.ManageNullStr(dr["NombreCompleto"]),
                                IdCargo = ManejoNulos.ManageNullInteger(dr["IdCargo"]),
                                Cargo = ManejoNulos.ManageNullStr(dr["Cargo"]),
                                IdEmpresa = ManejoNulos.ManageNullInteger(dr["IdEmpresa"]),
                                Empresa = ManejoNulos.ManageNullStr(dr["Empresa"]),
                                FechaCese = ManejoNulos.ManageNullDate(dr["FechaCese"]),
                                EstadoCese = ManejoNulos.ManegeNullBool(dr["EstadoCese"]),
                            };
                            result.Add(item);
                        }
                    }
                }
            } catch(Exception ex) {
                return new List<BUK_Empleado>();
            }
            return result;
        }
        public bool EditarEmpleado(BUK_Empleado item) {
            bool modificado = false;
            string consulta = @"
               UPDATE [dbo].[BUK_Empleado]
                       SET 
                          [TipoDocumento] = @TipoDocumento
                          ,[NumeroDocumento] = @NumeroDocumento
                          ,[Nombres] = @Nombres
                          ,[ApellidoPaterno] = @ApellidoPaterno
                          ,[ApellidoMaterno] = @ApellidoMaterno
                          ,[NombreCompleto] = @NombreCompleto
                          ,[IdCargo] = @IdCargo
                          ,[Cargo] = @Cargo
                          ,[IdEmpresa] = @IdEmpresa
                          ,[Empresa] = @Empresa
                          ,[FechaCese] = @FechaCese
                          ,[EstadoCese] = @EstadoCese,@FechaModificacion = getdate()
                     WHERE IdBuk = @IdBuk;
            ";
            try {
                using(var con = new SqlConnection(_conexion)) {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
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
                    query.Parameters.AddWithValue("@FechaCese", ManejoNulos.ManageNullDate(item.FechaCese));
                    query.Parameters.AddWithValue("@EstadoCese", ManejoNulos.ManageNullInteger(item.EstadoCese));
                    query.Parameters.AddWithValue("@IdBuk", ManejoNulos.ManageNullInteger(item.IdBuk));
                    query.ExecuteNonQuery();
                    modificado = true;
                }
            } catch(Exception ex) {
                modificado = false;
            }
            return modificado;
        }
    }
}
