namespace BmxStreetsMapManager.Core.Data.Models;
public class Profile
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<MapProfiles> MapProfiles { get; set; } = new List<MapProfiles>();
}
