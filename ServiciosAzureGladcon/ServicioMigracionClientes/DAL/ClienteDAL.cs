using ServicioMigracionClientes.Clases;
using ServicioMigracionClientes.utilitarios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioMigracionClientes.DAL
{
    public class ClienteDAL
    {
        private readonly string _conexion;
        public ClienteDAL()
        {
            _conexion = ConfigurationManager.ConnectionStrings["connectionBD"].ConnectionString;
        }
        public bool GuardarCliente(Cliente entidad)
        {
            bool respuesta = false;

            string consulta = @"

        IF EXISTS (SELECT * FROM [dbo].[Cliente] (nolock) WHERE IdIas =@IdIas)
        BEGIN
           SELECT 1 
        END
        ELSE
        BEGIN
           INSERT INTO [dbo].[Cliente]
               ([NroDoc]
               ,[NombreCompleto]
               ,[ApelPat]
               ,[ApelMat]
               ,[Genero]
               ,[Celular1]
               ,[Celular2]
               ,[Mail]
               ,[FechaNacimiento]
               ,[FechaRegistro]
               ,[FechaRegistroIas]
               ,[Nombre]
               ,[Sala]
               ,[TipoDocumento]
               ,[Ubigeo]
               ,[IdIas])
         VALUES
               (@NroDoc
               ,@NombreCompleto
               ,@ApelPat
               ,@ApelMat
               ,@Genero
               ,@Celular1
               ,@Celular2
               ,@Mail
               ,@FechaNacimiento
               ,getdate()
               ,@FechaRegistro
               ,@Nombre
               ,@Sala
               ,@TipoDocumento
               ,@Ubigeo
               ,@IdIas)
        END

        ";

            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@NroDoc",ManejoNulos.ManageNullStr(entidad.NroDoc));
                    query.Parameters.AddWithValue("@NombreCompleto", ManejoNulos.ManageNullStr(entidad.NombreCompleto));
                    query.Parameters.AddWithValue("@ApelPat", ManejoNulos.ManageNullStr(entidad.ApelPat));
                    query.Parameters.AddWithValue("@ApelMat", ManejoNulos.ManageNullStr(entidad.ApelMat));
                    query.Parameters.AddWithValue("@Genero", ManejoNulos.ManageNullStr(entidad.NombreGenero));
                    query.Parameters.AddWithValue("@Celular1", ManejoNulos.ManageNullStr(entidad.Celular1));
                    query.Parameters.AddWithValue("@Celular2", ManejoNulos.ManageNullStr(entidad.Celular2));
                    query.Parameters.AddWithValue("@Mail", ManejoNulos.ManageNullStr(entidad.Mail));
                    query.Parameters.AddWithValue("@FechaNacimiento", ManejoNulos.ManageNullDate(entidad.FechaNacimiento));
                    query.Parameters.AddWithValue("@FechaRegistro", ManejoNulos.ManageNullDate(entidad.FechaRegistro));
                    query.Parameters.AddWithValue("@Nombre", ManejoNulos.ManageNullStr(entidad.Nombre));
                    query.Parameters.AddWithValue("@Sala", ManejoNulos.ManageNullStr(entidad.NombreSala));
                    query.Parameters.AddWithValue("@TipoDocumento", ManejoNulos.ManageNullStr(entidad.NombreTipoDocumento));
                    query.Parameters.AddWithValue("@Ubigeo", ManejoNulos.ManageNullStr(entidad.NombreUbigeo));
                    query.Parameters.AddWithValue("@IdIas", ManejoNulos.ManageNullInteger(entidad.IdIas));
                    query.ExecuteNonQuery();
                    respuesta = true;
                }
            }
            catch (Exception ex)
            {
                var response = " - " + ex.Message.ToString() + "\n ----------- InnerException=\n" + ex.InnerException + "\n --------------- Stack: ------------\n" + ex.StackTrace.ToString();
                funciones.logueo("ERROR Insertando cliente - idIas= "+ entidad.IdIas+" -- " + response, "Error");
                respuesta = false;
            }
            return respuesta;
        }
        public int ObtenerMaximoIdIas()
        {
            int result = 0;
            string consulta = @"SELECT max(idias) as maximo
                              FROM [dbo].[Cliente] (nolock)";
            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);

                    using (var dr = query.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                result = ManejoNulos.ManageNullInteger(dr["maximo"]);
                            }
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                result= 0;
            }
            return result;
        }
    }
}
