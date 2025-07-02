using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository.Extensions;

public static class DbContextExtension
{
    public static void SetAuditFields(this DbContext dbContext)
    {
        var entries = dbContext.ChangeTracker.Entries();

        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                {
                    if (entry.Entity is BaseEntity entity)
                    {
                        entity.CreatedAt = DateTime.UtcNow;
                    }

                    break;
                }
                case EntityState.Modified:
                {
                    if (entry.Entity is BaseEntity modifiedEntity)
                    {
                        modifiedEntity.UpdatedAt = DateTime.UtcNow;
                    }

                    break;
                }
            }
        }
    }
}