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
    public class CMP_JugadasClienteDAL
    {
        private readonly string _conexion;
        public CMP_JugadasClienteDAL()
        {
            _conexion = ConfigurationManager.ConnectionStrings["connectionBD"].ConnectionString;
        }
        public int ObtenerMaximoIdIas()
        {
            int result = 0;
            string consulta = @"SELECT max(SesionMigracionId) as maximo
                              FROM [dbo].[CMP_JugadasCliente] (nolock)";
            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);

                    using (var dr = query.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                result = ManejoNulos.ManageNullInteger(dr["maximo"]);
                            }
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                result = 0;
            }
            return result;
        }
        public int GuardarJugadaCliente(CMP_JugadasCliente item)
        {
            //bool respuesta = false;
            int IdInsertado = 0;
            string consulta = @"
INSERT INTO [dbo].[CMP_JugadasCliente]
           ([SesionId]
           ,[SorteoId]
           ,[JugadaId]
           ,[CodMaquina]
           ,[FechaInicio]
           ,[FechaTermino]
           ,[ClienteIdIas]
           ,[NombreCliente]
           ,[NroDocumento]
           ,[UsuarioIas]
           ,[Terminado]
           ,[MotivoTermino]
           ,[UsuarioTermino]
           ,[EstadoEnvio]
           ,[CantidadCupones]
           ,[SerieIni]
           ,[SerieFin]
           ,[Mail]
           ,[TipoDocumentoId]
           ,[NombreTipoDocumento]
           ,[TipoSesion]
           ,[NombreTipoSesion]
           ,[CodSala]
           ,[NombreSala]
           ,[FechaRegistro]
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
           ,[CoinInAnterior]
           ,[SesionMigracionId])
output inserted.id
     VALUES
           (@SesionId
           ,@SorteoId
           ,@JugadaId
           ,@CodMaquina
           ,@FechaInicio
           ,@FechaTermino
           ,@ClienteIdIas
           ,@NombreCliente
           ,@NroDocumento
           ,@UsuarioIas
           ,@Terminado
           ,@MotivoTermino
           ,@UsuarioTermino
           ,@EstadoEnvio
           ,@CantidadCupones
           ,@SerieIni
           ,@SerieFin
           ,@Mail
           ,@TipoDocumentoId
           ,@NombreTipoDocumento
           ,@TipoSesion
           ,@NombreTipoSesion
           ,@CodSala
           ,@NombreSala
           ,@FechaRegistro
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
           ,@CoinInAnterior
           ,@SesionMigracionId)
           
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
                    query.Parameters.AddWithValue("@CodMaquina", ManejoNulos.ManageNullStr(item.CodMaquina));
                    query.Parameters.AddWithValue("@FechaInicio", ManejoNulos.ManageNullDate(item.FechaInicio));
                    query.Parameters.AddWithValue("@FechaTermino", ManejoNulos.ManageNullDate(item.FechaTermino));
                    query.Parameters.AddWithValue("@ClienteIdIas", ManejoNulos.ManageNullInteger(item.ClienteIdIas));
                    query.Parameters.AddWithValue("@NombreCliente", ManejoNulos.ManageNullStr(item.NombreCliente));
                    query.Parameters.AddWithValue("@NroDocumento", ManejoNulos.ManageNullStr(item.NroDocumento));
                    query.Parameters.AddWithValue("@UsuarioIas", ManejoNulos.ManageNullInteger(item.UsuarioIas));
                    query.Parameters.AddWithValue("@Terminado", ManejoNulos.ManageNullInteger(item.Terminado));
                    query.Parameters.AddWithValue("@MotivoTermino", ManejoNulos.ManageNullStr(item.MotivoTermino));
                    query.Parameters.AddWithValue("@UsuarioTermino", ManejoNulos.ManageNullInteger(item.UsuarioTermino));
                    query.Parameters.AddWithValue("@EstadoEnvio", ManejoNulos.ManageNullInteger(item.EstadoEnvio));
                    query.Parameters.AddWithValue("@CantidadCupones", ManejoNulos.ManageNullInteger(item.CantidadCupones));
                    query.Parameters.AddWithValue("@SerieIni", ManejoNulos.ManageNullStr(item.SerieIni));
                    query.Parameters.AddWithValue("@SerieFin", ManejoNulos.ManageNullStr(item.SerieFin));
                    query.Parameters.AddWithValue("@Mail", ManejoNulos.ManageNullStr(item.Mail));
                    query.Parameters.AddWithValue("@TipoDocumentoId", ManejoNulos.ManageNullInteger(item.TipoDocumentoId));
                    query.Parameters.AddWithValue("@NombreTipoDocumento", ManejoNulos.ManageNullStr(item.NombreTipoDocumento));
                    query.Parameters.AddWithValue("@TipoSesion", ManejoNulos.ManageNullInteger(item.TipoSesion));
                    query.Parameters.AddWithValue("@NombreTipoSesion", ManejoNulos.ManageNullStr(item.NombreTipoSesion));
                    query.Parameters.AddWithValue("@CodSala", ManejoNulos.ManageNullInteger(item.CodSala));
                    query.Parameters.AddWithValue("@NombreSala", ManejoNulos.ManageNullStr(item.NombreSala));
                    query.Parameters.AddWithValue("@FechaRegistro", ManejoNulos.ManageNullDate(item.FechaRegistro));
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
