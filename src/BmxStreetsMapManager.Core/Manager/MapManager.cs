using BmxStreetsMapManager.Core.Data;
using BmxStreetsMapManager.Core.Data.Models;

namespace BmxStreetsMapManager.Core.Manager;
public class MapManager : IDisposable
{
    private ApplicationDbContext? _context;
    protected ApplicationDbContext Context => _context ??= new();

    public List<Map> GetMaps()
    {

        return [];
    }

    public List<Map> DetectLocalMaps()
    {
        List<Map> maps = [];
        var foundMaps = Directory.GetDirectories(BmxStreetsConsts.BmxStreetsMapDirectory).ToList().Where(IsMap).ToList();

        return foundMaps.Select(x => new Map
        {
            LocalName = Path.GetFileName(x) ?? "",
            LocalPath = Path.GetFullPath(x)
        }).ToList();
    }

    private bool IsMap(string folderPath)
    {
        var files = Directory.GetFiles(folderPath);
        return files.Any(x =>
        {
            var ext = Path.GetExtension(x);
            return ext == ".bundle" || ext == "";
        });
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _context?.Dispose();
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
