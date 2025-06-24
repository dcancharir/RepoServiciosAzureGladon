using ServicioAzureIAS.Clases.Enum;
using ServicioAzureIAS.Clases.GestioProyectos;
using ServicioAzureIAS.utilitarios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ServicioAzureIAS.DAL.GestionProyectos {
    public class EmpleadoDAL {
        private readonly ConnectionHelperDAL _connectionHelperDAL;

        public EmpleadoDAL() {
            _connectionHelperDAL = new ConnectionHelperDAL();
        }

        public async Task<List<Empleado>> ObtenerEmpleados(BaseDatosEnum baseDatos) {
            List<Empleado> lista = new List<Empleado>();
            string consulta = @"
                SELECT * 
                FROM empleado
            ";

            try {
                using(var con = new SqlConnection(_connectionHelperDAL.ObtenerConexion(baseDatos))) {
                    await con.OpenAsync();
                    var query = new SqlCommand(consulta, con);
                    using(var dr = await query.ExecuteReaderAsync()) {
                        if(dr.HasRows) {
                            while(await dr.ReadAsync()) {
                                Empleado response = new Empleado {
                                    Id = ManejoNulos.ManageNullInteger64(dr["Id"]),
                                    IdBuk = ManejoNulos.ManageNullInteger64(dr["IdBuk"]),
                                    TipoDocumento = ManejoNulos.ManageNullStr(dr["TipoDocumento"]),
                                    NumeroDocumento = ManejoNulos.ManageNullStr(dr["NumeroDocumento"]),
                                    Nombres = ManejoNulos.ManageNullStr(dr["Nombres"]),
                                    ApellidoPaterno = ManejoNulos.ManageNullStr(dr["ApellidoPaterno"]),
                                    ApellidoMaterno = ManejoNulos.ManageNullStr(dr["ApellidoMaterno"]),
                                    NombreCompleto = ManejoNulos.ManageNullStr(dr["NombreCompleto"]),
                                    IdEmpresa = ManejoNulos.ManageNullInteger64(dr["IdEmpresa"]),
                                    Empresa = ManejoNulos.ManageNullStr(dr["Empresa"]),
                                    Correo = ManejoNulos.ManageNullStr(dr["Correo"]),
                                    Celular = ManejoNulos.ManageNullStr(dr["Celular"]),
                                    EstadoCese = ManejoNulos.ManegeNullBool(dr["EstadoCese"]),
                                    IdAreaBuk = ManejoNulos.ManageNullInteger64(dr["IdAreaBuk"]),
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

        public async Task<bool> ActualizarEmpleado(Empleado empleado, BaseDatosEnum baseDatos) {
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
                using(var con = new SqlConnection(_connectionHelperDAL.ObtenerConexion(baseDatos))) {
                    await con.OpenAsync();
                    using(var cmd = new SqlCommand(query, con)) {
                        cmd.Parameters.AddWithValue("@TipoDocumento", ManejoNulos.ManageNullStr(empleado.TipoDocumento));
                        cmd.Parameters.AddWithValue("@NumeroDocumento", ManejoNulos.ManageNullStr(empleado.NumeroDocumento));
                        cmd.Parameters.AddWithValue("@Nombres", ManejoNulos.ManageNullStr(empleado.Nombres));
                        cmd.Parameters.AddWithValue("@ApellidoPaterno", ManejoNulos.ManageNullStr(empleado.ApellidoPaterno));
                        cmd.Parameters.AddWithValue("@ApellidoMaterno", ManejoNulos.ManageNullStr(empleado.ApellidoMaterno));
                        cmd.Parameters.AddWithValue("@NombreCompleto", ManejoNulos.ManageNullStr(empleado.NombreCompleto));
                        cmd.Parameters.AddWithValue("@IdEmpresa", ManejoNulos.ManageNullInteger64(empleado.IdEmpresa));
                        cmd.Parameters.AddWithValue("@Empresa", ManejoNulos.ManageNullStr(empleado.Empresa));
                        cmd.Parameters.AddWithValue("@Correo", ManejoNulos.ManageNullStr(empleado.Correo));
                        cmd.Parameters.AddWithValue("@Celular", ManejoNulos.ManageNullStr(empleado.Celular));
                        cmd.Parameters.AddWithValue("@EstadoCese", ManejoNulos.ManegeNullBool(empleado.EstadoCese));
                        cmd.Parameters.AddWithValue("@IdAreaBuk", ManejoNulos.ManageNullInteger64(empleado.IdAreaBuk));
                        cmd.Parameters.AddWithValue("@Area", ManejoNulos.ManageNullStr(empleado.Area));
                        cmd.Parameters.AddWithValue("@IdBuk", ManejoNulos.ManageNullInteger64(empleado.IdBuk));
                        cmd.Parameters.AddWithValue("@Id", ManejoNulos.ManageNullInteger64(empleado.Id));

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

        public async Task<bool> InsertarEmpleado(Empleado empleado, BaseDatosEnum baseDatos) {
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
                using(var con = new SqlConnection(_connectionHelperDAL.ObtenerConexion(baseDatos))) {
                    await con.OpenAsync();
                    using(var cmd = new SqlCommand(query, con)) {
                        cmd.Parameters.AddWithValue("@IdBuk", ManejoNulos.ManageNullInteger64(empleado.IdBuk));
                        cmd.Parameters.AddWithValue("@TipoDocumento", ManejoNulos.ManageNullStr(empleado.TipoDocumento));
                        cmd.Parameters.AddWithValue("@NumeroDocumento", ManejoNulos.ManageNullStr(empleado.NumeroDocumento));
                        cmd.Parameters.AddWithValue("@Nombres", ManejoNulos.ManageNullStr(empleado.Nombres));
                        cmd.Parameters.AddWithValue("@ApellidoPaterno", ManejoNulos.ManageNullStr(empleado.ApellidoPaterno));
                        cmd.Parameters.AddWithValue("@ApellidoMaterno", ManejoNulos.ManageNullStr(empleado.ApellidoMaterno));
                        cmd.Parameters.AddWithValue("@NombreCompleto", ManejoNulos.ManageNullStr(empleado.NombreCompleto));
                        cmd.Parameters.AddWithValue("@IdEmpresa", ManejoNulos.ManageNullInteger64(empleado.IdEmpresa));
                        cmd.Parameters.AddWithValue("@Empresa", ManejoNulos.ManageNullStr(empleado.Empresa));
                        cmd.Parameters.AddWithValue("@Correo", ManejoNulos.ManageNullStr(empleado.Correo));
                        cmd.Parameters.AddWithValue("@Celular", ManejoNulos.ManageNullStr(empleado.Celular));
                        cmd.Parameters.AddWithValue("@EstadoCese", ManejoNulos.ManegeNullBool(empleado.EstadoCese));
                        cmd.Parameters.AddWithValue("@IdAreaBuk", ManejoNulos.ManageNullInteger64(empleado.IdAreaBuk));
                        cmd.Parameters.AddWithValue("@Area", ManejoNulos.ManageNullStr(empleado.Area));

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
