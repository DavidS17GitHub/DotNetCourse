using _09._JSON.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace _09._JSON.Data
{
    public class DataContextEF : DbContext
    {
        // Config code block
        private IConfiguration _config;
        public DataContextEF(IConfiguration config)
        {
            _config = config;
        }
        // End of Config code Block
        public DbSet<Computer>? Computer { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer(_config.GetConnectionString("DeafultConnection"),
                    options => options.EnableRetryOnFailure());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TutorialAppSchema");
            modelBuilder.Entity<Computer>()
                .HasKey(c => c.ComputerId);
        }

    }
}