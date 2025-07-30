using CBS.Data.DTO;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBS.Service.Cache
{
    public interface IUserPreferenceService
    {
        void ClearUserPreferences(int userId);

        void SetUserPermission(int userId, List<UserPermissionDTO> permissions);

        List<UserPermissionDTO> GetUserPermission(int userId);

        void ClearUserPermission(int userId);
    }
}