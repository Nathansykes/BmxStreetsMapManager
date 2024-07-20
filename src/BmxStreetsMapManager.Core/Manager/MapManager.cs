using BmxStreetsMapManager.Core.API;
using BmxStreetsMapManager.Core.Data;
using BmxStreetsMapManager.Core.Data.Models;
using BmxStreetsMapManager.Core.Utils;

namespace BmxStreetsMapManager.Core.Manager;
public class MapManager : IDisposable
{
    private ApplicationDbContext? _context;
    protected ApplicationDbContext Context => _context ??= new();

    private async Task MatchMaps()
    {
        var subscribedMaps = await GetMaps();
        var localMaps = DetectLocalMaps();

        var matchedMaps = localMaps.FuzzyZip(subscribedMaps, loc => loc.LocalName, sub => sub.Name!).ToList();

        foreach (var (source, matched) in matchedMaps)
        {
            if (matched is null)
                continue;
            var found = Context.Maps.FirstOrDefault(x => x.LocalPath == source.LocalPath);
            if (found is not null)
                continue;
            var entity = new Map
            {
                LocalName = source.LocalName,
                LocalPath = source.LocalPath,
                ModIOId = matched.Id,
                ModIOName = matched.Name,
                ModIOVersion = matched.Modfile?.Version,
            };
            Context.Maps.Add(entity);
            Context.SaveChanges();
        }
    }

    public async Task<List<ModObject>> GetMaps()
    {
        return await ModIOClient.GetSubscribedMods(null);
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
