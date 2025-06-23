using ServicioAzureIAS.Clases.GestioProyectos;
using ServicioAzureIAS.utilitarios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioAzureIAS.DAL.GestionProyectos {
    public class EmpleadoDAL {

        private readonly string _conexionDyd = string.Empty;
        private readonly string _conexionHolding = string.Empty;

        public EmpleadoDAL() {
            _conexionDyd = ConfigurationManager.ConnectionStrings["connectionBDTasklyFlowDyd"].ConnectionString;
            _conexionHolding = ConfigurationManager.ConnectionStrings["connectionBDTasklyFlowHolding"].ConnectionString;
        }

        public async Task<List<Empleado>> ObtenerEmpleados() {
            List<Empleado> lista = new List<Empleado>();
            string consulta = @"
                SELECT
                    * 
                FROM
                    empleado
                
            ";

            try {
                using(var con = new SqlConnection(_conexionDyd)) {
                    await con.OpenAsync();
                    var query = new SqlCommand(consulta, con);
                    using(var dr = await query.ExecuteReaderAsync()) {
                        if(dr.HasRows) {
                            while(await dr.ReadAsync()) {
                                Empleado response = new Empleado {
                                    Id = ManejoNulos.ManageNullInteger(dr["Id"]),
                                    IdBuk = ManejoNulos.ManageNullInteger(dr["IdBuk"]),
                                    TipoDocumento = ManejoNulos.ManageNullStr(dr["TipoDocumento"]),
                                    NumeroDocumento = ManejoNulos.ManageNullStr(dr["NumeroDocumento"]),
                                    Nombres = ManejoNulos.ManageNullStr(dr["Nombres"]),
                                    ApellidoPaterno = ManejoNulos.ManageNullStr(dr["ApellidoPaterno"]),
                                    ApellidoMaterno = ManejoNulos.ManageNullStr(dr["ApellidoMaterno"]),
                                    NombreCompleto = ManejoNulos.ManageNullStr(dr["NombreCompleto"]),
                                    IdEmpresa = ManejoNulos.ManageNullInteger(dr["IdEmpresa"]),
                                    Empresa = ManejoNulos.ManageNullStr(dr["Empresa"]),
                                    Correo = ManejoNulos.ManageNullStr(dr["Correo"]),
                                    Celular = ManejoNulos.ManageNullStr(dr["Celular"]),
                                    EstadoCese = ManejoNulos.ManegeNullBool(dr["EstadoCese"]),
                                    IdAreaBuk = ManejoNulos.ManageNullInteger(dr["IdAreaBuk"]),
                                    Area = ManejoNulos.ManageNullStr(dr["Area"])
                                };


                                lista.Add(response);
                            }
                        }
                    }
                }
            } catch {
                lista = new List<Empleado>();
            }
            return lista;
        }

        public async Task<List<Area>> ObtenerAreasEmpleados() {
            List<Area> lista = new List<Area>();
            string consulta = @"
        SELECT IdAreaBuk, MIN(Area) AS Area
        FROM Empleado
        GROUP BY IdAreaBuk
        ORDER BY Area;
    ";

            try {
                using(var con = new SqlConnection(_conexionDyd)) {
                    await con.OpenAsync();
                    var query = new SqlCommand(consulta, con);
                    using(var dr = await query.ExecuteReaderAsync()) {
                        if(dr.HasRows) {
                            while(await dr.ReadAsync()) {
                                Area area = new Area {
                                    IdAreaBuk = ManejoNulos.ManageNullInteger(dr["IdAreaBuk"]),
                                    area = ManejoNulos.ManageNullStr(dr["Area"])
                                };

                                lista.Add(area);
                            }
                        }
                    }
                }
            } catch {
                lista = new List<Area>();
            }
            return lista;
        }
        public async Task<bool> ActualizarEmpleado(Empleado empleado) {
           

            string query = @"
    UPDATE Empleado SET
        TipoDocumento = @TipoDocumento,
        NumeroDocumento = @NumeroDocumento,
        Nombres = @Nombres,
        ApellidoPaterno = @ApellidoPaterno,
        ApellidoMaterno = @ApellidoMaterno,
        NombreCompleto = @NombreCompleto,
        IdEmpresa = @IdEmpresa,
        Empresa = @Empresa,
        Correo = @Correo,
        Celular = @Celular,
        EstadoCese = @EstadoCese,
        IdAreaBuk = @IdAreaBuk,
        Area = @Area
    WHERE id = @Id
";

            try {
                using(var con = new SqlConnection(_conexionDyd)) {
                    await con.OpenAsync();
                    using(var cmd = new SqlCommand(query, con)) {
                        cmd.Parameters.AddWithValue("@TipoDocumento", empleado.TipoDocumento );
                        cmd.Parameters.AddWithValue("@NumeroDocumento", empleado.NumeroDocumento );
                        cmd.Parameters.AddWithValue("@Nombres", empleado.Nombres );
                        cmd.Parameters.AddWithValue("@ApellidoPaterno", empleado.ApellidoPaterno );
                        cmd.Parameters.AddWithValue("@ApellidoMaterno", empleado.ApellidoMaterno ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@NombreCompleto", empleado.NombreCompleto );
                        cmd.Parameters.AddWithValue("@IdEmpresa", empleado.IdEmpresa);
                        cmd.Parameters.AddWithValue("@Empresa", empleado.Empresa);
                        cmd.Parameters.AddWithValue("@Correo", empleado.Correo);
                        cmd.Parameters.AddWithValue("@Celular", empleado.Celular);
                        cmd.Parameters.AddWithValue("@EstadoCese", empleado.EstadoCese);
                        cmd.Parameters.AddWithValue("@IdAreaBuk", empleado.IdAreaBuk);
                        cmd.Parameters.AddWithValue("@Area", empleado.Area );
                        cmd.Parameters.AddWithValue("@IdBuk", empleado.IdBuk );
                        cmd.Parameters.AddWithValue("@Id", empleado.Id);

                        int rows = await cmd.ExecuteNonQueryAsync();
                        return rows > 0;
                    }
                }
            } catch(Exception ex) {
                string errorString = $"Error al actualizar el empleado: {ex.Message}\nEmpleado: {empleado.ToString()}";
                funciones.logueo(empleado.ToString());
                throw new Exception("Error al actualizar el empleado: " + ex.Message, ex);
            }
        }

        public async Task<bool> InsertarEmpleado(Empleado empleado) {
            string query = @"
    INSERT INTO Empleado (
        IdBuk,
        TipoDocumento,
        NumeroDocumento,
        Nombres,
        ApellidoPaterno,
        ApellidoMaterno,
        NombreCompleto,
        IdEmpresa,
        Empresa,
        Correo,
        Celular,
        EstadoCese,
        IdAreaBuk,
        Area
    )
    VALUES (
        @IdBuk,
        @TipoDocumento,
        @NumeroDocumento,
        @Nombres,
        @ApellidoPaterno,
        @ApellidoMaterno,
        @NombreCompleto,
        @IdEmpresa,
        @Empresa,
        @Correo,
        @Celular,
        @EstadoCese,
        @IdAreaBuk,
        @Area
    )
";

            try {
                using(var con = new SqlConnection(_conexionDyd)) {
                    await con.OpenAsync();
                    using(var cmd = new SqlCommand(query, con)) {
                        cmd.Parameters.AddWithValue("@IdBuk", empleado.IdBuk );
                        cmd.Parameters.AddWithValue("@TipoDocumento", empleado.TipoDocumento );
                        cmd.Parameters.AddWithValue("@NumeroDocumento", empleado.NumeroDocumento );
                        cmd.Parameters.AddWithValue("@Nombres", empleado.Nombres );
                        cmd.Parameters.AddWithValue("@ApellidoPaterno", empleado.ApellidoPaterno );
                        cmd.Parameters.AddWithValue("@ApellidoMaterno", empleado.ApellidoMaterno ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@NombreCompleto", empleado.NombreCompleto );
                        cmd.Parameters.AddWithValue("@IdEmpresa", empleado.IdEmpresa);
                        cmd.Parameters.AddWithValue("@Empresa", empleado.Empresa );
                        cmd.Parameters.AddWithValue("@Correo", empleado.Correo );
                        cmd.Parameters.AddWithValue("@Celular", empleado.Celular );
                        cmd.Parameters.AddWithValue("@EstadoCese", empleado.EstadoCese );
                        cmd.Parameters.AddWithValue("@IdAreaBuk", empleado.IdAreaBuk);
                        cmd.Parameters.AddWithValue("@Area", empleado.Area );

                        int rows = await cmd.ExecuteNonQueryAsync();
                        return rows > 0;
                    }
                }
            } catch(Exception ex) {
                string errorString = $"Error al insertar el empleado: {ex.Message}\nEmpleado: {empleado.ToString()}";
                funciones.logueo(errorString);
                throw new Exception("Error al insertar el empleado: " + ex.Message, ex);
            }
        }


        public async Task<List<Empleado>> ObtenerEmpleadosHolding() {
            List<Empleado> lista = new List<Empleado>();
            string consulta = @"
                SELECT
                    * 
                FROM
                    empleado
                
            ";

            try {
                using(var con = new SqlConnection(_conexionHolding)) {
                    await con.OpenAsync();
                    var query = new SqlCommand(consulta, con);
                    using(var dr = await query.ExecuteReaderAsync()) {
                        if(dr.HasRows) {
                            while(await dr.ReadAsync()) {
                                Empleado response = new Empleado {
                                    Id = ManejoNulos.ManageNullInteger(dr["Id"]),
                                    IdBuk = ManejoNulos.ManageNullInteger(dr["IdBuk"]),
                                    TipoDocumento = ManejoNulos.ManageNullStr(dr["TipoDocumento"]),
                                    NumeroDocumento = ManejoNulos.ManageNullStr(dr["NumeroDocumento"]),
                                    Nombres = ManejoNulos.ManageNullStr(dr["Nombres"]),
                                    ApellidoPaterno = ManejoNulos.ManageNullStr(dr["ApellidoPaterno"]),
                                    ApellidoMaterno = ManejoNulos.ManageNullStr(dr["ApellidoMaterno"]),
                                    NombreCompleto = ManejoNulos.ManageNullStr(dr["NombreCompleto"]),
                                    IdEmpresa = ManejoNulos.ManageNullInteger(dr["IdEmpresa"]),
                                    Empresa = ManejoNulos.ManageNullStr(dr["Empresa"]),
                                    Correo = ManejoNulos.ManageNullStr(dr["Correo"]),
                                    Celular = ManejoNulos.ManageNullStr(dr["Celular"]),
                                    EstadoCese = ManejoNulos.ManegeNullBool(dr["EstadoCese"]),
                                    IdAreaBuk = ManejoNulos.ManageNullInteger(dr["IdAreaBuk"]),
                                    Area = ManejoNulos.ManageNullStr(dr["Area"])
                                };


                                lista.Add(response);
                            }
                        }
                    }
                }
            } catch {
                lista = new List<Empleado>();
            }
            return lista;
        }

        public async Task<bool> ActualizarEmpleadoHolding(Empleado empleado) {


            string query = @"
    UPDATE Empleado SET
        TipoDocumento = @TipoDocumento,
        NumeroDocumento = @NumeroDocumento,
        Nombres = @Nombres,
        ApellidoPaterno = @ApellidoPaterno,
        ApellidoMaterno = @ApellidoMaterno,
        NombreCompleto = @NombreCompleto,
        IdEmpresa = @IdEmpresa,
        Empresa = @Empresa,
        Correo = @Correo,
        Celular = @Celular,
        EstadoCese = @EstadoCese,
        IdAreaBuk = @IdAreaBuk,
        Area = @Area
    WHERE id = @Id
";

            try {
                using(var con = new SqlConnection(_conexionHolding)) {
                    await con.OpenAsync();
                    using(var cmd = new SqlCommand(query, con)) {
                        cmd.Parameters.AddWithValue("@TipoDocumento", empleado.TipoDocumento);
                        cmd.Parameters.AddWithValue("@NumeroDocumento", empleado.NumeroDocumento);
                        cmd.Parameters.AddWithValue("@Nombres", empleado.Nombres);
                        cmd.Parameters.AddWithValue("@ApellidoPaterno", empleado.ApellidoPaterno);
                        cmd.Parameters.AddWithValue("@ApellidoMaterno", empleado.ApellidoMaterno ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@NombreCompleto", empleado.NombreCompleto);
                        cmd.Parameters.AddWithValue("@IdEmpresa", empleado.IdEmpresa);
                        cmd.Parameters.AddWithValue("@Empresa", empleado.Empresa);
                        cmd.Parameters.AddWithValue("@Correo", empleado.Correo);
                        cmd.Parameters.AddWithValue("@Celular", empleado.Celular);
                        cmd.Parameters.AddWithValue("@EstadoCese", empleado.EstadoCese);
                        cmd.Parameters.AddWithValue("@IdAreaBuk", empleado.IdAreaBuk);
                        cmd.Parameters.AddWithValue("@Area", empleado.Area);
                        cmd.Parameters.AddWithValue("@IdBuk", empleado.IdBuk);
                        cmd.Parameters.AddWithValue("@Id", empleado.Id);

                        int rows = await cmd.ExecuteNonQueryAsync();
                        return rows > 0;
                    }
                }
            } catch(Exception ex) {
                string errorString = $"Error al actualizar el empleado: {ex.Message}\nEmpleado: {empleado.ToString()}";
                funciones.logueo(empleado.ToString());
                throw new Exception("Error al actualizar el empleado: " + ex.Message, ex);
            }
        }

        public async Task<bool> InsertarEmpleadoHolding(Empleado empleado) {
            string query = @"
    INSERT INTO Empleado (
        IdBuk,
        TipoDocumento,
        NumeroDocumento,
        Nombres,
        ApellidoPaterno,
        ApellidoMaterno,
        NombreCompleto,
        IdEmpresa,
        Empresa,
        Correo,
        Celular,
        EstadoCese,
        IdAreaBuk,
        Area
    )
    VALUES (
        @IdBuk,
        @TipoDocumento,
        @NumeroDocumento,
        @Nombres,
        @ApellidoPaterno,
        @ApellidoMaterno,
        @NombreCompleto,
        @IdEmpresa,
        @Empresa,
        @Correo,
        @Celular,
        @EstadoCese,
        @IdAreaBuk,
        @Area
    )
";

            try {
                using(var con = new SqlConnection(_conexionHolding)) {
                    await con.OpenAsync();
                    using(var cmd = new SqlCommand(query, con)) {
                        cmd.Parameters.AddWithValue("@IdBuk", empleado.IdBuk);
                        cmd.Parameters.AddWithValue("@TipoDocumento", empleado.TipoDocumento);
                        cmd.Parameters.AddWithValue("@NumeroDocumento", empleado.NumeroDocumento);
                        cmd.Parameters.AddWithValue("@Nombres", empleado.Nombres);
                        cmd.Parameters.AddWithValue("@ApellidoPaterno", empleado.ApellidoPaterno);
                        cmd.Parameters.AddWithValue("@ApellidoMaterno", empleado.ApellidoMaterno ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@NombreCompleto", empleado.NombreCompleto);
                        cmd.Parameters.AddWithValue("@IdEmpresa", empleado.IdEmpresa);
                        cmd.Parameters.AddWithValue("@Empresa", empleado.Empresa);
                        cmd.Parameters.AddWithValue("@Correo", empleado.Correo);
                        cmd.Parameters.AddWithValue("@Celular", empleado.Celular);
                        cmd.Parameters.AddWithValue("@EstadoCese", empleado.EstadoCese);
                        cmd.Parameters.AddWithValue("@IdAreaBuk", empleado.IdAreaBuk);
                        cmd.Parameters.AddWithValue("@Area", empleado.Area);

                        int rows = await cmd.ExecuteNonQueryAsync();
                        return rows > 0;
                    }
                }
            } catch(Exception ex) {
                string errorString = $"Error al insertar el empleado: {ex.Message}\nEmpleado: {empleado.ToString()}";
                funciones.logueo(errorString);
                throw new Exception("Error al insertar el empleado: " + ex.Message, ex);
            }
        }


    }


}
