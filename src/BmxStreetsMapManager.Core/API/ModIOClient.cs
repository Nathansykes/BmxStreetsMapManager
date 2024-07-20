using BmxStreetsMapManager.Core.Data;
using BmxStreetsMapManager.Core.Utils.Models;
using System.Text.Json;

namespace BmxStreetsMapManager.Core.API;
public static class ModIOClient
{
    private static HttpClient? _client;
    private static HttpClient Client => _client ??= SetupClient();
    private static HttpClient SetupClient()
    {
        using var dbContext = new ApplicationDbContext();
        var userSettings = dbContext.UserConfig.Single();
        var client = new HttpClient
        {
            BaseAddress = new Uri($"https://u-{userSettings.ModIOUserId}.modapi.io/v1/")
        };
        client.DefaultRequestHeaders.Authorization = new("Bearer", userSettings.ModIOApiToken);

        return client;
    }

    public static async Task<bool> TestConnection()
    {
        var response = await Client.GetAsync("me");
        return response.IsSuccessStatusCode;
    }

    public static async Task<List<ModObject>> GetSubscribedMods(int? gameId)
    {
        var path = new UrlQueryParameterDictionary("me/subscribed");
        if (gameId.HasValue)
            path.Add("game_id", gameId);

        var mods = new List<ModObject>();

        int offset = 0;
        int limit = 100;
        int total;
        do
        {
            path["_offset"] = offset;
            path["_limit"] = limit;

            var response = await Client.GetAsync(path.ToString());
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var responseObj = JsonSerializer.Deserialize<APIListResponse<ModObject>>(content)!;
            mods.AddRange(responseObj.Data);

            offset += limit;
            total = responseObj.ResultTotal;
        } while (total != 0 && offset < total);

        return mods;
    }
}
