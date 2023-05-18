using Microsoft.Extensions.Options;
using ShoppingCart_DAL.Contacts;
using ShoppingCart_DAL.Data;
using ShoppingCart_DAL.Models;
using System.Data.SqlClient;
using X.PagedList;

namespace ShoppingCart_DAL.Repositories
{
    public class CustomerAddressesRepository : IRepository<CustomerAddresses>
    {
        private readonly Connection _connection;
        public CustomerAddressesRepository(IOptions<Connection> connection)
        {
            _connection = connection.Value;
        }

        public List<CustomerAddresses> GetAll(int? page)
        {
            List<CustomerAddresses> customerAddresses = new List<CustomerAddresses>();
            var pageSize = 10;
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                var offset = (pageIndex - 1) * pageSize;
                using (SqlCommand command = new SqlCommand("SELECT * FROM CustomerAddresses ORDER BY id OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY", connection))
                {
                    command.Parameters.AddWithValue("@offset", offset);
                    command.Parameters.AddWithValue("@pageSize", pageSize);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CustomerAddresses model = new CustomerAddresses();
                            model.id = (int)reader["id"];
                            model.customerId = (int)reader["customerId"];
                            model.addressId = (int)reader["addressId"];
                            model.createdAt = (DateTime)reader["createdAt"];
                            model.updatedAt = (DateTime)reader["updatedAt"];
                            customerAddresses.Add(model);
                        }
                    }
                }
                connection.Close();
            }
            var customerAddress = customerAddresses.ToPagedList(pageIndex, pageSize);
            return customerAddress.ToList();
        }

        public CustomerAddresses GetById(int id)
        {
            CustomerAddresses model = new CustomerAddresses();
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM CustomerAddresses WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        model.id = reader.GetInt32(0);
                        model.customerId = reader.GetInt32(1);
                        model.addressId = reader.GetInt32(2);
                        model.createdAt = reader.GetDateTime(3);
                        model.updatedAt = reader.GetDateTime(4);
                    }
                    reader.Close();
                }
                connection.Close();
            }
            return model;
        }

        public CustomerAddresses Create(CustomerAddresses model)
        {
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                var query = "INSERT INTO CustomerAddresses  ( customerId, addressId) VALUES ( @customerId, @addressId)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@customerId", model.customerId);
                    command.Parameters.AddWithValue("@addressId", model.addressId);
                    command.ExecuteNonQuery();
                }
                connection.Close();
                return model;
            }
        }

        public CustomerAddresses Update(int id, CustomerAddresses model)
        {
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                var query = "UPDATE CustomerAddresses SET customerId = @customerId, addressId = @addressId WHERE id = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@customerId", model.customerId);
                    command.Parameters.AddWithValue("@addressId", model.addressId);
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
                using (SqlCommand command = new SqlCommand("DELETE FROM CustomerAddresses WHERE id = @id", connection))
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
