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
    public class SesionDAL
    {
        private readonly string _conexion = string.Empty;
        public SesionDAL()
        {
            _conexion = ConfigurationManager.ConnectionStrings["conectionBd"].ConnectionString;//coneccion Online
        }
        public int GuardarSesion(Sesion sesion)
        {
            //bool respuesta = false;
            int IdInsertado = 0;
            string consulta = @"
    INSERT INTO [dbo].[Sesion]
           ([SesionId],[CodSala],[CodMaquina]
           ,[FechaInicio]
           ,[ClienteIdIas]
           ,[NombreCliente]
           ,[NroDocumento]
           ,[UsuarioIas]
           ,[EstadoEnvio],[CantidadJugadas],[CantidadCupones],[SerieIni],[SerieFin],[Mail],[TipoDocumentoId],[NombreTipoDocumento],[TipoSesion],[FechaTermino],[Terminado],[MotivoTermino],[UsuarioTermino])
output inserted.SesionId
     VALUES
           (@SesionId,@CodSala,@CodMaquina
           ,@FechaInicio
           ,@ClienteIdIas
           ,@NombreCliente
           ,@NroDocumento
           ,@UsuarioIas
           ,@EstadoEnvio,@CantidadJugadas,@CantidadCupones,@SerieIni,@SerieFin,@Mail,@TipoDocumentoId,@NombreTipoDocumento,@TipoSesion,@FechaTermino,@Terminado,@MotivoTermino,@UsuarioTermino)
";
            try {
                using (var con = new SqlConnection(_conexion)) {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@SesionId", ManejoNulos.ManageNullInteger64(sesion.SesionId));
                    query.Parameters.AddWithValue("@CodSala", ManejoNulos.ManageNullInteger(sesion.CodSala));
                    query.Parameters.AddWithValue("@CodMaquina", ManejoNulos.ManageNullStr(sesion.CodMaquina));
                    query.Parameters.AddWithValue("@FechaInicio", ManejoNulos.ManageNullDate(sesion.FechaInicio));
                    query.Parameters.AddWithValue("@ClienteIdIas", ManejoNulos.ManageNullInteger(sesion.ClienteIdIas));
                    query.Parameters.AddWithValue("@NombreCliente", ManejoNulos.ManageNullStr(sesion.NombreCliente).ToUpper());
                    query.Parameters.AddWithValue("@NroDocumento", ManejoNulos.ManageNullStr(sesion.NroDocumento));
                    query.Parameters.AddWithValue("@UsuarioIas", ManejoNulos.ManageNullInteger(sesion.UsuarioIas));
                    query.Parameters.AddWithValue("@EstadoEnvio", ManejoNulos.ManageNullInteger(sesion.EstadoEnvio));
                    query.Parameters.AddWithValue("@CantidadCupones", ManejoNulos.ManageNullInteger(sesion.CantidadCupones));
                    query.Parameters.AddWithValue("@CantidadJugadas", ManejoNulos.ManageNullInteger(sesion.CantidadJugadas));
                    query.Parameters.AddWithValue("@SerieIni", ManejoNulos.ManageNullStr(sesion.SerieIni));
                    query.Parameters.AddWithValue("@SerieFin", ManejoNulos.ManageNullStr(sesion.SerieFin));
                    query.Parameters.AddWithValue("@Mail", ManejoNulos.ManageNullStr(sesion.Mail).ToUpper());
                    query.Parameters.AddWithValue("@TipoDocumentoId", ManejoNulos.ManageNullInteger(sesion.TipoDocumentoId));
                    query.Parameters.AddWithValue("@NombreTipoDocumento", ManejoNulos.ManageNullStr(sesion.NombreTipoDocumento).ToUpper());
                    query.Parameters.AddWithValue("@TipoSesion", ManejoNulos.ManageNullInteger(sesion.TipoSesion));
                    query.Parameters.AddWithValue("@FechaTermino", ManejoNulos.ManageNullDate(sesion.FechaTermino));
                    query.Parameters.AddWithValue("@Terminado", ManejoNulos.ManageNullInteger(sesion.Terminado));
                    query.Parameters.AddWithValue("@MotivoTermino", ManejoNulos.ManageNullStr(sesion.MotivoTermino));
                    query.Parameters.AddWithValue("@UsuarioTermino", ManejoNulos.ManageNullInteger(sesion.UsuarioTermino));
                    IdInsertado = Convert.ToInt32(query.ExecuteScalar());
                }
            }
            catch (Exception ex) {
                IdInsertado = 0;
            }
            return IdInsertado;
        }
        public int GuardarSesionSiNoExiste(Sesion sesion) {
            //bool respuesta = false;
            int IdInsertado = 0;
            string consulta = @"
IF not EXISTS (SELECT 1 FROM [dbo].[Sesion] (nolock) WHERE SesionId=@SesionId and CodSala=@CodSala)
begin
    INSERT INTO [dbo].[Sesion]
           ([SesionId],[CodSala],[CodMaquina]
           ,[FechaInicio]
           ,[ClienteIdIas]
           ,[NombreCliente]
           ,[NroDocumento]
           ,[UsuarioIas]
           ,[EstadoEnvio],[CantidadJugadas],[CantidadCupones],[SerieIni],[SerieFin],[Mail],[TipoDocumentoId],[NombreTipoDocumento],[TipoSesion],[FechaTermino],[Terminado],[MotivoTermino],[UsuarioTermino])
output inserted.SesionId
     VALUES
           (@SesionId,@CodSala,@CodMaquina
           ,@FechaInicio
           ,@ClienteIdIas
           ,@NombreCliente
           ,@NroDocumento
           ,@UsuarioIas
           ,@EstadoEnvio,@CantidadJugadas,@CantidadCupones,@SerieIni,@SerieFin,@Mail,@TipoDocumentoId,@NombreTipoDocumento,@TipoSesion,@FechaTermino,@Terminado,@MotivoTermino,@UsuarioTermino)
end
else

begin
    select -1
end

                      ";
            try {
                using (var con = new SqlConnection(_conexion)) {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@SesionId", ManejoNulos.ManageNullInteger64(sesion.SesionId));
                    query.Parameters.AddWithValue("@CodSala", ManejoNulos.ManageNullInteger(sesion.CodSala));
                    query.Parameters.AddWithValue("@CodMaquina", ManejoNulos.ManageNullStr(sesion.CodMaquina));
                    query.Parameters.AddWithValue("@FechaInicio", ManejoNulos.ManageNullDate(sesion.FechaInicio));
                    query.Parameters.AddWithValue("@ClienteIdIas", ManejoNulos.ManageNullInteger(sesion.ClienteIdIas));
                    query.Parameters.AddWithValue("@NombreCliente", ManejoNulos.ManageNullStr(sesion.NombreCliente).ToUpper());
                    query.Parameters.AddWithValue("@NroDocumento", ManejoNulos.ManageNullStr(sesion.NroDocumento));
                    query.Parameters.AddWithValue("@UsuarioIas", ManejoNulos.ManageNullInteger(sesion.UsuarioIas));
                    query.Parameters.AddWithValue("@EstadoEnvio", ManejoNulos.ManageNullInteger(sesion.EstadoEnvio));
                    query.Parameters.AddWithValue("@CantidadCupones", ManejoNulos.ManageNullInteger(sesion.CantidadCupones));
                    query.Parameters.AddWithValue("@CantidadJugadas", ManejoNulos.ManageNullInteger(sesion.CantidadJugadas));
                    query.Parameters.AddWithValue("@SerieIni", ManejoNulos.ManageNullStr(sesion.SerieIni));
                    query.Parameters.AddWithValue("@SerieFin", ManejoNulos.ManageNullStr(sesion.SerieFin));
                    query.Parameters.AddWithValue("@Mail", ManejoNulos.ManageNullStr(sesion.Mail).ToUpper());
                    query.Parameters.AddWithValue("@TipoDocumentoId", ManejoNulos.ManageNullInteger(sesion.TipoDocumentoId));
                    query.Parameters.AddWithValue("@NombreTipoDocumento", ManejoNulos.ManageNullStr(sesion.NombreTipoDocumento).ToUpper());
                    query.Parameters.AddWithValue("@TipoSesion", ManejoNulos.ManageNullInteger(sesion.TipoSesion));
                    query.Parameters.AddWithValue("@FechaTermino", ManejoNulos.ManageNullDate(sesion.FechaTermino));
                    query.Parameters.AddWithValue("@Terminado", ManejoNulos.ManageNullInteger(sesion.Terminado));
                    query.Parameters.AddWithValue("@MotivoTermino", ManejoNulos.ManageNullStr(sesion.MotivoTermino));
                    query.Parameters.AddWithValue("@UsuarioTermino", ManejoNulos.ManageNullInteger(sesion.UsuarioTermino));
                    IdInsertado = Convert.ToInt32(query.ExecuteScalar());
                }
            }
            catch (Exception ex) {
                IdInsertado = 0;
            }
            return IdInsertado;
        }
        public bool EliminarTodoPorSesionYSala(long sesionid, int codsala) {
            bool respuesta = false;

            string consulta = @"
delete from jugada where jugadaid in (select distinct jugadaid from sesionsorteosala where sesionid = @sesionid and codsala = @codsala) and codsala = @codsala
delete from sesionsorteosala where sesionid = @sesionid and codsala = @codsala
delete from sesion where sesionid = @sesionid and codsala = @codsala

        ";

            try {
                using (var con = new SqlConnection(_conexion)) {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@sesionid", ManejoNulos.ManageNullInteger64(sesionid));
                    query.Parameters.AddWithValue("@codsala", ManejoNulos.ManageNullInteger(codsala));
                    query.ExecuteNonQuery();
                    respuesta = true;
                }
            }
            catch (Exception ex) {
                var response = " - " + ex.Message.ToString() + "\n ----------- InnerException=\n" + ex.InnerException + "\n --------------- Stack: ------------\n" + ex.StackTrace.ToString();
                funciones.logueo("ERROR metodo RemoveSesionSorteoSala() = " + response, "Error");
                respuesta = false;
            }
            return respuesta;
        }
        public int ExisteSesion(long sesionid,int codsala) {
            //bool respuesta = false;
            int existente = 0;
            string consulta = @"
IF not EXISTS (SELECT 1 FROM [dbo].[Sesion] (nolock) WHERE SesionId=@sesionid and CodSala=@codsala)
begin
    select 0
end
else

begin
    select 1
end

                      ";
            try {
                using (var con = new SqlConnection(_conexion)) {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@sesionid", sesionid);
                    query.Parameters.AddWithValue("@codsala", codsala);
                    existente = Convert.ToInt32(query.ExecuteScalar());
                }
            }
            catch (Exception ex) {
                existente = 1;
            }
            return existente;
        }
    }
}
