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
    public class MarcasDAL
    {
        private readonly string _conexion = string.Empty;
        public MarcasDAL()
        {
            _conexion = ConfigurationManager.ConnectionStrings["conectionBd"].ConnectionString;//coneccion Online
        }
        public int GuardarMarcas(Marcas item)
        {
            //bool respuesta = false;
            int IdInsertado = 0;
            string consulta = @"
IF not EXISTS (SELECT * FROM [dbo].[Marcas] (nolock) WHERE CodMarcas=@CodMarcas and CodSala=@CodSala)
begin
INSERT INTO [dbo].[Marcas]
           ([CodSala]
           ,[CodMarcas]
           ,[Descripcion]
           ,[estado]
           ,[estadoT])
output inserted.CodSala
     VALUES
           (@CodSala
           ,@CodMarcas
           ,@Descripcion
           ,@estado
           ,@estadoT)
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
                    query.Parameters.AddWithValue("@CodSala", ManejoNulos.ManageNullInteger(item.CodSala));
                    query.Parameters.AddWithValue("@CodMarcas", ManejoNulos.ManageNullInteger(item.CodMarcas));
                    query.Parameters.AddWithValue("@Descripcion", ManejoNulos.ManageNullStr(item.Descripcion));
                    query.Parameters.AddWithValue("@estado", ManejoNulos.ManageNullStr(item.estado));
                    query.Parameters.AddWithValue("@estadoT", ManejoNulos.ManageNullInteger(item.estadoT));
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
