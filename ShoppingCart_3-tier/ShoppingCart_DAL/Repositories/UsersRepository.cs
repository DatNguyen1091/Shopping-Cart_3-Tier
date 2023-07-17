using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using ShoppingCart_DAL.Data;
using ShoppingCart_DAL.Models;
using System.Data.SqlClient;

namespace ShoppingCart_DAL.Repositories
{
    public class UsersRepository 
    {
        private readonly Connection _connection;
        public UsersRepository(IOptions<Connection> connection)
        {
            _connection = connection.Value;
        }

        public Users GetUserByUsername(string username)
        {
            Users? user = null;
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Users WHERE Username = @username", connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        user = new Users
                        {
                            id = reader.GetInt32(0),
                            Username = reader.GetString(1),
                            Password = reader.GetString(2),
                            Email = reader.GetString(3),
                        };
                    }
                    reader.Close();
                }
            }
            return user!;
        }
    }
}

