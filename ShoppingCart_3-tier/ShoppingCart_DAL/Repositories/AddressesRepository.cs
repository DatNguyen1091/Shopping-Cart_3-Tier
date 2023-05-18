using System.Data.SqlClient;
using ShoppingCart_DAL.Models;
using ShoppingCart_DAL.Data;
using X.PagedList;
using ShoppingCart_DAL.Contacts;
using Microsoft.Extensions.Options;

namespace ShoppingCart_DAL.Repositories
{
    public class AddressesRepository : IRepository<Addresses>
    {   
        private readonly Connection _connection;
        public AddressesRepository(IOptions<Connection> connection)
        {
            _connection = connection.Value;
        }

        public List<Addresses> GetAll(int? page)
        {
            List<Addresses> addresses = new List<Addresses>();
            var pageSize = 10;
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                var offset = (pageIndex - 1) * pageSize;
                using (SqlCommand command = new SqlCommand("SELECT * FROM Addresses ORDER BY id OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY", connection))
                {
                    command.Parameters.AddWithValue("@offset", offset);
                    command.Parameters.AddWithValue("@pageSize", pageSize);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Addresses model = new Addresses();
                            model.id = (int)reader["id"];
                            model.name = (string)reader["name"];
                            model.addressLine1 = (string)reader["addressLine1"];
                            model.addressLine2 = (string)reader["addressLine2"];
                            model.city = (string)reader["city"];
                            model.state = (string)reader["state"];
                            model.country = (string)reader["country"];
                            model.zipCode = (string)reader["zipCode"];
                            model.addressType = (string)reader["addressType"];
                            model.isDeleted = (bool)reader["isDeleted"];
                            model.createdAt = (DateTime)reader["createdAt"];
                            model.updatedAt = (DateTime)reader["updatedAt"];
                            addresses.Add(model);
                        }
                        reader.Close();
                    }
                }
                connection.Close();
            }
            var address = addresses.ToPagedList(pageIndex, pageSize);
            return address.ToList();
        }

        public Addresses GetById(int id)
        {
            Addresses model = new Addresses();
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {   
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Addresses WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        model.id = reader.GetInt32(0);
                        model.name = reader.GetString(1);
                        model.addressLine1 = reader.GetString(2);
                        model.addressLine2 = reader.GetString(3);
                        model.city = reader.GetString(4);
                        model.state = reader.GetString(5);
                        model.country = reader.GetString(6);
                        model.zipCode = reader.GetString(7);
                        model.addressType = reader.GetString(8);
                        model.isDeleted = reader.GetBoolean(9);
                        model.createdAt = reader.GetDateTime(10);
                        model.updatedAt = reader.GetDateTime(11);
                    }
                    reader.Close();
                }
                connection.Close();
            }
            return model;
        }

        public Addresses Create(Addresses model)
        {
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                var query = "INSERT INTO Addresses ( name, addressLine1, addressLine2, city, state, country, zipCode, addressType, isDeleted) VALUES ( @name, @addressLine1, @addressLine2, @city, @state, @country, @zipCode, @addressType, @isDeleted)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", model.name);
                    command.Parameters.AddWithValue("@addressLine1", model.addressLine1);
                    command.Parameters.AddWithValue("@addressLine2", model.addressLine2);
                    command.Parameters.AddWithValue("@city", model.city);
                    command.Parameters.AddWithValue("@state", model.state);
                    command.Parameters.AddWithValue("@country", model.country);
                    command.Parameters.AddWithValue("@zipCode", model.zipCode);
                    command.Parameters.AddWithValue("@addressType", model.addressType);
                    command.Parameters.AddWithValue("@isDeleted", model.isDeleted);
                    command.ExecuteNonQuery();
                }
                connection.Close();
                return model; 
            }
        }

        public Addresses Update(int id, Addresses model)
        {
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                var query = "UPDATE Addresses SET name = @name, addressLine1 = @addressLine1, addressLine2 = @addressLine2, city = @city, state = @state, country = @country, zipCode = @zipCode, addressType = @addressType, isDeleted = @isDeleted WHERE id = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@name", model.name);
                    command.Parameters.AddWithValue("@addressLine1", model.addressLine1);
                    command.Parameters.AddWithValue("@addressLine2", model.addressLine2);
                    command.Parameters.AddWithValue("@city", model.city);
                    command.Parameters.AddWithValue("@state", model.state);
                    command.Parameters.AddWithValue("@country", model.country);
                    command.Parameters.AddWithValue("@zipCode", model.zipCode);
                    command.Parameters.AddWithValue("@addressType", model.addressType);
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
                using (SqlCommand command = new SqlCommand("DELETE FROM Addresses WHERE id = @id", connection))
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
