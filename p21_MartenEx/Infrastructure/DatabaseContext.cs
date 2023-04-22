using Microsoft.EntityFrameworkCore;
using p21_MartenEx.Models;

namespace p21_MartenEx.Infrastructure;

public class DatabaseContext : DbContext
{
    public DbSet<FieldSecurity> FieldSecurities { get; set; }
    public DbSet<Security> Securities { get; set; }
    public DbSet<SomeRule> SomeRules { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
}