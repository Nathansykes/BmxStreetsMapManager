using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BmxStreetsMapManager.Core.Data.Models;
public class MapProfiles
{
    public int ProfileId { get; set; }
    public int MapId { get; set; }

    public bool IsEnabled { get; set; }

    public virtual Profile Profile { get; set; } = null!;
    public virtual Map Map { get; set; } = null!;
}
