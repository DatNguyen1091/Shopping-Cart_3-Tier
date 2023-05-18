using Microsoft.Extensions.Options;
using ShoppingCart_DAL.Contacts;
using ShoppingCart_DAL.Data;
using ShoppingCart_DAL.Models;
using System.Data.SqlClient;
using X.PagedList;

namespace ShoppingCart_DAL.Repositories
{
    public class ProductBandsRepository : IRepository<ProductBrands>
    {
        private readonly Connection _connection;
        public ProductBandsRepository(IOptions<Connection> connection)
        {
            _connection = connection.Value;
        }

        public List<ProductBrands> GetAll(int? page)
        {
            List<ProductBrands> productBrands = new List<ProductBrands>();
            var pageSize = 10;
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                var offset = (pageIndex - 1) * pageSize;
                using (SqlCommand command = new SqlCommand("SELECT * FROM ProductBrands ORDER BY id OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY", connection))
                {
                    command.Parameters.AddWithValue("@offset", offset);
                    command.Parameters.AddWithValue("@pageSize", pageSize);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ProductBrands model = new ProductBrands();
                            model.id = (int)reader["id"];
                            model.productId = (int)reader["productId"];
                            model.brandId = (int)reader["brandId"];
                            model.createdAt = (DateTime)reader["createdAt"];
                            model.updatedAt = (DateTime)reader["updatedAt"];
                            productBrands.Add(model);
                        }
                    }
                }
                connection.Close();
            }
            var productBrand = productBrands.ToPagedList(pageIndex, pageSize);
            return productBrand.ToList();
        }

        public ProductBrands GetById(int id)
        {
            ProductBrands model = new ProductBrands();
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM ProductBrands WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        model.id = reader.GetInt32(0);
                        model.productId = reader.GetInt32(1);
                        model.brandId = reader.GetInt32(2);
                        model.createdAt = reader.GetDateTime(3);
                        model.updatedAt = reader.GetDateTime(4);
                    }
                    reader.Close();
                }
                connection.Close();
            }
            return model;
        }

        public ProductBrands Create(ProductBrands model)
        {
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                var query = "INSERT INTO ProductBrands  ( productId, brandId) VALUES ( @productId, @brandId)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@productId", model.productId);
                    command.Parameters.AddWithValue("@brandId", model.brandId);
                    command.ExecuteNonQuery();
                }
                connection.Close();
                return model;
            }
        }

        public ProductBrands Update(int id, ProductBrands model)
        {
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                var query = "UPDATE ProductBrands SET productId = @productId, brandId = @brandId WHERE id = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@productId", model.productId);
                    command.Parameters.AddWithValue("@brandId", model.brandId);
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
                using (SqlCommand command = new SqlCommand("DELETE FROM ProductBrands WHERE id = @id", connection))
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
