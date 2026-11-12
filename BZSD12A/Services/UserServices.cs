using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BZSD12A.Models;
using MySql.Data.MySqlClient;

namespace BZSD12A.Services
{
    internal class UserServices
    {
        MySqlConnection _conn;
        public UserServices(MySqlConnection conn) {
            _conn = conn;
        }

        public List<User> GetUsers()
        {
            try
            {
                List<User> users = new List<User>();
                string sql = "SELECT * FROM user";
                MySqlCommand cmd = new MySqlCommand(sql, _conn);
                _conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        User user = new User
                        {
                            Id = reader.GetInt32("ID"),
                            Name = reader.GetString("Name"),
                            Age = reader.GetInt32("Age"),
                            Role = reader.GetString("Role"),
                            IdentityCardNumber = reader.GetString("IdentityCardNum"),
                            Password = reader.GetString("Password")
                        };
                        users.Add(user);
                    }
                }
                _conn.Close();
                return users;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<User>();
            }
        }
    }
}
