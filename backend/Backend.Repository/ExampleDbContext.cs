using Backend.Models;
using Backend.Repository.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository;

public class ExampleDbContext(DbContextOptions<ExampleDbContext> options) : DbContext(options)
{
    public DbSet<WeatherForecastEntity> WeatherForecastEntities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.SetChangeTrackedEntityAndId<WeatherForecastEntity>();
    }
    
    public override int SaveChanges()
    {
        this.SetAuditFields();
        return base.SaveChanges();
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        this.SetAuditFields();
        return await base.SaveChangesAsync(cancellationToken);
    }
}