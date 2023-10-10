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
    public class Asignacion_M_TDAL
    {
        private readonly string _conexion = string.Empty;
        public Asignacion_M_TDAL()
        {
            _conexion = ConfigurationManager.ConnectionStrings["conectionBd"].ConnectionString;//coneccion Online
        }
        public int GuardarAsignacionMT(Asignacion_M_T item)
        {
            //bool respuesta = false;
            int IdInsertado = 0;
            string consulta = @"
IF not EXISTS (SELECT * FROM [dbo].[Asignacion_M_T] (nolock) WHERE COD_SALA=@COD_SALA and trim(CodMaq)=trim(@CodMaq))
begin
INSERT INTO [dbo].[Asignacion_M_T]
           ([COD_EMPRESA]
           ,[COD_SALA]
           ,[nro]
           ,[CodTarjeta]
           ,[CodTarjeta_Seg]
           ,[CodMaq]
           ,[CodMaqMin]
           ,[Modelo]
           ,[COD_TIPOMAQUINA]
           ,[Posicion]
           ,[Tpro]
           ,[Estado]
           ,[CodFicha]
           ,[CodMarca]
           ,[CodModelo]
           ,[Hopper]
           ,[CredOtor]
           ,[Token]
           ,[NroContradores]
           ,[Precio_Credito]
           ,[NUM_SERIE]
           ,[idTipoMoneda]
           ,[MODALIDAD]
           ,[MAQ_X]
           ,[MAQ_Y]
           ,[MAQ_ASIGLAYOUT]
           ,[MAQ_POSILAYOUT]
           ,[CIn]
           ,[COut]
           ,[TipoTranSac]
           ,[cod_caja]
           ,[TopeCreditos]
           ,[S_ONLINE]
           ,[FORMULA_MAQ]
           ,[MAQ_DEVPORCENTAJE]
           ,[FormulaFinal]
           ,[Dbase]
           ,[EnviaDbase]
           ,[cod_servidor]
           ,[Cod_Socio]
           ,[Status_Online]
           ,[COD_MODELO]
           ,[Sistema]
           ,[PosicionBilletero]
           ,[con_sorteo]
           ,[Block]
           ,[codigo_extra]
           ,[PromoBonus]
           ,[PromoTicket]
           ,[estadoT]
           ,[retiroTemporal])
output inserted.COD_SALA
     VALUES
           (@COD_EMPRESA
           ,@COD_SALA
           ,@nro
           ,@CodTarjeta
           ,@CodTarjeta_Seg
           ,@CodMaq
           ,@CodMaqMin
           ,@Modelo
           ,@COD_TIPOMAQUINA
           ,@Posicion
           ,@Tpro
           ,@Estado
           ,@CodFicha
           ,@CodMarca
           ,@CodModelo
           ,@Hopper
           ,@CredOtor
           ,@Token
           ,@NroContradores
           ,@Precio_Credito
           ,@NUM_SERIE
           ,@idTipoMoneda
           ,@MODALIDAD
           ,@MAQ_X
           ,@MAQ_Y
           ,@MAQ_ASIGLAYOUT
           ,@MAQ_POSILAYOUT
           ,@CIn
           ,@COut
           ,@TipoTranSac
           ,@cod_caja
           ,@TopeCreditos
           ,@S_ONLINE
           ,@FORMULA_MAQ
           ,@MAQ_DEVPORCENTAJE
           ,@FormulaFinal
           ,@Dbase
           ,@EnviaDbase
           ,@cod_servidor
           ,@Cod_Socio
           ,@Status_Online
           ,@COD_MODELO
           ,@Sistema
           ,@PosicionBilletero
           ,@con_sorteo
           ,@Block
           ,@codigo_extra
           ,@PromoBonus
           ,@PromoTicket
           ,@estadoT
           ,@retiroTemporal)
end
else

begin
    select 0
end

                      ";
            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@COD_EMPRESA", ManejoNulos.ManageNullStr(item.COD_EMPRESA));
                    query.Parameters.AddWithValue("@COD_SALA", ManejoNulos.ManageNullStr(item.COD_SALA));
                    query.Parameters.AddWithValue("@nro", ManejoNulos.ManageNullInteger(item.nro));
                    query.Parameters.AddWithValue("@CodTarjeta", ManejoNulos.ManageNullStr(item.CodTarjeta));
                    query.Parameters.AddWithValue("@CodTarjeta_Seg", ManejoNulos.ManageNullInteger(item.CodTarjeta_Seg));
                    query.Parameters.AddWithValue("@CodMaq", ManejoNulos.ManageNullStr(item.CodMaq));
                    query.Parameters.AddWithValue("@CodMaqMin", ManejoNulos.ManageNullStr(item.CodMaqMin));
                    query.Parameters.AddWithValue("@Modelo", ManejoNulos.ManageNullStr(item.Modelo));
                    query.Parameters.AddWithValue("@COD_TIPOMAQUINA", ManejoNulos.ManageNullInteger(item.COD_TIPOMAQUINA));
                    query.Parameters.AddWithValue("@Posicion", ManejoNulos.ManageNullStr(item.Posicion));
                    query.Parameters.AddWithValue("@Tpro", ManejoNulos.ManageNullInteger(item.Tpro));
                    query.Parameters.AddWithValue("@Estado", ManejoNulos.ManageNullInteger(item.Estado));
                    query.Parameters.AddWithValue("@CodFicha", ManejoNulos.ManageNullInteger(item.CodFicha));
                    query.Parameters.AddWithValue("@CodMarca", ManejoNulos.ManageNullInteger(item.CodMarca));
                    query.Parameters.AddWithValue("@CodModelo", ManejoNulos.ManageNullInteger(item.CodModelo));
                    query.Parameters.AddWithValue("@Hopper", ManejoNulos.ManageNullInteger(item.Hopper));
                    query.Parameters.AddWithValue("@CredOtor", ManejoNulos.ManageNullInteger(item.CredOtor));
                    query.Parameters.AddWithValue("@Token", ManejoNulos.ManageNullDecimal(item.Token));
                    query.Parameters.AddWithValue("@NroContradores", ManejoNulos.ManageNullInteger(item.NroContradores));
                    query.Parameters.AddWithValue("@Precio_Credito", ManejoNulos.ManageNullDecimal(item.Precio_Credito));
                    query.Parameters.AddWithValue("@NUM_SERIE", ManejoNulos.ManageNullStr(item.NUM_SERIE));
                    query.Parameters.AddWithValue("@idTipoMoneda", ManejoNulos.ManageNullInteger(item.idTipoMoneda));
                    query.Parameters.AddWithValue("@MODALIDAD", ManejoNulos.ManageNullInteger(item.MODALIDAD));
                    query.Parameters.AddWithValue("@MAQ_X", ManejoNulos.ManageNullInteger(item.MAQ_X));
                    query.Parameters.AddWithValue("@MAQ_Y", ManejoNulos.ManageNullInteger(item.MAQ_Y));
                    query.Parameters.AddWithValue("@MAQ_ASIGLAYOUT", ManejoNulos.ManageNullStr(item.MAQ_ASIGLAYOUT));
                    query.Parameters.AddWithValue("@MAQ_POSILAYOUT", ManejoNulos.ManageNullInteger(item.MAQ_POSILAYOUT));
                    query.Parameters.AddWithValue("@CIn", ManejoNulos.ManageNullInteger(item.CIn));
                    query.Parameters.AddWithValue("@COut", ManejoNulos.ManageNullInteger(item.COut));
                    query.Parameters.AddWithValue("@TipoTranSac", ManejoNulos.ManageNullInteger(item.TipoTranSac));
                    query.Parameters.AddWithValue("@cod_caja", ManejoNulos.ManageNullInteger(item.cod_caja));
                    query.Parameters.AddWithValue("@TopeCreditos", ManejoNulos.ManageNullInteger(item.TopeCreditos));
                    query.Parameters.AddWithValue("@S_ONLINE", ManejoNulos.ManageNullInteger(item.S_ONLINE));
                    query.Parameters.AddWithValue("@FORMULA_MAQ", ManejoNulos.ManageNullStr(item.FORMULA_MAQ));
                    query.Parameters.AddWithValue("@MAQ_DEVPORCENTAJE", ManejoNulos.ManageNullDecimal(item.MAQ_DEVPORCENTAJE));
                    query.Parameters.AddWithValue("@FormulaFinal", ManejoNulos.ManageNullStr(item.FormulaFinal));
                    query.Parameters.AddWithValue("@Dbase", ManejoNulos.ManageNullStr(item.Dbase));
                    query.Parameters.AddWithValue("@EnviaDbase", ManejoNulos.ManegeNullBool(item.EnviaDbase));
                    query.Parameters.AddWithValue("@cod_servidor", ManejoNulos.ManageNullInteger(item.cod_servidor));
                    query.Parameters.AddWithValue("@Cod_Socio", ManejoNulos.ManageNullInteger(item.Cod_Socio));
                    query.Parameters.AddWithValue("@Status_Online", ManejoNulos.ManageNullInteger(item.Status_Online));
                    query.Parameters.AddWithValue("@COD_MODELO", ManejoNulos.ManageNullInteger(item.COD_MODELO));
                    query.Parameters.AddWithValue("@Sistema", ManejoNulos.ManageNullInteger(item.Sistema));
                    query.Parameters.AddWithValue("@PosicionBilletero", ManejoNulos.ManageNullStr(item.PosicionBilletero));
                    query.Parameters.AddWithValue("@con_sorteo", ManejoNulos.ManageNullInteger(item.con_sorteo));
                    query.Parameters.AddWithValue("@Block", ManejoNulos.ManageNullInteger(item.Block));
                    query.Parameters.AddWithValue("@codigo_extra", ManejoNulos.ManageNullStr(item.codigo_extra));
                    query.Parameters.AddWithValue("@PromoBonus", ManejoNulos.ManageNullInteger(item.PromoBonus));
                    query.Parameters.AddWithValue("@PromoTicket", ManejoNulos.ManageNullInteger(item.PromoTicket));
                    query.Parameters.AddWithValue("@estadoT", ManejoNulos.ManageNullInteger(item.estadoT));
                    query.Parameters.AddWithValue("@retiroTemporal", ManejoNulos.ManageNullInteger(item.retiroTemporal));

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
