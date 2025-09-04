using ServicioServidorVPN.clases.Clientes.ControlAcceso;
using ServicioServidorVPN.utilitarios;
using ServicioServidorVPN.utilitarios.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ServicioServidorVPN.DAL.ClientesControlAcceso {
    public class ClientesControlAccesoDAL {
        private readonly string _conexion = string.Empty;

        public ClientesControlAccesoDAL() {
            _conexion = ConfigurationManager.ConnectionStrings["conectionBd"].ConnectionString;
        }

        public async Task<bool> InsertarClientesControlAccesiMasivamente(List<ClienteControlAccesoEntidad> clientes) {
            bool success = false;

            using(SqlConnection connection = new SqlConnection(_conexion)) {
                await connection.OpenAsync();
                using(SqlTransaction transaction = connection.BeginTransaction())
                using(SqlBulkCopy bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction)) {
                    try {
                        bulkCopy.DestinationTableName = "ClienteControlAcceso";

                        // Configurar el mapeo de las columnas entre la lista y la tabla de destino
                        bulkCopy.ColumnMappings.Add("IdCliente", "IdCliente");
                        bulkCopy.ColumnMappings.Add("CodSala", "CodSala");
                        bulkCopy.ColumnMappings.Add("NombreSala", "NombreSala");
                        bulkCopy.ColumnMappings.Add("IdTipoDocumento", "IdTipoDocumento");
                        bulkCopy.ColumnMappings.Add("TipoDocumento", "TipoDocumento");
                        bulkCopy.ColumnMappings.Add("NumeroDocumento", "NumeroDocumento");
                        bulkCopy.ColumnMappings.Add("NombreCliente", "NombreCliente");
                        bulkCopy.ColumnMappings.Add("TipoRegistro", "TipoRegistro");
                        bulkCopy.ColumnMappings.Add("FechaRegistro", "FechaRegistro");
                        bulkCopy.ColumnMappings.Add("FechaNacimiento", "FechaNacimiento");
                        bulkCopy.ColumnMappings.Add("FechaMigracion", "FechaMigracion");

                        await Task.Run(() => bulkCopy.WriteToServer(clientes.ToDataTable()));

                        transaction.Commit();
                        success = true;
                    } catch(Exception ex) {
                        transaction.Rollback();
                        success = false;
                        funciones.logueo($"Error en BulkInsert InsertarClientesControlAccesiMasivamente. {ex.Message}", "Error");
                    }
                }
                return success;
            }
        }

        public async Task<List<ClienteControlAccesoEntidad>> ObtenerClientesControlAcceso(List<ClienteControlAccesoIds> ids) {
            List<ClienteControlAccesoEntidad> clientes = new List<ClienteControlAccesoEntidad>();

            try {
                IEnumerable<List<ClienteControlAccesoIds>> batches = ids.Chunk(300);
                using(SqlConnection con = new SqlConnection(_conexion)) {
                    con.Open();
                    foreach(var batch in batches) {
                        List<string> conditions = batch
                           .Select((item, index) => $"(IdCliente = @idCliente{index} AND CodSala = @codSala{index})")
                           .ToList();

                        string query = $@"
                            SELECT *
                            FROM ClienteControlAcceso 
                            WHERE {string.Join(" OR ", conditions)}
                        ";

                        using(SqlCommand cmd = new SqlCommand(query, con)) {
                            for(int i = 0; i < batch.Count; i++) {
                                cmd.Parameters.AddWithValue($"@idCliente{i}", batch[i].IdCliente);
                                cmd.Parameters.AddWithValue($"@codSala{i}", batch[i].CodSala);
                            }

                            using(SqlDataReader dr = await cmd.ExecuteReaderAsync()) {
                                while(await dr.ReadAsync()) {
                                    clientes.Add(ConstruirObjeto(dr));
                                }
                            }
                        }
                    }
                }
            } catch(Exception ex) {
                funciones.logueo($"Error al obtener los registros de ingresos de clientes por lista de id. {ex.Message}", "Error");
            }
            return clientes;
        }

        private ClienteControlAccesoEntidad ConstruirObjeto(SqlDataReader dr) {
            return new ClienteControlAccesoEntidad {
                IdCliente = ManejoNulos.ManageNullInteger(dr["IdCliente"]),
                CodSala = ManejoNulos.ManageNullInteger(dr["IdCliente"]),
                NombreSala = ManejoNulos.ManageNullStr(dr["NombreSala"]),
                IdTipoDocumento = ManejoNulos.ManageNullInteger(dr["IdTipoDocumento"]),
                TipoDocumento = ManejoNulos.ManageNullStr(dr["TipoDocumento"]),
                NumeroDocumento = ManejoNulos.ManageNullStr(dr["NumeroDocumento"]),
                NombreCliente = ManejoNulos.ManageNullStr(dr["NombreCliente"]),
                FechaRegistro = ManejoNulos.ManageNullDate(dr["FechaRegistro"]),
                TipoRegistro = ManejoNulos.ManageNullStr(dr["TipoRegistro"]),
                FechaNacimiento = dr["FechaNacimiento"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["FechaNacimiento"]),
                FechaMigracion = ManejoNulos.ManageNullDate(dr["FechaMigracion"]),
            };
        }
    }
}
