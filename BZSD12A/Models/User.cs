using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BZSD12A.Models
{
    internal class User
    {
        public User(int id, string name, int age, string role, string identityCardNumber, string password)
        {
            Id = id;
            Name = name;
            Age = age;
            Role = role;
            IdentityCardNumber = identityCardNumber;
            Password = password;
        }

        public User() { }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Role { get; set; }
        public string IdentityCardNumber { get; set; }
        public string Password { get; set; }

    }
}
