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
    public class UsuarioDAL {
        private readonly string _conexionDyd = string.Empty;
        private readonly string _conexionHolding = string.Empty;

        public UsuarioDAL() {
            _conexionDyd = ConfigurationManager.ConnectionStrings["connectionBDTasklyFlowDyd"].ConnectionString;
            _conexionHolding = ConfigurationManager.ConnectionStrings["connectionBDTasklyFlowHolding"].ConnectionString;
        }

        public async Task<List<Usuario>> ObtenerUsuarios() {
            List<Usuario> lista = new List<Usuario>();
            string consulta = @"
              select u.UsuarioId UsuarioId, u.Nombres Nombres,u.Apellidos Apellidos,u.Correo Correo,u.Estado Estado,u.telefono Telefono, e.IdEmpresa EmpresaId, e.IdAreaBuk AreaId,u.empleadoId from Usuario u inner join Empleado e on e.Id = u.EmpleadoId;

        ";

            try {
                using(var con = new SqlConnection(_conexionDyd)) {
                    await con.OpenAsync();
                    var query = new SqlCommand(consulta, con);
                    using(var dr = await query.ExecuteReaderAsync()) {
                        if(dr.HasRows) {
                            while(await dr.ReadAsync()) {
                                Usuario user = new Usuario {
                                    UsuarioId = ManejoNulos.ManageNullInteger(dr["UsuarioId"]),
                                    Nombres = ManejoNulos.ManageNullStr(dr["Nombres"]),
                                    Apellidos = ManejoNulos.ManageNullStr(dr["Apellidos"]),
                                    Correo = ManejoNulos.ManageNullStr(dr["Correo"]),
                                    Estado = ManejoNulos.ManegeNullBool(dr["Estado"]),
                                    Telefono = ManejoNulos.ManageNullStr(dr["Telefono"]),
                                    EmpresaId = ManejoNulos.ManageNullInteger(dr["EmpresaId"]),
                                    AreaId = ManejoNulos.ManageNullInteger(dr["AreaId"]),
                                    EmpleadoId = ManejoNulos.ManageNullInteger(dr["EmpleadoId"]),
                                };

                                lista.Add(user);
                            }
                        }
                    }
                }
            } catch {
                lista = new List<Usuario>();
            }

            return lista;
        }

        public async Task ActualizarUsuario(Usuario user) {
            string consulta = @"
            UPDATE Usuario
            SET
                Nombres = @Nombres,
                Apellidos = @Apellidos,
                Correo = @Correo,
                Telefono = @Telefono,
                EmpresaId = @EmpresaId,
                Estado = @Estado,
                AreaId = @AreaId
            WHERE UsuarioId = @UsuarioId
        ";

            try {
                using(var con = new SqlConnection(_conexionDyd)) {
                    await con.OpenAsync();
                    using(var cmd = new SqlCommand(consulta, con)) {
                        cmd.Parameters.AddWithValue("@UsuarioId", user.UsuarioId);
                        cmd.Parameters.AddWithValue("@Nombres", user.Nombres ?? "");
                        cmd.Parameters.AddWithValue("@Apellidos", user.Apellidos ?? "");
                        cmd.Parameters.AddWithValue("@Correo", user.Correo ?? "");
                        cmd.Parameters.AddWithValue("@Telefono", user.Telefono);
                        cmd.Parameters.AddWithValue("@Estado", user.Estado);
                        cmd.Parameters.AddWithValue("@EmpresaId", user.EmpresaId);
                        cmd.Parameters.AddWithValue("@AreaId", user.AreaId);

                        await cmd.ExecuteNonQueryAsync();
                       
                    }
                }
            } catch(Exception ex) {
                funciones.logueo($"❌ Error en ActualizarUsuario: {ex.Message}");
            }
        }


        public async Task<List<Usuario>> ObtenerUsuariosHolding() {
            List<Usuario> lista = new List<Usuario>();
            string consulta = @"
              select u.UsuarioId UsuarioId, u.Nombres Nombres,u.Apellidos Apellidos,u.Correo Correo,u.Estado Estado,u.telefono Telefono, e.IdEmpresa EmpresaId, e.IdAreaBuk AreaId,u.empleadoId from Usuario u inner join Empleado e on e.Id = u.EmpleadoId;

        ";

            try {
                using(var con = new SqlConnection(_conexionHolding)) {
                    await con.OpenAsync();
                    var query = new SqlCommand(consulta, con);
                    using(var dr = await query.ExecuteReaderAsync()) {
                        if(dr.HasRows) {
                            while(await dr.ReadAsync()) {
                                Usuario user = new Usuario {
                                    UsuarioId = ManejoNulos.ManageNullInteger(dr["UsuarioId"]),
                                    Nombres = ManejoNulos.ManageNullStr(dr["Nombres"]),
                                    Apellidos = ManejoNulos.ManageNullStr(dr["Apellidos"]),
                                    Correo = ManejoNulos.ManageNullStr(dr["Correo"]),
                                    Estado = ManejoNulos.ManegeNullBool(dr["Estado"]),
                                    Telefono = ManejoNulos.ManageNullStr(dr["Telefono"]),
                                    EmpresaId = ManejoNulos.ManageNullInteger(dr["EmpresaId"]),
                                    AreaId = ManejoNulos.ManageNullInteger(dr["AreaId"]),
                                    EmpleadoId = ManejoNulos.ManageNullInteger(dr["EmpleadoId"]),
                                };

                                lista.Add(user);
                            }
                        }
                    }
                }
            } catch {
                lista = new List<Usuario>();
            }

            return lista;
        }

        public async Task ActualizarUsuarioHolding(Usuario user) {
            string consulta = @"
            UPDATE Usuario
            SET
                Nombres = @Nombres,
                Apellidos = @Apellidos,
                Correo = @Correo,
                Telefono = @Telefono,
                EmpresaId = @EmpresaId,
                Estado = @Estado,
                AreaId = @AreaId
            WHERE UsuarioId = @UsuarioId
        ";

            try {
                using(var con = new SqlConnection(_conexionHolding)) {
                    await con.OpenAsync();
                    using(var cmd = new SqlCommand(consulta, con)) {
                        cmd.Parameters.AddWithValue("@UsuarioId", user.UsuarioId);
                        cmd.Parameters.AddWithValue("@Nombres", user.Nombres ?? "");
                        cmd.Parameters.AddWithValue("@Apellidos", user.Apellidos ?? "");
                        cmd.Parameters.AddWithValue("@Correo", user.Correo ?? "");
                        cmd.Parameters.AddWithValue("@Telefono", user.Telefono);
                        cmd.Parameters.AddWithValue("@Estado", user.Estado);
                        cmd.Parameters.AddWithValue("@EmpresaId", user.EmpresaId);
                        cmd.Parameters.AddWithValue("@AreaId", user.AreaId);

                        await cmd.ExecuteNonQueryAsync();

                    }
                }
            } catch(Exception ex) {
                funciones.logueo($"❌ Error en ActualizarUsuario: {ex.Message}");
            }
        }
    }
}
