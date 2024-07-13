using BmxStreetsMapManager.Core.Data.Models;
using BmxStreetsMapManager.Core.Manager;
using BmxStreetsMapManager.UI.ViewModels;
using System.Windows;

namespace BmxStreetsMapManager.UI;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private static readonly MainViewModel _viewModel = new();

    public MainWindow()
    {
        InitializeComponent();
        _viewModel.LoadMaps();
    }

    private void btnDetectMaps_Click(object sender, RoutedEventArgs e)
    {
        btnDetectMaps.IsEnabled = false;
        var ogContent = btnDetectMaps.Content.ToString();
        btnDetectMaps.Content = "Detecting Maps...";

        _viewModel.LoadMaps();
        
        btnDetectMaps.Content = ogContent;
        btnDetectMaps.IsEnabled = true;
    }
}