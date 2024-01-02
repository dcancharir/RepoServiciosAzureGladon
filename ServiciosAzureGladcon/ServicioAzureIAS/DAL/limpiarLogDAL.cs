using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioAzureIAS.DAL {
    public class limpiarLogDAL {
        private string _conexion = string.Empty;
        public limpiarLogDAL() {
            _conexion = ConfigurationManager.ConnectionStrings["conectionBdSeguridad"].ConnectionString;
        }

        public bool LimpiarLogDBSeguridadPJ() {
            string consulta = @" USE BD_SEGURIDAD_PJ;
                            ALTER DATABASE BD_SEGURIDAD_PJ SET RECOVERY SIMPLE;
                            DBCC SHRINKFILE (BD_SEGURIDAD_PJ_log, 1);
                            ALTER DATABASE BD_SEGURIDAD_PJ SET RECOVERY FULL";
            try {
                using(var con = new SqlConnection(_conexion)) {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.ExecuteNonQuery();  
                }
            } catch(Exception ex) {
                //Console.WriteLine(ex.ToString());
                return false;
            }

            return true;
        }

        public class FileDetail {
            public string Name { get; set; }
            public double SizeInMB { get; set; }
        }

        public List<FileDetail> ObtenerPesoBaseDeDatos() {
            List<FileDetail> fileDetails = new List<FileDetail>();

            string consulta = @"
        SELECT name, size * 8 / 1024 AS [Tamaño en MB]
        FROM sys.master_files
        WHERE database_id = DB_ID('BD_SEGURIDAD_PJ')";

            try {
                using(var con = new SqlConnection(_conexion)) {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    using(SqlDataReader reader = query.ExecuteReader()) {
                        while(reader.Read()) {
                            FileDetail detail = new FileDetail {
                                Name = reader["name"].ToString(),
                                SizeInMB = Convert.ToDouble(reader["Tamaño en MB"])
                            };
                            fileDetails.Add(detail);
                        }
                    }
                }
            } catch(Exception ex) {
                // Console.WriteLine(ex.ToString());
            }

            return fileDetails;
        }

    }
}
