using ServicioServidorVPN.clases;
using ServicioServidorVPN.utilitarios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioServidorVPN.DAL
{
    public class CMP_SesionSorteoSalaExcaDAL
    {
        string _conexion = string.Empty;
        public CMP_SesionSorteoSalaExcaDAL()
        {
            _conexion = ConfigurationManager.ConnectionStrings["conectionBdExca"].ConnectionString;//coneccion Online
        }
        public int GuardarSesionSorteSala(CMP_SesionSorteoSalaExca item)
        {
            bool respuesta = false;
            int idInsertado = 0;
            string consulta = @"
            INSERT INTO [dbo].[CMP_SesionSorteoSala]
           ([SesionId]
           ,[SorteoId]
           ,[JugadaId]
           ,[CantidadCupones]
           ,[FechaRegistro]
           ,[SerieIni]
           ,[SerieFin]
           ,[NombreSorteo]
           ,[CondicionWin]
           ,[WinCalculado]
           ,[CondicionBet]
           ,[BetCalculado]
           ,[TopeCuponesxJugada])
output inserted.SesionId
     VALUES
           (@SesionId
           ,@SorteoId
           ,@JugadaId
           ,@CantidadCupones
           ,@FechaRegistro
           ,@SerieIni
           ,@SerieFin
           ,@NombreSorteo
           ,@CondicionWin
           ,@WinCalculado
           ,@CondicionBet
           ,@BetCalculado
           ,@TopeCuponesxJugada);";

            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@SesionId", ManejoNulos.ManageNullInteger64(item.SesionId));
                    query.Parameters.AddWithValue("@SorteoId", ManejoNulos.ManageNullInteger64(item.SorteoId));
                    query.Parameters.AddWithValue("@JugadaId", ManejoNulos.ManageNullInteger64(item.JugadaId));
                    query.Parameters.AddWithValue("@CantidadCupones", ManejoNulos.ManageNullInteger(item.CantidadCupones));
                    query.Parameters.AddWithValue("@FechaRegistro", ManejoNulos.ManageNullDate(item.FechaRegistro));
                    query.Parameters.AddWithValue("@SerieIni", ManejoNulos.ManageNullStr(item.SerieIni));
                    query.Parameters.AddWithValue("@SerieFin", ManejoNulos.ManageNullStr(item.SerieFin));
                    query.Parameters.AddWithValue("@NombreSorteo", ManejoNulos.ManageNullStr(item.NombreSorteo));
                    query.Parameters.AddWithValue("@CondicionWin", ManejoNulos.ManageNullDouble(item.CondicionWin));
                    query.Parameters.AddWithValue("@WinCalculado", ManejoNulos.ManageNullDouble(item.WinCalculado));
                    query.Parameters.AddWithValue("@CondicionBet", ManejoNulos.ManageNullDouble(item.CondicionBet));
                    query.Parameters.AddWithValue("@BetCalculado", ManejoNulos.ManageNullDouble(item.BetCalculado));
                    query.Parameters.AddWithValue("@TopeCuponesxJugada", ManejoNulos.ManageNullInteger(item.TopeCuponesxJugada));
                    //query.ExecuteNonQuery();
                    idInsertado = Convert.ToInt32(query.ExecuteScalar());
                    //respuesta = true;
                }
            }
            catch (Exception ex)
            {
                idInsertado = 0;
                //respuesta = false;
            }
            return idInsertado;
        }
    }
}
