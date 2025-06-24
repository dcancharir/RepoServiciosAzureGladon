using ServicioAzureIAS.Clases.Enum;
using ServicioAzureIAS.Clases.GestioProyectos;
using ServicioAzureIAS.utilitarios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ServicioAzureIAS.DAL.GestionProyectos {
    public class UsuarioDAL {
        private readonly ConnectionHelperDAL _connectionHelperDAL;

        public UsuarioDAL() {
            _connectionHelperDAL = new ConnectionHelperDAL();
        }

        public async Task<List<Usuario>> ObtenerUsuarios(BaseDatosEnum baseDatos) {
            List<Usuario> lista = new List<Usuario>();
            string consulta = @"
              select u.UsuarioId UsuarioId, u.Nombres Nombres,u.Apellidos Apellidos,u.Correo Correo,u.Estado Estado,u.telefono Telefono, e.IdEmpresa EmpresaId, e.IdAreaBuk AreaId,u.empleadoId from Usuario u inner join Empleado e on e.Id = u.EmpleadoId;

        ";

            try {
                using(var con = new SqlConnection(_connectionHelperDAL.ObtenerConexion(baseDatos))) {
                    await con.OpenAsync();
                    var query = new SqlCommand(consulta, con);
                    using(var dr = await query.ExecuteReaderAsync()) {
                        if(dr.HasRows) {
                            while(await dr.ReadAsync()) {
                                Usuario user = new Usuario {
                                    UsuarioId = ManejoNulos.ManageNullInteger64(dr["UsuarioId"]),
                                    Nombres = ManejoNulos.ManageNullStr(dr["Nombres"]),
                                    Apellidos = ManejoNulos.ManageNullStr(dr["Apellidos"]),
                                    Correo = ManejoNulos.ManageNullStr(dr["Correo"]),
                                    Estado = ManejoNulos.ManegeNullBool(dr["Estado"]),
                                    Telefono = ManejoNulos.ManageNullStr(dr["Telefono"]),
                                    EmpresaId = ManejoNulos.ManageNullInteger64(dr["EmpresaId"]),
                                    AreaId = ManejoNulos.ManageNullInteger64(dr["AreaId"]),
                                    EmpleadoId = ManejoNulos.ManageNullInteger64(dr["EmpleadoId"]),
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
        public async Task ActualizarUsuario(Usuario user,BaseDatosEnum baseDatos) {
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
                using(var con = new SqlConnection(_connectionHelperDAL.ObtenerConexion(baseDatos))) {
                    await con.OpenAsync();
                    using(var cmd = new SqlCommand(consulta, con)) {
                        cmd.Parameters.AddWithValue("@UsuarioId", ManejoNulos.ManageNullInteger64(user.UsuarioId));
                        cmd.Parameters.AddWithValue("@Nombres", ManejoNulos.ManageNullStr(user.Nombres));
                        cmd.Parameters.AddWithValue("@Apellidos", ManejoNulos.ManageNullStr(user.Apellidos));
                        cmd.Parameters.AddWithValue("@Correo", ManejoNulos.ManageNullStr(user.Correo));
                        cmd.Parameters.AddWithValue("@Telefono", ManejoNulos.ManageNullStr(user.Telefono));
                        cmd.Parameters.AddWithValue("@Estado", ManejoNulos.ManegeNullBool(user.Estado));
                        cmd.Parameters.AddWithValue("@EmpresaId", ManejoNulos.ManageNullInteger64(user.EmpresaId));
                        cmd.Parameters.AddWithValue("@AreaId", ManejoNulos.ManageNullInteger64(user.AreaId));
                        await cmd.ExecuteNonQueryAsync();

                    }
                }
            } catch(Exception ex) {
                funciones.logueo($"❌ Error en ActualizarUsuario: {ex.Message}");
            }
        }
        
        
    }
}
