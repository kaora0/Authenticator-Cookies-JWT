using Microsoft.EntityFrameworkCore;
using Lab_7._1.Data.Entities;

namespace Lab_7._1.Data
{
    public class LearningDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        public LearningDbContext(DbContextOptions<LearningDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }
        }
    }
}
