using BmxStreetsMapManager.Core.API;
using BmxStreetsMapManager.Core.Data;
using BmxStreetsMapManager.Core.Data.Models;
using BmxStreetsMapManager.Core.Utils;
using System.IO.Compression;

namespace BmxStreetsMapManager.Core.Manager;
public class MapManager : IDisposable
{
    private static readonly string[] imgExtensions = [ "png", "jpg", "bmp", "jpeg" ];
    private ApplicationDbContext? _context;
    protected ApplicationDbContext Context => _context ??= new();
    public int? CurrentProfileId { get; private set; }

    public void LoadProfile(int profileId)
    {
        CurrentProfileId = profileId;
        _ = GetCurrentProfile(createIfNotFound: true);
    }

    public async Task MatchMaps()
    {
        var subscribedMaps = Enumerable.Empty<ModObject>(); //await GetMaps();
        var localMaps = DetectLocalMaps();

        var matchedMaps = localMaps.FuzzyJoin(subscribedMaps, loc => loc.LocalName, sub => sub.Name!).ToList();

        foreach (var (source, matched) in matchedMaps)
        {
            var found = Context.Maps.FirstOrDefault(x => x.LocalPath == source.LocalPath);
            if (found is not null)
            {
                var isInProfile = Context.MapProfiles.Any(x => x.MapId == found.Id && x.ProfileId == CurrentProfileId);
                AddMapToCurrentProfile(found);
                continue;
            }
            var entity = new Map
            {
                LocalName = source.LocalName,
                LocalPath = source.LocalPath,
                ImageFileName = FindFileName(source.LocalPath),
                ModIOId = matched?.Id,
                ModIOName = matched?.Name,
                ModIOVersion = matched?.Modfile?.Version,
            };
            Context.Maps.Add(entity);
            await Context.SaveChangesAsync();
            AddMapToCurrentProfile(entity);
        }
    }

    private static string? FindFileName(string localPath)
    {
        var files = Directory.GetFiles(localPath);
        var image = files.FirstOrDefault(x => imgExtensions.Contains(Path.GetExtension(x)));
        return Path.GetFileName(image);
    }


    private void AddMapToCurrentProfile(Map entity)
    {
        var profile = GetCurrentProfile(createIfNotFound: true);
        profile.MapProfiles.Add(new MapProfiles
        {
            Map = entity,
            Profile = profile,
            IsEnabled = true
        });
    }

    private Profile GetCurrentProfile(bool createIfNotFound)
    {
        var profile = Context.Profiles.FirstOrDefault(x => x.Id == CurrentProfileId);
        if (profile is null && (!createIfNotFound))
            throw new Exception();
        else if (profile is null)
        {
            profile = new Profile()
            {
                Name = GeProfileName("Default"),
            };
            Context.Add(profile);
            Context.SaveChanges();
            LoadProfile(profile.Id);
        }

        return profile;
    }
    private string GeProfileName(string name)
    {
        var existing = Context.Profiles.FirstOrDefault(x => x.Name == name);
        int num = 1;
        string newName = name;
        while (existing is not null)
        {
            newName = $"{name}-{num}";
            existing = Context.Profiles.FirstOrDefault(x => x.Name == name);
        }
        return newName;
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

    public void RenameMap(string mapPath, string newName)
    {
        var newPath = Renamer.RenameMap(mapPath, newName);
    }

    public void EnableMap(string mapPath)
    {
        var mapProfile = Context.MapProfiles.FirstOrDefault(x => x.Map.LocalPath == mapPath && x.ProfileId == CurrentProfileId);
        if (mapProfile is not null)
        {
            mapProfile.IsEnabled = true;
            Context.SaveChanges();
        }
    }

    public void DisableMap(string mapPath)
    {
        var mapProfile = Context.MapProfiles.FirstOrDefault(x => x.Map.LocalPath == mapPath && x.ProfileId == CurrentProfileId);
        if (mapProfile is not null)
        {
            mapProfile.IsEnabled = false;
            Context.SaveChanges();
        }
    }

    public void UnZipNewMaps()
    {
        var mapsPath = BmxStreetsConsts.BmxStreetsMapDirectory;
        var zipsFolder = Path.Combine(mapsPath, "_zips");
        var zipFiles = Directory.GetFiles(mapsPath, "*.zip");
        foreach (var zipFile in zipFiles)
        {
            var zipName = Path.GetFileName(zipFile);
            Console.WriteLine($"Unzipping {zipName}");

            var folderName = Path.GetFileNameWithoutExtension(zipFile);
            var folderPath = Path.Combine(mapsPath, folderName);

            ZipFile.ExtractToDirectory(zipFile, folderPath);

            var subFolders = Directory.GetDirectories(folderPath);
            var files = Directory.GetFiles(folderPath);
            if (subFolders.Length == 1 && files.Length == 0)
            {
                var subFolder = subFolders.First();
                var newFolderName = Path.GetFileName(subFolder);
                var newFolderPath = Path.Combine(mapsPath, newFolderName);
                Directory.Move(subFolder, newFolderPath);

                Directory.Delete(folderPath);
                folderName = newFolderName;
                folderPath = newFolderPath;
            }

            File.Move(zipFile, Path.Combine(zipsFolder, zipName));
        }
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
