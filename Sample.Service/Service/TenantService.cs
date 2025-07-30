using CBS.Data.RoutingDB;
using CBS.Repository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBS.Service.Service
{
    public class TenantService : ITenantService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public TenantService(IHttpContextAccessor contextAccessor, IUnitOfWork unitOfWork)
        {
            _contextAccessor = contextAccessor;
            _unitOfWork = unitOfWork;
        }

        public Tenant GetCurrentTenant(string tenantId)
        {
            if (string.IsNullOrEmpty(tenantId))
                throw new Exception("Tenant-Id header is missing.");

            // In a real-world application, retrieve tenant info from a database or configuration
            var tenants = _unitOfWork.TenantRepository.GetQuerable();
            var clientId = int.Parse(tenantId);
            var tenant = tenants.FirstOrDefault(t => t.Id == clientId);

            if (tenant == null)
                throw new Exception("Invalid Tenant-Id.");

            return tenant;
        }
    }
}