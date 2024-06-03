using ServicioMigracionWGDB_000.models;
using ServicioMigracionWGDB_000.utilitarios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioMigracionWGDB_000.dal
{
    public class promogames_dal
    {
        private readonly string _conexion = string.Empty;

        public promogames_dal()
        {
            _conexion = ConfigurationManager.ConnectionStrings["connection_wgdb_000"].ConnectionString;
        }
        public List<promogames> GetPromogamesPaginated(long lastid, int skip, int pageSize)
        {
            var result = new List<promogames>();
            try
            {
                string query = $@"
SELECT [pg_id]
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
      ,[pg_personalized_image_opened_3]
  FROM [dbo].[promogames]
  where pg_id > {lastid}
  order by pg_id asc
  OFFSET {skip} ROWS -- Número de filas para omitir
  FETCH NEXT {pageSize} ROWS ONLY; -- Número de filas para devolver
    ";
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var command = new SqlCommand(query, con);
                    using (var dr = command.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                var item = new promogames()
                                {
                                    pg_id = ManejoNulos.ManageNullInteger64(dr["pg_id"]),
                                    pg_name = ManejoNulos.ManageNullStr(dr["pg_name"]),
                                    pg_type = ManejoNulos.ManageNullInteger64(dr["pg_type"]),
                                    pg_price = ManejoNulos.ManageNullDecimal(dr["pg_price"]),
                                    pg_price_units = ManejoNulos.ManageNullInteger(dr["pg_price_units"]),
                                    pg_return_price = ManejoNulos.ManegeNullBool(dr["pg_return_price"]),
                                    pg_return_units = ManejoNulos.ManageNullInteger(dr["pg_return_units"]),
                                    pg_percentatge_cost_return = ManejoNulos.ManageNullDecimal(dr["pg_percentatge_cost_return"]),
                                    pg_game_url = ManejoNulos.ManageNullStr(dr["pg_game_url"]),
                                    pg_mandatory_ic = ManejoNulos.ManegeNullBool(dr["pg_mandatory_ic"]),
                                    pg_show_buy_dialog = ManejoNulos.ManegeNullBool(dr["pg_show_buy_dialog"]),
                                    pg_player_can_cancel = ManejoNulos.ManegeNullBool(dr["pg_player_can_cancel"]),
                                    pg_autocancel = ManejoNulos.ManageNullInteger(dr["pg_autocancel"]),
                                    pg_transfer_screen = ManejoNulos.ManegeNullBool(dr["pg_transfer_screen"]),
                                    pg_transfer_timeout = ManejoNulos.ManageNullInteger(dr["pg_transfer_timeout"]),
                                    pg_pay_table = ManejoNulos.ManageNullStr(dr["pg_pay_table"]),
                                    pg_last_update = ManejoNulos.ManageNullDate(dr["pg_last_update"]),
                                    pg_request_pin = ManejoNulos.ManegeNullBool(dr["pg_request_pin"]),
                                    pg_use_personalized_image = ManejoNulos.ManegeNullBool(dr["pg_use_personalized_image"]),
                                    pg_personalized_image_closed = ManejoNulos.ManageNullInteger64(dr["pg_personalized_image_closed"]),
                                    pg_personalized_image_closed_shared = ManejoNulos.ManegeNullBool(dr["pg_personalized_image_closed_shared"]),
                                    pg_personalized_image_closed_2 = ManejoNulos.ManageNullInteger64(dr["pg_personalized_image_closed_2"]),
                                    pg_personalized_image_closed_3 = ManejoNulos.ManageNullInteger64(dr["pg_personalized_image_closed_3"]),
                                    pg_personalized_image_opened = ManejoNulos.ManageNullInteger64(dr["pg_personalized_image_opened"]),
                                    pg_personalized_image_opened_shared = ManejoNulos.ManegeNullBool(dr["pg_personalized_image_opened_shared"]),
                                    pg_personalized_image_opened_2 = ManejoNulos.ManageNullInteger64(dr["pg_personalized_image_opened_2"]),
                                    pg_personalized_image_opened_3 = ManejoNulos.ManageNullInteger64(dr["pg_personalized_image_opened_3"]),
                                };
                                result.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = new List<promogames>();
            }
            return result;
        }
        public int GetTotalPromogamesForMigration(long lastid)
        {
            int total = 0;

            string query = @"
            select count(*) as total from 
            [dbo].[promogames]
where pg_id > @lastid
";

            try
            {
                using (SqlConnection conecction = new SqlConnection(_conexion))
                {
                    conecction.Open();
                    SqlCommand command = new SqlCommand(query, conecction);
                    command.Parameters.AddWithValue("@lastid", lastid);
                    using (SqlDataReader data = command.ExecuteReader())
                    {
                        if (data.Read())
                        {
                            total = ManejoNulos.ManageNullInteger(data["total"]);
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
        public int SavePromoGames(promogames item)
        {
            //bool respuesta = false;
            int IdInsertado = 0;
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
output inserted.pg_id
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
                using (var con = new SqlConnection(_conexion))
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
