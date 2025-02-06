using ServicioServidorVPN.clases;
using ServicioServidorVPN.utilitarios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioServidorVPN.DAL
{
    public class SesionSorteoSalaDAL
    {
        string _conexion = string.Empty;
        public SesionSorteoSalaDAL()
        {
            _conexion = ConfigurationManager.ConnectionStrings["conectionBd"].ConnectionString;//coneccion Online
        }
        public int GuardarSesionSorteSala(SesionSorteoSala item)
        {
            bool respuesta = false;
            int idInsertado = 0;
            string consulta = @"
            INSERT INTO [dbo].[SesionSorteoSala]
               ([CodSala],[SesionId],[SorteoId],[JugadaId],[CantidadCupones],[FechaRegistro],[NombreSorteo],[CondicionWin],[CondicionBet],[TopeCuponesxJugada],[WinCalculado],[BetCalculado],[ParametrosImpresion],[Factor],[DescartePorFactor],[SerieIni],[SerieFin])
output inserted.SesionId             
VALUES
                (@CodSala,@SesionId,@SorteoId,@JugadaId,@CantidadCupones,@FechaRegistro,@NombreSorteo,@CondicionWin,@CondicionBet,@TopeCuponesxJugada,@WinCalculado,@BetCalculado,@ParametrosImpresion,@Factor,@DescartePorFactor,@SerieIni,@SerieFin);";

            try
            {
                using(var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@CodSala", ManejoNulos.ManageNullInteger(item.CodSala));
                    query.Parameters.AddWithValue("@SesionId", ManejoNulos.ManageNullInteger64(item.SesionId));
                    query.Parameters.AddWithValue("@SorteoId", ManejoNulos.ManageNullInteger64(item.SorteoId));
                    query.Parameters.AddWithValue("@JugadaId", ManejoNulos.ManageNullInteger64(item.JugadaId));
                    query.Parameters.AddWithValue("@CantidadCupones", ManejoNulos.ManageNullInteger(item.CantidadCupones));
                    query.Parameters.AddWithValue("@NombreSorteo", ManejoNulos.ManageNullStr(item.NombreSorteo));
                    query.Parameters.AddWithValue("@CondicionWin", ManejoNulos.ManageNullDouble(item.CondicionWin));
                    query.Parameters.AddWithValue("@CondicionBet", ManejoNulos.ManageNullDouble(item.CondicionBet));
                    query.Parameters.AddWithValue("@TopeCuponesxJugada", ManejoNulos.ManageNullInteger(item.TopeCuponesxJugada));
                    query.Parameters.AddWithValue("@WinCalculado", ManejoNulos.ManageNullDouble(item.WinCalculado));
                    query.Parameters.AddWithValue("@BetCalculado", ManejoNulos.ManageNullDouble(item.BetCalculado));
                    query.Parameters.AddWithValue("@ParametrosImpresion", ManejoNulos.ManageNullStr(item.ParametrosImpresion));
                    query.Parameters.AddWithValue("@Factor", ManejoNulos.ManageNullDecimal(item.Factor));
                    query.Parameters.AddWithValue("@DescartePorFactor", ManejoNulos.ManageNullDecimal(item.DescartePorFactor));
                    query.Parameters.AddWithValue("@SerieIni", ManejoNulos.ManageNullStr(item.SerieIni));
                    query.Parameters.AddWithValue("@SerieFin", ManejoNulos.ManageNullStr(item.SerieFin));
                    query.Parameters.AddWithValue("@FechaRegistro", ManejoNulos.ManageNullDate(item.FechaRegistro));
                    idInsertado = Convert.ToInt32(query.ExecuteScalar());
                }
            } catch(Exception ex)
            {
                idInsertado = 0;
            }
            return idInsertado;
        }
        public bool GuardarSesionSorteSalaBulk(List<SesionSorteoSala> items) {
            bool respuesta = false;


            try {
                // Crear DataTable con la estructura exacta de la tabla
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("SesionId", typeof(long));
                dataTable.Columns.Add("SorteoId", typeof(long));
                dataTable.Columns.Add("JugadaId", typeof(long));
                dataTable.Columns.Add("CodSala", typeof(int));
                dataTable.Columns.Add("CantidadCupones", typeof(int));
                dataTable.Columns.Add("FechaRegistro", typeof(DateTime));
                dataTable.Columns.Add("SerieIni", typeof(string));
                dataTable.Columns.Add("SerieFin", typeof(string));
                dataTable.Columns.Add("NombreSorteo", typeof(string));
                dataTable.Columns.Add("CondicionWin", typeof(decimal));
                dataTable.Columns.Add("WinCalculado", typeof(decimal));
                dataTable.Columns.Add("CondicionBet", typeof(decimal));
                dataTable.Columns.Add("BetCalculado", typeof(decimal));
                dataTable.Columns.Add("TopeCuponesxJugada", typeof(int));
                dataTable.Columns.Add("ParametrosImpresion", typeof(string));
                dataTable.Columns.Add("Factor", typeof(decimal));
                dataTable.Columns.Add("DescartePorFactor", typeof(decimal));

                // Llenar el DataTable con los datos
                foreach (var item in items) {
                    dataTable.Rows.Add(
                        item.SesionId,
                        item.SorteoId,
                        item.JugadaId,
                        item.CodSala,
                        item.CantidadCupones,
                        item.FechaRegistro ?? (object)DBNull.Value,
                        string.IsNullOrEmpty(item.SerieIni) ? (object)DBNull.Value : item.SerieIni,
                        string.IsNullOrEmpty(item.SerieFin) ? (object)DBNull.Value : item.SerieFin,
                        string.IsNullOrEmpty(item.NombreSorteo) ? (object)DBNull.Value : item.NombreSorteo,
                        Convert.ToDecimal(item.CondicionWin),
                        Convert.ToDecimal(item.WinCalculado),
                        Convert.ToDecimal(item.CondicionBet),
                        Convert.ToDecimal(item.BetCalculado),
                        item.TopeCuponesxJugada ?? (object)DBNull.Value,
                        string.IsNullOrEmpty(item.ParametrosImpresion) ? (object)DBNull.Value : item.ParametrosImpresion,
                        Convert.ToDecimal(item.Factor),
                        Convert.ToDecimal(item.DescartePorFactor)
                    );
                }

                using (SqlConnection connection = new SqlConnection(_conexion)) {
                    connection.Open();

                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection)) {
                        // Configurar el destino y mapeos
                        bulkCopy.DestinationTableName = "SesionSorteoSala";

                        // Mapear todas las columnas
                        foreach (DataColumn column in dataTable.Columns) {
                            bulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName);
                        }

                        //// Configurar opciones de rendimiento
                        bulkCopy.BatchSize = 5000;
                        bulkCopy.BulkCopyTimeout = 120; // 2 minutos

                        try {
                            bulkCopy.WriteToServer(dataTable);
                        }
                        catch (Exception ex) {
                            throw new Exception($"Error en el bulk insert de SesionSorteoSala: {ex.Message}", ex);
                        }
                    }
                }
                respuesta = true;
            }
            catch (Exception ex) {
                funciones.logueo($"Error metodo GuardarSesionSorteSalaBulk() {ex.Message}");
                respuesta = false;
            }
            return respuesta;
        }


    }
}
