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
    public class CMP_SesionClienteMigracionDAL
    {
        private readonly string _conexion;
        public CMP_SesionClienteMigracionDAL()
        {
            _conexion = ConfigurationManager.ConnectionStrings["connectionBD"].ConnectionString;
        }
        public int GuardarSesionCliente(CMP_SesionClienteMigracion sesion)
        {
            //bool respuesta = false;
            int IdInsertado = 0;
            string consulta = @"
IF not EXISTS (SELECT * FROM [dbo].[CMP_SesionClienteMigracion] WHERE trim(NroDocumento)=trim(@NroDocumento) and TipoDocumentoId=@TipoDocumentoId)
begin
    INSERT INTO [dbo].[CMP_SesionClienteMigracion]
           ([NroDocumento]
           ,[NombreTipoDocumento]
           ,[TipoDocumentoId]
           ,[CantidadSesiones]
           ,[NombreCliente]
           ,[Mail]
           ,[PrimeraSesion])
    output inserted.id
     VALUES
           (@NroDocumento
           ,@NombreTipoDocumento
           ,@TipoDocumentoId
           ,@CantidadSesiones
           ,@NombreCliente
           ,@Mail
           ,@PrimeraSesion)
end
else
begin
update [dbo].[CMP_SesionClienteMigracion] set cantidadSesiones=cantidadsesiones+@CantidadSesiones WHERE trim(NroDocumento)=trim(@NroDocumento) and TipoDocumentoId=@TipoDocumentoId

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
                    query.Parameters.AddWithValue("@CantidadSesiones", ManejoNulos.ManageNullInteger(sesion.CantidadSesiones));
                    query.Parameters.AddWithValue("@NombreCliente", ManejoNulos.ManageNullStr(sesion.NombreCliente));
                    query.Parameters.AddWithValue("@Mail", ManejoNulos.ManageNullStr(sesion.Mail));
                    query.Parameters.AddWithValue("@PrimeraSesion", ManejoNulos.ManageNullDate(sesion.PrimeraSesion));
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
