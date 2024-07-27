namespace BmxStreetsMapManager.Core.Data.Models;
public class Map
{
    public int Id { get; set; }

    public required string LocalPath { get; set; } = null!;
    public required string LocalName { get; set; } = null!;
    public string? ImageFileName { get; set; }

    public int? ModIOId { get; set; }
    public string? ModIOName { get; set; }
    public string? ModIOVersion { get; set; }

    public virtual ICollection<MapProfiles> MapProfiles { get; set; } = [];
}
