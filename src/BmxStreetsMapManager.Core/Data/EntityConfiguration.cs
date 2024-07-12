using BmxStreetsMapManager.Core.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq.Expressions;

namespace BmxStreetsMapManager.Core.Data;
public static class EntityConfiguration
{
    public static void Configure(ModelBuilder builder)
    {
        builder.Entity<UserConfig>(e =>
        {
            e.HasGeneratedKey(x => x.Id);
        });

        builder.Entity<Map>(e =>
        {
            e.HasGeneratedKey(x => x.Id);
            e.HasMany(x => x.MapProfiles).WithOne(x => x.Map).HasForeignKey(x => x.MapId);
        });

        builder.Entity<Profile>(e =>
        {
            e.HasGeneratedKey(x => x.Id);
            e.HasMany(x => x.MapProfiles).WithOne(x => x.Profile).HasForeignKey(x => x.ProfileId);
        });

        builder.Entity<MapProfiles>(e =>
        {
            e.HasKey(x => new { x.ProfileId, x.MapId });
            e.HasOne(x => x.Profile).WithMany(x => x.MapProfiles).HasForeignKey(x => x.ProfileId);
            e.HasOne(x => x.Map).WithMany(x => x.MapProfiles).HasForeignKey(x => x.MapId);
            e.Property(x => x.IsEnabled).HasDefaultValue(true);
        });
    }

    public static void HasGeneratedKey<TEntity, TProperty>(this EntityTypeBuilder<TEntity> e, Expression<Func<TEntity, TProperty>> propertyExpression)
        where TEntity : class
    {
        e.HasKey("Id");
        e.Property("Id").ValueGeneratedOnAdd();
    }
}
