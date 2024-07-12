using BmxStreetsMapManager.Core.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BmxStreetsMapManager.Core.Data;
public class ApplicationDbContext() : DbContext
{
    public virtual DbSet<Profile> Profiles { get; set; }
    public virtual DbSet<Map> Maps { get; set; }
    public virtual DbSet<MapProfiles> MapProfiles { get; set; }
    public virtual DbSet<UserConfig> UserConfig { get; set; }

    private static readonly string DbFolderPath = Path.Combine(Constants.WorkingDirectory, "data");
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
        EntityConfiguration.Configure(builder);

        base.OnModelCreating(builder);
    }
}
