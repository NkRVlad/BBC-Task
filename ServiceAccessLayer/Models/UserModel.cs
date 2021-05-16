using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceAccessLayer.Models
{
    public class UserModel
    {
        public string Token { get; set; }
        public Role Role { get; set; }
    }
}
