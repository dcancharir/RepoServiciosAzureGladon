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
    public class venues_dal
    {
        private readonly string _conexion = string.Empty;

        public venues_dal()
        {
            _conexion = ConfigurationManager.ConnectionStrings["connection_wgdb_000"].ConnectionString;
        }
        public List<venues> GetVenuesPaginated(long lastid, int skip, int pageSize)
        {
            var result = new List<venues>();
            try
            {
                string query = $@"
SELECT [ve_venue_id]
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
      ,[ve_db_update]
  FROM [dbo].[venues]
  where ve_venue_id > {lastid}
  order by ve_venue_id asc
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
                                var item = new venues()
                                {
                                    ve_venue_id = ManejoNulos.ManageNullInteger(dr["ve_venue_id"]),
                                    ve_external_venue_id = ManejoNulos.ManageNullStr(dr["ve_external_venue_id"]),
                                    ve_venue_type_id = ManejoNulos.ManageNullInteger(dr["ve_venue_type_id"]),
                                    ve_shortname = ManejoNulos.ManageNullStr(dr["ve_shortname"]),
                                    ve_fullname = ManejoNulos.ManageNullStr(dr["ve_fullname"]),
                                    ve_location = ManejoNulos.ManageNullStr(dr["ve_location"]),
                                    ve_geolocation = ManejoNulos.ManageNullStr(dr["ve_geolocation"]),
                                    ve_zipcode = ManejoNulos.ManageNullStr(dr["ve_zipcode"]),
                                    ve_status_id = ManejoNulos.ManageNullInteger(dr["ve_status_id"]),
                                    ve_surface_m2 = ManejoNulos.ManageNullDecimal(dr["ve_surface_m2"]),
                                    ve_enable = ManejoNulos.ManegeNullBool(dr["ve_enable"]),
                                    ve_disabletime = ManejoNulos.ManageNullDate(dr["ve_disabletime"]),
                                    ve_reason = ManejoNulos.ManageNullStr(dr["ve_reason"]),
                                    ve_threshold_amount = ManejoNulos.ManageNullDecimal(dr["ve_threshold_amount"]),
                                    ve_netwin_pct = ManejoNulos.ManageNullDecimal(dr["ve_netwin_pct"]),
                                    ve_vault_id = ManejoNulos.ManageNullInteger(dr["ve_vault_id"]),
                                    ve_operator_id = ManejoNulos.ManageNullInteger(dr["ve_operator_id"]),
                                    ve_dbversion = ManejoNulos.ManageNullStr(dr["ve_dbversion"]),
                                    ve_db_description = ManejoNulos.ManageNullStr(dr["ve_db_description"]),
                                    ve_db_update = ManejoNulos.ManageNullDate(dr["ve_db_update"]),
                                };
                                result.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = new List<venues>();
            }
            return result;
        }
        public int GetTotalVenuesForMigration(long lastid)
        {
            int total = 0;

            string query = @"
            select count(*) as total from 
            [dbo].[venues]
where ve_venue_id > @lastid
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
        public int SaveAreas(venues item)
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
output inserted.ve_venue_id
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
                using (var con = new SqlConnection(_conexion))
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
