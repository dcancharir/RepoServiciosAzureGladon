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
                    query.Parameters.AddWithValue("@ve_venue_id", ManejoNulos.ManageNullInteger(item.ve_venue_id));
                    query.Parameters.AddWithValue("@ve_external_venue_id", ManejoNulos.ManageNullStr(item.ve_external_venue_id));
                    query.Parameters.AddWithValue("@ve_venue_type_id", ManejoNulos.ManageNullInteger(item.ve_venue_type_id));
                    query.Parameters.AddWithValue("@ve_shortname", ManejoNulos.ManageNullStr(item.ve_shortname));
                    query.Parameters.AddWithValue("@ve_fullname", ManejoNulos.ManageNullStr(item.ve_fullname));
                    query.Parameters.AddWithValue("@ve_location", ManejoNulos.ManageNullStr(item.ve_location));
                    query.Parameters.AddWithValue("@ve_geolocation", ManejoNulos.ManageNullStr(item.ve_geolocation));
                    query.Parameters.AddWithValue("@ve_zipcode", ManejoNulos.ManageNullStr(item.ve_zipcode));
                    query.Parameters.AddWithValue("@ve_status_id", ManejoNulos.ManageNullInteger(item.ve_status_id));
                    query.Parameters.AddWithValue("@ve_surface_m2", ManejoNulos.ManageNullDecimal(item.ve_surface_m2));
                    query.Parameters.AddWithValue("@ve_enable", ManejoNulos.ManegeNullBool(item.ve_enable));
                    query.Parameters.AddWithValue("@ve_disabletime", ManejoNulos.ManageNullDate(item.ve_disabletime));
                    query.Parameters.AddWithValue("@ve_reason", ManejoNulos.ManageNullStr(item.ve_reason));
                    query.Parameters.AddWithValue("@ve_threshold_amount", ManejoNulos.ManageNullDecimal(item.ve_threshold_amount));
                    query.Parameters.AddWithValue("@ve_netwin_pct", ManejoNulos.ManageNullDecimal(item.ve_netwin_pct));
                    query.Parameters.AddWithValue("@ve_vault_id", ManejoNulos.ManageNullInteger(item.ve_vault_id));
                    query.Parameters.AddWithValue("@ve_operator_id", ManejoNulos.ManageNullInteger(item.ve_operator_id));
                    query.Parameters.AddWithValue("@ve_dbversion", ManejoNulos.ManageNullStr(item.ve_dbversion));
                    query.Parameters.AddWithValue("@ve_db_description", ManejoNulos.ManageNullStr(item.ve_db_description));
                    query.Parameters.AddWithValue("@ve_db_update", ManejoNulos.ManageNullDate(item.ve_db_update));
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
