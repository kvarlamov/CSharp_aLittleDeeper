using Microsoft.EntityFrameworkCore;

namespace p10_DotNetDbTools
{
    public class ApplicationContext : DbContext
    {
        private string _connectionString;
        
        public DbSet<User> Users { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Deal> Deals { get; set; }

        public ApplicationContext(string connectionString)
        {
            _connectionString = connectionString;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CityToCategory>()
                .HasKey(t => new {t.CityId, t.CategoryId});
            modelBuilder.Entity<CityToCategory>()
                .HasOne(sc => sc.City)
                .WithMany(s => s.CityToCategories)
                .HasForeignKey(sc => sc.CityId);
            modelBuilder.Entity<CityToCategory>()
                .HasOne(sc => sc.Category)
                .WithMany(s => s.CityToCategories)
                .HasForeignKey(sc => sc.CategoryId);
        }
    }
}