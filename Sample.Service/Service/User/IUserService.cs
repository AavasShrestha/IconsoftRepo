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
    }
}