using CBS.Data.DTO;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBS.Service.Cache
{
    public class UserPreferenceService : IUserPreferenceService
    {
        private readonly IMemoryCache _cache;

        public UserPreferenceService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public void ClearUserPreferences(int userId)
        {
            _cache.Remove(userId);
        }

        public void SetUserPermission(int userId, List<UserPermissionDTO> permissions)
        {
            _cache.Set("permission" + userId, permissions, new MemoryCacheEntryOptions
            {
                Size = 1
            });
        }

        public List<UserPermissionDTO> GetUserPermission(int userId)
        {
            _cache.TryGetValue("permission" + userId, out List<UserPermissionDTO> userPermission);
            return userPermission;
        }

        public void ClearUserPermission(int userId)
        {
            _cache.Remove("permission" + userId);
        }
    }
}