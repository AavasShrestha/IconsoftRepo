using CBS.Data.RoutingDB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBS.Data
{
    public class RoutingDbContext : DbContext
    {
        public virtual DbSet<Tenant> Tenant { get; set; }
        public virtual DbSet<tblClientDetails> tblClientDetails { get; set; }

        public RoutingDbContext(DbContextOptions<RoutingDbContext> options) : base(options)
        {
        }
    }
}