using ServicioServidorVPN.utilitarios;
using ServicioServidorVPN.WGDB_000.model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioServidorVPN.WGDB_000.dal
{
    public class promogames_dal
    {
        private readonly string bd_username = string.Empty;
        private readonly string bd_password = string.Empty;
        private readonly string bd_datasource = string.Empty;
        private readonly string connectionString = string.Empty;
        public promogames_dal(string database_name)
        {
            bd_username = ConfigurationManager.AppSettings["bd_username"];
            bd_password = ConfigurationManager.AppSettings["bd_password"];
            bd_datasource = ConfigurationManager.AppSettings["bd_datasource"];
            connectionString = $"Data Source={bd_datasource};Initial Catalog={database_name};Integrated Security=False;User ID={bd_username};Password={bd_password}";

        }
        public long SavePromoGames(promogames item)
        {
            //bool respuesta = false;
            long IdInsertado = 0;
            string consulta = @"
INSERT INTO [dbo].[promogames]
           ([pg_id]
           ,[pg_name]
           ,[pg_type]
           ,[pg_price]
           ,[pg_price_units]
           ,[pg_return_price]
           ,[pg_return_units]
           ,[pg_percentatge_cost_return]
           ,[pg_game_url]
           ,[pg_mandatory_ic]
           ,[pg_show_buy_dialog]
           ,[pg_player_can_cancel]
           ,[pg_autocancel]
           ,[pg_transfer_screen]
           ,[pg_transfer_timeout]
           ,[pg_pay_table]
           ,[pg_last_update]
           ,[pg_request_pin]
           ,[pg_use_personalized_image]
           ,[pg_personalized_image_closed]
           ,[pg_personalized_image_closed_shared]
           ,[pg_personalized_image_closed_2]
           ,[pg_personalized_image_closed_3]
           ,[pg_personalized_image_opened]
           ,[pg_personalized_image_opened_shared]
           ,[pg_personalized_image_opened_2]
           ,[pg_personalized_image_opened_3])
--output inserted.pg_id
     VALUES
           (@pg_id
           ,@pg_name
           ,@pg_type
           ,@pg_price
           ,@pg_price_units
           ,@pg_return_price
           ,@pg_return_units
           ,@pg_percentatge_cost_return
           ,@pg_game_url
           ,@pg_mandatory_ic
           ,@pg_show_buy_dialog
           ,@pg_player_can_cancel
           ,@pg_autocancel
           ,@pg_transfer_screen
           ,@pg_transfer_timeout
           ,@pg_pay_table
           ,@pg_last_update
           ,@pg_request_pin
           ,@pg_use_personalized_image
           ,@pg_personalized_image_closed
           ,@pg_personalized_image_closed_shared
           ,@pg_personalized_image_closed_2
           ,@pg_personalized_image_closed_3
           ,@pg_personalized_image_opened
           ,@pg_personalized_image_opened_shared
           ,@pg_personalized_image_opened_2
           ,@pg_personalized_image_opened_3)
                      ";
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@pg_id", ManejoNulos.ManageNullInteger64(item.pg_id));
                    query.Parameters.AddWithValue("@pg_name", ManejoNulos.ManageNullStr(item.pg_name));
                    query.Parameters.AddWithValue("@pg_type", ManejoNulos.ManageNullInteger64(item.pg_type));
                    query.Parameters.AddWithValue("@pg_price", ManejoNulos.ManageNullDecimal(item.pg_price));
                    query.Parameters.AddWithValue("@pg_price_units", ManejoNulos.ManageNullInteger(item.pg_price_units));
                    query.Parameters.AddWithValue("@pg_return_price", ManejoNulos.ManegeNullBool(item.pg_return_price));
                    query.Parameters.AddWithValue("@pg_return_units", ManejoNulos.ManageNullInteger(item.pg_return_units));
                    query.Parameters.AddWithValue("@pg_percentatge_cost_return", ManejoNulos.ManageNullDecimal(item.pg_percentatge_cost_return));
                    query.Parameters.AddWithValue("@pg_game_url", ManejoNulos.ManageNullStr(item.pg_game_url));
                    query.Parameters.AddWithValue("@pg_mandatory_ic", ManejoNulos.ManegeNullBool(item.pg_mandatory_ic));
                    query.Parameters.AddWithValue("@pg_show_buy_dialog", ManejoNulos.ManegeNullBool(item.pg_show_buy_dialog));
                    query.Parameters.AddWithValue("@pg_player_can_cancel", ManejoNulos.ManegeNullBool(item.pg_player_can_cancel));
                    query.Parameters.AddWithValue("@pg_autocancel", ManejoNulos.ManageNullInteger(item.pg_autocancel));
                    query.Parameters.AddWithValue("@pg_transfer_screen", ManejoNulos.ManegeNullBool(item.pg_transfer_screen));
                    query.Parameters.AddWithValue("@pg_transfer_timeout", ManejoNulos.ManageNullInteger(item.pg_transfer_timeout));
                    query.Parameters.AddWithValue("@pg_pay_table", ManejoNulos.ManageNullStr(item.pg_pay_table));
                    query.Parameters.AddWithValue("@pg_last_update", ManejoNulos.ManageNullDate(item.pg_last_update));
                    query.Parameters.AddWithValue("@pg_request_pin", ManejoNulos.ManegeNullBool(item.pg_request_pin));
                    query.Parameters.AddWithValue("@pg_use_personalized_image", ManejoNulos.ManegeNullBool(item.pg_use_personalized_image));
                    query.Parameters.AddWithValue("@pg_personalized_image_closed", ManejoNulos.ManageNullInteger64(item.pg_personalized_image_closed));
                    query.Parameters.AddWithValue("@pg_personalized_image_closed_shared", ManejoNulos.ManegeNullBool(item.pg_personalized_image_closed_shared));
                    query.Parameters.AddWithValue("@pg_personalized_image_closed_2", ManejoNulos.ManageNullInteger64(item.pg_personalized_image_closed_2));
                    query.Parameters.AddWithValue("@pg_personalized_image_closed_3", ManejoNulos.ManageNullInteger64(item.pg_personalized_image_closed_3));
                    query.Parameters.AddWithValue("@pg_personalized_image_opened", ManejoNulos.ManageNullInteger64(item.pg_personalized_image_opened));
                    query.Parameters.AddWithValue("@pg_personalized_image_opened_shared", ManejoNulos.ManegeNullBool(item.pg_personalized_image_opened_shared));
                    query.Parameters.AddWithValue("@pg_personalized_image_opened_2", ManejoNulos.ManageNullInteger64(item.pg_personalized_image_opened_2));
                    query.Parameters.AddWithValue("@pg_personalized_image_opened_3", ManejoNulos.ManageNullInteger64(item.pg_personalized_image_opened_3));
                    //IdInsertado = Convert.ToInt32(query.ExecuteScalar());
                    query.ExecuteNonQuery();
                    IdInsertado = item.pg_id;
                }
            }
            catch (Exception ex)
            {
                IdInsertado = 0;
            }
            return IdInsertado;
        }
        public int GetLastIdInserted()
        {
            int total = 0;

            string query = @"
            select top 1 pg_id as lastid from 
            [dbo].[promogames]
            order by pg_id desc
";

            try
            {
                using (SqlConnection conecction = new SqlConnection(connectionString))
                {
                    conecction.Open();
                    SqlCommand command = new SqlCommand(query, conecction);
                    using (SqlDataReader data = command.ExecuteReader())
                    {
                        if (data.Read())
                        {
                            total = (int)data["lastid"];
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                total = 0;
            }

            return total;
        }
    }
}
