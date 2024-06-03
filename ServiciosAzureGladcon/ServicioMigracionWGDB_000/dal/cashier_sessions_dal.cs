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
    public class cashier_sessions_dal
    {
        private readonly string _conexion = string.Empty;

        public cashier_sessions_dal()
        {
            _conexion = ConfigurationManager.ConnectionStrings["connection_wgdb_000"].ConnectionString;
        }
        public List<cashier_sessions> GetAreasPaginated(long lastid, int skip, int pageSize)
        {
            var result = new List<cashier_sessions>();
            try
            {
                string query = $@"
SELECT [cs_session_id]
      ,[cs_name]
      ,[cs_cashier_id]
      ,[cs_user_id]
      ,[cs_opening_date]
      ,[cs_closing_date]
      ,[cs_status]
      ,[cs_balance]
      ,[cs_other_balance_1]
      ,[cs_other_balance_2]
      ,[cs_tax_a_pct]
      ,[cs_tax_b_pct]
      ,[cs_collected_amount]
      ,[cs_sales_limit]
      ,[cs_total_sold]
      ,[cs_mb_sales_limit]
      ,[cs_mb_total_sold]
      ,[cs_session_by_terminal]
      ,[cs_history]
      ,[cs_gaming_day]
      ,[cs_short_over_history]
      ,[cs_has_pinpad_operations]
      ,[cs_venue_id]
      ,[cs_is_session_collector]
  FROM [dbo].[cashier_sessions]
  where cs_session_id > {lastid}
  order by cs_session_id asc
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
                                var item = new cashier_sessions()
                                {
                                    cs_session_id = ManejoNulos.ManageNullInteger64(dr["cs_session_id"]),
                                    cs_name = ManejoNulos.ManageNullStr(dr["cs_name"]),
                                    cs_cashier_id = ManejoNulos.ManageNullInteger(dr["cs_cashier_id"]),
                                    cs_user_id = ManejoNulos.ManageNullInteger(dr["cs_user_id"]),
                                    cs_opening_date = ManejoNulos.ManageNullDate(dr["cs_opening_date"]),
                                    cs_closing_date = ManejoNulos.ManageNullDate(dr["cs_closing_date"]),
                                    cs_status = ManejoNulos.ManageNullInteger(dr["cs_status"]),
                                    cs_balance = ManejoNulos.ManageNullDecimal(dr["cs_balance"]),
                                    cs_other_balance_1 = ManejoNulos.ManageNullDecimal(dr["cs_other_balance_1"]),
                                    cs_other_balance_2 = ManejoNulos.ManageNullDecimal(dr["cs_other_balance_2"]),
                                    cs_tax_a_pct = ManejoNulos.ManageNullDecimal(dr["cs_tax_a_pct"]),
                                    cs_tax_b_pct = ManejoNulos.ManageNullDecimal(dr["cs_tax_b_pct"]),
                                    cs_collected_amount = ManejoNulos.ManageNullDecimal(dr["cs_collected_amount"]),
                                    cs_sales_limit = ManejoNulos.ManageNullDecimal(dr["cs_sales_limit"]),
                                    cs_total_sold = ManejoNulos.ManageNullDecimal(dr["cs_total_sold"]),
                                    cs_mb_sales_limit = ManejoNulos.ManageNullDecimal(dr["cs_mb_sales_limit"]),
                                    cs_mb_total_sold = ManejoNulos.ManageNullDecimal(dr["cs_mb_total_sold"]),
                                    cs_session_by_terminal = ManejoNulos.ManegeNullBool(dr["cs_session_by_terminal"]),
                                    cs_history = ManejoNulos.ManegeNullBool(dr["cs_history"]),
                                    cs_gaming_day = ManejoNulos.ManageNullDate(dr["cs_gaming_day"]),
                                    cs_short_over_history = ManejoNulos.ManegeNullBool(dr["cs_short_over_history"]),
                                    cs_has_pinpad_operations = ManejoNulos.ManageNullInteger64(dr["cs_has_pinpad_operations"]),
                                    cs_venue_id = ManejoNulos.ManageNullInteger(dr["cs_venue_id"]),
                                    cs_is_session_collector = ManejoNulos.ManegeNullBool(dr["cs_is_session_collector"]),
                                };
                                result.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = new List<cashier_sessions>();
            }
            return result;
        }
        public int GetTotalCashierSessionsForMigration(long lastid)
        {
            int total = 0;

            string query = @"
            select count(*) as total from 
            [dbo].[cashier_sessions]
where cs_session_id > @lastid
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
        public int SaveCashierSessions(cashier_sessions item)
        {
            //bool respuesta = false;
            int IdInsertado = 0;
            string consulta = @"
INSERT INTO [dbo].[cashier_sessions]
           ([cs_session_id]
           ,[cs_name]
           ,[cs_cashier_id]
           ,[cs_user_id]
           ,[cs_opening_date]
           ,[cs_closing_date]
           ,[cs_status]
           ,[cs_balance]
           ,[cs_other_balance_1]
           ,[cs_other_balance_2]
           ,[cs_tax_a_pct]
           ,[cs_tax_b_pct]
           ,[cs_collected_amount]
           ,[cs_sales_limit]
           ,[cs_total_sold]
           ,[cs_mb_sales_limit]
           ,[cs_mb_total_sold]
           ,[cs_session_by_terminal]
           ,[cs_history]
           ,[cs_gaming_day]
           ,[cs_short_over_history]
           ,[cs_has_pinpad_operations]
           ,[cs_venue_id]
           ,[cs_is_session_collector])
		   output inserted.cs_session_id
     VALUES
           (@cs_session_id
           ,@cs_name
           ,@cs_cashier_id
           ,@cs_user_id
           ,@cs_opening_date
           ,@cs_closing_date
           ,@cs_status
           ,@cs_balance
           ,@cs_other_balance_1
           ,@cs_other_balance_2
           ,@cs_tax_a_pct
           ,@cs_tax_b_pct
           ,@cs_collected_amount
           ,@cs_sales_limit
           ,@cs_total_sold
           ,@cs_mb_sales_limit
           ,@cs_mb_total_sold
           ,@cs_session_by_terminal
           ,@cs_history
           ,@cs_gaming_day
           ,@cs_short_over_history
           ,@cs_has_pinpad_operations
           ,@cs_venue_id
           ,@cs_is_session_collector)
                      ";
            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@cs_session_id", ManejoNulos.ManageNullInteger64(item.cs_session_id));
                    query.Parameters.AddWithValue("@cs_name", ManejoNulos.ManageNullStr(item.cs_name));
                    query.Parameters.AddWithValue("@cs_cashier_id", ManejoNulos.ManageNullInteger(item.cs_cashier_id));
                    query.Parameters.AddWithValue("@cs_user_id", ManejoNulos.ManageNullInteger(item.cs_user_id));
                    query.Parameters.AddWithValue("@cs_opening_date", ManejoNulos.ManageNullDate(item.cs_opening_date));
                    query.Parameters.AddWithValue("@cs_closing_date", ManejoNulos.ManageNullDate(item.cs_closing_date));
                    query.Parameters.AddWithValue("@cs_status", ManejoNulos.ManageNullInteger(item.cs_status));
                    query.Parameters.AddWithValue("@cs_balance", ManejoNulos.ManageNullDecimal(item.cs_balance));
                    query.Parameters.AddWithValue("@cs_other_balance_1", ManejoNulos.ManageNullDecimal(item.cs_other_balance_1));
                    query.Parameters.AddWithValue("@cs_other_balance_2", ManejoNulos.ManageNullDecimal(item.cs_other_balance_2));
                    query.Parameters.AddWithValue("@cs_tax_a_pct", ManejoNulos.ManageNullDecimal(item.cs_tax_a_pct));
                    query.Parameters.AddWithValue("@cs_tax_b_pct", ManejoNulos.ManageNullDecimal(item.cs_tax_b_pct));
                    query.Parameters.AddWithValue("@cs_collected_amount", ManejoNulos.ManageNullDecimal(item.cs_collected_amount));
                    query.Parameters.AddWithValue("@cs_sales_limit", ManejoNulos.ManageNullDecimal(item.cs_sales_limit));
                    query.Parameters.AddWithValue("@cs_total_sold", ManejoNulos.ManageNullDecimal(item.cs_total_sold));
                    query.Parameters.AddWithValue("@cs_mb_sales_limit", ManejoNulos.ManageNullDecimal(item.cs_mb_sales_limit));
                    query.Parameters.AddWithValue("@cs_mb_total_sold", ManejoNulos.ManageNullDecimal(item.cs_mb_total_sold));
                    query.Parameters.AddWithValue("@cs_session_by_terminal", ManejoNulos.ManegeNullBool(item.cs_session_by_terminal));
                    query.Parameters.AddWithValue("@cs_history", ManejoNulos.ManegeNullBool(item.cs_history));
                    query.Parameters.AddWithValue("@cs_gaming_day", ManejoNulos.ManageNullDate(item.cs_gaming_day));
                    query.Parameters.AddWithValue("@cs_short_over_history", ManejoNulos.ManegeNullBool(item.cs_short_over_history));
                    query.Parameters.AddWithValue("@cs_has_pinpad_operations", ManejoNulos.ManageNullInteger64(item.cs_has_pinpad_operations));
                    query.Parameters.AddWithValue("@cs_venue_id", ManejoNulos.ManageNullInteger(item.cs_venue_id));
                    query.Parameters.AddWithValue("@cs_is_session_collector", ManejoNulos.ManegeNullBool(item.cs_is_session_collector));
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
