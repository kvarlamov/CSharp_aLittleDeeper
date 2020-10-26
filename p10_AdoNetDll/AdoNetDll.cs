using System;
using System.Data;
using Microsoft.Data.SqlClient;
using p10_DotNetDbTools;

namespace p10_AdoNetDll
{
    public class AdoNetDll: IDatabase
    {
        private readonly string _connectionString;

        public AdoNetDll(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        
        public User GetUserById(int id)
        {
            string sql = $"SELECT * FROM Users WHERE Id={id}";
            User user = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                var row = ds.Tables[0].Rows[0];
                user = new User
                {
                    Id = (int) row["Id"],
                    Login = row["Login"] as string,
                    Password = row["Password"] as string,
                    FirstName = row["FirstName"] as string,
                    LastName = row["LastName"] as string,
                    Patronymic = row["Patronymic"] as string,
                    CityId = row["CityId"] as int?
                };
            }
            return user;
        }

        public bool CreateUser(User user)
        {
            string sql = "SELECT * FROM Users";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                DataTable dt = ds.Tables[0];
                DataRow row = dt.NewRow();
                row["Login"] = user.Login;
                row["Password"] = user.Password;
                row["FirstName"] = user.FirstName;
                row["LastName"] = user.LastName;
                row["Patronymic"] = user.Patronymic;
                if (user.CityId.HasValue)
                    row["CityId"] = user.CityId;

                dt.Rows.Add(row);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                try
                {
                    adapter.Update(dt);
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            return true;
        }

        public bool DeleteUser(int id)
        {
            string sql = $"DELETE FROM Users WHERE Id={id}";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                int i = command.ExecuteNonQuery();
                if (i > 0)
                    return true;
                return false;
            }
        }

        public bool UpdateUser(User user, int id)
        {
            string sql = $"SELECT * FROM Users WHERE Id={id}";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                DataTable dt = ds.Tables[0];
                var row = dt.Rows[0];
                
                row["Login"] = user.Login;
                row["Password"] = user.Password;
                row["FirstName"] = user.FirstName;
                row["LastName"] = user.LastName;
                row["Patronymic"] = user.Patronymic;
                if (user.CityId.HasValue)
                    row["CityId"] = user.CityId;
                
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                try
                {
                    adapter.Update(ds);
                }
                catch (Exception e)
                {
                    return false;
                } 
            }

            return true;
        }
    }
}