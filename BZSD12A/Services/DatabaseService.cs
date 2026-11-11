using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BZSD12A.Services
{
    internal class DatabaseService
    {
        private readonly string _connectionString = "SERVER = localhost;" +
            "DATABASE = cardb;" +
            "USER = root;" +
            "PASSWORD =;";
        public MySqlConnection CreateConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        public DatabaseService() { }
    }
}
