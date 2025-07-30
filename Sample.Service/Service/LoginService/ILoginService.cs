using CBS.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBS.Service.Service
{
    public interface ILoginService
    {
        (bool isLoginSuccess, UserDetail userDetail) GetLoggedInUserDetail(string userName, string password, int company);
    }
}