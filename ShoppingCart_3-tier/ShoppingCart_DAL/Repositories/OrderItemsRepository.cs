using Microsoft.Extensions.Options;
using ShoppingCart_DAL.Contacts;
using ShoppingCart_DAL.Data;
using ShoppingCart_DAL.Models;
using System.Data.SqlClient;

namespace ShoppingCart_DAL.Repositories
{
    public class OrderItemsRepository : IRepository<OrderItems>
    {
        private readonly Connection _connection;
        public OrderItemsRepository(IOptions<Connection> connection)
        {
            _connection = connection.Value;
        }

        public List<OrderItems> GetAll(int? page)
        {
            List<OrderItems> orderItems = new List<OrderItems>();
            var pageSize = 10;
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                var offset = (pageIndex - 1) * pageSize;
                using (SqlCommand command = new SqlCommand("SELECT * FROM OrderItems ORDER BY id OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY", connection))
                {
                    command.Parameters.AddWithValue("@offset", offset);
                    command.Parameters.AddWithValue("@pageSize", pageSize);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            OrderItems model = new OrderItems();
                            model.id = (int)reader["id"];
                            model.quantity = (int)reader["quantity"];
                            model.price = (decimal)reader["price"];
                            model.orderId = (int)reader["orderId"];
                            model.productId = (int)reader["productId"];
                            model.createdAt = (DateTime)reader["createdAt"];
                            model.updatedAt = (DateTime)reader["updatedAt"];
                            orderItems.Add(model);
                        }
                    }
                }
                connection.Close();
            }
            return orderItems.ToList();
        }

        public OrderItems GetById(int id)
        {
            OrderItems item = new OrderItems();
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM OrderItems WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        item.id = reader.GetInt32(0);
                        item.quantity = reader.GetInt32(1);
                        item.price = reader.GetDecimal(2);
                        item.productId = reader.GetInt32(3);
                        item.orderId = reader.GetInt32(4);
                        item.createdAt = reader.GetDateTime(5);
                        item.updatedAt = reader.GetDateTime(6);
                    }
                    reader.Close();
                }
                connection.Close();
            }
            return item;
        }

        public OrderItems Create(OrderItems model)
        {
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                var query = "INSERT INTO OrderItems (quantity, price, orderId, productId) VALUES (@quantity, @price, @orderId, @productId)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@quantity", model.quantity);
                    command.Parameters.AddWithValue("@price", model.price);
                    command.Parameters.AddWithValue("@orderId", model.orderId);
                    command.Parameters.AddWithValue("@productId", model.productId);
                    command.ExecuteNonQuery();
                }
                connection.Close();
                return model;
            }
        }

        public OrderItems Update(int id, OrderItems model)
        {
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                var query = "UPDATE OrderItems SET quantity = @quantity, price = @price, orderId = @orderId, productId = @productId WHERE id = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@quantity", model.quantity);
                    command.Parameters.AddWithValue("@price", model.price);
                    command.Parameters.AddWithValue("@orderId", model.orderId);
                    command.Parameters.AddWithValue("@productId", model.productId);
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
                using (SqlCommand command = new SqlCommand("DELETE FROM OrderItems WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    int rows = command.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        return "Deleted successfully.";
                    }
                }
                connection.Close();
            }
            return "Failed to delete.";
        }
    }
}
