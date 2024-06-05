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
    public class gift_instances_dal
    {
        private readonly string _conexion = string.Empty;

        public gift_instances_dal()
        {
            _conexion = ConfigurationManager.ConnectionStrings["connection_wgdb_000"].ConnectionString;
        }
        public List<gift_instances> GetGiftInstancesPaginated(long lastid, int skip, int pageSize)
        {
            var result = new List<gift_instances>();
            try
            {
                string query = $@"
SELECT [gin_gift_instance_id]
      ,[gin_oper_request_id]
      ,[gin_oper_delivery_id]
      ,[gin_account_id]
      ,[gin_gift_id]
      ,[gin_gift_name]
      ,[gin_gift_type]
      ,[gin_points]
      ,[gin_conversion_to_nrc]
      ,[gin_spent_points]
      ,[gin_requested]
      ,[gin_delivered]
      ,[gin_expiration]
      ,[gin_request_status]
      ,[gin_num_items]
      ,[gin_data_01]
      ,[gin_notification]
      ,[gin_row_version]
  FROM [dbo].[gift_instances]
  where gin_gift_instance_id > {lastid}
  order by gin_gift_instance_id asc
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
                                var item = new gift_instances()
                                {
                                    gin_gift_instance_id = (long)dr["gin_gift_instance_id"],
                                    gin_oper_request_id = (long)dr["gin_oper_request_id"],
                                    gin_oper_delivery_id = dr["gin_oper_delivery_id"] == DBNull.Value ? null : (long?)dr["gin_oper_delivery_id"],
                                    gin_account_id = (long)dr["gin_account_id"],
                                    gin_gift_id = (long)dr["gin_gift_id"],
                                    gin_gift_name = dr["gin_gift_name"] == DBNull.Value ? null : (string)dr["gin_gift_name"],
                                    gin_gift_type = (int)dr["gin_gift_type"],
                                    gin_points = (decimal)dr["gin_points"],
                                    gin_conversion_to_nrc = dr["gin_conversion_to_nrc"] == DBNull.Value ? null : (decimal?)dr["gin_conversion_to_nrc"],
                                    gin_spent_points = (decimal)dr["gin_spent_points"],
                                    gin_requested = (DateTime)dr["gin_requested"],
                                    gin_delivered = dr["gin_delivered"] == DBNull.Value ? null : (DateTime?)dr["gin_delivered"],
                                    gin_expiration = (DateTime)dr["gin_expiration"],
                                    gin_request_status = (int)dr["gin_request_status"],
                                    gin_num_items = (int)dr["gin_num_items"],
                                    gin_data_01 = dr["gin_data_01"] == DBNull.Value ? null : (long?)dr["gin_data_01"],
                                    gin_notification = dr["gin_notification"] == DBNull.Value ? null : (int?)dr["gin_notification"],
                                    gin_row_version = dr["gin_row_version"] == DBNull.Value ? null : (byte[])dr["gin_row_version"],
                                };
                                result.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = new List<gift_instances>();
            }
            return result;
        }
        public int GetTotalGiftInstancesForMigration(long lastid)
        {
            int total = 0;

            string query = @"
            select count(*) as total from 
            [dbo].[gift_instances]
where gin_gift_instance_id > @lastid
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
        public int SaveGistInstances(gift_instances item)
        {
            //bool respuesta = false;
            int IdInsertado = 0;
            string consulta = @"
INSERT INTO [dbo].[gift_instances]
           ([gin_gift_instance_id]
           ,[gin_oper_request_id]
           ,[gin_oper_delivery_id]
           ,[gin_account_id]
           ,[gin_gift_id]
           ,[gin_gift_name]
           ,[gin_gift_type]
           ,[gin_points]
           ,[gin_conversion_to_nrc]
           ,[gin_spent_points]
           ,[gin_requested]
           ,[gin_delivered]
           ,[gin_expiration]
           ,[gin_request_status]
           ,[gin_num_items]
           ,[gin_data_01]
           ,[gin_notification])
output inserted.gin_gift_instance_id
     VALUES
           (@gin_gift_instance_id
           ,@gin_oper_request_id
           ,@gin_oper_delivery_id
           ,@gin_account_id
           ,@gin_gift_id
           ,@gin_gift_name
           ,@gin_gift_type
           ,@gin_points
           ,@gin_conversion_to_nrc
           ,@gin_spent_points
           ,@gin_requested
           ,@gin_delivered
           ,@gin_expiration
           ,@gin_request_status
           ,@gin_num_items
           ,@gin_data_01
           ,@gin_notification)
                      ";
            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@gin_gift_instance_id", ManejoNulos.ManageNullInteger64(item.gin_gift_instance_id));
                    query.Parameters.AddWithValue("@gin_oper_request_id", ManejoNulos.ManageNullInteger64(item.gin_oper_request_id));
                    query.Parameters.AddWithValue("@gin_oper_delivery_id", ManejoNulos.ManageNullInteger64(item.gin_oper_delivery_id));
                    query.Parameters.AddWithValue("@gin_account_id", ManejoNulos.ManageNullInteger64(item.gin_account_id));
                    query.Parameters.AddWithValue("@gin_gift_id", ManejoNulos.ManageNullInteger64(item.gin_gift_id));
                    query.Parameters.AddWithValue("@gin_gift_name", ManejoNulos.ManageNullStr(item.gin_gift_name));
                    query.Parameters.AddWithValue("@gin_gift_type", ManejoNulos.ManageNullInteger(item.gin_gift_type));
                    query.Parameters.AddWithValue("@gin_points", ManejoNulos.ManageNullDecimal(item.gin_points));
                    query.Parameters.AddWithValue("@gin_conversion_to_nrc", ManejoNulos.ManageNullDecimal(item.gin_conversion_to_nrc));
                    query.Parameters.AddWithValue("@gin_spent_points", ManejoNulos.ManageNullDecimal(item.gin_spent_points));
                    query.Parameters.AddWithValue("@gin_requested", ManejoNulos.ManageNullDate(item.gin_requested));
                    query.Parameters.AddWithValue("@gin_delivered", ManejoNulos.ManageNullDate(item.gin_delivered));
                    query.Parameters.AddWithValue("@gin_expiration", ManejoNulos.ManageNullDate(item.gin_expiration));
                    query.Parameters.AddWithValue("@gin_request_status", ManejoNulos.ManageNullInteger(item.gin_request_status));
                    query.Parameters.AddWithValue("@gin_num_items", ManejoNulos.ManageNullInteger(item.gin_num_items));
                    query.Parameters.AddWithValue("@gin_data_01", ManejoNulos.ManageNullInteger64(item.gin_data_01));
                    query.Parameters.AddWithValue("@gin_notification", ManejoNulos.ManageNullInteger(item.gin_notification));
                    query.Parameters.AddWithValue("@gin_row_version", ManejoNulos.ManageNullInteger64(item.gin_row_version));
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
