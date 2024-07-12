namespace BmxStreetsMapManager.Core;

public static class Constants
{
    public static readonly string WorkingDirectory = Path.Combine(BmxStreetsConsts.BmxStreetsDirectory, "Bmx Streets Map Manager");
}

public static class BmxStreetsConsts
{
    public static readonly string BmxStreetsDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "BMX Streets");
    public static readonly string BmxStreetsMapDirectory = Path.Combine(BmxStreetsDirectory, "Maps");
}