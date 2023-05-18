using Microsoft.Extensions.Options;
using ShoppingCart_DAL.Contacts;
using ShoppingCart_DAL.Data;
using ShoppingCart_DAL.Models;
using System.Data.SqlClient;
using X.PagedList;

namespace ShoppingCart_DAL.Repositories
{
    public class ProductsRepository : IRepository<Products>
    {
        private readonly Connection _connection;
        public ProductsRepository(IOptions<Connection> connection)
        {
            _connection = connection.Value;
        }

        public List<Products> GetAll(int? page)
        {
            List<Products> products = new List<Products>();
            var pageSize = 10;
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                var offset = (pageIndex - 1) * pageSize;
                using (SqlCommand command = new SqlCommand("SELECT * FROM Products ORDER BY id OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY", connection))
                {
                    command.Parameters.AddWithValue("@offset", offset);
                    command.Parameters.AddWithValue("@pageSize", pageSize);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Products model = new Products();
                            model.id = (int)reader["id"];
                            model.name = (string)reader["name"];
                            model.slug = (string)reader["slug"];
                            model.description = (string)reader["description"];
                            model.metaDescription = (string)reader["metaDescription"];
                            model.metaKeywords = (string)reader["metaKeywords"];
                            model.sku = (string)reader["sku"];
                            model.model = (string)reader["model"];
                            model.price = (decimal)reader["price"];
                            model.oldPrice = (decimal)reader["oldPrice"];
                            model.imageUrl = (string)reader["imageUrl"];
                            model.isBestseller = (bool)reader["isBestseller"];
                            model.isFeatured = (bool)reader["isFeatured"];
                            model.quantity = (int)reader["quantity"];
                            model.productStatus = (string)reader["productStatus"];
                            model.isDeleted = (bool)reader["isDeleted"];
                            model.createdAt = (DateTime)reader["createdAt"];
                            model.updatedAt = (DateTime)reader["updatedAt"];
                            products.Add(model);
                        }
                        connection.Close();
                    }
                }
            }
            var items = products.ToPagedList(pageIndex, pageSize);
            return items.ToList();
        }

        public Products GetById(int id)
        {
            Products item = new Products();
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Products WHERE id = @id", connection))
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
                        item.sku = reader.GetString(6);
                        item.model = reader.GetString(7);
                        item.price = reader.GetDecimal(8);
                        item.oldPrice = reader.GetDecimal(9);
                        item.imageUrl = reader.GetString(10);
                        item.isBestseller = reader.GetBoolean(11);
                        item.isFeatured = reader.GetBoolean(12);
                        item.quantity = reader.GetInt32(13);
                        item.productStatus = reader.GetString(14);
                        item.isDeleted = reader.GetBoolean(15);
                        item.createdAt = reader.GetDateTime(16);
                        item.updatedAt = reader.GetDateTime(17);
                    }
                    reader.Close();
                }
                connection.Close();
            }
            return item;
        }

        public Products Create(Products model)
        {
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                var query = "INSERT INTO Products (name, slug, description, metaDescription, metaKeywords, sku, model, price, oldPrice, imageUrl, isBestseller, isFeatured, quantity, productStatus, isDeleted) VALUES (@name, @slug, @description, @metaDescription, @metaKeywords, @sku, @model, @price, @oldPrice, @imageUrl, @isBestseller, @isFeatured, @quantity, @productStatus, @isDeleted)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", model.name);
                    command.Parameters.AddWithValue("@slug", model.slug);
                    command.Parameters.AddWithValue("@description", model.description);
                    command.Parameters.AddWithValue("@metaDescription", model.metaDescription);
                    command.Parameters.AddWithValue("@metaKeywords", model.metaKeywords);
                    command.Parameters.AddWithValue("@sku", model.sku);
                    command.Parameters.AddWithValue("@model", model.model);
                    command.Parameters.AddWithValue("@price", model.price);
                    command.Parameters.AddWithValue("@oldPrice", model.oldPrice);
                    command.Parameters.AddWithValue("@quantity", model.quantity);
                    command.Parameters.AddWithValue("@imageUrl", model.imageUrl);
                    command.Parameters.AddWithValue("@isBestseller", model.isBestseller);
                    command.Parameters.AddWithValue("@isFeatured", model.isFeatured);
                    command.Parameters.AddWithValue("@productStatus", model.productStatus);
                    command.Parameters.AddWithValue("@isDeleted", model.isDeleted);
                    command.ExecuteNonQuery();
                }
                connection.Close();
                return model;
            }
        }

        public Products Update(int id, Products model)
        {
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                var query = "UPDATE Products SET name = @name, slug = @slug, description = @description, metaDescription = @metaDescription, metaKeywords = @metaKeywords, sku = @sku, model = @model, price = @price, oldPrice = @oldPrice, imageUrl = @imageUrl, isBestseller = @isBestseller, isFeatured = @isFeatured, quantity = @quantity, productStatus = @productStatus, isDeleted = @isDeleted WHERE id = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@name", model.name);
                    command.Parameters.AddWithValue("@slug", model.slug);
                    command.Parameters.AddWithValue("@description", model.description);
                    command.Parameters.AddWithValue("@metaDescription", model.metaDescription);
                    command.Parameters.AddWithValue("@metaKeywords", model.metaKeywords);
                    command.Parameters.AddWithValue("@sku", model.sku);
                    command.Parameters.AddWithValue("@model", model.model);
                    command.Parameters.AddWithValue("@price", model.price);
                    command.Parameters.AddWithValue("@oldPrice", model.oldPrice);
                    command.Parameters.AddWithValue("@quantity", model.quantity);
                    command.Parameters.AddWithValue("@imageUrl", model.imageUrl);
                    command.Parameters.AddWithValue("@isBestseller", model.isBestseller);
                    command.Parameters.AddWithValue("@isFeatured", model.isFeatured);
                    command.Parameters.AddWithValue("@productStatus", model.productStatus);
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
                using (SqlCommand command = new SqlCommand("DELETE FROM Products WHERE id = @id", connection))
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
