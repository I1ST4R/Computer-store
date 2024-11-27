using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_store.Domain.Entities
{
    public class User : Entity
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public User(string login, string password, string role)
        {
            Login = login;
            Password = password;
            Role = role;
        }

        public User()
        {
            Role = "Seller";
        }
    }
}
