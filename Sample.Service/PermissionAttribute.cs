using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBS.Service
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class PermissionAttribute : Attribute
    {
        public string PermissionName { get; }
        public string Action { get; }

        public PermissionAttribute(string permissionName, string action)
        {
            PermissionName = permissionName;
            Action = action;
        }
    }
}