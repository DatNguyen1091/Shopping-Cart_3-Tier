using Microsoft.Extensions.Options;
using ShoppingCart_DAL.Contacts;
using ShoppingCart_DAL.Data;
using ShoppingCart_DAL.Models;
using System.Data.SqlClient;

namespace ShoppingCart_DAL.Repositories
{
    public class CartsRepository : IRepository<Carts>
    {
        private readonly Connection _connection;
        public CartsRepository(IOptions<Connection> connection)
        {
            _connection = connection.Value;
        }

        public List<Carts> GetAll(int? page)
        {
            List<Carts> carts = new List<Carts>();
            var pageSize = 10;
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                var offset = (pageIndex - 1) * pageSize;
                using (SqlCommand command = new SqlCommand("SELECT * FROM Carts ORDER BY id OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY", connection))
                {
                    command.Parameters.AddWithValue("@offset", offset);
                    command.Parameters.AddWithValue("@pageSize", pageSize);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Carts model = new Carts();
                            model.id = (int)reader["id"];
                            model.uniqueCartId = (string)reader["uniqueCartId"];
                            model.cartStatus = (string)reader["cartStatus"];
                            model.createdAt = (DateTime)reader["createdAt"];
                            model.updatedAt = (DateTime)reader["updatedAt"];
                            carts.Add(model);
                        }
                    }
                }
                connection.Close();
            }
            return carts;
        }

        public Carts GetById(int id)
        {
            Carts cart = new Carts();
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Carts WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        cart.id = reader.GetInt32(0);
                        cart.uniqueCartId = reader.GetString(1);
                        cart.cartStatus = reader.GetString(2);
                        cart.createdAt = reader.GetDateTime(3);
                        cart.updatedAt = reader.GetDateTime(4);
                    }
                    reader.Close();
                }
                connection.Close();
            }
            return cart;
        }

        public Carts Create(Carts model)
        {
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                var query = "INSERT INTO Carts (uniqueCartId, cartStatus) VALUES (@uniqueCartId, @cartStatus)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@uniqueCartId", model.uniqueCartId);
                    command.Parameters.AddWithValue("@cartStatus", model.cartStatus);
                    command.ExecuteNonQuery();
                }
                connection.Close();
                return model;
            }
        }

        public Carts Update(int id, Carts model)
        {
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                var query = "UPDATE Carts SET uniqueCartId = @uniqueCartId, cartStatus = @cartStatus WHERE id = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@uniqueCartId", model.uniqueCartId);
                    command.Parameters.AddWithValue("@cartStatus", model.cartStatus);
                    int rows = command.ExecuteNonQuery();
                    if (rows == 0)
                    {
                        throw new Exception("Update failed");
                    }
                }
                return model;
            }
        }

        public string Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("DELETE FROM Carts WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    int rows = command.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        return "Cart deleted successfully.";
                    }
                }
                connection.Close();
            }
            return "Failed to delete cart.";
        }
    }
}
