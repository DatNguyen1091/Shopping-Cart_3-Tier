using Microsoft.Extensions.Options;
using ShoppingCart_DAL.Data;
using ShoppingCart_DAL.Models;
using System.Data.SqlClient;

namespace ShoppingCart_DAL.Repositories
{
    public class UsersJWTRepository
    {
        private readonly Connection _connection;
        public UsersJWTRepository(IOptions<Connection> connection)
        {
            _connection = connection.Value;
        }

        public UsersJWT GetUserByUsername(string username)
        {
            UsersJWT? account = null;
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM UsersJWT WHERE Username = @username", connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        account = new UsersJWT
                        {
                            id = reader.GetInt32(0),
                            Username = reader.GetString(1),
                            Password = reader.GetString(2),
                        };
                    }
                    reader.Close();
                }
            }
            return account!;
        }

        public UsersJWT CreatNewUserAcc(UsersJWT acc)
        {
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                var query = "INSERT INTO UsersJWT ( Username, Password, Email) VALUES ( @Username, @Password, @Email)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", acc.Username);
                    command.Parameters.AddWithValue("@Password", acc.Password);
                    command.Parameters.AddWithValue("@Email", acc.Email);
                    command.ExecuteNonQuery();
                }
                connection.Close();
                return acc;
            }
        }
    }
}
