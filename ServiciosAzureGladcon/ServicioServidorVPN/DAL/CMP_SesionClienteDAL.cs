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
    public class CMP_SesionClienteDAL
    {
        private readonly string _conexion;
        public CMP_SesionClienteDAL()
        {
            _conexion = ConfigurationManager.ConnectionStrings["conectionBd"].ConnectionString;
        }
        public int GuardarSesionCliente(CMP_SesionCliente sesion)
        {
            //bool respuesta = false;
            int IdInsertado = 0;
            string consulta = @"
 declare @cantidad int = (select COUNT(*) from CMP_Sesion (nolock)  where trim(NroDocumento)=trim(@NroDocumento) and TipoDocumentoId=@TipoDocumentoId and CodSala=@CodSala)
IF not EXISTS (SELECT * FROM [dbo].[CMP_SesionClienteMigracion] (nolock) WHERE trim(NroDocumento)=trim(@NroDocumento) and TipoDocumentoId=@TipoDocumentoId and CodSala=@CodSala)
begin
    INSERT INTO [dbo].[CMP_SesionClienteMigracion]
           ([NroDocumento]
           ,[NombreTipoDocumento]
           ,[TipoDocumentoId]
           ,[CantidadSesiones]
           ,[NombreCliente]
           ,[Mail]
           ,[PrimeraSesion],[CodSala],[UltimaSesion])
    output inserted.id
     VALUES
           (@NroDocumento
           ,@NombreTipoDocumento
           ,@TipoDocumentoId
           ,@cantidad
           ,@NombreCliente
           ,@Mail
           ,@PrimeraSesion,@CodSala,@UltimaSesion)
end
else
begin
update [dbo].[CMP_SesionClienteMigracion] set cantidadSesiones=@cantidad WHERE trim(NroDocumento)=trim(@NroDocumento) and TipoDocumentoId=@TipoDocumentoId and CodSala=@CodSala
update [dbo].[CMP_SesionClienteMigracion] set UltimaSesion=@UltimaSesion WHERE trim(NroDocumento)=trim(@NroDocumento) and TipoDocumentoId=@TipoDocumentoId and CodSala=@CodSala
    select 0
end

                      ";
            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@NroDocumento", ManejoNulos.ManageNullStr(sesion.NroDocumento));
                    query.Parameters.AddWithValue("@NombreTipoDocumento", ManejoNulos.ManageNullStr(sesion.NombreTipoDocumento));
                    query.Parameters.AddWithValue("@TipoDocumentoId", ManejoNulos.ManageNullInteger(sesion.TipoDocumentoId));
                    query.Parameters.AddWithValue("@NombreCliente", ManejoNulos.ManageNullStr(sesion.NombreCliente));
                    query.Parameters.AddWithValue("@Mail", ManejoNulos.ManageNullStr(sesion.Mail));
                    query.Parameters.AddWithValue("@PrimeraSesion", ManejoNulos.ManageNullDate(sesion.PrimeraSesion));
                    query.Parameters.AddWithValue("@CodSala", ManejoNulos.ManageNullInteger(sesion.CodSala));
                    query.Parameters.AddWithValue("@UltimaSesion", ManejoNulos.ManageNullDate(sesion.UltimaSesion));
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
