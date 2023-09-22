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
    public class CMP_SorteoSalaExcaDAL
    {
        string _conexion = string.Empty;
        public CMP_SorteoSalaExcaDAL()
        {
            _conexion = ConfigurationManager.ConnectionStrings["conectionBdExca"].ConnectionString;
        }
        public int GuardarSorteoSala(CMP_SorteoSalaExca sorteo)
        {
            //bool respuesta = false;
            int IdInsertado = 0;
            string consulta = @"
IF not EXISTS (SELECT * FROM [dbo].[CMP_SorteoSala] (nolock) WHERE SorteoId=@SorteoId)
begin
INSERT INTO [dbo].[CMP_SorteoSala]
           ([SorteoId]
           ,[CodSala]
           ,[Nombre]
           ,[Descripcion]
           ,[FechaRegistro]
           ,[FechaModificacion]
           ,[FechaInicio]
           ,[FechaFin]
           ,[UsuarioCreacion]
           ,[Estado]
           ,[CondicionWin]
           ,[CondicionBet]
           ,[EstadoCondicionBet]
           ,[TopeCuponesxJugada])
output inserted.SorteoId
     VALUES
           (@SorteoId
           ,@CodSala
           ,@Nombre
           ,@Descripcion
           ,@FechaRegistro
           ,@FechaModificacion
           ,@FechaInicio
           ,@FechaFin
           ,@UsuarioCreacion
           ,@Estado
           ,@CondicionWin
           ,@CondicionBet
           ,@EstadoCondicionBet
           ,@TopeCuponesxJugada)
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
                    query.Parameters.AddWithValue("@SorteoId", ManejoNulos.ManageNullInteger(sorteo.SorteoId));
                    query.Parameters.AddWithValue("@CodSala", ManejoNulos.ManageNullInteger(sorteo.CodSala));
                    query.Parameters.AddWithValue("@Nombre", ManejoNulos.ManageNullStr(sorteo.Nombre));
                    query.Parameters.AddWithValue("@Descripcion", ManejoNulos.ManageNullStr(sorteo.Descripcion));
                    query.Parameters.AddWithValue("@FechaRegistro", ManejoNulos.ManageNullDate(sorteo.FechaRegistro));
                    query.Parameters.AddWithValue("@FechaModificacion", ManejoNulos.ManageNullDate(sorteo.FechaModificacion));
                    query.Parameters.AddWithValue("@FechaInicio", ManejoNulos.ManageNullDate(sorteo.FechaInicio));
                    query.Parameters.AddWithValue("@FechaFin", ManejoNulos.ManageNullDate(sorteo.FechaFin));
                    query.Parameters.AddWithValue("@UsuarioCreacion", ManejoNulos.ManageNullStr(sorteo.UsuarioCreacion));
                    query.Parameters.AddWithValue("@Estado", ManejoNulos.ManageNullInteger(sorteo.Estado));
                    query.Parameters.AddWithValue("@CondicionWin", ManejoNulos.ManageNullDouble(sorteo.CondicionWin));
                    query.Parameters.AddWithValue("@CondicionBet", ManejoNulos.ManageNullDouble(sorteo.CondicionBet));
                    query.Parameters.AddWithValue("@EstadoCondicionBet", ManejoNulos.ManageNullInteger(sorteo.EstadoCondicionBet));
                    query.Parameters.AddWithValue("@TopeCuponesxJugada", ManejoNulos.ManageNullInteger(sorteo.TopeCuponesxJugada));
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
