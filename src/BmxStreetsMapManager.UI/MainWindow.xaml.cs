using BmxStreetsMapManager.Core.Data.Models;
using BmxStreetsMapManager.Core.Manager;
using System.Windows;

namespace BmxStreetsMapManager.UI;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{

    public MainWindow()
    {
        InitializeComponent();
    }

    private async void btnDetectMaps_Click(object sender, RoutedEventArgs e)
    {
        int count = 0;
        btnDetectMaps.IsEnabled = false;
        var ogContent = btnDetectMaps.Content.ToString();
        btnDetectMaps.Content = "Detecting Maps...";
        await foreach (var map in LoadMaps())
        {
            count++;
            txtTotalMapCount.Text = count.ToString();
        }
        btnDetectMaps.Content = ogContent;
        btnDetectMaps.IsEnabled = true;
    }

    private async IAsyncEnumerable<Map> LoadMaps()
    {
        using var mapManager = new MapManager();
        var maps = mapManager.DetectLocalMaps();

        foreach (var map in maps)
        {
            await Task.Delay(10);
            yield return map;
        }
    }
}