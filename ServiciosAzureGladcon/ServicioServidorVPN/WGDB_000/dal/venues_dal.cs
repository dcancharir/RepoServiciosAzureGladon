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
    public class venues_dal
    {
        private readonly string bd_username = string.Empty;
        private readonly string bd_password = string.Empty;
        private readonly string bd_datasource = string.Empty;
        private readonly string connectionString = string.Empty;
        public venues_dal(string database_name)
        {
            bd_username = ConfigurationManager.AppSettings["bd_username"];
            bd_password = ConfigurationManager.AppSettings["bd_password"];
            bd_datasource = ConfigurationManager.AppSettings["bd_datasource"];
            connectionString = $"Data Source={bd_datasource};Initial Catalog={database_name};Integrated Security=False;User ID={bd_username};Password={bd_password}";

        }
        public int SaveVenues(venues item)
        {
            //bool respuesta = false;
            int IdInsertado = 0;
            string consulta = @"
INSERT INTO [dbo].[venues]
           ([ve_venue_id]
           ,[ve_external_venue_id]
           ,[ve_venue_type_id]
           ,[ve_shortname]
           ,[ve_fullname]
           ,[ve_location]
           ,[ve_geolocation]
           ,[ve_zipcode]
           ,[ve_status_id]
           ,[ve_surface_m2]
           ,[ve_enable]
           ,[ve_disabletime]
           ,[ve_reason]
           ,[ve_threshold_amount]
           ,[ve_netwin_pct]
           ,[ve_vault_id]
           ,[ve_operator_id]
           ,[ve_dbversion]
           ,[ve_db_description]
           ,[ve_db_update])
--output inserted.ve_venue_id
     VALUES
           (@ve_venue_id
           ,@ve_external_venue_id
           ,@ve_venue_type_id
           ,@ve_shortname
           ,@ve_fullname
           ,@ve_location
           ,@ve_geolocation
           ,@ve_zipcode
           ,@ve_status_id
           ,@ve_surface_m2
           ,@ve_enable
           ,@ve_disabletime
           ,@ve_reason
           ,@ve_threshold_amount
           ,@ve_netwin_pct
           ,@ve_vault_id
           ,@ve_operator_id
           ,@ve_dbversion
           ,@ve_db_description
           ,@ve_db_update)
                      ";
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@ve_venue_id", item.ve_venue_id == null ? DBNull.Value : (object)item.ve_venue_id);
                    query.Parameters.AddWithValue("@ve_external_venue_id", item.ve_external_venue_id == null ? DBNull.Value : (object)item.ve_external_venue_id);
                    query.Parameters.AddWithValue("@ve_venue_type_id", item.ve_venue_type_id == null ? DBNull.Value : (object)item.ve_venue_type_id);
                    query.Parameters.AddWithValue("@ve_shortname", item.ve_shortname == null ? DBNull.Value : (object)item.ve_shortname);
                    query.Parameters.AddWithValue("@ve_fullname", item.ve_fullname == null ? DBNull.Value : (object)item.ve_fullname);
                    query.Parameters.AddWithValue("@ve_location", item.ve_location == null ? DBNull.Value : (object)item.ve_location);
                    query.Parameters.AddWithValue("@ve_geolocation", item.ve_geolocation == null ? DBNull.Value : (object)item.ve_geolocation);
                    query.Parameters.AddWithValue("@ve_zipcode", item.ve_zipcode == null ? DBNull.Value : (object)item.ve_zipcode);
                    query.Parameters.AddWithValue("@ve_status_id", item.ve_status_id == null ? DBNull.Value : (object)item.ve_status_id);
                    query.Parameters.AddWithValue("@ve_surface_m2", item.ve_surface_m2 == null ? DBNull.Value : (object)item.ve_surface_m2);
                    query.Parameters.AddWithValue("@ve_enable", item.ve_enable == null ? DBNull.Value : (object)item.ve_enable);
                    query.Parameters.AddWithValue("@ve_disabletime", item.ve_disabletime == null ? DBNull.Value : (object)item.ve_disabletime);
                    query.Parameters.AddWithValue("@ve_reason", item.ve_reason == null ? DBNull.Value : (object)item.ve_reason);
                    query.Parameters.AddWithValue("@ve_threshold_amount", item.ve_threshold_amount == null ? DBNull.Value : (object)item.ve_threshold_amount);
                    query.Parameters.AddWithValue("@ve_netwin_pct", item.ve_netwin_pct == null ? DBNull.Value : (object)item.ve_netwin_pct);
                    query.Parameters.AddWithValue("@ve_vault_id", item.ve_vault_id == null ? DBNull.Value : (object)item.ve_vault_id);
                    query.Parameters.AddWithValue("@ve_operator_id", item.ve_operator_id == null ? DBNull.Value : (object)item.ve_operator_id);
                    query.Parameters.AddWithValue("@ve_dbversion", item.ve_dbversion == null ? DBNull.Value : (object)item.ve_dbversion);
                    query.Parameters.AddWithValue("@ve_db_description", item.ve_db_description == null ? DBNull.Value : (object)item.ve_db_description);
                    query.Parameters.AddWithValue("@ve_db_update", item.ve_db_update == null ? DBNull.Value : (object)item.ve_db_update);
                    //IdInsertado = Convert.ToInt32(query.ExecuteScalar());
                    query.ExecuteNonQuery();
                    IdInsertado = item.ve_venue_id;
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
            select top 1 ve_venue_id as lastid from 
            [dbo].[venues]
            order by ve_venue_id desc
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
