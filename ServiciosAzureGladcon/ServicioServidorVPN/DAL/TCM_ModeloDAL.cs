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
    public class TCM_ModeloDAL
    {
        private readonly string _conexion = string.Empty;
        public TCM_ModeloDAL()
        {
            _conexion = ConfigurationManager.ConnectionStrings["conectionBd"].ConnectionString;//coneccion Online
        }
        public int GuardarModelos(TCM_Modelo item)
        {
            //bool respuesta = false;
            int IdInsertado = 0;
            string consulta = @"
IF not EXISTS (SELECT * FROM [dbo].[TCM_MODELO] (nolock) WHERE COD_MODELO=@COD_MODELO and CodSala=@CodSala and COD_MARCA=@COD_MARCA)
begin
INSERT INTO [dbo].[TCM_MODELO]
           ([CodSala]
           ,[COD_MODELO]
           ,[COD_MARCA]
           ,[DESC_MODELO]
           ,[S_ESTADO]
           ,[CE_ENTRADA]
           ,[CE_SALIDA]
           ,[CE_PAGOMANUAL]
           ,[CM_ENTRADA]
           ,[CM_SALIDA]
           ,[CM_PAGOMANUAL]
           ,[CB_BILLETERO]
           ,[estadoT])
output inserted.CodSala
     VALUES
           (@CodSala
           ,@COD_MODELO
           ,@COD_MARCA
           ,@DESC_MODELO
           ,@S_ESTADO
           ,@CE_ENTRADA
           ,@CE_SALIDA
           ,@CE_PAGOMANUAL
           ,@CM_ENTRADA
           ,@CM_SALIDA
           ,@CM_PAGOMANUAL
           ,@CB_BILLETERO
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
                    query.Parameters.AddWithValue("@COD_MODELO", ManejoNulos.ManageNullInteger(item.COD_MODELO));
                    query.Parameters.AddWithValue("@COD_MARCA", ManejoNulos.ManageNullInteger(item.COD_MARCA));
                    query.Parameters.AddWithValue("@DESC_MODELO", ManejoNulos.ManageNullStr(item.DESC_MODELO));
                    query.Parameters.AddWithValue("@S_ESTADO", ManejoNulos.ManageNullStr(item.S_ESTADO));
                    query.Parameters.AddWithValue("@CE_ENTRADA", ManejoNulos.ManageNullInteger(item.CE_ENTRADA));
                    query.Parameters.AddWithValue("@CE_SALIDA", ManejoNulos.ManageNullInteger(item.CE_SALIDA));
                    query.Parameters.AddWithValue("@CE_PAGOMANUAL", ManejoNulos.ManageNullInteger(item.CE_PAGOMANUAL));
                    query.Parameters.AddWithValue("@CM_ENTRADA", ManejoNulos.ManageNullInteger(item.CM_ENTRADA));
                    query.Parameters.AddWithValue("@CM_SALIDA", ManejoNulos.ManageNullInteger(item.CM_SALIDA));
                    query.Parameters.AddWithValue("@CM_PAGOMANUAL", ManejoNulos.ManageNullInteger(item.CM_PAGOMANUAL));
                    query.Parameters.AddWithValue("@CB_BILLETERO", ManejoNulos.ManageNullInteger(item.CB_BILLETERO));
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
