using ServicioAzureIAS.Clases.Ludopata;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace ServicioAzureIAS.DAL.ControlAcceso {
    public class HistorialLudopataDAL {
        private readonly string _conexion = string.Empty;

        public HistorialLudopataDAL() {
            _conexion = ConfigurationManager.ConnectionStrings["conectionBdSeguridad"].ConnectionString;
        }

        public int InsertarHistorialLudopata(HistorialLudopata historialLudopata) {
            int idInsertado = 0;
            string consulta = @"
                INSERT INTO CAL_HistorialLudopata(IdLudopata, TipoMovimiento, TipoRegistro, IdUsuario)
                OUTPUT INSERTED.Id
                VALUES(@IdLudopata, @TipoMovimiento, @TipoRegistro, @IdUsuario)
            ";
            try {
                using(SqlConnection con = new SqlConnection(_conexion)) {
                    con.Open();
                    SqlCommand query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@IdLudopata", historialLudopata.IdLudopata);
                    query.Parameters.AddWithValue("@TipoMovimiento", historialLudopata.TipoMovimiento);
                    query.Parameters.AddWithValue("@TipoRegistro", historialLudopata.TipoRegistro);
                    query.Parameters.AddWithValue("@IdUsuario", historialLudopata.IdUsuario == 0 ? SqlInt32.Null : historialLudopata.IdUsuario);
                    idInsertado = Convert.ToInt32(query.ExecuteScalar());
                }
            } catch(Exception ex) {
                Console.WriteLine(ex.Message);
            }
            return idInsertado;
        }
    }
}
