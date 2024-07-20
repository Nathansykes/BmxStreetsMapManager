using BmxStreetsMapManager.Core.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BmxStreetsMapManager.Core.Data;
public class ApplicationDbContext() : DbContext
{
    public virtual DbSet<Profile> Profiles { get; set; }
    public virtual DbSet<Map> Maps { get; set; }
    public virtual DbSet<MapProfiles> MapProfiles { get; set; }
    protected virtual DbSet<UserConfig> UserConfigs { get; set; }
    public virtual UserConfig UserConfig
    {
        get
        {
            var config = UserConfigs.SingleOrDefault();
            if (config is null)
            {
                config = new UserConfig();
                UserConfigs.Add(config);
                SaveChanges();
            }
            return config;
        }
    }

    private static readonly string DbFolderPath = Path.Combine(Constants.WorkingDirectory, "data");
    private static readonly string DbFilePath = Path.Combine(DbFolderPath, "app.db");
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!Directory.Exists(DbFolderPath))
            Directory.CreateDirectory(DbFolderPath);
        options.UseSqlite($"Data Source={DbFilePath}");

        options.UseLazyLoadingProxies();

        base.OnConfiguring(options);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        EntityConfiguration.Configure(builder);

        base.OnModelCreating(builder);
    }

    public override int SaveChanges()
    {
        OnBeforeSaveChanges();
        int num = base.SaveChanges();
        OnAfterSaveChanges();
        return num;
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        OnBeforeSaveChanges();
        int num = base.SaveChanges(acceptAllChangesOnSuccess);
        OnAfterSaveChanges();
        return num;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        OnBeforeSaveChanges();
        int num = await base.SaveChangesAsync(cancellationToken);
        OnAfterSaveChanges();
        return num;
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        OnBeforeSaveChanges();
        int num = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        OnAfterSaveChanges();
        return num;
    }

    private void OnBeforeSaveChanges()
    {
        var countNewAdded = ChangeTracker.Entries<UserConfig>().Count(e => e.State == EntityState.Added);
        var countInTable = UserConfigs.Count();

        if (countNewAdded > 1 || (countNewAdded > 0 && countInTable > 0))
            throw new InvalidOperationException("Only one UserConfig entity can exist at a time.");
    }

    private void OnAfterSaveChanges()
    {

    }
}
