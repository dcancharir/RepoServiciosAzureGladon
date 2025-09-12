using ServicioServidorVPN.clases.Clientes;
using ServicioServidorVPN.utilitarios;
using ServicioServidorVPN.utilitarios.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ServicioServidorVPN.DAL.Clientes {
    public class IngresoClienteSalaDAL {
        private readonly string _conexion = string.Empty;

        public IngresoClienteSalaDAL() {
            _conexion = ConfigurationManager.ConnectionStrings["conectionBd"].ConnectionString;
        }

        public async Task<bool> InsertarIngresosClientesMasivamente(List<CAL_IngresoClienteSala> ingresosCliente) {
            bool success = false;

            using(SqlConnection connection = new SqlConnection(_conexion)) {
                await connection.OpenAsync();
                using(SqlTransaction transaction = connection.BeginTransaction())
                using(SqlBulkCopy bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction)) {
                    try {
                        bulkCopy.DestinationTableName = "CAL_IngresoClienteSala";

                        // Configurar el mapeo de las columnas entre la lista y la tabla de destino
                        bulkCopy.ColumnMappings.Add("Id", "Id");
                        bulkCopy.ColumnMappings.Add("NombreSala", "NombreSala");
                        bulkCopy.ColumnMappings.Add("NumeroDocumento", "NumeroDocumento");
                        bulkCopy.ColumnMappings.Add("NombreCliente", "NombreCliente");
                        bulkCopy.ColumnMappings.Add("Tipo", "Tipo");
                        bulkCopy.ColumnMappings.Add("Codigo", "Codigo");
                        bulkCopy.ColumnMappings.Add("FechaRegistro", "FechaRegistro");
                        bulkCopy.ColumnMappings.Add("FechaMigracion", "FechaMigracion");

                        await Task.Run(() => bulkCopy.WriteToServer(ingresosCliente.ToDataTable()));

                        transaction.Commit();
                        success = true;
                    } catch(Exception ex) {
                        transaction.Rollback();
                        success = false;
                        funciones.logueo($"Error en BulkInsert InsertarIngresosClientesMasivamente. {ex.Message}", "Error");
                    }
                }
                return success;
            }
        }

        public async Task<List<CAL_IngresoClienteSala>> ObtenerIngresosClientesPorIds(List<int> ids) {
            List<CAL_IngresoClienteSala> ingresos = new List<CAL_IngresoClienteSala>();

            try {
                IEnumerable<List<int>> batches = ids.Chunk(500);
                using(SqlConnection con = new SqlConnection(_conexion)) {
                    con.Open();
                    foreach(var batch in batches) {
                        List<string> parameterNames = batch.Select((id, index) => $"@id{index}").ToList();
                        string query = $@"
                            SELECT *
                            FROM CAL_IngresoClienteSala 
                            WHERE Id IN ({string.Join(",", parameterNames)})
                        ";
                        SqlCommand cmd = new SqlCommand(query, con);
                        for(int i = 0; i < batch.Count; i++) {
                            cmd.Parameters.AddWithValue($"@id{i}", batch[i]);
                        }
                        using(SqlDataReader dr = await cmd.ExecuteReaderAsync()) {
                            while(dr.Read()) {
                                ingresos.Add(ConstruirObjeto(dr));
                            }
                        }
                    }
                }
            } catch(Exception ex) {
                funciones.logueo($"Error al obtener los registros de ingresos de clientes por lista de id. {ex.Message}", "Error");
            }
            return ingresos;
        }

        private CAL_IngresoClienteSala ConstruirObjeto(SqlDataReader dr) {
            return new CAL_IngresoClienteSala {
                Id = ManejoNulos.ManageNullInteger(dr["Id"]),
                NombreSala = ManejoNulos.ManageNullStr(dr["NombreSala"]),
                NombreCliente = ManejoNulos.ManageNullStr(dr["NombreCliente"]),
                NumeroDocumento = ManejoNulos.ManageNullStr(dr["NumeroDocumento"]),
                Tipo = ManejoNulos.ManageNullStr(dr["Tipo"]),
                Codigo = ManejoNulos.ManageNullStr(dr["Codigo"]),
                FechaRegistro = ManejoNulos.ManageNullDate(dr["FechaRegistro"]),
                FechaMigracion = ManejoNulos.ManageNullDate(dr["FechaMigracion"]),
            };
        }
    }
}
