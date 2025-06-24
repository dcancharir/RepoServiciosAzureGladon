using ServicioAzureIAS.Clases.Enum;
using ServicioAzureIAS.Clases.GestioProyectos;
using ServicioAzureIAS.utilitarios;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ServicioAzureIAS.DAL.GestionProyectos {
    public class EmpresaDAL {
        private readonly ConnectionHelperDAL _connectionHelperDAL;

        public EmpresaDAL() {
            _connectionHelperDAL = new ConnectionHelperDAL();
        }

        public async Task<List<Empresa>> ObtenerEmpresas(BaseDatosEnum baseDatos) {
            List<Empresa> lista = new List<Empresa>();
            string consulta = @"
        SELECT
            * 
        FROM
            Empresa
    ";

            try {
                using(var con = new SqlConnection(_connectionHelperDAL.ObtenerConexion(baseDatos))) {
                    await con.OpenAsync();
                    using(var query = new SqlCommand(consulta, con)) {
                        using(var dr = await query.ExecuteReaderAsync()) {
                            if(dr.HasRows) {
                                while(await dr.ReadAsync()) {
                                    Empresa empresa = new Empresa {
                                        EmpresaId = ManejoNulos.ManageNullInteger(dr["EmpresaId"]),
                                        CO_EMPR = ManejoNulos.ManageNullInteger(dr["CO_EMPR"]),
                                        DE_NOMB = ManejoNulos.ManageNullStr(dr["DE_NOMB"]),
                                        ID_BUK = ManejoNulos.ManageNullInteger(dr["ID_BUK"]),
                                        NU_RUCS = ManejoNulos.ManageNullStr(dr["NU_RUCS"]),
                                        DE_DIRE = ManejoNulos.ManageNullStr(dr["DE_DIRE"]),
                                    };

                                    lista.Add(empresa);
                                }
                            }
                        }
                    }
                }
            } catch {
                lista = new List<Empresa>();
            }
            return lista;
        }
        public async Task<bool> ActualizarEmpresa(Empresa empresa,BaseDatosEnum baseDatos) {
            string query = @"
        UPDATE Empresa SET
            CO_EMPR = @CO_EMPR,
            DE_NOMB = @DE_NOMB,
            NU_RUCS = @NU_RUCS,
            DE_DIRE = @DE_DIRE
        WHERE ID_BUK = @ID_BUK
    ";

            using(var con = new SqlConnection(_connectionHelperDAL.ObtenerConexion(baseDatos))) {
                await con.OpenAsync();
                using(var cmd = new SqlCommand(query, con)) {
                    cmd.Parameters.AddWithValue("@CO_EMPR", ManejoNulos.ManageNullInteger64(empresa.CO_EMPR));
                    cmd.Parameters.AddWithValue("@DE_NOMB", ManejoNulos.ManageNullStr(empresa.DE_NOMB));
                    cmd.Parameters.AddWithValue("@NU_RUCS", ManejoNulos.ManageNullStr(empresa.NU_RUCS));
                    cmd.Parameters.AddWithValue("@DE_DIRE", ManejoNulos.ManageNullStr(empresa.DE_DIRE));
                    cmd.Parameters.AddWithValue("@ID_BUK", ManejoNulos.ManageNullInteger64(empresa.ID_BUK));
                    int rows = await cmd.ExecuteNonQueryAsync();
                    return rows > 0;
                }
            }
        }
        public async Task<bool> InsertarEmpresa(Empresa empresa, BaseDatosEnum baseDatos) {
            string query = @"
        INSERT INTO Empresa (CO_EMPR, DE_NOMB, NU_RUCS, DE_DIRE, ID_BUK)
        VALUES (@CO_EMPR, @DE_NOMB, @NU_RUCS, @DE_DIRE, @ID_BUK)
    ";

            using(var con = new SqlConnection(_connectionHelperDAL.ObtenerConexion(baseDatos))) {
                await con.OpenAsync();
                using(var cmd = new SqlCommand(query, con)) {
                    cmd.Parameters.AddWithValue("@CO_EMPR", ManejoNulos.ManageNullInteger64(empresa.CO_EMPR));
                    cmd.Parameters.AddWithValue("@DE_NOMB", ManejoNulos.ManageNullStr(empresa.DE_NOMB));
                    cmd.Parameters.AddWithValue("@NU_RUCS", ManejoNulos.ManageNullStr(empresa.NU_RUCS));
                    cmd.Parameters.AddWithValue("@DE_DIRE", ManejoNulos.ManageNullStr(empresa.DE_DIRE));
                    cmd.Parameters.AddWithValue("@ID_BUK", ManejoNulos.ManageNullInteger64(empresa.ID_BUK));

                    int rows = await cmd.ExecuteNonQueryAsync();
                    return rows > 0;
                }
            }
        }

      
    }
}
