using Microsoft.Extensions.Options;
using ShoppingCart_DAL.Contacts;
using ShoppingCart_DAL.Data;
using ShoppingCart_DAL.Models;
using System.Data.SqlClient;

namespace ShoppingCart_DAL.Repositories
{
    public class CategoriesRepository : IRepository<Categories>
    {
        private readonly Connection _connection;
        public CategoriesRepository(IOptions<Connection> connection)
        {
            _connection = connection.Value;
        }

        public List<Categories> GetAll(int? page)
        {
            List<Categories> categories = new List<Categories>();
            int pageSize = 10;
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            var offset = (pageIndex - 1) * pageSize;
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                var query = "SELECT * FROM Categories ORDER BY id OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@offset", offset);
                    command.Parameters.AddWithValue("@pageSize", pageSize);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Categories model = new Categories();
                            model.id = (int)reader["id"];
                            model.name = (string)reader["name"];
                            model.slug = (string)reader["slug"];
                            model.description = (string)reader["description"];
                            model.metaDescription = (string)reader["metaDescription"];
                            model.metaKeywords = (string)reader["metaKeywords"];
                            model.categoryStatus = (string)reader["categoryStatus"];
                            model.isDeleted = (bool)reader["isDeleted"];
                            model.createdAt = (DateTime)reader["createdAt"];
                            model.updatedAt = (DateTime)reader["updatedAt"];
                            categories.Add(model);
                        }
                    }
                }
                connection.Close();
            }
            return categories;
        }

        public Categories GetById(int id)
        {
            Categories item = new Categories();
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Categories WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        item.id = reader.GetInt32(0);
                        item.name = reader.GetString(1);
                        item.slug = reader.GetString(2);
                        item.description = reader.GetString(3);
                        item.metaDescription = reader.GetString(4);
                        item.metaKeywords = reader.GetString(5);
                        item.categoryStatus = reader.GetString(6);
                        item.isDeleted = reader.GetBoolean(7);
                        item.createdAt = reader.GetDateTime(8);
                        item.updatedAt = reader.GetDateTime(9);
                    }
                    reader.Close();
                }
                connection.Close();
            }
            return item;
        }

        public Categories Create(Categories model)
        {
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                var query = "INSERT INTO Categories (name, slug, description, metaDescription, metaKeywords, categoryStatus,isDeleted) VALUES (@name, @slug, @description, @metaDescription, @metaKeywords, @categoryStatus,@isDeleted)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", model.name);
                    command.Parameters.AddWithValue("@slug", model.slug);
                    command.Parameters.AddWithValue("@description", model.description);
                    command.Parameters.AddWithValue("@metaDescription", model.metaDescription);
                    command.Parameters.AddWithValue("@metaKeywords", model.metaKeywords);
                    command.Parameters.AddWithValue("@categoryStatus", model.categoryStatus);
                    command.Parameters.AddWithValue("@isDeleted", model.isDeleted);
                    command.ExecuteNonQuery();
                }
                connection.Close();
                return model;
            }
        }

        public Categories Update(int id, Categories model)
        {
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                var query = "UPDATE Categories SET name = @name, slug = @slug, description = @description, metaDescription = @metaDescription, metaKeywords = @metaKeywords, categoryStatus = @categoryStatus, isDeleted = @isDeleted WHERE id = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@name", model.name);
                    command.Parameters.AddWithValue("@slug", model.slug);
                    command.Parameters.AddWithValue("@description", model.description);
                    command.Parameters.AddWithValue("@metaDescription", model.metaDescription);
                    command.Parameters.AddWithValue("@metaKeywords", model.metaKeywords);
                    command.Parameters.AddWithValue("@categoryStatus", model.categoryStatus);
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
                using (SqlCommand command = new SqlCommand("DELETE FROM Categories WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    int rows = command.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        return "Category deleted successfully.";
                    }
                }
                connection.Close();
            }
            return "Failed to delete category.";
        }
    }
}
