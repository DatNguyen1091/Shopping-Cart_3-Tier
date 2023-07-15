using Microsoft.Extensions.Options;
using ShoppingCart_DAL.Contacts;
using ShoppingCart_DAL.Data;
using ShoppingCart_DAL.Models;
using System.Data.SqlClient;

namespace ShoppingCart_DAL.Repositories
{
    public class CustomersRepository : IRepository<Customers>
    {
        private readonly Connection _connection;
        public CustomersRepository(IOptions<Connection> connection)
        {
            _connection = connection.Value;
        }

        public List<Customers> GetAll(int? page)
        {
            List<Customers> customers = new List<Customers>();
            var pageSize = 10;
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                var offset = (pageIndex - 1) * pageSize;
                using (SqlCommand command = new SqlCommand("SELECT * FROM Customers ORDER BY id OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY", connection))
                {
                    command.Parameters.AddWithValue("@offset", offset);
                    command.Parameters.AddWithValue("@pageSize", pageSize);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Customers model = new Customers();
                            model.id = (int)reader["id"];
                            model.personId = (int)reader["personId"];
                            model.isDeleted = (bool)reader["isDeleted"];
                            model.createdAt = (DateTime)reader["createdAt"];
                            model.updatedAt = (DateTime)reader["updatedAt"];
                            customers.Add(model);
                        }
                    }
                }
                connection.Close();
            }
            return customers.ToList();
        }

        public Customers GetById(int id)
        {
            Customers model = new Customers();
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Customers WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        model.id = reader.GetInt32(0);
                        model.personId = reader.GetInt32(1);
                        model.isDeleted = reader.GetBoolean(2);
                        model.createdAt = reader.GetDateTime(3);
                        model.updatedAt = reader.GetDateTime(4);
                    }
                    reader.Close();
                }
                connection.Close();
            }
            return model;
        }

        public Customers Create(Customers model)
        {
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                var query = "INSERT INTO Customers  ( personId, isDeleted) VALUES ( @personId, @isDeleted)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@personId", model.personId);
                    command.Parameters.AddWithValue("@isDeleted", model.isDeleted);
                    command.ExecuteNonQuery();
                }
                connection.Close();
                return model;
            }
        }

        public Customers Update(int id, Customers model)
        {
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                var query = "UPDATE Customers SET personId = @personId, isDeleted = @isDeleted WHERE id = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@personId", model.personId);
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
                using (SqlCommand command = new SqlCommand("DELETE FROM Customers WHERE id = @id", connection))
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
