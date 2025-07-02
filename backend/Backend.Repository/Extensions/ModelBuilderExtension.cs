using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository.Extensions;

public static class ModelBuilderExtension
{
    public static void SetChangeTrackedEntityAndId<T>(this ModelBuilder modelBuilder) where T : BaseEntity
    {
        modelBuilder.Entity<T>()
            .HasKey(e => e.Id);

        modelBuilder.Entity<T>()
            .Property(e => e.CreatedAt)
            .IsRequired();
    }
}