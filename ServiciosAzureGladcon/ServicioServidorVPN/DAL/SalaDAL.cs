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
    public class SalaDAL
    {
        string _conexion = string.Empty;
        public SalaDAL()
        {
            _conexion = ConfigurationManager.ConnectionStrings["conectionBd"].ConnectionString;
        }
        public int GuardarSala(Sala sala)
        {
            //bool respuesta = false;
            int IdInsertado = 0;
            string consulta = @"
IF not EXISTS (SELECT * FROM [dbo].[Sala] (nolock) where CodSala=@CodSala)
begin
    INSERT INTO [dbo].[Sala]
                ([CodSala]
                ,[Nombre])
    output inserted.CodSala
            VALUES
                (@CodSala
                ,@Nombre)
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
                    query.Parameters.AddWithValue("@Nombre", ManejoNulos.ManageNullStr(sala.Nombre));
                    query.Parameters.AddWithValue("@CodSala", ManejoNulos.ManageNullInteger(sala.CodSala));
                    IdInsertado = Convert.ToInt32(query.ExecuteScalar());
                    //query.ExecuteNonQuery();
                    //respuesta = true;
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
