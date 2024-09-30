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
    }
}
