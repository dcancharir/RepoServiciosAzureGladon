using FastMember;
using ServicioServidorVPN.clases;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioServidorVPN.DAL
{
    public class ContadoresOnlineDataWhereHouseDAL
    {
        string _conexion = string.Empty;
        public ContadoresOnlineDataWhereHouseDAL()
        {
            _conexion = ConfigurationManager.ConnectionStrings["conectionBd"].ConnectionString;
        }
        public DateTime ObtenerFechaDeUltimoContadorPorCodSala(int codSala)
        {
            DateTime fecha = DateTime.Now.AddMinutes(-1);
            string consulta = @"
                DECLARE @fechaUltima DATETIME
                SELECT TOP 1 
	                @fechaUltima = Fecha
                FROM 
	                ContadoresOnline (nolock)
                WHERE
	                Sala = @codSala
                ORDER BY
	                Fecha DESC

                SELECT ISNULL(@fechaUltima, CONVERT(DATE,CONVERT(CHAR(10), GETDATE()-2,120))) fecha
            ";
            try
            {
                using(var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@codSala", codSala);
                    fecha = utilitarios.ManejoNulos.ManageNullDate(query.ExecuteScalar());
                }
            } catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return fecha;
        }


        public bool GuarGuardarContadoresOnline(List<ContadoresOnline> contadoresOnline)
        {
            bool success = true;
            try
            {
                using(SqlConnection connection = new SqlConnection(_conexion))
                {
                    connection.Open();

                    using(SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                    {
                        using(var reader = ObjectReader.Create(contadoresOnline))
                        {
                            // Asignar el nombre de la tabla de destino
                            bulkCopy.DestinationTableName = "ContadoresOnline";

                            // Configurar el mapeo de las columnas entre la lista y la tabla de destino
                            bulkCopy.ColumnMappings.Add("Fecha", "Fecha");
                            bulkCopy.ColumnMappings.Add("Hora", "Hora");
                            bulkCopy.ColumnMappings.Add("CodMaq", "CodMaq");
                            bulkCopy.ColumnMappings.Add("CoinIn", "CoinIn");
                            bulkCopy.ColumnMappings.Add("CoinOut", "CoinOut");
                            bulkCopy.ColumnMappings.Add("HandPay", "HandPay");
                            bulkCopy.ColumnMappings.Add("CurrentCredits", "CurrentCredits");
                            bulkCopy.ColumnMappings.Add("CancelCredits", "CancelCredits");
                            bulkCopy.ColumnMappings.Add("Jackpot", "Jackpot");
                            bulkCopy.ColumnMappings.Add("GamesPlayed", "GamesPlayed");
                            bulkCopy.ColumnMappings.Add("TotalDrop", "TotalDrop");
                            bulkCopy.ColumnMappings.Add("Token", "Token");
                            bulkCopy.ColumnMappings.Add("Empresa", "Empresa");
                            bulkCopy.ColumnMappings.Add("Sala", "Sala");

                            // Realizar la inserción masiva
                            bulkCopy.WriteToServer(reader);
                        }
                    }
                }
            } catch(Exception)
            {
                success = false;
            }

            return success;
        }

    }
}
