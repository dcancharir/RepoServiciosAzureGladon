using ServicioAzureIAS.Clases.Ludopata;
using ServicioAzureIAS.utilitarios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace ServicioAzureIAS.ControlAcceso.CAL {
    public class LudopataDAL {
        private readonly string _conexion = string.Empty;

        public LudopataDAL() {
            _conexion = ConfigurationManager.ConnectionStrings["conectionBdSeguridad"].ConnectionString;
        }

        public List<Ludopata> ObtenerLudopatas() {
            List<Ludopata> ludopatas = new List<Ludopata>();
            string consulta = @"
                SELECT 
                    l.LudopataID AS LudopataID,
                    l.CodRegistro AS CodigoRegistroMincetur,
                    l.Nombre AS NombreLudopata,
                    l.ApellidoPaterno AS ApellidoPaternoLudopata,
                    l.ApellidoMaterno AS ApellidoMaternoLudopata,
                    l.TipoDoiID AS TipoDocumentoID,
                    l.DNI AS NumeroDocumento,
                    l.Imagen AS Imagen,
                    l.Foto AS Foto,
                    l.CodUbigeo AS CodigoUbigeo,
                    l.Telefono AS Telefono,
                    l.FechaInscripcion AS FechaDeInscripcion,
                    l.ContactoID AS ContactoLudopataID,
                    l.Estado AS EstadoLudopata,
                    c.ContactoID AS ContactoID,
                    c.Nombre AS NombreContacto,
                    c.ApellidoPaterno AS ApellidoPaternoContacto,
                    c.ApellidoMaterno AS ApellidoMaternoContacto,
                    c.Telefono AS TelefonoContacto,
                    c.Celular AS CelularContacto
                FROM CAL_Ludopata AS l WITH (NOLOCK)
                LEFT JOIN CAL_Contacto AS c ON c.ContactoID = l.ContactoID
            ";
            try {
                using(var con = new SqlConnection(_conexion)) {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    using(var dr = query.ExecuteReader()) {
                        while(dr.Read()) {
                            ContactoLudopata contacto = new ContactoLudopata {
                                ContactoID = ManejoNulos.ManageNullInteger(dr["ContactoID"]),
                                Nombre = ManejoNulos.ManageNullStr(dr["NombreContacto"]).Trim(),
                                ApellidoPaterno = ManejoNulos.ManageNullStr(dr["ApellidoPaternoContacto"]).Trim(),
                                ApellidoMaterno = ManejoNulos.ManageNullStr(dr["ApellidoMaternoContacto"]).Trim(),
                                Telefono = ManejoNulos.ManageNullStr(dr["TelefonoContacto"]).Trim(),
                                Celular = ManejoNulos.ManageNullStr(dr["CelularContacto"]).Trim(),
                            };
                            Ludopata ludopata = new Ludopata {
                                LudopataID = ManejoNulos.ManageNullInteger(dr["LudopataID"]),
                                CodRegistro = ManejoNulos.ManageNullStr(dr["CodigoRegistroMincetur"]).Trim(),
                                Nombre = ManejoNulos.ManageNullStr(dr["NombreLudopata"]).Trim(),
                                ApellidoPaterno = ManejoNulos.ManageNullStr(dr["ApellidoPaternoLudopata"]).Trim(),
                                ApellidoMaterno = ManejoNulos.ManageNullStr(dr["ApellidoMaternoLudopata"]).Trim(),
                                TipoDoiID = ManejoNulos.ManageNullInteger(dr["TipoDocumentoID"]),
                                DNI = ManejoNulos.ManageNullStr(dr["NumeroDocumento"]).Trim(),
                                Imagen = ManejoNulos.ManageNullStr(dr["Imagen"]).Trim(),
                                Foto = ManejoNulos.ManageNullStr(dr["Foto"]).Trim(),
                                Telefono = ManejoNulos.ManageNullStr(dr["Telefono"]).Trim(),
                                CodUbigeo = ManejoNulos.ManageNullInteger(dr["CodigoUbigeo"]),
                                FechaInscripcion = ManejoNulos.ManageNullDate(dr["FechaDeInscripcion"]),
                                ContactoID = ManejoNulos.ManageNullInteger(dr["ContactoLudopataID"]),
                                Estado = ManejoNulos.ManegeNullBool(dr["EstadoLudopata"]),
                                Contacto = contacto
                            };
                            ludopatas.Add(ludopata);
                        }
                    }
                }
            } catch(Exception ex) {
                Console.WriteLine(ex.Message);
            }
            return ludopatas;
        }

        public bool ExisteLudopataEnClientes(int idTipoDocumento, string numeroDocumento) {
            bool existe = false;
            string consulta = @"
                SELECT COUNT(NroDoc)
                FROM AST_Cliente WITH (NOLOCK)
                WHERE NroDoc = @numeroDocumento
                  AND TipoDocumentoId = @idTipoDocumento;
            ";
            try {
                using(var con = new SqlConnection(_conexion)) {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@idTipoDocumento", idTipoDocumento);
                    query.Parameters.AddWithValue("@numeroDocumento", numeroDocumento);
                    existe = Convert.ToInt32(query.ExecuteScalar()) >= 1;
                }
            } catch(Exception ex) {
                Console.WriteLine(ex.Message);
            }
            return existe;
        }

        public int ModificarEstadoLudopata(bool estado, int idLudopata) {
            int idActualizado = 0;
            string consulta = @"
                UPDATE CAL_Ludopata
                SET Estado = @estado
                OUTPUT INSERTED.LudopataID
                WHERE LudopataID = @idLudopata
            ";
            try {
                using(var con = new SqlConnection(_conexion)) {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@idLudopata", idLudopata);
                    query.Parameters.AddWithValue("@estado", estado);
                    idActualizado = Convert.ToInt32(query.ExecuteScalar());
                }
            } catch(Exception ex) {
                Console.WriteLine(ex.Message);
            }
            return idActualizado;
        }

        public int InsertarLudopataConContacto(Ludopata ludopata) {
            int idInsertado = 0;
            string consulta = @"
                BEGIN TRANSACTION;

                BEGIN TRY
                    INSERT INTO CAL_Contacto(Nombre, ApellidoPaterno, ApellidoMaterno, Telefono, Celular)
                    VALUES(@nombreContacto, @apellidoPaternoContacto, @apellidoMaternoContacto, @telefonoContacto, @celularContacto)

                    DECLARE @contactoId INT;
                    SET @contactoId = SCOPE_IDENTITY();

                    INSERT INTO CAL_Ludopata (Nombre, ApellidoPaterno, ApellidoMaterno, FechaInscripcion, TipoExclusion, DNI, Foto, ContactoID, Telefono, CodRegistro, Estado, Imagen, TipoDoiID, CodUbigeo, FechaRegistro, UsuarioRegistro)
                    OUTPUT Inserted.LudopataID
                    VALUES(@nombre, @apellidoPaterno, @apellidoMaterno, @fechaInscripcion, @tipoExclusion, @dni, @foto, @contactoId, @telefono, @codRegistro, @estado, @imagen, @tipoDoiID, @codUbigeo, @fechaRegistro, @usuarioRegistro)

                    COMMIT TRANSACTION;
                END TRY
                BEGIN CATCH
                    ROLLBACK TRANSACTION;

                    DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
                    DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
                    DECLARE @ErrorState INT = ERROR_STATE();

                    RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
                END CATCH;
            ";
            try {
                using(var con = new SqlConnection(_conexion)) {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@nombreContacto", ludopata.Contacto.Nombre);
                    query.Parameters.AddWithValue("@apellidoPaternoContacto", ludopata.Contacto.ApellidoPaterno);
                    query.Parameters.AddWithValue("@apellidoMaternoContacto", ludopata.Contacto.ApellidoMaterno);
                    query.Parameters.AddWithValue("@telefonoContacto", ludopata.Contacto.Telefono);
                    query.Parameters.AddWithValue("@celularContacto", ludopata.Contacto.Celular);
                    query.Parameters.AddWithValue("@nombre", ludopata.Nombre);
                    query.Parameters.AddWithValue("@apellidoPaterno", ludopata.ApellidoPaterno);
                    query.Parameters.AddWithValue("@apellidoMaterno", ludopata.ApellidoMaterno);
                    query.Parameters.AddWithValue("@fechaInscripcion", ludopata.FechaInscripcion);
                    query.Parameters.AddWithValue("@tipoExclusion", ludopata.TipoExclusion);
                    query.Parameters.AddWithValue("@dni", ludopata.DNI);
                    query.Parameters.AddWithValue("@foto", ludopata.Foto);
                    query.Parameters.AddWithValue("@telefono", ludopata.Telefono);
                    query.Parameters.AddWithValue("@codRegistro", ludopata.CodRegistro);
                    query.Parameters.AddWithValue("@estado", ludopata.Estado);
                    query.Parameters.AddWithValue("@imagen", ludopata.Imagen);
                    query.Parameters.AddWithValue("@tipoDoiID", ludopata.TipoDoiID);
                    query.Parameters.AddWithValue("@codUbigeo", ludopata.CodUbigeo);
                    query.Parameters.AddWithValue("@fechaRegistro", ludopata.FechaRegistro);
                    query.Parameters.AddWithValue("@usuarioRegistro", ludopata.UsuarioRegistro);
                    idInsertado = Convert.ToInt32(query.ExecuteScalar());
                }
            } catch(Exception ex) {
                Console.WriteLine(ex.Message);
            }
            return idInsertado;
        }

        public int InsertarContactoLudopata(ContactoLudopata contacto) {
            int idInsertado = 0;
            string consulta = @"
                INSERT INTO CAL_Contacto(Nombre, ApellidoPaterno, ApellidoMaterno, Telefono, Celular)
                OUTPUT INSERTED.ContactoID
                VALUES(@nombreContacto, @apellidoPaternoContacto, @apellidoMaternoContacto, @telefonoContacto, @celularContacto)
            ";
            try {
                using(var con = new SqlConnection(_conexion)) {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@nombreContacto", contacto.Nombre);
                    query.Parameters.AddWithValue("@apellidoPaternoContacto", contacto.ApellidoPaterno);
                    query.Parameters.AddWithValue("@apellidoMaternoContacto", contacto.ApellidoMaterno);
                    query.Parameters.AddWithValue("@telefonoContacto", contacto.Telefono);
                    query.Parameters.AddWithValue("@celularContacto", contacto.Celular);
                    idInsertado = Convert.ToInt32(query.ExecuteScalar());
                }
            } catch(Exception ex) {
                Console.WriteLine(ex.Message);
            }
            return idInsertado;
        }

        public int ModificarLudopataContacto(Ludopata ludopata) {
            int idModificado = 0;
            string consulta = @"
                BEGIN TRANSACTION;

                BEGIN TRY
                    UPDATE CAL_Contacto
                    SET 
                        Nombre = @nombreContacto,
                        ApellidoPaterno = @apellidoPaternoContacto,
                        ApellidoMaterno = @apellidoMaternoContacto,
                        Telefono = @telefonoContacto,
                        Celular = @celularContacto
                    WHERE
                        ContactoID = @contactoId

                    UPDATE CAL_Ludopata
                    SET
                        Nombre = @nombre,
                        ApellidoPaterno = @apellidoPaterno,
                        ApellidoMaterno = @apellidoMaterno,
                        FechaInscripcion = @fechaInscripcion,
                        DNI = @dni,
                        CodRegistro = @codRegistro,
                        Estado = @estado,
                        TipoDoiID = @tipoDoiID,
                        ContactoID = @contactoId
                    OUTPUT Inserted.LudopataID
                    WHERE
                        LudopataID = @ludopataID

                    COMMIT TRANSACTION;
                END TRY
                BEGIN CATCH
                    ROLLBACK TRANSACTION;

                    DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
                    DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
                    DECLARE @ErrorState INT = ERROR_STATE();

                    RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
                END CATCH;
            ";
            try {
                using(var con = new SqlConnection(_conexion)) {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@contactoId", ludopata.Contacto.ContactoID);
                    query.Parameters.AddWithValue("@nombreContacto", ludopata.Contacto.Nombre);
                    query.Parameters.AddWithValue("@apellidoPaternoContacto", ludopata.Contacto.ApellidoPaterno);
                    query.Parameters.AddWithValue("@apellidoMaternoContacto", ludopata.Contacto.ApellidoMaterno);
                    query.Parameters.AddWithValue("@telefonoContacto", ludopata.Contacto.Telefono);
                    query.Parameters.AddWithValue("@celularContacto", ludopata.Contacto.Celular);

                    query.Parameters.AddWithValue("@ludopataID", ludopata.LudopataID);
                    query.Parameters.AddWithValue("@nombre", ludopata.Nombre);
                    query.Parameters.AddWithValue("@apellidoPaterno", ludopata.ApellidoPaterno);
                    query.Parameters.AddWithValue("@apellidoMaterno", ludopata.ApellidoMaterno);
                    query.Parameters.AddWithValue("@fechaInscripcion", ludopata.FechaInscripcion);
                    query.Parameters.AddWithValue("@dni", ludopata.DNI);
                    query.Parameters.AddWithValue("@codRegistro", ludopata.CodRegistro);
                    query.Parameters.AddWithValue("@estado", ludopata.Estado);
                    query.Parameters.AddWithValue("@tipoDoiID", ludopata.TipoDoiID);
                    idModificado = Convert.ToInt32(query.ExecuteScalar());
                }
            } catch(Exception ex) {
                Console.WriteLine(ludopata.DNI);
                Console.WriteLine(ex.Message);
            }
            return idModificado;
        }

        public int ModificarFechaEnvioCorreo(int idLudopata, DateTime fechaEnvio) {
            int idModificado = 0;
            string consulta = @"
                UPDATE CAL_Ludopata
                SET FechaEnvioCorreo = @fechaEnvioCorreo
                WHERE LudopataID = @idLudopata
            ";
            try {
                using(var con = new SqlConnection(_conexion)) {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@fechaEnvioCorreo", fechaEnvio);
                    query.Parameters.AddWithValue("@idLudopata", idLudopata);
                    idModificado = Convert.ToInt32(query.ExecuteScalar());
                }
            } catch(Exception ex) {
                Console.WriteLine(ex.Message);
            }
            return idModificado;
        }
    }
}
