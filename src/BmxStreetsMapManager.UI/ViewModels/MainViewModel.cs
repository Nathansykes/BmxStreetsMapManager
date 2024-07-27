using BmxStreetsMapManager.Core.Data.Models;
using BmxStreetsMapManager.Core.Manager;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;

namespace BmxStreetsMapManager.UI.ViewModels;
public class MainViewModel : IDisposable, INotifyPropertyChanged
{
    private bool _disposedValue;

    public ObservableCollection<Map> Maps { get; set; }
    public int MapCount { get => Maps.Count; set { } }
    private Map? _selectedMap;
    public Map? SelectedMap { 
        get => _selectedMap;
        set
        {
            if (_selectedMap != value)
            {
                _selectedMap = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SelectedMapImagePath));
            }
        }
    }
    public string SelectedMapImagePath => SelectedMap?.ImageFileName is null
        ? "" //TODO: Add a placeholder image
        : Path.Combine(SelectedMap.LocalPath, SelectedMap.ImageFileName);

    private readonly MapManager _manager;

    public MainViewModel()
    {
        _manager = new MapManager();
        Maps = [];
    }

    public void LoadMaps(ICollection<Map> maps)
    {
        Maps.Clear();
        foreach (var item in maps.OrderBy(x => x.LocalName))
        {
            Maps.Add(item);
        }
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

    public void Setup()
    {
        InitialSetup.Run();
        var maps = _manager.GetMaps();
        LoadMaps(maps);
    }



    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

}
