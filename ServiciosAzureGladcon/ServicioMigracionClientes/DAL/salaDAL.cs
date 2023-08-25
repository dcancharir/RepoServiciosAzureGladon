using ServicioMigracionClientes.Clases;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioMigracionClientes.DAL
{
    public class salaDAL
    {
        private string _conexion = string.Empty;
        public salaDAL()
        {
            _conexion = ConfigurationManager.ConnectionStrings["connectionBD"].ConnectionString;
        }
        public int InsertarSala(sala obj)
        {
            int idInsertado = 0;
            string consulta = @"
IF EXISTS (SELECT * FROM [dbo].[sala] (nolock) WHERE id_sala = @id_sala)
        BEGIN
           SELECT 1 
        END
        ELSE
        BEGIN
INSERT INTO [sala]
           ([id_sala]
           ,[nombre_sala]
           ,[nombre_operador]
           ,[departamento_sala]
           ,[provincia_sala])
     Output Inserted.sala_id
     VALUES
           (@id_sala
           ,@nombre_sala
           ,@nombre_operador
           ,@departamento_sala
           ,@provincia_sala)

end";
            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@id_sala", obj.id_sala);
                    query.Parameters.AddWithValue("@nombre_sala", obj.nombre_sala);
                    query.Parameters.AddWithValue("@nombre_operador", obj.nombre_operador);
                    query.Parameters.AddWithValue("@departamento_sala", obj.departamento_sala);
                    query.Parameters.AddWithValue("@provincia_sala", obj.provincia_sala);
                    idInsertado = Convert.ToInt32(query.ExecuteScalar());

                }
            }
            catch (Exception ex)
            {
                idInsertado = 0;
            }

            return idInsertado;
        }
    }
}
