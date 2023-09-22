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
    public class CMP_SesionClienteExcaDAL
    {
        private readonly string _conexion;
        public CMP_SesionClienteExcaDAL()
        {
            _conexion = ConfigurationManager.ConnectionStrings["conectionBdExca"].ConnectionString;
        }
        public int GuardarSesionCliente(CMP_SesionClienteExca sesion)
        {
            //bool respuesta = false;
            int IdInsertado = 0;
            string consulta = @"
 declare @cantidad int = (select COUNT(*) from CMP_Sesion (nolock)  where trim(NroDocumento)=trim(@NroDocumento))
IF not EXISTS (SELECT * FROM [dbo].[CMP_SesionClienteMigracion] (nolock) WHERE trim(NroDocumento)=trim(@NroDocumento))
begin
    INSERT INTO [dbo].[CMP_SesionClienteMigracion]
           ([NroDocumento]
           ,[CantidadSesiones]
           ,[NombreCliente]
           ,[Mail]
           ,[PrimeraSesion])
    output inserted.id
     VALUES
           (@NroDocumento
           ,@cantidad
           ,@NombreCliente
           ,@Mail
           ,@PrimeraSesion)
end
else
begin
update [dbo].[CMP_SesionClienteMigracion] set cantidadSesiones=@cantidad WHERE trim(NroDocumento)=trim(@NroDocumento)

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
