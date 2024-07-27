using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BmxStreetsMapManager.Core.Manager;
public class Renamer
{
    private static readonly string StorePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "BMX Streets", "MapsLog.json");
    private static HashSet<string>? _smps;
    private static HashSet<string> StoredMaps
    {
        get
        {
            if (_smps is null)
            {
                _smps = new HashSet<string>();
                if (File.Exists(StorePath))
                {
                    var json = File.ReadAllText(StorePath);
                    _smps = JsonSerializer.Deserialize<HashSet<string>>(json) ?? new HashSet<string>();
                }
            }

            return _smps;
        }
    }
    public static void Run(bool skipSome)
    {

        Console.WriteLine("=== BMX Streets Map Renamer ===");


        // Get the path to the BMX Streets maps folder
        string? mapsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "BMX Streets", "Maps");

        bool isMapPathValid = Directory.Exists(mapsPath);

        while (!isMapPathValid)
        {
            Console.WriteLine("The BMX Streets maps folder was not found. Please enter the path to the maps folder:");
            mapsPath = Console.ReadLine();
            isMapPathValid = Directory.Exists(mapsPath);
        }


        Console.WriteLine($"Found maps folder at: {mapsPath}");

        Console.WriteLine("Unzipping any zips");


        Console.WriteLine("Getting All Maps");

        var folders = Directory.GetDirectories(mapsPath!).ToList();

        skipSome = GetYNResponse("Skip already renamed maps?");
        if (skipSome)
        {
            //folders = folders.Where(x => !HasMapBeenRenamedBefore(x)).ToArray();
            folders = folders.Where(x => !StoredMaps.Contains(x)).ToList();
        }

        var zipFolder = folders.Find(x => x.EndsWith("_zips"));
        if (zipFolder is not null)
            folders.Remove(zipFolder);

        var invalidFolders = folders.Where(x => Directory.GetFiles(x).Length is not (1 or 2)).ToArray();
        if (invalidFolders.Length > 0)
        {
            Console.WriteLine("Found folders with more than 2 files, these will be skipped: ");
            foreach (var folder in invalidFolders)
            {
                Console.WriteLine(" : " + folder);
            }

            var cont = GetYNResponse("Continue?");
            if (!cont)
                return;
        }

        Console.WriteLine($"Found {folders.Count} maps");

        foreach (var folder in folders)
        {
            //var newMapPath = RenameMap(folder, "");
            //StoredMaps.Add(newMapPath);
        }

        var json = JsonSerializer.Serialize(StoredMaps);
        File.WriteAllText(StorePath, json);

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }

    public static string RenameMap(string originalFolderPath, string newMapName)
    {
        var originalFolderName = originalFolderPath.Split("\\").Last();
        Console.WriteLine();
        Console.WriteLine("=====================================");
        Console.WriteLine("Accessing Map Folder: " + originalFolderName);

        var filesInFolder = Directory.GetFiles(originalFolderPath);
        if (filesInFolder.Length > 2)
        {
            throw new Exception("Folder contains more than 2 files");
        }

        var originalFileMapName = Path.GetFileNameWithoutExtension(filesInFolder.First().Split("\\").Last());
        Console.WriteLine("Map Name: " + filesInFolder.First().Split("\\").Last());

        Console.WriteLine("New name (press enter to skip): ");


        var newMapPath = Path.Combine(BmxStreetsConsts.BmxStreetsMapDirectory!, newMapName);
        if (originalFolderPath != newMapPath)
        {
            Directory.Move(originalFolderPath, newMapPath);
        }

        filesInFolder = Directory.GetFiles(newMapPath);
        foreach (var file in filesInFolder)
        {
            var extension = Path.GetExtension(file);
            if (string.IsNullOrEmpty(extension))
            {
                Console.WriteLine($"Adding missing .bundle extension to map file");
                extension = ".bundle";
            }

            var newFilePath = Path.Combine(newMapPath, newMapName + extension);
            if (file != newFilePath)
            {
                File.Move(file, newFilePath);
            }
        }
        Console.WriteLine($"Folder and contained files renamed to: {newMapName}");
        return newMapPath;
    }

    static bool HasMapBeenRenamedBefore(string folderPath)
    {
        var folderName = folderPath.Split("\\").Last();
        var files = Directory.GetFiles(folderPath);
        var file = files.FirstOrDefault(x => Path.GetExtension(x) == ".bundle" || string.IsNullOrEmpty(Path.GetExtension(x)));

        var fileName = Path.GetFileNameWithoutExtension(file);

        bool folderFirstCharIsCaps = char.IsUpper(folderName[0]);
        bool fileMatchesFolder = fileName == folderName;
        return folderFirstCharIsCaps && fileMatchesFolder;
    }

    static bool GetYNResponse(string prompt)
    {
        while (true)
        {
            Console.WriteLine(prompt + " (y/n)");
            var input = Console.ReadLine()?.ToLower();
            if (input == "y" || input == "n")
            {
                return input == "y";
            }
            Console.WriteLine("Invalid input. Please enter 'y' or 'n'");
        }
    }
}
