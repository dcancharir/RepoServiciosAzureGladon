using ServicioMigracionWGDB_000.models;
using ServicioMigracionWGDB_000.utilitarios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioMigracionWGDB_000.dal
{
    public class account_movements_dal
    {
        private readonly string _conexion_wgdb_000 = string.Empty;
        private readonly string _conexion_wgdb_000_migration = string.Empty;
        public account_movements_dal()
        {
            _conexion_wgdb_000 = ConfigurationManager.ConnectionStrings["connection_wgdb_000"].ConnectionString;
            _conexion_wgdb_000_migration = ConfigurationManager.ConnectionStrings["connection_wgdb_000_migration"].ConnectionString;
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
                                var item = new account_movements() {
                                    am_movement_id = ManejoNulos.ManageNullInteger64(dr["am_movement_id"]),
                                    am_play_session_id = dr["am_play_session_id"] == DBNull.Value ? null : (long?)dr["am_play_session_id"],
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
                funciones.logueo($"Error metodo GetAccountMovementsPaginated - {ex.Message}");
                result = new List<account_movements>();
            }
            return result;
        }
        public int GetTotalAccountMovementsForMigration(long lastid)
        {
            int total = 0;

            string query = @"
            select count(*) as total from 
            [dbo].[account_movements]
where am_movement_id > @lastid
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
                            total = ManejoNulos.ManageNullInteger(data["total"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                funciones.logueo($"Error metodo GetTotalAccountMovementsForMigration - {ex.Message}");
                total = 0;
            }

            return total;
        }
        public long SaveAccountMovements(account_movements item)
        {
            //bool respuesta = false;
            long IdInsertado = 0;
            string consulta = @"
INSERT INTO [dbo].[account_movements]
           ([am_movement_id]
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
           ,[am_data_after])
--output inserted.am_movement_id
     VALUES
           (@am_movement_id
           ,@am_play_session_id
           ,@am_account_id
           ,@am_terminal_id
           ,@am_wcp_sequence_id
           ,@am_wcp_transaction_id
           ,@am_datetime
           ,@am_type
           ,@am_initial_balance
           ,@am_sub_amount
           ,@am_add_amount
           ,@am_final_balance
           ,@am_cashier_id
           ,@am_cashier_name
           ,@am_terminal_name
           ,@am_operation_id
           ,@am_details
           ,@am_reasons
           ,@am_undo_status
           ,@am_track_data
           ,@am_modified_bucket_reason
           ,@am_data_before
           ,@am_data_after)

                      ";
            try
            {
                using (var con = new SqlConnection(_conexion_wgdb_000_migration))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@am_movement_id", ManejoNulos.ManageNullInteger64(item.am_movement_id));
                    query.Parameters.AddWithValue("@am_play_session_id", ManejoNulos.ManageNullInteger64(item.am_play_session_id));
                    query.Parameters.AddWithValue("@am_account_id", ManejoNulos.ManageNullInteger64(item.am_account_id));
                    query.Parameters.AddWithValue("@am_terminal_id", ManejoNulos.ManageNullInteger(item.am_terminal_id));
                    query.Parameters.AddWithValue("@am_wcp_sequence_id", ManejoNulos.ManageNullInteger64(item.am_wcp_sequence_id));
                    query.Parameters.AddWithValue("@am_wcp_transaction_id", ManejoNulos.ManageNullInteger64(item.am_wcp_transaction_id));
                    query.Parameters.AddWithValue("@am_datetime", ManejoNulos.ManageNullDate(item.am_datetime));
                    query.Parameters.AddWithValue("@am_type", ManejoNulos.ManageNullInteger(item.am_type));
                    query.Parameters.AddWithValue("@am_initial_balance", ManejoNulos.ManageNullDecimal(item.am_initial_balance));
                    query.Parameters.AddWithValue("@am_sub_amount", ManejoNulos.ManageNullDecimal(item.am_sub_amount));
                    query.Parameters.AddWithValue("@am_add_amount", ManejoNulos.ManageNullDecimal(item.am_add_amount));
                    query.Parameters.AddWithValue("@am_final_balance", ManejoNulos.ManageNullDecimal(item.am_final_balance));
                    query.Parameters.AddWithValue("@am_cashier_id", ManejoNulos.ManageNullInteger(item.am_cashier_id));
                    query.Parameters.AddWithValue("@am_cashier_name", ManejoNulos.ManageNullStr(item.am_cashier_name));
                    query.Parameters.AddWithValue("@am_terminal_name", ManejoNulos.ManageNullStr(item.am_terminal_name));
                    query.Parameters.AddWithValue("@am_operation_id", ManejoNulos.ManageNullInteger64(item.am_operation_id));
                    query.Parameters.AddWithValue("@am_details", ManejoNulos.ManageNullStr(item.am_details));
                    query.Parameters.AddWithValue("@am_reasons", ManejoNulos.ManageNullStr(item.am_reasons));
                    query.Parameters.AddWithValue("@am_undo_status", ManejoNulos.ManageNullInteger(item.am_undo_status));
                    query.Parameters.AddWithValue("@am_track_data", ManejoNulos.ManageNullStr(item.am_track_data));
                    query.Parameters.AddWithValue("@am_modified_bucket_reason", ManejoNulos.ManageNullInteger(item.am_modified_bucket_reason));
                    query.Parameters.AddWithValue("@am_data_before", ManejoNulos.ManageNullStr(item.am_data_before));
                    query.Parameters.AddWithValue("@am_data_after", ManejoNulos.ManageNullStr(item.am_data_after));
                    //IdInsertado = Convert.ToInt32(query.ExecuteScalar());
                    query.ExecuteNonQuery();
                    IdInsertado = item.am_movement_id;
                }
            }
            catch (Exception ex)
            {
                funciones.logueo($"Error metodo SaveAccountMovements - {ex.Message}");
                IdInsertado = 0;
            }
            return IdInsertado;
        }
        public long GetLastIdInserted()
        {
            long total = 0;

            string query = @"
            select top 1 am_movement_id as lastid from 
            [dbo].[account_movements]
            order by am_movement_id desc
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
                            total = (long)data["lastid"];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                funciones.logueo($"Error metodo GetLastIdInserted - {ex.Message}");
                total = 0;
            }

            return total;
        }
    }
}
