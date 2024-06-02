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
    internal class account_movements_dal
    {
        private readonly string _conexion = string.Empty;

        public account_movements_dal()
        {
            _conexion = ConfigurationManager.ConnectionStrings["connection_wgdb_000"].ConnectionString;
        }
        public List<account_movements> GetAccountMovementsPaginated(long lastid, int skip, int pageSize)
        {
            var result = new List<account_movements>();
            try
            {
                string query = $@"
SELECT [am_movement_id]
      ,[am_play_session_id]
      ,[am_account_id]
      ,[am_terminal_id]
      ,[am_wcp_sequence_id]
      ,[am_wcp_transaction_id]
      ,[am_datetime]
      ,[am_type]
      ,[am_initial_balance]
      ,[am_sub_amount]
      ,[am_add_amount]
      ,[am_final_balance]
      ,[am_cashier_id]
      ,[am_cashier_name]
      ,[am_terminal_name]
      ,[am_operation_id]
      ,[am_details]
      ,[am_reasons]
      ,[am_undo_status]
      ,[am_track_data]
      ,[am_modified_bucket_reason]
      ,[am_data_before]
      ,[am_data_after]
  FROM [dbo].[account_movements]
  where am_movement_id > {lastid}
  order by am_movement_id asc
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
                                var item = new account_movements() {
                                    am_movement_id = ManejoNulos.ManageNullInteger64(dr["am_movement_id"]),
                                    am_play_session_id = ManejoNulos.ManageNullInteger64(dr["am_play_session_id"]),
                                    am_account_id = ManejoNulos.ManageNullInteger64(dr["am_account_id"]),
                                    am_terminal_id = ManejoNulos.ManageNullInteger(dr["am_terminal_id"]),
                                    am_wcp_sequence_id = ManejoNulos.ManageNullInteger64(dr["am_wcp_sequence_id"]),
                                    am_wcp_transaction_id = ManejoNulos.ManageNullInteger64(dr["am_wcp_transaction_id"]),
                                    am_datetime = ManejoNulos.ManageNullDate(dr["am_datetime"]),
                                    am_type = ManejoNulos.ManageNullInteger(dr["am_type"]),
                                    am_initial_balance = ManejoNulos.ManageNullDecimal(dr["am_initial_balance"]),
                                    am_sub_amount = ManejoNulos.ManageNullDecimal(dr["am_sub_amount"]),
                                    am_add_amount = ManejoNulos.ManageNullDecimal(dr["am_add_amount"]),
                                    am_final_balance = ManejoNulos.ManageNullDecimal(dr["am_final_balance"]),
                                    am_cashier_id = ManejoNulos.ManageNullInteger(dr["am_cashier_id"]),
                                    am_cashier_name = ManejoNulos.ManageNullStr(dr["am_cashier_name"]),
                                    am_terminal_name = ManejoNulos.ManageNullStr(dr["am_terminal_name"]),
                                    am_operation_id = ManejoNulos.ManageNullInteger64(dr["am_operation_id"]),
                                    am_details = ManejoNulos.ManageNullStr(dr["am_details"]),
                                    am_reasons = ManejoNulos.ManageNullStr(dr["am_reasons"]),
                                    am_undo_status = ManejoNulos.ManageNullInteger(dr["am_undo_status"]),
                                    am_track_data = ManejoNulos.ManageNullStr(dr["am_track_data"]),
                                    am_modified_bucket_reason = ManejoNulos.ManageNullInteger(dr["am_modified_bucket_reason"]),
                                    am_data_before = ManejoNulos.ManageNullStr(dr["am_data_before"]),
                                    am_data_after = ManejoNulos.ManageNullStr(dr["am_data_after"]),
                                };
                                result.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = new List<account_movements>();
            }
            return result;
        }
        public int GetTotalAccountMovementssForMigration(long lastid)
        {
            int total = 0;

            string query = @"
            select count(*) as total from 
            [dbo].[account_movements]
where am_movement_id > @lastid
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
    }
}
