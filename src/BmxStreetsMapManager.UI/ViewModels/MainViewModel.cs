using BmxStreetsMapManager.Core.Data.Models;
using BmxStreetsMapManager.Core.Manager;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace BmxStreetsMapManager.UI.ViewModels;
public class MainViewModel
{
    public ObservableCollection<Map> Maps { get; set; }
    public int MapCount { get => Maps.Count; set { } }
    public Map? SelectedMap { get; set; }

    public MainViewModel()
    {
        LoadMaps();
    }

    [MemberNotNull(nameof(Maps))]
    public void LoadMaps()
    {
        using var mapManager = new MapManager();
        var maps = mapManager.DetectLocalMaps();

        Maps = new ObservableCollection<Map>(maps);
    }
}
