using _06._Entity_Framework.Models;
using Microsoft.EntityFrameworkCore;

namespace _06._Entity_Framework.Data
{
    public class DataContextEF : DbContext // We inherit from DbContext
    {
        public DbSet<Computer>? Computer { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured) // If not configured yet, do the following:
            {
                options.UseSqlServer("Server=localhost;Database=DotNetCourseDatabase;TrustServerCertificate=true;Trusted_Connection=true;",
                    options => options.EnableRetryOnFailure()); // This second argument will retry connection if for some reason the database cannot be reached
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TutorialAppSchema");
            modelBuilder.Entity<Computer>()
                //.HasNoKey(); // Whenever the Table has no primary key defined
                .HasKey(c => c.ComputerId);

                //.ToTable("Computer", "TutorialAppSchema"); 
                //.ToTable("TableName", "SchemaName"); 
                // We need to pass the schema, because otherwise EntityFramework will look for the deault `dbo` SQL Server schema, 
                // you could do this by calling it as a method, but it is better to override it with the `.HasDefaultSchema()` method
        }

    }
}