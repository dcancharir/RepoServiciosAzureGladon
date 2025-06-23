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
    public class AreaDAL {

        private readonly string _conexionDyd = string.Empty;
        private readonly string _conexionHolding = string.Empty;

        public AreaDAL() {
            _conexionDyd = ConfigurationManager.ConnectionStrings["connectionBDTasklyFlowDyd"].ConnectionString;
            _conexionHolding = ConfigurationManager.ConnectionStrings["connectionBDTasklyFlowHolding"].ConnectionString;
        }

        public async Task<List<Area>> ObtenerAreas() {
            List<Area> lista = new List<Area>();
            string consulta = @"
                SELECT
                    * 
                FROM
                    area
                
            ";

            try {
                using(var con = new SqlConnection(_conexionDyd)) {
                    await con.OpenAsync();
                    var query = new SqlCommand(consulta, con);
                    using(var dr = await query.ExecuteReaderAsync()) {
                        if(dr.HasRows) {
                            while(await dr.ReadAsync()) {
                                Area response = new Area {
                                    IdArea = ManejoNulos.ManageNullInteger(dr["IdArea"]),
                                    IdAreaBuk = ManejoNulos.ManageNullInteger(dr["IdAreaBuk"]),
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

        public async Task<Area> ObtenerAreaPorIdBuk(long idAreaBuk) {
            string consulta = "SELECT * FROM area WHERE IdAreaBuk = @idAreaBuk AND estado = 1";

            try {
                using(var con = new SqlConnection(_conexionDyd)) {
                    await con.OpenAsync();
                    using(var cmd = new SqlCommand(consulta, con)) {
                        cmd.Parameters.AddWithValue("@idAreaBuk", idAreaBuk);
                        using(var dr = await cmd.ExecuteReaderAsync()) {
                            if(await dr.ReadAsync()) {
                                return new Area {
                                    IdArea = ManejoNulos.ManageNullInteger(dr["IdArea"]),
                                    IdAreaBuk = ManejoNulos.ManageNullInteger(dr["IdAreaBuk"]),
                                    area = ManejoNulos.ManageNullStr(dr["Area"]),
                                    estado = ManejoNulos.ManegeNullBool(dr["estado"]),
                                };
                            }
                        }
                    }
                }
            } catch {
               
            }

            return null; 
        }

        public async Task<bool> ObtenerAreaPorIdBukHolding(long idAreaBuk) {
            string consulta = "SELECT COUNT(*) FROM area WHERE IdAreaBuk = @idAreaBuk AND estado = 1";

            try {
                using(var con = new SqlConnection(_conexionHolding)) {
                    await con.OpenAsync();
                    using(var cmd = new SqlCommand(consulta, con)) {
                        cmd.Parameters.AddWithValue("@idAreaBuk", idAreaBuk);
                        int count = (int)await cmd.ExecuteScalarAsync();
                        return count > 0;
                    }
                }
            } catch {
                return false;
            }
        }
        public async Task InsertarArea(Area area) {
            string consulta = @"
        INSERT INTO Area (IdAreaBuk, Area, Estado)
        VALUES (@IdAreaBuk, @Area, @Estado)
    ";

            using(var con = new SqlConnection(_conexionDyd)) {
                await con.OpenAsync();
                using(var cmd = new SqlCommand(consulta, con)) {
                    cmd.Parameters.AddWithValue("@IdAreaBuk", area.IdAreaBuk);
                    cmd.Parameters.AddWithValue("@Area", area.area);
                    cmd.Parameters.AddWithValue("@Estado", area.estado);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task ActualizarArea(Area area) {
            string consulta = @"
                        UPDATE Area
                        SET 
                            IdAreaBuk  = @IdAreaBuk
                            Area = @Area,
                            Estado = @Estado
                        WHERE IdArea = @IdArea
                    ";

            using(var con = new SqlConnection(_conexionDyd)) {
                await con.OpenAsync();
                using(var cmd = new SqlCommand(consulta, con)) {
                    cmd.Parameters.AddWithValue("@Area", area.area);
                    cmd.Parameters.AddWithValue("@Estado", area.estado);
                    cmd.Parameters.AddWithValue("@IdAreaBuk", area.IdAreaBuk);
                    cmd.Parameters.AddWithValue("@IdArea", area.IdArea);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }


        public async Task<List<Area>> ObtenerAreasHolding() {
            List<Area> lista = new List<Area>();
            string consulta = @"
                SELECT
                    * 
                FROM
                    area
                
            ";

            try {
                using(var con = new SqlConnection(_conexionHolding)) {
                    await con.OpenAsync();
                    var query = new SqlCommand(consulta, con);
                    using(var dr = await query.ExecuteReaderAsync()) {
                        if(dr.HasRows) {
                            while(await dr.ReadAsync()) {
                                Area response = new Area {
                                    IdArea = ManejoNulos.ManageNullInteger(dr["IdArea"]),
                                    IdAreaBuk = ManejoNulos.ManageNullInteger(dr["IdAreaBuk"]),
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

        public async Task InsertarAreaHolding(Area area) {
            string consulta = @"
        INSERT INTO Area (IdAreaBuk, Area, Estado)
        VALUES (@IdAreaBuk, @Area, @Estado)
    ";

            using(var con = new SqlConnection(_conexionHolding)) {
                await con.OpenAsync();
                using(var cmd = new SqlCommand(consulta, con)) {
                    cmd.Parameters.AddWithValue("@IdAreaBuk", area.IdAreaBuk);
                    cmd.Parameters.AddWithValue("@Area", area.area);
                    cmd.Parameters.AddWithValue("@Estado", area.estado);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<bool> ActualizarAreaHolding(Area area) {
            string consulta = @"
        UPDATE Area
        SET 
            IdAreaBuk = @IdAreaBuk,
            Area = @Area,
            Estado = @Estado
        WHERE IdArea = @IdArea
    ";

            try {
                using(var con = new SqlConnection(_conexionHolding)) {
                    await con.OpenAsync();
                    using(var cmd = new SqlCommand(consulta, con)) {
                        cmd.Parameters.AddWithValue("@IdAreaBuk", area.IdAreaBuk);
                        cmd.Parameters.AddWithValue("@Area", area.area);
                        cmd.Parameters.AddWithValue("@Estado", area.estado);
                        cmd.Parameters.AddWithValue("@IdArea", area.IdArea);

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
