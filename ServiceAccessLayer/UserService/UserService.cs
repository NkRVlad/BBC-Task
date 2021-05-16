using ServiceAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceAccessLayer.UserService
{
    public class UserService : IUserService
    {
        private Dictionary<int, UserModel> _usersDictionaryContext { get; set; }

        public UserService()
        {
            _usersDictionaryContext = new Dictionary<int, UserModel>
            {
                { 1, new UserModel { Role = Role.Guest, Token = Guid.NewGuid().ToString() } },
                { 2, new UserModel { Role = Role.Admin, Token = Guid.NewGuid().ToString() } },
            };
        }

        public UserModel GetUserToken(int id)
        {
            var result = _usersDictionaryContext.FirstOrDefault(k => k.Key == id);

            if(result.Value != null)
            {
                return new UserModel
                {
                    Role = result.Value.Role,
                    Token = result.Value.Token
                };
            }
            else
            {
                return null;
            }
        }
    }
}
