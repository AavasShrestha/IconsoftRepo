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
    public DbSet<Document> Documents { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //modelBuilder.Entity<Client>().ToTable("Clients"); // Optional if using default plural naming




        // Optional table naming
        modelBuilder.Entity<Client>().ToTable("Clients");
        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<Document>().ToTable("Documents");



        //  Define relationships
        // Document → Client
        modelBuilder.Entity<Document>()
            .HasOne(d => d.Client)
            .WithMany(c => c.Documents)
            .HasForeignKey(d => d.ClientId)
            .OnDelete(DeleteBehavior.Restrict); // Prevents cascade delete

        // Document → User
        modelBuilder.Entity<Document>()
            .HasOne(d => d.CreatedByUser)        // User => CreatedByUser
            .WithMany(u => u.Documents)
            .HasForeignKey(d => d.CreatedBy)     // UserID => CreatedBy
            .OnDelete(DeleteBehavior.Restrict); // Prevents cascade delete
    }


}