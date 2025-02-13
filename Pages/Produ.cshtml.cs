using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace pruebadesarrollo.Pages
{
    public class ProduModel : PageModel
    {
        public List<ProduInfo> ProduList { get; set; } = new List<ProduInfo>(); // Lista para almacenar los productos

        public void OnGet()
        {
            try
            { 
                // Cadena de conexión a la base de datos
                string connectionString = "Server=.;Database=desarrollocrud;Trusted_Connection=True;TrustServerCertificate=True";

                // Abriendo la conexión a la base de datos
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Consulta SQL actualizada para la tabla Productos
                    string sql = "SELECT ProductoID, Nombre, Categoria, Stock, Precio FROM Productos";

                    // Ejecutando la consulta
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Leyendo los datos
                            while (reader.Read())
                            {
                                ProduInfo produInfo = new ProduInfo();
                                produInfo.id = reader.GetInt32(0); // ID del producto (ProductoID)
                                produInfo.nombre = reader.GetString(1); // Nombre del producto
                                produInfo.categoria = reader.GetString(2); // Categoría del producto
                                produInfo.stock = reader.GetInt32(3); // Stock del producto
                                produInfo.precio = reader.GetDouble(4); // Usando GetDouble para leer Precio

                                // Agregando a la lista
                                ProduList.Add(produInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Aquí se podría registrar el error de manera más eficiente
                Console.WriteLine("Existe un error: " + ex.Message);
                // O usar el sistema de logging de ASP.NET Core, por ejemplo:
                // _logger.LogError(ex, "Error al obtener los productos");
            }
        }

        // Clase para contener la información del producto
        public class ProduInfo
        {
            public int id { get; set; }
            public string nombre { get; set; } = "";
            public string categoria { get; set; } = "";
            public int stock { get; set; }
            public double precio { get; set; }  // Cambiar a double
        }
    }
}
