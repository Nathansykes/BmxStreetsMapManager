﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BmxStreetsMapManager.Core.Data.Models;
public class Map
{
    public int Id { get; set; }


    public string LocalPath { get; set; } = null!;
    public string LocalName { get; set; } = null!;

    public int? ModIOId { get; set; }
    public string? ModIOName { get; set; }
    public string? ModIOVersion { get; set; }

    public virtual ICollection<MapProfiles> MapProfiles { get; set; } = new HashSet<MapProfiles>();
}