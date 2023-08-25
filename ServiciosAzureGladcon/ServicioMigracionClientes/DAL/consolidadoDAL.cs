using ServicioMigracionClientes.Clases;
using ServicioMigracionClientes.utilitarios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioMigracionClientes.DAL
{
    public class consolidadoDAL
    {
        private string _conexion = string.Empty;
        public consolidadoDAL()
        {
            _conexion = ConfigurationManager.ConnectionStrings["connectionBD"].ConnectionString;
        }
        public int ConsolidadoInsertar(consolidado item)
        {
            int idInsertado = 0;
            //bool response = false;
            string consulta = @"
 IF EXISTS (SELECT * FROM [dbo].[consolidado] (nolock) WHERE consolidado_id_ias = @consolidado_id_ias)
        BEGIN
           SELECT 1 
        END
        ELSE
        BEGIN
INSERT INTO [dbo].[consolidado]
           ([consolidado_id_ias]
           ,[fecha]
           ,[id_sala_consolidado]
           ,[id_maquina]
           ,[juego]
           ,[cod_maquina]
           ,[serie]
           ,[coin_in]
           ,[net_win]
           ,[average_bet]
           ,[game_played]
           ,[isla]
           ,[zona]
           ,[tipo_maquina]
           ,[fecha_ultimo_ingre]
           ,[marca_modelo]
           ,[posicion]
           ,[id_consolidad])
output inserted.consolidado_id
     VALUES
           (@consolidado_id_ias
           ,@fecha 
           ,@id_sala_consolidado 
           ,@id_maquina 
           ,@juego 
           ,@cod_maquina
           ,@serie 
           ,@coin_in 
           ,@net_win 
           ,@average_bet 
           ,@game_played 
           ,@isla 
           ,@zona 
           ,@tipo_maquina 
           ,@fecha_ultimo_ingre 
           ,@marca_modelo 
           ,@posicion 
           ,@id_consolidad
           ) 
end";
            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@consolidado_id_ias", ManejoNulos.ManageNullInteger(item.consolidado_id_ias));
                    query.Parameters.AddWithValue("@fecha", ManejoNulos.ManageNullDate(item.fecha));
                    query.Parameters.AddWithValue("@id_sala_consolidado", ManejoNulos.ManageNullInteger(item.id_sala_consolidado));
                    query.Parameters.AddWithValue("@id_maquina", ManejoNulos.ManageNullInteger(item.id_maquina));
                    query.Parameters.AddWithValue("@juego", ManejoNulos.ManageNullStr(item.juego));
                    query.Parameters.AddWithValue("@cod_maquina", ManejoNulos.ManageNullStr(item.cod_maquina));
                    query.Parameters.AddWithValue("@serie", ManejoNulos.ManageNullStr(item.serie));
                    query.Parameters.AddWithValue("@coin_in", ManejoNulos.ManageNullDouble(item.coin_in));
                    query.Parameters.AddWithValue("@net_win", ManejoNulos.ManageNullDouble(item.net_win));
                    query.Parameters.AddWithValue("@average_bet", ManejoNulos.ManageNullDouble(item.average_bet));
                    query.Parameters.AddWithValue("@game_played", ManejoNulos.ManageNullInteger(item.game_played));
                    query.Parameters.AddWithValue("@isla", ManejoNulos.ManageNullStr(item.isla));
                    query.Parameters.AddWithValue("@zona", ManejoNulos.ManageNullStr(item.zona));
                    query.Parameters.AddWithValue("@tipo_maquina", ManejoNulos.ManageNullStr(item.tipo_maquina));
                    query.Parameters.AddWithValue("@fecha_ultimo_ingre", ManejoNulos.ManageNullDate(item.fecha_ultimo_ingre));
                    query.Parameters.AddWithValue("@marca_modelo", ManejoNulos.ManageNullStr(item.marca_modelo));
                    query.Parameters.AddWithValue("@posicion", ManejoNulos.ManageNullStr(item.posicion));
                    query.Parameters.AddWithValue("@id_consolidad", ManejoNulos.ManageNullInteger(item.id_consolidad));
                    idInsertado=Convert.ToInt32(query.ExecuteScalar());
                    //query.ExecuteNonQuery();
                    //response = true;
                }
            }
            catch (Exception ex)
            { 
                idInsertado = 0;
            }
            return idInsertado;
        }
    }
}
