using CBS.Data.TenantDB;
using Microsoft.EntityFrameworkCore;
using Sample.Data.TenantDB;
using System.Reflection.Emit;

public class TenantDbContext : DbContext
{
    public TenantDbContext(DbContextOptions<TenantDbContext> options)
        : base(options)
    {
        
    }

    public DbSet<User> User { get; set; }
    public DbSet<Client> Clients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Client>().ToTable("Clients"); // Optional if using default plural naming
    }


}