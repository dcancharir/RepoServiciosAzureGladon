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
    public class areas_dal
    {
        private readonly string _conexion_wgdb_000 = string.Empty;
        private readonly string _conexion_wgdb_000_migration = string.Empty;

        public areas_dal()
        {
            _conexion_wgdb_000 = ConfigurationManager.ConnectionStrings["connection_wgdb_000"].ConnectionString;
            _conexion_wgdb_000_migration = ConfigurationManager.ConnectionStrings["connection_wgdb_000_migration"].ConnectionString;
        }
        public List<areas> GetAreasPaginated(long lastid, int skip, int pageSize)
        {
            var result = new List<areas>();
            try
            {
                string query = $@"
SELECT [ar_area_id]
      ,[ar_name]
      ,[ar_smoking]
      ,[ar_timestamp]
      ,[ar_venue_id]
      ,[ar_external_id]
  FROM [dbo].[areas]
  where ar_area_id >= {lastid}
  order by ar_area_id asc
  OFFSET {skip} ROWS -- Número de filas para omitir
  FETCH NEXT {pageSize} ROWS ONLY; -- Número de filas para devolver
    ";
                using (var con = new SqlConnection(_conexion_wgdb_000))
                {
                    con.Open();
                    var command = new SqlCommand(query, con);
                    using (var dr = command.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                var item = new areas()
                                {
                                    ar_area_id = (int)dr["ar_area_id"],
                                    ar_name = dr["ar_name"] == DBNull.Value ? null : (string)dr["ar_name"],
                                    ar_smoking = (bool)dr["ar_smoking"],
                                    ar_timestamp = dr["ar_timestamp"] == DBNull.Value ? null : (byte[])dr["ar_timestamp"],
                                    ar_venue_id = (int)dr["ar_venue_id"],
                                    ar_external_id = dr["ar_external_id"] == DBNull.Value ? null : (string)dr["ar_external_id"],
                                };
                                result.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                funciones.logueo($"Error metodo GetAreasPaginated - {ex.Message}");
                result = new List<areas>();
            }
            return result;
        }
        public long GetTotalAreasForMigration(long lastid)
        {
            long total = 0;

            string query = @"
            select count(*) as total from 
            [dbo].[areas]
where ar_area_id > @lastid
";

            try
            {
                using (SqlConnection conecction = new SqlConnection(_conexion_wgdb_000))
                {
                    conecction.Open();
                    SqlCommand command = new SqlCommand(query, conecction);
                    command.Parameters.AddWithValue("@lastid", lastid);
                    using (SqlDataReader data = command.ExecuteReader())
                    {
                        if (data.Read())
                        {
                            total = ManejoNulos.ManageNullInteger64(data["total"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                funciones.logueo($"Error metodo GetTotalAreasForMigration - {ex.Message}", "Error");
                total = 0;
            }

            return total;
        }
        public int SaveAreas(areas item)
        {
            //bool respuesta = false;
            int IdInsertado = 0;
            string consulta = @"
INSERT INTO [dbo].[areas]
           ([ar_area_id]
           ,[ar_name]
           ,[ar_smoking]
           ,[ar_venue_id]
           ,[ar_external_id])
--output inserted.ar_area_id
     VALUES
           (@ar_area_id
           ,@ar_name
           ,@ar_smoking
           ,@ar_venue_id
           ,@ar_external_id)
                      ";
            try
            {
                using (var con = new SqlConnection(_conexion_wgdb_000_migration))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@ar_area_id", ManejoNulos.ManageNullInteger(item.ar_area_id));
                    query.Parameters.AddWithValue("@ar_name", ManejoNulos.ManageNullStr(item.ar_name));
                    query.Parameters.AddWithValue("@ar_smoking", ManejoNulos.ManegeNullBool(item.ar_smoking));
                    query.Parameters.AddWithValue("@ar_venue_id", ManejoNulos.ManageNullInteger(item.ar_venue_id));
                    query.Parameters.AddWithValue("@ar_external_id", ManejoNulos.ManageNullStr(item.ar_external_id));
                    //query.Parameters.AddWithValue("@ar_timestamp", ManejoNulos.ManageNullByteArray(item.ar_timestamp));
                    //IdInsertado = Convert.ToInt32(query.ExecuteScalar());
                    query.ExecuteNonQuery();
                    IdInsertado = item.ar_area_id;
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
            select top 1 ar_area_id as lastid from 
            [dbo].[areas]
            order by ar_area_id desc
";

            try
            {
                using (SqlConnection conecction = new SqlConnection(_conexion_wgdb_000_migration))
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
