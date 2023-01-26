using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalShopWpf.Model
{
    internal class Users
    {
        public int id { get; set; }
        public string email { get; set; }
        public string login { get; set; }
        public string password { get; set; }

        public Users()
        {
        }

        public Users(string email, string login, string password)
        {
            this.email = email;
            this.login = login;
            this.password = password;
        }
    }
}
