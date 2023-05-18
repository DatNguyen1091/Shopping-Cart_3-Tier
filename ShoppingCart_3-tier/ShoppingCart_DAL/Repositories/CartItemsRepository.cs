using Microsoft.Extensions.Options;
using ShoppingCart_DAL.Contacts;
using ShoppingCart_DAL.Data;
using ShoppingCart_DAL.Models;
using System.Data.SqlClient;
using X.PagedList;

namespace ShoppingCart_DAL.Repositories
{
    public class CartItemsRepository : IRepository<CartItems>
    {
        private readonly Connection _connection;
        public CartItemsRepository(IOptions<Connection> connection)
        {
            _connection = connection.Value;
        }

        public List<CartItems> GetAll(int? page)
        {
            int pageSize = 10;
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            List<CartItems> cartItems = new List<CartItems>();
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                var offset = (pageIndex - 1) * pageSize;
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM CartItems ORDER BY id OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY", connection))
                {
                    command.Parameters.AddWithValue("@offset", offset);
                    command.Parameters.AddWithValue("@pageSize", pageSize);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CartItems model = new CartItems();
                            model.id = (int)reader["id"];
                            model.quantity = (int)reader["quantity"];
                            model.cartId = (int)reader["cartId"];
                            model.productId = (int)reader["productId"];
                            model.isDeleted = (bool)reader["isDeleted"];
                            model.createdAt = (DateTime)reader["createdAt"];
                            model.updatedAt = (DateTime)reader["updatedAt"];
                            cartItems.Add(model);
                        }
                    }
                }
                connection.Close();
            }
            var cartitem = cartItems.ToPagedList(pageIndex, pageSize);
            return cartitem.ToList();
        }

        public CartItems GetById(int id)
        {
            CartItems item = new CartItems();
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM CartItems WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        item.id = reader.GetInt32(0);
                        item.quantity = reader.GetInt32(1);
                        item.cartId = reader.GetInt32(2);
                        item.productId = reader.GetInt32(3);
                        item.isDeleted = reader.GetBoolean(4);
                        item.createdAt = reader.GetDateTime(5);
                        item.updatedAt = reader.GetDateTime(6);
                    }
                    reader.Close();
                }
                connection.Close();
            }
            return item;
        }

        public CartItems Create(CartItems model)
        {
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                var query = "INSERT INTO CartItems (quantity, cartId, productId, isDeleted) VALUES (@quantity, @cartId, @productId, @isDeleted)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@quantity", model.quantity);
                    command.Parameters.AddWithValue("@cartId", model.cartId);
                    command.Parameters.AddWithValue("@productId", model.productId);
                    command.Parameters.AddWithValue("@isDeleted", model.isDeleted);
                    command.ExecuteNonQuery();
                }
                connection.Close();
                return model;
            }
        }

        public CartItems Update(int id, CartItems model)
        {
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                var query = "UPDATE CartItems SET quantity = @quantity, cartId = @cartId, productId = @productId, isDeleted = @isDeleted WHERE id = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@quantity", model.quantity);
                    command.Parameters.AddWithValue("@cartId", model.cartId);
                    command.Parameters.AddWithValue("@productId", model.productId);
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
                using (SqlCommand command = new SqlCommand("DELETE FROM CartItems WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    int rows = command.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        return "Item deleted successfully.";
                    }
                }
                connection.Close();
            }
            return "Failed to delete item.";
        }
    }
}
