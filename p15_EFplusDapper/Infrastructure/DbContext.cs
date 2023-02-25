using Microsoft.EntityFrameworkCore;
using p15_EFplusDapper.Infrastructure.Data;
using p15_EFplusDapper.Infrastructure.Entities;

namespace p15_EFplusDapper.Infrastructure;

public class DatabaseContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Address> Addresses { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasOne(p => p.User)
                .WithMany(e => e.Address)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}