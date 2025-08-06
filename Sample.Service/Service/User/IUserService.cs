using CBS.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBS.Service
{
    public interface IUserService
    {
        UserDetail GetLoggedInUserDetail(string userName, string encryptedPassword);
        bool UserAuthentication(int userId, string password);



        // New CRUD methods:
        UserDetail CreateUser(int userId, UserDetail userDetail);
        UserDetail UpdateUser(int id, UserDetail userDetail);
        bool DeleteUser(int id);
        IEnumerable<UserDetail> GetAllUsers();
        UserDetail GetUserById(int id);
    }
}