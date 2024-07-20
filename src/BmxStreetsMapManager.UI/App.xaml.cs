using BmxStreetsMapManager.Core.Data;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace BmxStreetsMapManager.UI;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        Task.Run(MigrateDatabaseAsync);
    }

    private static async Task MigrateDatabaseAsync()
    {
        using var context = new ApplicationDbContext();
        await context.Database.EnsureCreatedAsync();
        await context.Database.MigrateAsync();
    }
}