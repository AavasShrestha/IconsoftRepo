using CBS.Data.DTO;
using CBS.Repository;
using CBS.Service.Cache;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBS.Service
{
    public interface IPermissionService
    {
        bool UserHasPermission(int userId, string permissionName, string action);
    }

    public class PermissionService : IPermissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserPreferenceService _userPreferenceService;

        public PermissionService(IUnitOfWork unitofwork, IUserPreferenceService userPreferenceService)
        {
            _unitOfWork = unitofwork;
            _userPreferenceService = userPreferenceService;
        }

        public bool UserHasPermission(int userId, string permissionName, string action)
        {
            var userPermissionList = new List<UserPermissionDTO>();
            var cachedPermissionList = _userPreferenceService.GetUserPermission(userId);
            if (cachedPermissionList != null && cachedPermissionList.Count() > 0)
            {
                userPermissionList = cachedPermissionList;
            }
            else
            {
                //userPermissionList = (from ur in _unitOfWork.ApplicationUserRoleRepository.GetQuerable()
                //                      join ar in _unitOfWork.ApplicationRoleFunctionRepository.GetQuerable() on ur.RoleId equals ar.RoleId
                //                      join af in _unitOfWork.ApplicationMenuFunctionRepository.GetQuerable() on ar.FunctionId equals af.Id
                //                      join am in _unitOfWork.ApplicationMenuRepository.GetQuerable() on af.MenuId equals am.MenuId
                //                      where ur.UserId == userId
                //                      select new UserPermissionDTO
                //                      {
                //                          MenuName = am.MenuName,
                //                          PermissionName = af.FunctionName
                //                      }).ToList();

                _userPreferenceService.SetUserPermission(userId, userPermissionList);
            }
            return userPermissionList.Where(q => q.MenuName == permissionName && q.PermissionName == action).Any();
        }
    }
}