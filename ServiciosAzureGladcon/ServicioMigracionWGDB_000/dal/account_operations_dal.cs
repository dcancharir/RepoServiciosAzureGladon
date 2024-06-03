using ServicioMigracionWGDB_000.models;
using ServicioMigracionWGDB_000.utilitarios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
namespace ServicioMigracionWGDB_000.dal
{
    public class account_operations_dal
    {
        private readonly string _conexion = string.Empty;

        public account_operations_dal()
        {
            _conexion = ConfigurationManager.ConnectionStrings["connection_wgdb_000"].ConnectionString;
        }
        public List<account_operations> GetAccountOperationsPaginated(long lastid, int skip, int pageSize)
        {
            var result = new List<account_operations>();
            try
            {
                string query = $@"
SELECT [ao_operation_id]
      ,[ao_datetime]
      ,[ao_code]
      ,[ao_account_id]
      ,[ao_cashier_session_id]
      ,[ao_mb_account_id]
      ,[ao_promo_id]
      ,[ao_amount]
      ,[ao_non_redeemable]
      ,[ao_won_lock]
      ,[ao_operation_data]
      ,[ao_automatically_printed]
      ,[ao_non_redeemable2]
      ,[ao_redeemable]
      ,[ao_promo_redeemable]
      ,[ao_promo_not_redeemable]
      ,[ao_user_id]
      ,[ao_undo_status]
      ,[ao_undo_operation_id]
      ,[ao_reason_id]
      ,[ao_comment]
      ,[ao_comment_handpay]
      ,[ao_venue_id]
      ,[ao_spent_used]
      ,[ao_spent_remaining]
  FROM [dbo].[account_operations]
  where ao_operation_id > {lastid}
  order by ao_operation_id asc
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
                                var item = new account_operations()
                                {
                                    ao_operation_id = ManejoNulos.ManageNullInteger64(dr["ao_operation_id"]),
                                    ao_datetime = ManejoNulos.ManageNullDate(dr["ao_datetime"]),
                                    ao_code = ManejoNulos.ManageNullInteger(dr["ao_code"]),
                                    ao_account_id = ManejoNulos.ManageNullInteger64(dr["ao_account_id"]),
                                    ao_cashier_session_id = ManejoNulos.ManageNullInteger64(dr["ao_cashier_session_id"]),
                                    ao_mb_account_id = ManejoNulos.ManageNullInteger64(dr["ao_mb_account_id"]),
                                    ao_promo_id = ManejoNulos.ManageNullInteger64(dr["ao_promo_id"]),
                                    ao_amount = ManejoNulos.ManageNullDecimal(dr["ao_amount"]),
                                    ao_non_redeemable = ManejoNulos.ManageNullDecimal(dr["ao_non_redeemable"]),
                                    ao_won_lock = ManejoNulos.ManageNullDecimal(dr["ao_won_lock"]),
                                    ao_operation_data = ManejoNulos.ManageNullInteger64(dr["ao_operation_data"]),
                                    ao_automatically_printed = ManejoNulos.ManegeNullBool(dr["ao_automatically_printed"]),
                                    ao_non_redeemable2 = ManejoNulos.ManageNullDecimal(dr["ao_non_redeemable2"]),
                                    ao_redeemable = ManejoNulos.ManageNullDecimal(dr["ao_redeemable"]),
                                    ao_promo_redeemable = ManejoNulos.ManageNullDecimal(dr["ao_promo_redeemable"]),
                                    ao_promo_not_redeemable = ManejoNulos.ManageNullDecimal(dr["ao_promo_not_redeemable"]),
                                    ao_user_id = ManejoNulos.ManageNullInteger64(dr["ao_user_id"]),
                                    ao_undo_status = ManejoNulos.ManageNullInteger(dr["ao_undo_status"]),
                                    ao_undo_operation_id = ManejoNulos.ManageNullInteger64(dr["ao_undo_operation_id"]),
                                    ao_reason_id = ManejoNulos.ManageNullInteger64(dr["ao_reason_id"]),
                                    ao_comment = ManejoNulos.ManageNullStr(dr["ao_comment"]),
                                    ao_comment_handpay = ManejoNulos.ManageNullStr(dr["ao_comment_handpay"]),
                                    ao_venue_id = ManejoNulos.ManageNullInteger(dr["ao_venue_id"]),
                                    ao_spent_used = ManejoNulos.ManageNullDecimal(dr["ao_spent_used"]),
                                    ao_spent_remaining = ManejoNulos.ManageNullDecimal(dr["ao_spent_remaining"]),
                                };
                                result.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = new List<account_operations>();
            }
            return result;
        }
        public int GetTotalAccountOperationsForMigration(long lastid)
        {
            int total = 0;

            string query = @"
            select count(*) as total from 
            [dbo].[account_operations]
where ao_operation_id > @lastid
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
        public int SaveAccountOperations(account_operations item)
        {
            //bool respuesta = false;
            int IdInsertado = 0;
            string consulta = @"
INSERT INTO [dbo].[account_operations]
           ([ao_operation_id]
           ,[ao_datetime]
           ,[ao_code]
           ,[ao_account_id]
           ,[ao_cashier_session_id]
           ,[ao_mb_account_id]
           ,[ao_promo_id]
           ,[ao_amount]
           ,[ao_non_redeemable]
           ,[ao_won_lock]
           ,[ao_operation_data]
           ,[ao_automatically_printed]
           ,[ao_non_redeemable2]
           ,[ao_redeemable]
           ,[ao_promo_redeemable]
           ,[ao_promo_not_redeemable]
           ,[ao_user_id]
           ,[ao_undo_status]
           ,[ao_undo_operation_id]
           ,[ao_reason_id]
           ,[ao_comment]
           ,[ao_comment_handpay]
           ,[ao_venue_id]
           ,[ao_spent_used])
output inserted.ao_operation_id
     VALUES
           (@ao_operation_id
           ,@ao_datetime
           ,@ao_code
           ,@ao_account_id
           ,@ao_cashier_session_id
           ,@ao_mb_account_id
           ,@ao_promo_id
           ,@ao_amount
           ,@ao_non_redeemable
           ,@ao_won_lock
           ,@ao_operation_data
           ,@ao_automatically_printed
           ,@ao_non_redeemable2
           ,@ao_redeemable
           ,@ao_promo_redeemable
           ,@ao_promo_not_redeemable
           ,@ao_user_id
           ,@ao_undo_status
           ,@ao_undo_operation_id
           ,@ao_reason_id
           ,@ao_comment
           ,@ao_comment_handpay
           ,@ao_venue_id
           ,@ao_spent_used)
                      ";
            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@ao_operation_id", ManejoNulos.ManageNullInteger64(item.ao_operation_id));
                    query.Parameters.AddWithValue("@ao_datetime", ManejoNulos.ManageNullDate(item.ao_datetime));
                    query.Parameters.AddWithValue("@ao_code", ManejoNulos.ManageNullInteger(item.ao_code));
                    query.Parameters.AddWithValue("@ao_account_id", ManejoNulos.ManageNullInteger64(item.ao_account_id));
                    query.Parameters.AddWithValue("@ao_cashier_session_id", ManejoNulos.ManageNullInteger64(item.ao_cashier_session_id));
                    query.Parameters.AddWithValue("@ao_mb_account_id", ManejoNulos.ManageNullInteger64(item.ao_mb_account_id));
                    query.Parameters.AddWithValue("@ao_promo_id", ManejoNulos.ManageNullInteger64(item.ao_promo_id));
                    query.Parameters.AddWithValue("@ao_amount", ManejoNulos.ManageNullDecimal(item.ao_amount));
                    query.Parameters.AddWithValue("@ao_non_redeemable", ManejoNulos.ManageNullDecimal(item.ao_non_redeemable));
                    query.Parameters.AddWithValue("@ao_won_lock", ManejoNulos.ManageNullDecimal(item.ao_won_lock));
                    query.Parameters.AddWithValue("@ao_operation_data", ManejoNulos.ManageNullInteger64(item.ao_operation_data));
                    query.Parameters.AddWithValue("@ao_automatically_printed", ManejoNulos.ManegeNullBool(item.ao_automatically_printed));
                    query.Parameters.AddWithValue("@ao_non_redeemable2", ManejoNulos.ManageNullDecimal(item.ao_non_redeemable2));
                    query.Parameters.AddWithValue("@ao_redeemable", ManejoNulos.ManageNullDecimal(item.ao_redeemable));
                    query.Parameters.AddWithValue("@ao_promo_redeemable", ManejoNulos.ManageNullDecimal(item.ao_promo_redeemable));
                    query.Parameters.AddWithValue("@ao_promo_not_redeemable", ManejoNulos.ManageNullDecimal(item.ao_promo_not_redeemable));
                    query.Parameters.AddWithValue("@ao_user_id", ManejoNulos.ManageNullInteger64(item.ao_user_id));
                    query.Parameters.AddWithValue("@ao_undo_status", ManejoNulos.ManageNullInteger(item.ao_undo_status));
                    query.Parameters.AddWithValue("@ao_undo_operation_id", ManejoNulos.ManageNullInteger64(item.ao_undo_operation_id));
                    query.Parameters.AddWithValue("@ao_reason_id", ManejoNulos.ManageNullInteger64(item.ao_reason_id));
                    query.Parameters.AddWithValue("@ao_comment", ManejoNulos.ManageNullStr(item.ao_comment));
                    query.Parameters.AddWithValue("@ao_comment_handpay", ManejoNulos.ManageNullStr(item.ao_comment_handpay));
                    query.Parameters.AddWithValue("@ao_venue_id", ManejoNulos.ManageNullInteger(item.ao_venue_id));
                    query.Parameters.AddWithValue("@ao_spent_used", ManejoNulos.ManageNullDecimal(item.ao_spent_used));
                    query.Parameters.AddWithValue("@ao_spent_remaining", ManejoNulos.ManageNullDecimal(item.ao_spent_remaining));
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
