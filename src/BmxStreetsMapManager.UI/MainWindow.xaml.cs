using BmxStreetsMapManager.UI.ViewModels;
using System.Windows;

namespace BmxStreetsMapManager.UI;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    protected MainViewModel ViewModel { get; }

    public MainWindow()
    {
        InitializeComponent();
        ViewModel = (MainViewModel)this.DataContext!;
        ViewModel.LoadMaps();
    }

    private void btnDetectMaps_Click(object sender, RoutedEventArgs e)
    {
        btnDetectMaps.IsEnabled = false;
        var ogContent = btnDetectMaps.Content.ToString();
        btnDetectMaps.Content = "Detecting Maps...";

        ViewModel.LoadMaps();

        btnDetectMaps.Content = ogContent;
        btnDetectMaps.IsEnabled = true;
    }
}