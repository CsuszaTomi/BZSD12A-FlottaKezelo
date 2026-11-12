using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using BZSD12A.Encryption;
using BZSD12A.Models;
using MySql.Data.MySqlClient;

namespace BZSD12A.Services
{
    internal class UserServices
    {
        private readonly MySqlConnection _conn;

        public UserServices(MySqlConnection conn)
        {
            _conn = conn;
        }

        public List<User> GetUsers()
        {
            try
            {
                List<User> users = new List<User>();
                string sql = "SELECT * FROM user";
                MySqlCommand cmd = new MySqlCommand(sql, _conn);

                if (_conn.State != ConnectionState.Open) _conn.Open();

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
                return users;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<User>();
            }
            finally
            {
                if (_conn.State == ConnectionState.Open) _conn.Close();
            }
        }
        public bool AddUser(User newUser)
        {
            try
            {
                string hashedPassword = PasswordEncrypter.HashPassword(newUser.Password);

                string sql = @"INSERT INTO user (Name, Age, Role, IdentityCardNum, Password) 
                               VALUES (@Name, @Age, @Role, @IdCard, @Password)";

                MySqlCommand cmd = new MySqlCommand(sql, _conn);
                cmd.Parameters.AddWithValue("@Name", newUser.Name);
                cmd.Parameters.AddWithValue("@Age", newUser.Age);
                cmd.Parameters.AddWithValue("@Role", newUser.Role);
                cmd.Parameters.AddWithValue("@IdCard", newUser.IdentityCardNumber);
                cmd.Parameters.AddWithValue("@Password", hashedPassword);

                if (_conn.State != ConnectionState.Open) _conn.Open();

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open) _conn.Close();
            }
        }
        public bool Login(string username, string rawPassword)
        {
            try
            {
                string sql = "SELECT Password FROM user WHERE Name = @Name";
                MySqlCommand cmd = new MySqlCommand(sql, _conn);
                cmd.Parameters.AddWithValue("@Name", username);

                if (_conn.State != ConnectionState.Open) _conn.Open();

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string storedHash = reader.GetString("Password");
                        return PasswordEncrypter.VerifyPassword(rawPassword, storedHash);
                    }
                }
                return false; // Nincs ilyen felhasználó
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open) _conn.Close();
            }
        }
    }
}