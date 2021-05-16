using ServiceAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceAccessLayer.UserService
{
    public interface IUserService
    {
        UserModel GetUserToken(int id);
    }
}
