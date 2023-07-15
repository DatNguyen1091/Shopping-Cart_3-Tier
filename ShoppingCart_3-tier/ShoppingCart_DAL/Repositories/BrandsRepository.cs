using Microsoft.Extensions.Options;
using ShoppingCart_DAL.Contacts;
using ShoppingCart_DAL.Data;
using ShoppingCart_DAL.Models;
using System.Data.SqlClient;

namespace ShoppingCart_DAL.Repositories
{
    public class BrandsRepository : IRepository<Brands>
    {
        private readonly Connection _connection;
        public BrandsRepository(IOptions<Connection> connection)
        {
            _connection = connection.Value;
        }

        public List<Brands> GetAll(int? page)
        {
            List<Brands> brands = new List<Brands>();
            var pageSize = 10;
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                var offset = (pageIndex - 1) * pageSize;
                using (SqlCommand command = new SqlCommand("SELECT * FROM Brands ORDER BY id OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY", connection))
                {
                    command.Parameters.AddWithValue("@offset", offset);
                    command.Parameters.AddWithValue("@pageSize", pageSize);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Brands model = new Brands();
                            model.id = (int)reader["id"];
                            model.name = (string)reader["name"];
                            model.slug = (string)reader["slug"];
                            model.description = (string)reader["description"];
                            model.metaDescription = (string)reader["metaDescription"];
                            model.metaKeywords = (string)reader["metaKeywords"];
                            model.brandStatus = (string)reader["brandStatus"];
                            model.isDelete = (bool)reader["isDeleted"];
                            model.createdAt = (DateTime)reader["createdAt"];
                            model.updatedAt = (DateTime)reader["updatedAt"];
                            brands.Add(model);
                        }
                    }
                }
                connection.Close();
            }
            return brands.ToList();
        }

        public Brands GetById(int id)
        {
            Brands model = new Brands();
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Brands WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        model.id = reader.GetInt32(0);
                        model.name = reader.GetString(1);
                        model.slug = reader.GetString(2);
                        model.description = reader.GetString(3);
                        model.metaDescription = reader.GetString(4);
                        model.metaKeywords = reader.GetString(5);
                        model.brandStatus = reader.GetString(6);
                        model.isDelete = reader.GetBoolean(7);
                        model.createdAt = reader.GetDateTime(8);
                        model.updatedAt = reader.GetDateTime(9);
                    }
                    reader.Close();
                }
                connection.Close();
            }
            return model;
        }

        public Brands Create(Brands model)
        {
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                var query = "INSERT INTO Brands ( name, slug, description, metaDescription, metaKeywords, brandStatus, isDeleted) VALUES ( @name, @slug, @description, @metaDescription, @metaKeywords, @brandStatus, @isDelete)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", model.name);
                    command.Parameters.AddWithValue("@slug", model.slug);
                    command.Parameters.AddWithValue("@description", model.description);
                    command.Parameters.AddWithValue("@metaDescription", model.metaDescription);
                    command.Parameters.AddWithValue("@metaKeywords", model.metaKeywords);
                    command.Parameters.AddWithValue("@brandStatus", model.brandStatus);
                    command.Parameters.AddWithValue("@isDelete", model.isDelete);
                    command.ExecuteNonQuery();
                }
                connection.Close();
                return model;
            }
        }

        public Brands Update(int id, Brands model)
        {
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                var query = "UPDATE Brands SET name = @name, slug = @slug, description = @description, metaDescription = @metaDescription, metaKeywords = @metaKeywords, brandStatus = @brandStatus, isDeleted = @isDelete WHERE id = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@name", model.name);
                    command.Parameters.AddWithValue("@slug", model.slug);
                    command.Parameters.AddWithValue("@description", model.description);
                    command.Parameters.AddWithValue("@metaDescription", model.metaDescription);
                    command.Parameters.AddWithValue("@metaKeywords", model.metaKeywords);
                    command.Parameters.AddWithValue("@brandStatus", model.brandStatus);
                    command.Parameters.AddWithValue("@isDelete", model.isDelete);
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
                using (SqlCommand command = new SqlCommand("DELETE FROM Brands WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    int rows = command.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        return "Brand deleted successfully.";
                    }
                }
                connection.Close();
            }
            return "Failed to delete brand.";
        }
    }
}
