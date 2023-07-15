using Microsoft.Extensions.Options;
using ShoppingCart_DAL.Contacts;
using ShoppingCart_DAL.Data;
using ShoppingCart_DAL.Models;
using System.Data.SqlClient;

namespace ShoppingCart_DAL.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private readonly Connection _connection;
        public OrderRepository(IOptions<Connection> connection)
        {
            _connection = connection.Value;
        }

        public List<Order> GetAll(int? page)
        {
            List<Order> orders = new List<Order>();
            var pageSize = 10;
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                var offset = (pageIndex - 1) * pageSize;
                using (SqlCommand command = new SqlCommand("SELECT * FROM Orders ORDER BY id OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY", connection))
                {
                    command.Parameters.AddWithValue("@offset", offset);
                    command.Parameters.AddWithValue("@pageSize", pageSize);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Order model = new Order();
                            model.id = (int)reader["id"];
                            model.orderTotal = (decimal)reader["orderTotal"];
                            model.orderItemTotal = (decimal)reader["orderItemTotal"];
                            model.shippingCharge = (decimal)reader["shippingCharge"];
                            model.deliveryAddressId = (int)reader["deliveryAddressId"];
                            model.customerId = (int)reader["customerId"];
                            model.orderStatus = (string)reader["orderStatus"];
                            model.isDeleted = (bool)reader["isDeleted"];
                            model.createdAt = (DateTime)reader["createdAt"];
                            model.updatedAt = (DateTime)reader["updatedAt"];
                            orders.Add(model);
                        }
                    }
                }
                connection.Close();
            }
            return orders.ToList();
        }

        public Order GetById(int id)
        {
            Order model = new Order();
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Orders WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        model.id = reader.GetInt32(0);
                        model.orderTotal = reader.GetDecimal(1);
                        model.orderItemTotal = reader.GetDecimal(2);
                        model.shippingCharge = reader.GetDecimal(3);
                        model.deliveryAddressId = reader.GetInt32(4);
                        model.customerId = reader.GetInt32(5);
                        model.orderStatus = reader.GetString(6);
                        model.isDeleted = reader.GetBoolean(7);
                        model.createdAt = reader.GetDateTime(8);
                        model.updatedAt = reader.GetDateTime(9);
                    }
                    reader.Close();
                }
                connection.Close();
            }
            return model;
        }

        public Order Create(Order model)
        {
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                var query = "INSERT INTO Orders ( orderTotal, orderItemTotal, shippingCharge, deliveryAddressId, customerId, orderStatus, isDeleted) VALUES ( @orderTotal, @orderItemTotal, @shippingCharge, @deliveryAddressId, @customerId, @orderStatus, @isDeleted)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@orderTotal", model.orderTotal);
                    command.Parameters.AddWithValue("@orderItemTotal", model.orderItemTotal);
                    command.Parameters.AddWithValue("@shippingCharge", model.shippingCharge);
                    command.Parameters.AddWithValue("@deliveryAddressId", model.deliveryAddressId);
                    command.Parameters.AddWithValue("@customerId", model.customerId);
                    command.Parameters.AddWithValue("@orderStatus", model.orderStatus);
                    command.Parameters.AddWithValue("@isDeleted", model.isDeleted);
                    command.ExecuteNonQuery();
                }
                connection.Close();
                return model;
            }
        }

        public Order Update(int id, Order model)
        {
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                var query = "UPDATE Orders SET orderTotal = @orderTotal, orderItemTotal = @orderItemTotal, shippingCharge = @shippingCharge, deliveryAddressId = @deliveryAddressId, customerId = @customerId, orderStatus = @orderStatus, isDeleted = @isDeleted WHERE id = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@orderTotal", model.orderTotal);
                    command.Parameters.AddWithValue("@orderItemTotal", model.orderItemTotal);
                    command.Parameters.AddWithValue("@shippingCharge", model.shippingCharge);
                    command.Parameters.AddWithValue("@deliveryAddressId", model.deliveryAddressId);
                    command.Parameters.AddWithValue("@customerId", model.customerId);
                    command.Parameters.AddWithValue("@orderStatus", model.orderStatus);
                    command.Parameters.AddWithValue("@isDeleted", model.isDeleted);
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
                using (SqlCommand command = new SqlCommand("DELETE FROM Orders WHERE id = @id", connection))
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
