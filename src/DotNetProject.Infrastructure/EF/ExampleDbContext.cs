using DotNetProject.Infrastructure.ExampleService;
using Microsoft.EntityFrameworkCore;

namespace DotNetProject.Infrastructure.EF;

public class ExampleDbContext : DbContext
{
    public ExampleDbContext(DbContextOptions<ExampleDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ExampleEntity>().HasData(
             new ExampleEntity { Id = 1, Name = "Fireball" },
             new ExampleEntity { Id = 2, Name = "Frenzy" }
            );
    }

    public DbSet<ExampleEntity> Example { get; set; }
}

