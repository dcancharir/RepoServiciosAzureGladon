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
                    query.Parameters.AddWithValue("@pg_id", item.pg_id == null ? DBNull.Value : (object)item.pg_id);
                    query.Parameters.AddWithValue("@pg_name", item.pg_name == null ? DBNull.Value : (object)item.pg_name);
                    query.Parameters.AddWithValue("@pg_type", item.pg_type == null ? DBNull.Value : (object)item.pg_type);
                    query.Parameters.AddWithValue("@pg_price", item.pg_price == null ? DBNull.Value : (object)item.pg_price);
                    query.Parameters.AddWithValue("@pg_price_units", item.pg_price_units == null ? DBNull.Value : (object)item.pg_price_units);
                    query.Parameters.AddWithValue("@pg_return_price", item.pg_return_price == null ? DBNull.Value : (object)item.pg_return_price);
                    query.Parameters.AddWithValue("@pg_return_units", item.pg_return_units == null ? DBNull.Value : (object)item.pg_return_units);
                    query.Parameters.AddWithValue("@pg_percentatge_cost_return", item.pg_percentatge_cost_return == null ? DBNull.Value : (object)item.pg_percentatge_cost_return);
                    query.Parameters.AddWithValue("@pg_game_url", item.pg_game_url == null ? DBNull.Value : (object)item.pg_game_url);
                    query.Parameters.AddWithValue("@pg_mandatory_ic", item.pg_mandatory_ic == null ? DBNull.Value : (object)item.pg_mandatory_ic);
                    query.Parameters.AddWithValue("@pg_show_buy_dialog", item.pg_show_buy_dialog == null ? DBNull.Value : (object)item.pg_show_buy_dialog);
                    query.Parameters.AddWithValue("@pg_player_can_cancel", item.pg_player_can_cancel == null ? DBNull.Value : (object)item.pg_player_can_cancel);
                    query.Parameters.AddWithValue("@pg_autocancel", item.pg_autocancel == null ? DBNull.Value : (object)item.pg_autocancel);
                    query.Parameters.AddWithValue("@pg_transfer_screen", item.pg_transfer_screen == null ? DBNull.Value : (object)item.pg_transfer_screen);
                    query.Parameters.AddWithValue("@pg_transfer_timeout", item.pg_transfer_timeout == null ? DBNull.Value : (object)item.pg_transfer_timeout);
                    query.Parameters.AddWithValue("@pg_pay_table", item.pg_pay_table == null ? DBNull.Value : (object)item.pg_pay_table);
                    query.Parameters.AddWithValue("@pg_last_update", item.pg_last_update == null ? DBNull.Value : (object)item.pg_last_update);
                    query.Parameters.AddWithValue("@pg_request_pin", item.pg_request_pin == null ? DBNull.Value : (object)item.pg_request_pin);
                    query.Parameters.AddWithValue("@pg_use_personalized_image", item.pg_use_personalized_image == null ? DBNull.Value : (object)item.pg_use_personalized_image);
                    query.Parameters.AddWithValue("@pg_personalized_image_closed", item.pg_personalized_image_closed == null ? DBNull.Value : (object)item.pg_personalized_image_closed);
                    query.Parameters.AddWithValue("@pg_personalized_image_closed_shared", item.pg_personalized_image_closed_shared == null ? DBNull.Value : (object)item.pg_personalized_image_closed_shared);
                    query.Parameters.AddWithValue("@pg_personalized_image_closed_2", item.pg_personalized_image_closed_2 == null ? DBNull.Value : (object)item.pg_personalized_image_closed_2);
                    query.Parameters.AddWithValue("@pg_personalized_image_closed_3", item.pg_personalized_image_closed_3 == null ? DBNull.Value : (object)item.pg_personalized_image_closed_3);
                    query.Parameters.AddWithValue("@pg_personalized_image_opened", item.pg_personalized_image_opened == null ? DBNull.Value : (object)item.pg_personalized_image_opened);
                    query.Parameters.AddWithValue("@pg_personalized_image_opened_shared", item.pg_personalized_image_opened_shared == null ? DBNull.Value : (object)item.pg_personalized_image_opened_shared);
                    query.Parameters.AddWithValue("@pg_personalized_image_opened_2", item.pg_personalized_image_opened_2 == null ? DBNull.Value : (object)item.pg_personalized_image_opened_2);
                    query.Parameters.AddWithValue("@pg_personalized_image_opened_3", item.pg_personalized_image_opened_3 == null ? DBNull.Value : (object)item.pg_personalized_image_opened_3);
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
