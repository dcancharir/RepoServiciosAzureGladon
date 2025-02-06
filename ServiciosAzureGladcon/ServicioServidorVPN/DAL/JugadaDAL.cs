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
    public class JugadaDAL
    {
        string _conexion = string.Empty;
        public JugadaDAL()
        {
            _conexion = ConfigurationManager.ConnectionStrings["conectionBd"].ConnectionString;//coneccion Online
        }
        public int GuardarJugada(Jugada contador)
        {
            //bool respuesta = false;
            int IdInsertado = 0;
            string consulta = @"INSERT INTO [dbo].[Jugada]
                                   ([JugadaId],[CodSala],[Cod_Cont_OL]
                                   ,[Fecha]
                                   ,[Hora]
                                   ,[CodMaq]
                                   ,[CoinOut]
                                   ,[CurrentCredits]
                                   ,[Monto]
                                   ,[Token]
                                   ,[CoinOutAnterior]
                                   ,[Estado_Oln]
                                   ,[FechaRegistro]
                                   ,[HandPay]
                                   ,[JackPot]
                                   ,[HandPayAnterior]
                                   ,[JackPotAnterior],[CoinIn],[CoinInAnterior],[Cod_Cont_OLAnterior],[GamesPlayed],[GamesPlayedAnterior]
                                  )
output inserted.JugadaId
                             VALUES
                                   (@JugadaId,@CodSala,@Cod_Cont_OL
                                   ,@Fecha
                                   ,@Hora
                                   ,@CodMaq
                                   ,@CoinOut
                                   ,@CurrentCredits
                                   ,@Monto
                                   ,@Token
                                   ,@CoinOutAnterior
                                   ,@Estado_Oln
                                   ,@FechaRegistro
                                   ,@HandPay
                                   ,@JackPot
                                   ,@HandPayAnterior
                                   ,@JackPotAnterior,@CoinIn,@CoinInAnterior,@Cod_Cont_OLAnterior,@GamesPlayed,@GamesPlayedAnterior
                                   )";

            try
            {
                using(var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@JugadaId", ManejoNulos.ManageNullInteger64(contador.JugadaId));
                    query.Parameters.AddWithValue("@CodSala", ManejoNulos.ManageNullInteger(contador.CodSala));
                    query.Parameters.AddWithValue("@Cod_Cont_OL", ManejoNulos.ManageNullInteger64(contador.Cod_Cont_OL));
                    query.Parameters.AddWithValue("@Fecha", ManejoNulos.ManageNullDate(contador.Fecha));
                    query.Parameters.AddWithValue("@Hora", ManejoNulos.ManageNullDate(contador.Hora));
                    query.Parameters.AddWithValue("@CodMaq", ManejoNulos.ManageNullStr(contador.CodMaq));
                    query.Parameters.AddWithValue("@CoinOut", ManejoNulos.ManageNullDouble(contador.CoinOut));
                    query.Parameters.AddWithValue("@CurrentCredits", ManejoNulos.ManageNullDouble(contador.CurrentCredits));
                    query.Parameters.AddWithValue("@Monto", ManejoNulos.ManageNullDouble(contador.Monto));
                    query.Parameters.AddWithValue("@Token", ManejoNulos.ManageNullDouble(contador.Token));
                    query.Parameters.AddWithValue("@CoinOutAnterior", ManejoNulos.ManageNullDouble(contador.CoinOutAnterior));
                    query.Parameters.AddWithValue("@Estado_Oln", ManejoNulos.ManageNullInteger(contador.Estado_Oln));
                    query.Parameters.AddWithValue("@FechaRegistro", ManejoNulos.ManageNullDate(contador.FechaRegistro));
                    query.Parameters.AddWithValue("@HandPay", ManejoNulos.ManageNullDouble(contador.HandPay));
                    query.Parameters.AddWithValue("@JackPot", ManejoNulos.ManageNullDouble(contador.JackPot));
                    query.Parameters.AddWithValue("@HandPayAnterior", ManejoNulos.ManageNullDouble(contador.HandPayAnterior));
                    query.Parameters.AddWithValue("@JackPotAnterior", ManejoNulos.ManageNullDouble(contador.JackPotAnterior));
                    query.Parameters.AddWithValue("@CoinIn", ManejoNulos.ManageNullDouble(contador.CoinIn));
                    query.Parameters.AddWithValue("@CoinInAnterior", ManejoNulos.ManageNullDouble(contador.CoinInAnterior));
                    query.Parameters.AddWithValue("@Cod_Cont_OLAnterior", ManejoNulos.ManageNullInteger64(contador.Cod_Cont_OLAnterior));
                    query.Parameters.AddWithValue("@GamesPlayed", ManejoNulos.ManageNullDouble(contador.GamesPlayed));
                    query.Parameters.AddWithValue("@GamesPlayedAnterior", ManejoNulos.ManageNullDouble(contador.GamesPlayedAnterior));
                    IdInsertado = Convert.ToInt32(query.ExecuteScalar());
                }
            } catch(Exception ex)
            {
                IdInsertado = 0;
            }
            return IdInsertado;
        }
        public bool GuardarJugadaBulk(List<Jugada> items) {
            bool respuesta = false;


            try {
                // Crear DataTable con la estructura exacta de la tabla
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("JugadaId", typeof(long));
                dataTable.Columns.Add("CodSala", typeof(int));
                dataTable.Columns.Add("Cod_Cont_OL", typeof(long));
                dataTable.Columns.Add("Fecha", typeof(DateTime));
                dataTable.Columns.Add("Hora", typeof(DateTime));
                dataTable.Columns.Add("CodMaq", typeof(string));
                dataTable.Columns.Add("CoinOut", typeof(decimal));
                dataTable.Columns.Add("CurrentCredits", typeof(decimal));
                dataTable.Columns.Add("Monto", typeof(decimal));
                dataTable.Columns.Add("Token", typeof(decimal));
                dataTable.Columns.Add("CoinOutAnterior", typeof(decimal));
                dataTable.Columns.Add("Estado_Oln", typeof(int));
                dataTable.Columns.Add("FechaRegistro", typeof(DateTime));
                dataTable.Columns.Add("HandPay", typeof(decimal));
                dataTable.Columns.Add("JackPot", typeof(decimal));
                dataTable.Columns.Add("HandPayAnterior", typeof(decimal));
                dataTable.Columns.Add("JackPotAnterior", typeof(decimal));
                dataTable.Columns.Add("CoinIn", typeof(decimal));
                dataTable.Columns.Add("CoinInAnterior", typeof(decimal));
                dataTable.Columns.Add("Cod_Cont_OLAnterior", typeof(long));
                dataTable.Columns.Add("GamesPlayed", typeof(decimal));
                dataTable.Columns.Add("GamesPlayedAnterior", typeof(decimal));

                // Llenar el DataTable con los datos
                foreach (var item in items) {
                    dataTable.Rows.Add(
                        item.JugadaId,
                        item.CodSala,
                        item.Cod_Cont_OL,
                        item.Fecha ?? (object)DBNull.Value,
                        item.Hora ?? (object)DBNull.Value,
                        string.IsNullOrEmpty(item.CodMaq) ? (object)DBNull.Value : item.CodMaq,
                        item.CoinOut,
                        item.CurrentCredits,
                        item.Monto,
                        item.Token,
                        item.CoinOutAnterior,
                        item.Estado_Oln,
                        item.FechaRegistro ?? (object)DBNull.Value,
                        item.HandPay,
                        item.JackPot,
                        item.HandPayAnterior,
                        item.JackPotAnterior,
                        item.CoinIn,
                        item.CoinInAnterior,
                        item.Cod_Cont_OLAnterior,
                        item.GamesPlayed,
                        item.GamesPlayedAnterior
                    );
                }

                using (SqlConnection connection = new SqlConnection(_conexion)) {
                    connection.Open();

                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection)) {
                        // Configurar el destino y mapeos
                        bulkCopy.DestinationTableName = "Jugada";

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
                            throw new Exception($"Error en el bulk insert de Jugada: {ex.Message}", ex);
                        }
                    }
                }
                respuesta = true;
            }
            catch (Exception ex) {
                funciones.logueo($"Error metodo GuardarJugadaBulk() {ex.Message}");
                respuesta = false;
            }
            return respuesta;
        }
    }
}
