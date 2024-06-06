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
    public class cashier_sessions_dal
    {
        private readonly string bd_username = string.Empty;
        private readonly string bd_password = string.Empty;
        private readonly string bd_datasource = string.Empty;
        private readonly string connectionString = string.Empty;
        public cashier_sessions_dal(string database_name)
        {
            bd_username = ConfigurationManager.AppSettings["bd_username"];
            bd_password = ConfigurationManager.AppSettings["bd_password"];
            bd_datasource = ConfigurationManager.AppSettings["bd_datasource"];
            connectionString = $"Data Source={bd_datasource};Initial Catalog={database_name};Integrated Security=False;User ID={bd_username};Password={bd_password}";

        }
        public bool SaveCashierSessions(cashier_sessions item)
        {
            //bool respuesta = false;
            long IdInsertado = 0;
            string consulta = @"
if not exists (select cs_session_id from [dbo].[cashier_sessions] where cs_session_id = @cs_session_id)
begin
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
		   --output inserted.cs_session_id
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
end

                      ";
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@cs_session_id", item.cs_session_id == null ? DBNull.Value : (object)item.cs_session_id);
                    query.Parameters.AddWithValue("@cs_name", item.cs_name == null ? DBNull.Value : (object)item.cs_name);
                    query.Parameters.AddWithValue("@cs_cashier_id", item.cs_cashier_id == null ? DBNull.Value : (object)item.cs_cashier_id);
                    query.Parameters.AddWithValue("@cs_user_id", item.cs_user_id == null ? DBNull.Value : (object)item.cs_user_id);
                    query.Parameters.AddWithValue("@cs_opening_date", item.cs_opening_date == null ? DBNull.Value : (object)item.cs_opening_date);
                    query.Parameters.AddWithValue("@cs_closing_date", item.cs_closing_date == null ? DBNull.Value : (object)item.cs_closing_date);
                    query.Parameters.AddWithValue("@cs_status", item.cs_status == null ? DBNull.Value : (object)item.cs_status);
                    query.Parameters.AddWithValue("@cs_balance", item.cs_balance == null ? DBNull.Value : (object)item.cs_balance);
                    query.Parameters.AddWithValue("@cs_other_balance_1", item.cs_other_balance_1 == null ? DBNull.Value : (object)item.cs_other_balance_1);
                    query.Parameters.AddWithValue("@cs_other_balance_2", item.cs_other_balance_2 == null ? DBNull.Value : (object)item.cs_other_balance_2);
                    query.Parameters.AddWithValue("@cs_tax_a_pct", item.cs_tax_a_pct == null ? DBNull.Value : (object)item.cs_tax_a_pct);
                    query.Parameters.AddWithValue("@cs_tax_b_pct", item.cs_tax_b_pct == null ? DBNull.Value : (object)item.cs_tax_b_pct);
                    query.Parameters.AddWithValue("@cs_collected_amount", item.cs_collected_amount == null ? DBNull.Value : (object)item.cs_collected_amount);
                    query.Parameters.AddWithValue("@cs_sales_limit", item.cs_sales_limit == null ? DBNull.Value : (object)item.cs_sales_limit);
                    query.Parameters.AddWithValue("@cs_total_sold", item.cs_total_sold == null ? DBNull.Value : (object)item.cs_total_sold);
                    query.Parameters.AddWithValue("@cs_mb_sales_limit", item.cs_mb_sales_limit == null ? DBNull.Value : (object)item.cs_mb_sales_limit);
                    query.Parameters.AddWithValue("@cs_mb_total_sold", item.cs_mb_total_sold == null ? DBNull.Value : (object)item.cs_mb_total_sold);
                    query.Parameters.AddWithValue("@cs_session_by_terminal", item.cs_session_by_terminal == null ? DBNull.Value : (object)item.cs_session_by_terminal);
                    query.Parameters.AddWithValue("@cs_history", item.cs_history == null ? DBNull.Value : (object)item.cs_history);
                    query.Parameters.AddWithValue("@cs_gaming_day", item.cs_gaming_day == null ? DBNull.Value : (object)item.cs_gaming_day);
                    query.Parameters.AddWithValue("@cs_short_over_history", item.cs_short_over_history == null ? DBNull.Value : (object)item.cs_short_over_history);
                    query.Parameters.AddWithValue("@cs_has_pinpad_operations", item.cs_has_pinpad_operations == null ? DBNull.Value : (object)item.cs_has_pinpad_operations);
                    query.Parameters.AddWithValue("@cs_venue_id", item.cs_venue_id == null ? DBNull.Value : (object)item.cs_venue_id);
                    query.Parameters.AddWithValue("@cs_is_session_collector", item.cs_is_session_collector == null ? DBNull.Value : (object)item.cs_is_session_collector);
                    //IdInsertado = Convert.ToInt32(query.ExecuteScalar());
                    query.ExecuteNonQuery();
                    //IdInsertado = item.cs_session_id;
                    return true;
                }
            }
            catch (Exception ex)
            {
                funciones.logueo($"Error metodo SaveCashierSessions - {ex.Message}");
            }
            return false;
        }
        public long GetLastIdInserted()
        {
            long total = 0;

            string query = @"
            select top 1 cs_session_id as lastid from 
            [dbo].[cashier_sessions]
            order by cs_session_id desc
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
                            total = ManejoNulos.ManageNullInteger64(data["lastid"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                funciones.logueo($"Error metodo GetLastIdInserted cashier_sessions_dal.cs- {ex.Message}", "Error");
                total = 0;
            }

            return total;
        }
    }
}
