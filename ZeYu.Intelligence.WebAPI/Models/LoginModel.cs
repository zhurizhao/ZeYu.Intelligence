using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZeYu.Intelligence.WebAPI.Models
{
    public class LoginModel
    {
        public string Account { get; set; }
        public string Password { get; set; }
    }

    public class LoginModel2
    {
        public string email { get; set; }
        public string password { get; set; }
        public bool rememberMe { get; set; }
    }
}
