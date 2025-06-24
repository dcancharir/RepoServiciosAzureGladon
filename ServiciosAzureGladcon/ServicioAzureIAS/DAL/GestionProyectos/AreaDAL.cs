using ServicioAzureIAS.Clases.Enum;
using ServicioAzureIAS.Clases.GestioProyectos;
using ServicioAzureIAS.DAL;
using ServicioAzureIAS.utilitarios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ServicioAzureIAS.DAL.GestionProyectos {
    public class AreaDAL {
        private readonly ConnectionHelperDAL _connectionHelperDAL;

        public AreaDAL() {
            _connectionHelperDAL = new ConnectionHelperDAL();
        }

        public async Task<List<Area>> ObtenerAreas(BaseDatosEnum baseDatos) {
            List<Area> lista = new List<Area>();
            string consulta = @"
                SELECT *  FROM area
            ";

            try {
                using(var con = new SqlConnection(_connectionHelperDAL.ObtenerConexion(baseDatos))) {
                    await con.OpenAsync();
                    var query = new SqlCommand(consulta, con);
                    using(var dr = await query.ExecuteReaderAsync()) {
                        if(dr.HasRows) {
                            while(await dr.ReadAsync()) {
                                Area response = new Area {
                                    IdArea = ManejoNulos.ManageNullInteger64(dr["IdArea"]),
                                    IdAreaBuk = ManejoNulos.ManageNullInteger64(dr["IdAreaBuk"]),
                                    area = ManejoNulos.ManageNullStr(dr["Area"]),
                                    estado = ManejoNulos.ManegeNullBool(dr["estado"]),

                                };

                                lista.Add(response);
                            }
                        }
                    }
                }
            } catch {
                lista = new List<Area>();
            }
            return lista;
        }
        public async Task InsertarArea(Area area, BaseDatosEnum baseDatos) {
            string consulta = @"
        INSERT INTO Area (IdAreaBuk, Area, Estado)
        VALUES (@IdAreaBuk, @Area, @Estado)
    "
    ;

            using(var con = new SqlConnection(_connectionHelperDAL.ObtenerConexion(baseDatos))) {
                await con.OpenAsync();
                using(var cmd = new SqlCommand(consulta, con)) {
                    cmd.Parameters.AddWithValue("@IdAreaBuk", ManejoNulos.ManageNullInteger64(area.IdAreaBuk));
                    cmd.Parameters.AddWithValue("@Area", ManejoNulos.ManageNullStr(area.area));
                    cmd.Parameters.AddWithValue("@Estado", ManejoNulos.ManegeNullBool(area.estado));

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task<bool> ActualizarArea(Area area,BaseDatosEnum baseDatos) {
            string consulta = @"
                        UPDATE Area
                        SET 
                            IdAreaBuk  = @IdAreaBuk
                            Area = @Area,
                            Estado = @Estado
                        WHERE IdArea = @IdArea
                    ";

            try {
                using(var con = new SqlConnection(_connectionHelperDAL.ObtenerConexion(baseDatos))) {
                    await con.OpenAsync();
                    using(var cmd = new SqlCommand(consulta, con)) {
                        cmd.Parameters.AddWithValue("@IdAreaBuk", ManejoNulos.ManageNullInteger64(area.IdAreaBuk));
                        cmd.Parameters.AddWithValue("@Area", ManejoNulos.ManageNullStr(area.area));
                        cmd.Parameters.AddWithValue("@Estado", ManejoNulos.ManegeNullBool(area.estado));
                        cmd.Parameters.AddWithValue("@IdArea", ManejoNulos.ManageNullInteger64(area.IdArea));

                        int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                        return filasAfectadas > 0;
                    }
                }
            } catch(Exception ex) {
                Console.WriteLine($"Error al actualizar el área: {ex.Message}");
                return false;
            }
        }
       
    }
}
