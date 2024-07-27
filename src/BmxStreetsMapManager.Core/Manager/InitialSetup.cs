using BmxStreetsMapManager.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BmxStreetsMapManager.Core.Manager;
public static class InitialSetup
{
    public static void Run()
    {
        using var manager = new MapManager();


        var maps = manager.MatchMaps();


    }
}
