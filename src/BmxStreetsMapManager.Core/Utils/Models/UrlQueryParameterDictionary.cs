using System.Text;

namespace BmxStreetsMapManager.Core.Utils.Models;

public class UrlQueryParameterDictionary : Dictionary<string, object?>
{
    public UrlQueryParameterDictionary() { }
    public UrlQueryParameterDictionary(string basePath)
    {
        BasePath = basePath;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append(BasePath);
        sb.Append('?');

        for (int i = 0; i < Count; i++)
        {
            if (i > 0)
                sb.Append('&');
            var item = this.ElementAt(i);
            sb.Append($"{item.Key}={item.Value}");
        }
        return sb.ToString();
    }

    public string BasePath { get; set; } = "";
}
public class UrlQueryParameter
{
    public string Key { get; set; } = "";
    public object? Value { get; set; }
}
