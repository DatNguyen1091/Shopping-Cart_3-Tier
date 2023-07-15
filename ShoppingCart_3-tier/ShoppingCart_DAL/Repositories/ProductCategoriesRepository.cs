using Microsoft.Extensions.Options;
using ShoppingCart_DAL.Contacts;
using ShoppingCart_DAL.Data;
using ShoppingCart_DAL.Models;
using System.Data.SqlClient;

namespace ShoppingCart_DAL.Repositories
{
    public class ProductCategoriesRepository :IRepository<ProductCategories>
    {
        private readonly Connection _connection;
        public ProductCategoriesRepository(IOptions<Connection> connection)
        {
            _connection = connection.Value;
        }

        public List<ProductCategories> GetAll(int? page)
        {
            List<ProductCategories> productCategories = new List<ProductCategories>();
            var pageSize = 10;
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                var offset = (pageIndex - 1) * pageSize;
                using (SqlCommand command = new SqlCommand("SELECT * FROM ProductCategories ORDER BY id OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY", connection))
                {
                    command.Parameters.AddWithValue("@offset", offset);
                    command.Parameters.AddWithValue("@pageSize", pageSize);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ProductCategories model = new ProductCategories();
                            model.id = (int)reader["id"];
                            model.productId = (int)reader["productId"];
                            model.categoryId = (int)reader["categoryId"];
                            model.createdAt = (DateTime)reader["createdAt"];
                            model.updatedAt = (DateTime)reader["updatedAt"];
                            productCategories.Add(model);
                        }
                    }
                }
                connection.Close();
            }
            return productCategories.ToList();
        }

        public ProductCategories GetById(int id)
        {
            ProductCategories model = new ProductCategories();
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM ProductCategories WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        model.id = reader.GetInt32(0);
                        model.productId = reader.GetInt32(1);
                        model.categoryId = reader.GetInt32(2);
                        model.createdAt = reader.GetDateTime(3);
                        model.updatedAt = reader.GetDateTime(4);
                    }
                    reader.Close();
                }
                connection.Close();
            }
            return model;
        }

        public ProductCategories Create(ProductCategories model)
        {
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                var query = "INSERT INTO ProductCategories  ( productId, categoryId) VALUES ( @productId, @categoryId)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@productId", model.productId);
                    command.Parameters.AddWithValue("@categoryId", model.categoryId);
                    command.ExecuteNonQuery();
                }
                connection.Close();
                return model;
            }

        }

        public ProductCategories Update(int id, ProductCategories model)
        {
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                var query = "UPDATE ProductCategories SET productId = @productId, categoryId = @categoryId WHERE id = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@productId", model.productId);
                    command.Parameters.AddWithValue("@categoryId", model.categoryId);
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
                using (SqlCommand command = new SqlCommand("DELETE FROM ProductCategories WHERE id = @id", connection))
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
