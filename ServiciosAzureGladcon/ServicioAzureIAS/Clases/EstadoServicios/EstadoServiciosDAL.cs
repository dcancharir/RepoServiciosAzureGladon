using ServicioAzureIAS.utilitarios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioAzureIAS.Clases.EstadoServicios
{
    public class EstadoServiciosDAL
    {
        string _conexion = ConfigurationManager.ConnectionStrings["conectionBdSeguridad"].ConnectionString;

        public bool InsertEstadoServicios(EstadoServiciosEntidad entidad)
        {
            bool respuesta = false;

            string consulta = @"INSERT INTO EstadoServicios
                                (CodSala
                                ,EstadoWebOnline
                                ,EstadoGladconServices
                                ,FechaRegistro)
                                VALUES
                                (@pCodSala
                                ,@pEstadoWebOnline
                                ,@pEstadoGladconServices
                                ,@pFechaRegistro)";

            try
            {


                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@pCodSala",entidad.CodSala);
                    query.Parameters.AddWithValue("@pEstadoWebOnline", entidad.EstadoWebOnline);
                    query.Parameters.AddWithValue("@pEstadoGladconServices", entidad.EstadoGladconServices);
                    query.Parameters.AddWithValue("@pFechaRegistro", entidad.FechaRegistro);
                    query.ExecuteNonQuery();
                    respuesta = true;

                }

            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());

            }

            return respuesta;

        }

        public List<SalaEntidad> ListadoSalaActivas()
        {
            List<SalaEntidad> lista = new List<SalaEntidad>();
            string consulta = @"SELECT CodSala,CodEmpresa,CodUbigeo,Nombre,UrlProgresivo,IpPublica,UrlSalaOnline FROM Sala where Estado=1";
            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    using (var dr = query.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var item = new SalaEntidad();

                            item.CodSala = Convert.ToInt32(dr["CodSala"]);
                            item.CodEmpresa = Convert.ToInt32(dr["CodEmpresa"]);
                            item.CodUbigeo = Convert.ToInt32(dr["CodUbigeo"]);
                            item.Nombre = Convert.ToString(dr["Nombre"]);
                            item.UrlProgresivo = Convert.ToString(dr["UrlProgresivo"]);
                            item.IpPublica = Convert.ToString(dr["IpPublica"]);
                            item.UrlSalaOnline = Convert.ToString(dr["UrlSalaOnline"]);

                            lista.Add(item);
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return lista;
        }
        public List<NotificacionDispositivo> GetDevicesToNotification(int codsala)
        {
            List<NotificacionDispositivo> lista = new List<NotificacionDispositivo>();
            string consulta = @"SELECT  [emd_id]
                                  ,[emd_imei]
                                  ,[emp_id]
                                  ,[emd_estado]
	                              ,emd_firebaseid
	                              ,e.CargoID
	                              ,cargoalerta.sala_id
                              FROM [EmpleadoDispositivo] ed
                              join SEG_Empleado e on e.EmpleadoID=ed.emp_id
                              join ALT_AlertaCargoConfiguracion cargoalerta on cargoalerta.cargo_id=e.CargoID
							  join SEG_Usuario usu on usu.EmpleadoID=ed.emp_id
							  join UsuarioSala ususala on ususala.UsuarioId=usu.UsuarioID
                              where cargoalerta.sala_id =@p0 and ususala.SalaId=@p1 and ed.emd_firebaseid IS not NUll order by emd_id desc";
            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", codsala);
                    query.Parameters.AddWithValue("@p1", codsala);

                    using (var dr = query.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var item = new NotificacionDispositivo
                            {
                                emd_id = ManejoNulos.ManageNullInteger(dr["emd_id"]),
                                emd_imei = ManejoNulos.ManageNullStr(dr["emd_imei"]),
                                emp_id = ManejoNulos.ManageNullInteger(dr["emp_id"]),
                                id = ManejoNulos.ManageNullStr(dr["emd_firebaseid"]),
                                CargoID = ManejoNulos.ManageNullInteger(dr["CargoID"]),
                                sala_id = ManejoNulos.ManageNullInteger(dr["sala_id"]),
                            };

                            lista.Add(item);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return lista;
        }

    }
}
