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
    public class general_params_dal
    {
        private readonly string _conexion = string.Empty;

        public general_params_dal()
        {
            _conexion = ConfigurationManager.ConnectionStrings["connection_wgdb_000"].ConnectionString;
        }
        public List<general_params> GetAllGeneralParams()
        {
            var result = new List<general_params>();
            try
            {
                string query = $@"
SELECT [gp_group_key]
      ,[gp_subject_key]
      ,[gp_key_value]
      ,[gp_description]
  FROM [dbo].[general_params]
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
                                var item = new general_params()
                                {
                                    gp_group_key = dr["gp_group_key"] == DBNull.Value ? null : (string)(dr["gp_group_key"]),
                                    gp_subject_key = dr["gp_subject_key"] == DBNull.Value ? null : (string)(dr["gp_subject_key"]),
                                    gp_key_value = dr["gp_key_value"] == DBNull.Value ? null : (string)(dr["gp_key_value"]),
                                    gp_description = dr["gp_description"] == DBNull.Value ? null : (string)(dr["gp_description"]),
                                };
                                result.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = new List<general_params>();
            }
            return result;
        }
        public long GetTotalGeneralParamsForMigration()
        {
            long total = 0;

            string query = @"
            select count(*) as total from 
            [dbo].[general_params]
";

            try
            {
                using (SqlConnection conecction = new SqlConnection(_conexion))
                {
                    conecction.Open();
                    SqlCommand command = new SqlCommand(query, conecction);
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
                funciones.logueo($"Error metodo GetTotalGeneralParamsForMigration - {ex.Message}", "Error");
                total = 0;
            }

            return total;
        }
    }
}
