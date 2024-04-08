using ServicioAzureIAS.utilitarios;
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
        public bool LimpiarLogDBRegadis() {
            string consulta = @" USE BD_GESASISv2;
                            ALTER DATABASE BD_GESASISv2 SET RECOVERY SIMPLE;
                            DBCC SHRINKFILE (BD_GESASISv2_log, 1);
                            ALTER DATABASE BD_GESASISv2 SET RECOVERY FULL";
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
        public class EspacioDiscoBD {

            public int Id { get; set; }
            public string NombreBD { get; set; }
            public string EspacioBD { get; set; }
            public string NombreLog { get; set; }
            public string EspacioLog { get; set; }
            public DateTime FechaCreacion { get; set; }
        }

        public List<EspacioDiscoBD> ListadoBDsAzure() {
            List<EspacioDiscoBD> lista = new List<EspacioDiscoBD>();
            string consulta = @"
                                SELECT sdt.database_id as Id, 
		                        sdt.name as NombreBD, 
		                        (SELECT size * 8 / 1024 FROM sys.master_files WHERE database_id>4 AND physical_name LIKE '%.mdf' AND database_id=sdt.database_id) as EspacioBD,
		                        smf.name as NombreLog, 
		                        smf.size * 8 / 1024 AS EspacioLog, 
		                        SDT.create_date as FechaCreacion
                                FROM sys.databases sdt
		                        INNER JOIN sys.master_files smf ON sdt.database_id=smf.database_id
		                        WHERE sdt.database_id>4 AND smf.name LIKE '%log'
                                ";
            try {
                using(var con = new SqlConnection(_conexion)) {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    using(var dr = query.ExecuteReader()) {
                        while(dr.Read()) {
                            var item = new EspacioDiscoBD {
                                Id = ManejoNulos.ManageNullInteger(dr["Id"]),
                                NombreBD = ManejoNulos.ManageNullStr(dr["NombreBD"]),
                                EspacioBD = ManejoNulos.ManageNullStr(dr["EspacioBD"]),
                                NombreLog = ManejoNulos.ManageNullStr(dr["NombreLog"]),
                                EspacioLog = ManejoNulos.ManageNullStr(dr["EspacioLog"]),
                                FechaCreacion = ManejoNulos.ManageNullDate(dr["FechaCreacion"]),
                            };
                            lista.Add(item);
                        }
                    }
                }

            } catch(Exception ex) {
                Console.WriteLine(ex.Message);
            }
            finally {
            }
            return lista;
        }

        public bool LimpiarLogBDAzure(string nombreBD, string nombreLog) {

            bool respuesta = false;
            string consulta = @"
                                USE [" + nombreBD + @"];  

                                ALTER DATABASE [" + nombreBD + @"]  
                                SET RECOVERY SIMPLE;  

                                DBCC SHRINKFILE ([" + nombreLog + @"], 1);

                                ALTER DATABASE [" + nombreBD + @"]  
                                SET RECOVERY FULL;  
                                ";
            try {
                using(var con = new SqlConnection(_conexion)) {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.ExecuteNonQuery();
                    respuesta = true;
                }
            } catch(Exception ex) {
                Console.WriteLine(ex.Message);
                respuesta = false;
            }
            return respuesta;
        }


    }
}
