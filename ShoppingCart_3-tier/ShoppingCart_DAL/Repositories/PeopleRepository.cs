using Microsoft.Extensions.Options;
using ShoppingCart_DAL.Contacts;
using ShoppingCart_DAL.Data;
using ShoppingCart_DAL.Models;
using System.Data.SqlClient;
using X.PagedList;

namespace ShoppingCart_DAL.Repositories
{
    public class PeopleRepository : IRepository<People>
    {
        private readonly Connection _connection;
        public PeopleRepository(IOptions<Connection> connection)
        {
            _connection = connection.Value;
        }

        public List<People> GetAll(int? page)
        {
            List<People> peoples = new List<People>();
            var pageSize = 10;
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                var offset = (pageIndex - 1) * pageSize;
                using (SqlCommand command = new SqlCommand("SELECT * FROM People ORDER BY id OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY", connection))
                {
                    command.Parameters.AddWithValue("@offset", offset);
                    command.Parameters.AddWithValue("@pageSize", pageSize);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            People model = new People();
                            model.id = (int)reader["id"];
                            model.firstName = (string)reader["firstName"];
                            model.middleName = (string)reader["middleName"];
                            model.lastName = (string)reader["lastName"];
                            model.emailAddress = (string)reader["emailAddress"];
                            model.phoneNumber = (string)reader["phoneNumber"];
                            model.gender = (string)reader["gender"];
                            model.dateOfBirth = (DateTime)reader["dateOfBirth"];
                            model.isDeleted = (bool)reader["isDeleted"];
                            model.createdAt = (DateTime)reader["createdAt"];
                            model.updatedAt = (DateTime)reader["updatedAt"];
                            peoples.Add(model);
                        }
                    }
                }
                connection.Close();
            }
            var people = peoples.ToPagedList(pageIndex, pageSize);
            return people.ToList();
        }

        public People GetById(int id)
        {
            People model = new People();
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM People WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        model.id = reader.GetInt32(0);
                        model.firstName = reader.GetString(1);
                        model.middleName = reader.GetString(2);
                        model.lastName = reader.GetString(3);
                        model.emailAddress = reader.GetString(4);
                        model.phoneNumber = reader.GetString(5);
                        model.gender = reader.GetString(6);
                        model.dateOfBirth = reader.GetDateTime(7);
                        model.isDeleted = reader.GetBoolean(8);
                        model.createdAt = reader.GetDateTime(9);
                        model.updatedAt = reader.GetDateTime(10);
                    }
                    reader.Close();
                }
                connection.Close();
            }
            return model;
        }

        public People Create(People model)
        {
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                var query = "INSERT INTO People ( firstName, middleName, lastName, emailAddress, phoneNumber, gender, dateOfBirth, isDeleted) VALUES ( @firstName, @middleName, @lastName, @emailAddress, @phoneNumber, @gender, @dateOfBirth, @isDeleted)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@firstName", model.firstName);
                    command.Parameters.AddWithValue("@middleName", model.middleName);
                    command.Parameters.AddWithValue("@lastName", model.lastName);
                    command.Parameters.AddWithValue("@emailAddress", model.emailAddress);
                    command.Parameters.AddWithValue("@phoneNumber", model.phoneNumber);
                    command.Parameters.AddWithValue("@gender", model.gender);
                    command.Parameters.AddWithValue("@dateOfBirth", model.dateOfBirth);
                    command.Parameters.AddWithValue("@isDeleted", model.isDeleted);
                    command.ExecuteNonQuery();
                }
                connection.Close();
                return model;
            }
        }

        public People Update(int id, People model)
        {
            using (SqlConnection connection = new SqlConnection(_connection.SQLString))
            {
                connection.Open();
                var query = "UPDATE People SET firstName = @firstName, middleName = @middleName, lastName = @lastName, emailAddress = @emailAddress, phoneNumber = @phoneNumber, gender = @gender, dateOfBirth = @dateOfBirth, isDeleted = @isDeleted WHERE id = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@firstName", model.firstName);
                    command.Parameters.AddWithValue("@middleName", model.middleName);
                    command.Parameters.AddWithValue("@lastName", model.lastName);
                    command.Parameters.AddWithValue("@emailAddress", model.emailAddress);
                    command.Parameters.AddWithValue("@phoneNumber", model.phoneNumber);
                    command.Parameters.AddWithValue("@gender", model.gender);
                    command.Parameters.AddWithValue("@dateOfBirth", model.dateOfBirth);
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
                using (SqlCommand command = new SqlCommand("DELETE FROM People WHERE id = @id", connection))
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
