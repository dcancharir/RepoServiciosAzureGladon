using ServicioMigracionClientes.Clases;
using ServicioMigracionClientes.utilitarios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioMigracionClientes.DAL
{
    public class CMP_SesionSorteoSalaMigracionDAL
    {
        private readonly string _conexion;
        public CMP_SesionSorteoSalaMigracionDAL()
        {
            _conexion = ConfigurationManager.ConnectionStrings["connectionBD"].ConnectionString;
        }
        public int GuardarSesionSorteoSalaMigracion(CMP_SesionSorteoSalaMigracion item)
        {
            //bool respuesta = false;
            int IdInsertado = 0;
            string consulta = @"
               INSERT INTO [dbo].[CMP_SesionSorteoSalaMigracion]
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
           ,[TopeCuponesxJugada]
           ,[ParametrosImpresion]
           ,[Cod_Cont_OL]
           ,[Fecha]
           ,[Hora]
           ,[CodMaq]
           ,[CoinOut]
           ,[CurrentCredits]
           ,[Monto]
           ,[Token]
           ,[CoinOutAnterior]
           ,[Estado_Oln]
           ,[HandPay]
           ,[JackPot]
           ,[HandPayAnterior]
           ,[JackPotAnterior]
           ,[CoinIn]
           ,[CoinInAnterior],[SesionMigracionId])
output inserted.id
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
           ,@TopeCuponesxJugada
           ,@ParametrosImpresion
           ,@Cod_Cont_OL
           ,@Fecha
           ,@Hora
           ,@CodMaq
           ,@CoinOut
           ,@CurrentCredits
           ,@Monto
           ,@Token
           ,@CoinOutAnterior
           ,@Estado_Oln
           ,@HandPay
           ,@JackPot
           ,@HandPayAnterior
           ,@JackPotAnterior
           ,@CoinIn
           ,@CoinInAnterior,@SesionMigracionId)
                      ";
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
                    query.Parameters.AddWithValue("@CondicionWin", ManejoNulos.ManageNullDecimal(item.CondicionWin));
                    query.Parameters.AddWithValue("@WinCalculado", ManejoNulos.ManageNullDecimal(item.WinCalculado));
                    query.Parameters.AddWithValue("@CondicionBet", ManejoNulos.ManageNullDecimal(item.CondicionBet));
                    query.Parameters.AddWithValue("@BetCalculado", ManejoNulos.ManageNullDecimal(item.BetCalculado));
                    query.Parameters.AddWithValue("@TopeCuponesxJugada", ManejoNulos.ManageNullInteger(item.TopeCuponesxJugada));
                    query.Parameters.AddWithValue("@ParametrosImpresion", ManejoNulos.ManageNullStr(item.ParametrosImpresion));
                    query.Parameters.AddWithValue("@Cod_Cont_OL", ManejoNulos.ManageNullInteger64(item.Cod_Cont_OL));
                    query.Parameters.AddWithValue("@Fecha", ManejoNulos.ManageNullDate(item.Fecha));
                    query.Parameters.AddWithValue("@Hora", ManejoNulos.ManageNullDate(item.Hora));
                    query.Parameters.AddWithValue("@CodMaq", ManejoNulos.ManageNullStr(item.CodMaq));
                    query.Parameters.AddWithValue("@CoinOut", ManejoNulos.ManageNullDouble(item.CoinOut));
                    query.Parameters.AddWithValue("@CurrentCredits", ManejoNulos.ManageNullDouble(item.CurrentCredits));
                    query.Parameters.AddWithValue("@Monto", ManejoNulos.ManageNullDecimal(item.Monto));
                    query.Parameters.AddWithValue("@Token", ManejoNulos.ManageNullDecimal(item.Token));
                    query.Parameters.AddWithValue("@CoinOutAnterior", ManejoNulos.ManageNullDouble(item.CoinOutAnterior));
                    query.Parameters.AddWithValue("@Estado_Oln", ManejoNulos.ManageNullInteger(item.Estado_Oln));
                    query.Parameters.AddWithValue("@HandPay", ManejoNulos.ManageNullDouble(item.HandPay));
                    query.Parameters.AddWithValue("@JackPot", ManejoNulos.ManageNullDouble(item.JackPot));
                    query.Parameters.AddWithValue("@HandPayAnterior", ManejoNulos.ManageNullDouble(item.HandPayAnterior));
                    query.Parameters.AddWithValue("@JackPotAnterior", ManejoNulos.ManageNullDouble(item.JackPotAnterior));
                    query.Parameters.AddWithValue("@CoinIn", ManejoNulos.ManageNullDecimal(item.CoinIn));
                    query.Parameters.AddWithValue("@CoinInAnterior", ManejoNulos.ManageNullDecimal(item.CoinInAnterior));
                    query.Parameters.AddWithValue("@SesionMigracionId", ManejoNulos.ManageNullInteger64(item.SesionMigracionId));
                    IdInsertado = Convert.ToInt32(query.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                IdInsertado = 0;
            }
            return IdInsertado;
        }
    }
}
