using Microsoft.Extensions.Options;
using ShoppingCart_DAL.Data;
using ShoppingCart_DAL.Models;
using System.Data.SqlClient;

namespace ShoppingCart_DAL.Repositories
{
    public class ListProCateReposiory
    {
        private readonly Connection _connection;
        public ListProCateReposiory(IOptions<Connection> connection)
        {
            _connection = connection.Value;
        }

        public List<ListProCate> GetAllInnerJoin(int? page)
        {
            List<ListProCate> listProCate = new List<ListProCate>();
            var pageSize = 10;
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                var offset = (pageIndex - 1) * pageSize;
                var query = "SELECT P.*, " +
                    "C.id AS CategoryId, C.name AS CategoryName, C.slug AS CategorySlug, C.description AS CategoryDescription, " +
                    "C.metaDescription AS CategoryMetaDescription, C.metaKeywords AS CategoryMetaKeywords, C.categoryStatus, " +
                    "C.isDeleted AS CategoryIsDeleted, C.createdAt AS CategoryCreatedAt, C.updatedAt AS CategoryUpdatedAt " +
                    "FROM Products AS P " +
                    "INNER JOIN ProductCategories AS PC ON P.id = PC.productId " +
                    "INNER JOIN Categories AS C ON PC.categoryId = C.id " +
                    "ORDER BY P.id OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@offset", offset);
                    command.Parameters.AddWithValue("@pageSize", pageSize);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ListProCate product = new ListProCate
                            {
                                products = new Products
                                {
                                    id = (int)reader["id"],
                                    name = (string)reader["name"],
                                    slug = (string)reader["slug"],
                                    description = (string)reader["description"],
                                    metaDescription = (string)reader["metaDescription"],
                                    metaKeywords = (string)reader["metaKeywords"],
                                    sku = (string)reader["sku"],
                                    model = (string)reader["model"],
                                    price = (decimal)reader["price"],
                                    oldPrice = (decimal)reader["oldPrice"],
                                    imageUrl = (string)reader["imageUrl"],
                                    isBestseller = (bool)reader["isBestseller"],
                                    isFeatured = (bool)reader["isFeatured"],
                                    quantity = (int)reader["quantity"],
                                    productStatus = (string)reader["productStatus"],
                                    isDeleted = (bool)reader["isDeleted"],
                                    createdAt = (DateTime)reader["createdAt"],
                                    updatedAt = (DateTime)reader["updatedAt"]
                                },
                                categories = new Categories
                                {
                                    id = (int)reader["CategoryId"],
                                    name = (string)reader["CategoryName"],
                                    slug = (string)reader["CategorySlug"],
                                    description = (string)reader["CategoryDescription"],
                                    metaDescription = (string)reader["CategoryMetaDescription"],
                                    metaKeywords = (string)reader["CategoryMetaKeywords"],
                                    categoryStatus = (string)reader["categoryStatus"],
                                    isDeleted = (bool)reader["CategoryIsDeleted"],
                                    createdAt = (DateTime)reader["CategoryCreatedAt"],
                                    updatedAt = (DateTime)reader["CategoryUpdatedAt"]
                                }
                            };
                            listProCate.Add(product);
                        }
                    }
                }
                connection.Close();
            }
            return listProCate.ToList();
        }

        public List<ListProCate> GetAllLeftJoin(int? page)
        {
            List<ListProCate> listProCate = new List<ListProCate>();
            var pageSize = 10;
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                var offset = (pageIndex - 1) * pageSize;
                var query = "SELECT P.*, " +
                    "C.id AS CategoryId, C.name AS CategoryName, C.slug AS CategorySlug, C.description AS CategoryDescription, " +
                    "C.metaDescription AS CategoryMetaDescription, C.metaKeywords AS CategoryMetaKeywords, C.categoryStatus, " +
                    "C.isDeleted AS CategoryIsDeleted, C.createdAt AS CategoryCreatedAt, C.updatedAt AS CategoryUpdatedAt " +
                    "FROM Products AS P " +
                    "LEFT JOIN ProductCategories AS PC ON P.id = PC.productId " +
                    "LEFT JOIN Categories AS C ON PC.categoryId = C.id " +
                    "ORDER BY P.id OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@offset", offset);
                    command.Parameters.AddWithValue("@pageSize", pageSize);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ListProCate product = new ListProCate
                            {
                                products = new Products
                                {
                                    id = (int)reader["id"],
                                    name = (string)reader["name"],
                                    slug = (string)reader["slug"],
                                    description = (string)reader["description"],
                                    metaDescription = (string)reader["metaDescription"],
                                    metaKeywords = (string)reader["metaKeywords"],
                                    sku = (string)reader["sku"],
                                    model = (string)reader["model"],
                                    price = (decimal)reader["price"],
                                    oldPrice = (decimal)reader["oldPrice"],
                                    imageUrl = (string)reader["imageUrl"],
                                    isBestseller = (bool)reader["isBestseller"],
                                    isFeatured = (bool)reader["isFeatured"],
                                    quantity = (int)reader["quantity"],
                                    productStatus = (string)reader["productStatus"],
                                    isDeleted = (bool)reader["isDeleted"],
                                    createdAt = (DateTime)reader["createdAt"],
                                    updatedAt = (DateTime)reader["updatedAt"]
                                },
                                categories = null
                            };
                            if (reader["CategoryId"] != DBNull.Value)
                            {
                                product.categories = new Categories
                                {
                                    id = (int)reader["CategoryId"],
                                    name = (string)reader["CategoryName"],
                                    slug = (string)reader["CategorySlug"],
                                    description = (string)reader["CategoryDescription"],
                                    metaDescription = (string)reader["CategoryMetaDescription"],
                                    metaKeywords = (string)reader["CategoryMetaKeywords"],
                                    categoryStatus = (string)reader["categoryStatus"],
                                    isDeleted = (bool)reader["CategoryIsDeleted"],
                                    createdAt = (DateTime)reader["CategoryCreatedAt"],
                                    updatedAt = (DateTime)reader["CategoryUpdatedAt"]
                                };
                            }
                            listProCate.Add(product);
                        }
                    }
                }
                connection.Close();
            }
            return listProCate.ToList();
        }

        public List<ListProCate> GetAllRightJoin(int? page)
        {
            List<ListProCate> listProCate = new List<ListProCate>();
            var pageSize = 10;
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                var offset = (pageIndex - 1) * pageSize;
                var query = "SELECT P.*, " +
                    "C.id AS CategoryId, C.name AS CategoryName, C.slug AS CategorySlug, C.description AS CategoryDescription, " +
                    "C.metaDescription AS CategoryMetaDescription, C.metaKeywords AS CategoryMetaKeywords, C.categoryStatus, " +
                    "C.isDeleted AS CategoryIsDeleted, C.createdAt AS CategoryCreatedAt, C.updatedAt AS CategoryUpdatedAt " +
                    "FROM Products AS P " +
                    "RIGHT JOIN ProductCategories AS PC ON P.id = PC.productId " +
                    "RIGHT JOIN Categories AS C ON PC.categoryId = C.id " +
                    "ORDER BY P.id OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@offset", offset);
                    command.Parameters.AddWithValue("@pageSize", pageSize);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ListProCate product = new ListProCate
                            {
                                products = null,
                                categories = new Categories
                                {
                                    id = (int)reader["CategoryId"],
                                    name = (string)reader["CategoryName"],
                                    slug = (string)reader["CategorySlug"],
                                    description = (string)reader["CategoryDescription"],
                                    metaDescription = (string)reader["CategoryMetaDescription"],
                                    metaKeywords = (string)reader["CategoryMetaKeywords"],
                                    categoryStatus = (string)reader["categoryStatus"],
                                    isDeleted = (bool)reader["CategoryIsDeleted"],
                                    createdAt = (DateTime)reader["CategoryCreatedAt"],
                                    updatedAt = (DateTime)reader["CategoryUpdatedAt"]
                                }
                            };
                            if (reader["id"] != DBNull.Value)
                            {
                                product.products = new Products
                                {
                                    id = (int)reader["id"],
                                    name = (string)reader["name"],
                                    slug = (string)reader["slug"],
                                    description = (string)reader["description"],
                                    metaDescription = (string)reader["metaDescription"],
                                    metaKeywords = (string)reader["metaKeywords"],
                                    sku = (string)reader["sku"],
                                    model = (string)reader["model"],
                                    price = (decimal)reader["price"],
                                    oldPrice = (decimal)reader["oldPrice"],
                                    imageUrl = (string)reader["imageUrl"],
                                    isBestseller = (bool)reader["isBestseller"],
                                    isFeatured = (bool)reader["isFeatured"],
                                    quantity = (int)reader["quantity"],
                                    productStatus = (string)reader["productStatus"],
                                    isDeleted = (bool)reader["isDeleted"],
                                    createdAt = (DateTime)reader["createdAt"],
                                    updatedAt = (DateTime)reader["updatedAt"]
                                };
                            }
                            listProCate.Add(product);
                        }
                    }
                }
                connection.Close();
            }
            return listProCate.ToList();
        }
    }
}
