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
    public class CMP_JugadaExcaDAL
    {
        string _conexion = string.Empty;
        public CMP_JugadaExcaDAL()
        {
            _conexion = ConfigurationManager.ConnectionStrings["conectionBdExca"].ConnectionString;//coneccion Online
        }
        public int GuardarJugada(CMP_JugadaExca contador)
        {
            //bool respuesta = false;
            int IdInsertado = 0;
            string consulta = @"INSERT INTO [dbo].[CMP_Jugada]
           ([JugadaId]
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
           ,[FechaRegistro]
           ,[HandPay]
           ,[JackPot]
           ,[HandPayAnterior]
           ,[JackPotAnterior]
           ,[CoinIn]
           ,[CoinInAnterior])
output inserted.jugadaid
     VALUES
           (@JugadaId
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
           ,@FechaRegistro
           ,@HandPay
           ,@JackPot
           ,@HandPayAnterior
           ,@JackPotAnterior
           ,@CoinIn
           ,@CoinInAnterior)";

            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@JugadaId", ManejoNulos.ManageNullInteger64(contador.JugadaId));
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
                    //string quer = query.ToString();
                    IdInsertado = Convert.ToInt32(query.ExecuteScalar());
                    //query.ExecuteNonQuery();
                    //respuesta = true;
                }
            }
            catch (Exception ex)
            {
                //respuesta = false;
                IdInsertado = 0;
            }
            return IdInsertado;
        }
    }
}
