using Microsoft.Data.SqlClient;

namespace CarsApi.Data
{
    public class CarsContext
    {
        private string connectionString = "Server=EMML_MAULEN;Database=CarsDB;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";

        public SqlConnection GetConnection()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                return conn;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al conectar con la base de datos: " + ex.Message);
            }
        }
    }
}
