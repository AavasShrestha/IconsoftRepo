using CBS.Data.TenantDB;
using Microsoft.EntityFrameworkCore;
using Sample.Data.TenantDB;

public class TenantDbContext : DbContext
{
    public TenantDbContext(DbContextOptions<TenantDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> User { get; set; }
    public DbSet<Client> Clients { get; set; }
}