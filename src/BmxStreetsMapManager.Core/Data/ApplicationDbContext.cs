using BmxStreetsMapManager.Core.Data.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BmxStreetsMapManager.Core.Data;
public class ApplicationDbContext() : DbContext
{
    public virtual DbSet<Profile> Profiles { get; set; }
    public virtual DbSet<Map> Maps { get; set; }
    public virtual DbSet<MapProfiles> MapProfiles { get; set; }
    public virtual DbSet<UserConfig> UserConfig { get; set; }

    private static readonly string DbFolderPath = Path.Combine(Constants.WorkingDirectory , "data");
    private static readonly string DbFilePath = Path.Combine(DbFolderPath, "app.db");
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!Directory.Exists(DbFolderPath))
            Directory.CreateDirectory(DbFolderPath);
        options.UseSqlite($"Data Source={DbFilePath}");
        base.OnConfiguring(options);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<UserConfig>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).ValueGeneratedOnAdd();
        });

        builder.Entity<Map>(e =>
        {
            e.HasKey(x => x.Id);
            e.HasMany<MapProfiles>().WithOne(x => x.Map).HasForeignKey(x => x.MapId);
        });

        builder.Entity<Profile>(e =>
        {
            e.HasKey(x => x.Id);
            e.HasMany<MapProfiles>().WithOne(x => x.Profile).HasForeignKey(x => x.ProfileId);
        });

        builder.Entity<MapProfiles>(e =>
        {
            e.HasKey(x => new { x.ProfileId, x.MapId });
            e.HasOne(x => x.Profile).WithMany(x => x.MapProfiles).HasForeignKey(x => x.ProfileId);
            e.HasOne(x => x.Map).WithMany(x => x.MapProfiles).HasForeignKey(x => x.MapId);
            e.Property(x => x.IsEnabled).HasDefaultValue(true);
        });

        base.OnModelCreating(builder);
    }
}
