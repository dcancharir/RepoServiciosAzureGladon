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
    public class banks_dal
    {
        private readonly string _conexion = string.Empty;

        public banks_dal()
        {
            _conexion = ConfigurationManager.ConnectionStrings["connection_wgdb_000"].ConnectionString;
        }
        public List<banks> GetBanksPaginated(long lastid, int skip, int pageSize)
        {
            var result = new List<banks>();
            try
            {
                string query = $@"
SELECT [bk_bank_id]
      ,[bk_area_id]
      ,[bk_name]
      ,[bk_timestamp]
      ,[bk_multiposition]
      ,[bk_external_id]
      ,[bk_shape_code]
      ,[bk_shape_w]
      ,[bk_shape_h]
      ,[bk_play_safe_distance]
  FROM [dbo].[banks]
  where bk_bank_id >= {lastid}
  order by bk_bank_id asc
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
                                var item = new banks()
                                {
                                    bk_bank_id = (int)dr["bk_bank_id"],
                                    bk_area_id = (int)dr["bk_area_id"],
                                    bk_name = dr["bk_name"] == DBNull.Value ? null : (string)dr["bk_name"],
                                    bk_timestamp = dr["bk_timestamp"] == DBNull.Value ? null : (byte[])dr["bk_timestamp"],
                                    bk_multiposition = (bool)dr["bk_multiposition"],
                                    bk_external_id = dr["bk_external_id"] == DBNull.Value ? null : (string)dr["bk_external_id"],
                                    bk_shape_code = dr["bk_shape_code"] == DBNull.Value ? null : (string)dr["bk_shape_code"],
                                    bk_shape_w = dr["bk_shape_w"] == DBNull.Value ? null : (int?)dr["bk_shape_w"],
                                    bk_shape_h = dr["bk_shape_h"] == DBNull.Value ? null : (int?)dr["bk_shape_h"],
                                    bk_play_safe_distance = dr["bk_play_safe_distance"] == DBNull.Value ? null : (int?)dr["bk_play_safe_distance"],
                                };
                                result.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = new List<banks>();
            }
            return result;
        }
        public long GetTotalBanksForMigration(long lastid)
        {
            long total = 0;

            string query = @"
            select count(*) as total from 
            [dbo].[banks]
where bk_bank_id > @lastid
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
                            total = ManejoNulos.ManageNullInteger64(data["total"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                funciones.logueo($"Error metodo GetTotalBanksForMigration - {ex.Message}", "Error");
                total = 0;
            }

            return total;
        }
        public int SaveBanks(banks item)
        {
            //bool respuesta = false;
            int IdInsertado = 0;
            string consulta = @"
INSERT INTO [dbo].[banks]
           ([bk_bank_id]
           ,[bk_area_id]
           ,[bk_name]
           ,[bk_multiposition]
           ,[bk_external_id]
           ,[bk_shape_code]
           ,[bk_shape_w]
           ,[bk_shape_h]
           ,[bk_play_safe_distance])
		   output inserted.bk_bank_id
     VALUES
           (@bk_bank_id
           ,@bk_area_id
           ,@bk_name
           ,@bk_multiposition
           ,@bk_external_id
           ,@bk_shape_code
           ,@bk_shape_w
           ,@bk_shape_h
           ,@bk_play_safe_distance)
                      ";
            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@bk_bank_id", ManejoNulos.ManageNullInteger(item.bk_bank_id));
                    query.Parameters.AddWithValue("@bk_area_id", ManejoNulos.ManageNullInteger(item.bk_area_id));
                    query.Parameters.AddWithValue("@bk_name", ManejoNulos.ManageNullStr(item.bk_name));
                    query.Parameters.AddWithValue("@bk_multiposition", ManejoNulos.ManegeNullBool(item.bk_multiposition));
                    query.Parameters.AddWithValue("@bk_external_id", ManejoNulos.ManageNullStr(item.bk_external_id));
                    query.Parameters.AddWithValue("@bk_shape_code", ManejoNulos.ManageNullStr(item.bk_shape_code));
                    query.Parameters.AddWithValue("@bk_shape_w", ManejoNulos.ManageNullInteger(item.bk_shape_w));
                    query.Parameters.AddWithValue("@bk_shape_h", ManejoNulos.ManageNullInteger(item.bk_shape_h));
                    query.Parameters.AddWithValue("@bk_play_safe_distance", ManejoNulos.ManageNullInteger(item.bk_play_safe_distance));
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
