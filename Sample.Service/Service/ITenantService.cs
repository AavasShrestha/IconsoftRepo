using CBS.Data.RoutingDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBS.Service.Service
{
    public interface ITenantService
    {
        Tenant GetCurrentTenant(string tenantId);
    }
}