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
    public class CMP_SesionMigracionDAL
    {
        private readonly string _conexion;
        public CMP_SesionMigracionDAL()
        {
            _conexion = ConfigurationManager.ConnectionStrings["connectionBD"].ConnectionString;
        }
        public int ObtenerMaximoIdIas()
        {
            int result = 0;
            string consulta = @"SELECT max(idias) as maximo
                              FROM [dbo].[CMP_SesionMigracion]";
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
        public int GuardarSesion(CMP_SesionMigracion sesion)
        {
            //bool respuesta = false;
            int IdInsertado = 0;
            string consulta = @"
            
INSERT INTO [dbo].[CMP_SesionMigracion]
([SesionId],[CodMaquina]
,[FechaInicio]
,[ClienteIdIas]
,[NombreCliente]
,[NroDocumento]
,[UsuarioIas]
,[EstadoEnvio],[CantidadJugadas],[CantidadCupones],[SerieIni],[SerieFin],[Mail],[TipoDocumentoId],[NombreTipoDocumento],[TipoSesion],[NombreTipoSesion],[CodSala],[NombreSala],[FechaTermino],[Terminado],UsuarioTermino,[MotivoTermino],[FechaRegistro],[IdIas])
output inserted.Id
VALUES
(@SesionId,@CodMaquina
,@FechaInicio
,@ClienteIdIas
,@NombreCliente
,@NroDocumento
,@UsuarioIas
,@EstadoEnvio,@CantidadJugadas,@CantidadCupones,@SerieIni,@SerieFin,@Mail,@TipoDocumentoId,@NombreTipoDocumento,@TipoSesion,@NombreTipoSesion,@CodSala,@NombreSala,@FechaTermino,@Terminado,@UsuarioTermino,@MotivoTermino,getdate(),@IdIas)
           
                      ";
            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@SesionId", ManejoNulos.ManageNullInteger64(sesion.SesionId));
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
                    query.Parameters.AddWithValue("@NombreTipoSesion", ManejoNulos.ManageNullStr(sesion.NombreTipoSesion));
                    query.Parameters.AddWithValue("@CodSala", ManejoNulos.ManageNullInteger(sesion.CodSala));
                    query.Parameters.AddWithValue("@NombreSala", ManejoNulos.ManageNullStr(sesion.NombreSala));
                    query.Parameters.AddWithValue("@FechaTermino", ManejoNulos.ManageNullDate(sesion.FechaTermino));
                    query.Parameters.AddWithValue("@Terminado", ManejoNulos.ManageNullInteger(sesion.Terminado));
                    query.Parameters.AddWithValue("@UsuarioTermino", ManejoNulos.ManageNullInteger(sesion.UsuarioTermino));
                    query.Parameters.AddWithValue("@MotivoTermino", ManejoNulos.ManageNullStr(sesion.MotivoTermino));
                    query.Parameters.AddWithValue("@IdIas", ManejoNulos.ManageNullInteger64(sesion.IdIas));
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
