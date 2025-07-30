using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBS.Data
{
    public interface ITenantUnitOfWorkFactory
    {
        IUnitOfWork Create(string tenantId);
    }

    public class TenantUnitOfWorkFactory : ITenantUnitOfWorkFactory
    {
        private readonly ITenantDbContextFactory _dbContextFactory;

        public TenantUnitOfWorkFactory(ITenantDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public IUnitOfWork Create(string tenantId)
        {
            var dbContext = _dbContextFactory.CreateDbContext(tenantId);
            return new TenantUnitOfWork(dbContext);
        }
    }
}