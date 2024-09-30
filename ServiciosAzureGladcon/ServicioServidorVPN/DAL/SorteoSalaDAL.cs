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
    public class SorteoSalaDAL
    {
        string _conexion = string.Empty;
        public SorteoSalaDAL()
        {
            _conexion = ConfigurationManager.ConnectionStrings["conectionBd"].ConnectionString;
        }
        public int GuardarSorteoSala(SorteoSala sorteo)
        {
            //bool respuesta = false;
            int IdInsertado = 0;
            string consulta = @"
IF not EXISTS (SELECT * FROM [dbo].[SorteoSala] (nolock) WHERE SorteoId=@SorteoId and CodSala=@CodSala)
begin
    INSERT INTO [dbo].[SorteoSala]
                ([SorteoId],[CodSala]
                ,[Nombre]
                ,[Descripcion]
                ,[FechaRegistro]
                ,[FechaInicio]
                ,[FechaFin]
                ,[UsuarioCreacion]
                ,[Estado]
                ,[CondicionWin]
                ,[TopeCuponesxJugada],[CondicionBet],[EstadoCondicionBet])
    output inserted.SorteoId
            VALUES
                (@SorteoId,@CodSala
                ,@Nombre
                ,@Descripcion
                ,getdate()
                ,@FechaInicio
                ,@FechaFin
                ,@UsuarioCreacion
                ,@Estado
                ,@CondicionWin
                ,@TopeCuponesxJugada,@CondicionBet,@EstadoCondicionBet)
end
else

begin
    select 0
end
                    ";

            try
            {
                using(var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@SorteoId", ManejoNulos.ManageNullInteger(sorteo.SorteoId));
                    query.Parameters.AddWithValue("@Nombre", ManejoNulos.ManageNullStr(sorteo.Nombre));
                    query.Parameters.AddWithValue("@Descripcion", ManejoNulos.ManageNullStr(sorteo.Descripcion));
                    query.Parameters.AddWithValue("@FechaInicio", ManejoNulos.ManageNullDate(sorteo.FechaInicio));
                    query.Parameters.AddWithValue("@FechaFin", ManejoNulos.ManageNullDate(sorteo.FechaFin));
                    query.Parameters.AddWithValue("@UsuarioCreacion", ManejoNulos.ManageNullStr(sorteo.UsuarioCreacion));
                    query.Parameters.AddWithValue("@Estado", ManejoNulos.ManageNullInteger(sorteo.Estado));
                    query.Parameters.AddWithValue("@CondicionWin", ManejoNulos.ManageNullDouble(sorteo.CondicionWin));
                    query.Parameters.AddWithValue("@CondicionBet", ManejoNulos.ManageNullDouble(sorteo.CondicionBet));
                    query.Parameters.AddWithValue("@EstadoCondicionBet", ManejoNulos.ManageNullInteger(sorteo.EstadoCondicionBet));
                    query.Parameters.AddWithValue("@TopeCuponesxJugada", ManejoNulos.ManageNullInteger(sorteo.TopeCuponesxJugada));
                    query.Parameters.AddWithValue("@CodSala", ManejoNulos.ManageNullInteger(sorteo.CodSala));
                    IdInsertado = Convert.ToInt32(query.ExecuteScalar());
                }
            } catch(Exception ex)
            {
                IdInsertado = 0;
            }
            return IdInsertado;
        }
    }
}
