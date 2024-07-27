using BmxStreetsMapManager.Core.Data.Models;
using BmxStreetsMapManager.Core.Manager;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace BmxStreetsMapManager.UI.ViewModels;
public class MainViewModel : IDisposable
{
    private bool _disposedValue;

    public ObservableCollection<Map> Maps { get; set; }
    public int MapCount { get => Maps.Count; set { } }
    public Map? SelectedMap { get; set; }

    private readonly MapManager _manager;

    public MainViewModel()
    {
        _manager = new MapManager();
        LoadMaps();
    }

    [MemberNotNull(nameof(Maps))]
    public void LoadMaps()
    {
        var maps = _manager.DetectLocalMaps();
        Maps = new ObservableCollection<Map>(maps);
    }

    public void RenameMap(string newMapName)
    {
        _manager.RenameMap(SelectedMap!.LocalPath, newMapName);
    }

    public void UnzipNewMaps()
    {
        _manager.UnZipNewMaps();
        _manager.DetectLocalMaps();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                _manager?.Dispose();
            }

            _disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    public void Setup()
    {
        LoadMaps();
        Task.Run(InitialSetup.Run);
    }
}
