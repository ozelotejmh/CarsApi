using CarsApi.Data;
using CarsApi.Model;
using Microsoft.Data.SqlClient;
using System.Data;
namespace CarsApi.Services
{
    public class ControllerContext : IControllerContext
    {
        CarsContext context = new CarsContext();
        List<CarsCharacter> carsitos = new List<CarsCharacter>();

        // Obtener todos los coches (Get)
        public List<CarsCharacter> GetCharacters()
        {
            try
            {
                using (SqlConnection conn = context.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_GetCars", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            carsitos.Add(new CarsCharacter
                            {
                                CarsID = Convert.ToInt32(reader["CarsID"]),
                                Nombre = reader["Nombre"].ToString(),
                                Color = reader["Color"].ToString(),
                                Imagen = reader["Imagen"].ToString(),
                                Descripcion = reader["Descripcion"].ToString(),
                            });
                        }
                    }
                }
                return carsitos;
            }
            catch (Exception)
            {
                return null; // En caso de error, retorna null
            }
        }

        // Insertar un nuevo coche (Post)
        public bool PostCharacter(string nombre, string color, string imagen, string descripcion)
        {
            try
            {
                using (SqlConnection conn = context.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_PostCar", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@Color", color);
                    cmd.Parameters.AddWithValue("@Imagen", imagen);
                    cmd.Parameters.AddWithValue("@Descripcion", descripcion);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0; // Si se afectaron filas, es un éxito
                }
            }
            catch (Exception)
            {
                return false; // Si ocurre un error, retorna false
            }
        }

        // Actualizar un coche existente (Put)
        public bool PutCharacter(int carsId, string nombre, string color, string imagen, string descripcion)
        {
            try
            {
                using (SqlConnection conn = context.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_PutCar", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CarsID", carsId);
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@Color", color);
                    cmd.Parameters.AddWithValue("@Imagen", imagen);
                    cmd.Parameters.AddWithValue("@Descripcion", descripcion);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0; // Si se afectaron filas, es un éxito
                }
            }
            catch (Exception)
            {
                return false; // Si ocurre un error, retorna false
            }
        }

        // Eliminar un coche por su ID (Delete)
        public bool DeleteCharacter(int carsId)
        {
            try
            {
                using (SqlConnection conn = context.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_DeleteCar", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CarsID", carsId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0; // Si se afectaron filas, es un éxito
                }
            }
            catch (Exception)
            {
                return false; // Si ocurre un error, retorna false
            }
        }
    }
}
