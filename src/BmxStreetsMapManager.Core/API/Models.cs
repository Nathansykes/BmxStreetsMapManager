using System.Text.Json.Serialization;

namespace BmxStreetsMapManager.Core.API;

public class APIResonse
{
    [JsonPropertyName("result_count")]
    public int ResultCount { get; set; }

    [JsonPropertyName("result_offset")]
    public int ResultOffset { get; set; }

    [JsonPropertyName("result_limit")]
    public int ResultLimit { get; set; }

    [JsonPropertyName("result_total")]
    public int ResultTotal { get; set; }
}
public class APIListResponse<T> : APIResonse
{
    [JsonPropertyName("data")]
    public List<T> Data { get; set; } = [];
}
public class APISingleResponse<T> : APIResonse
{
    [JsonPropertyName("data")]
    public T Data { get; set; } = default!;
}

public class Avatar
{
    [JsonPropertyName("filename")]
    public string? Filename { get; set; }

    [JsonPropertyName("original")]
    public string? Original { get; set; }

    [JsonPropertyName("thumb_50x50")]
    public string? Thumb50x50 { get; set; }

    [JsonPropertyName("thumb_100x100")]
    public string? Thumb100x100 { get; set; }
}

public class ModObject
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("game_id")]
    public int GameId { get; set; }

    [JsonPropertyName("status")]
    public int Status { get; set; }

    [JsonPropertyName("visible")]
    public int Visible { get; set; }

    [JsonPropertyName("submitted_by")]
    public UserProfile? SubmittedBy { get; set; }

    [JsonPropertyName("date_added")]
    public int DateAdded { get; set; }

    [JsonPropertyName("date_updated")]
    public int DateUpdated { get; set; }

    [JsonPropertyName("date_live")]
    public int DateLive { get; set; }

    [JsonPropertyName("maturity_option")]
    public int MaturityOption { get; set; }

    [JsonPropertyName("community_options")]
    public int CommunityOptions { get; set; }

    [JsonPropertyName("monetization_options")]
    public int MonetizationOptions { get; set; }

    [JsonPropertyName("stock")]
    public int Stock { get; set; }

    [JsonPropertyName("price")]
    public int Price { get; set; }

    [JsonPropertyName("tax")]
    public int Tax { get; set; }

    [JsonPropertyName("logo")]
    public Logo? Logo { get; set; }

    [JsonPropertyName("homepage_url")]
    public string? HomepageUrl { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("name_id")]
    public string? NameId { get; set; }

    [JsonPropertyName("summary")]
    public string? Summary { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("description_plaintext")]
    public string? DescriptionPlaintext { get; set; }

    [JsonPropertyName("metadata_blob")]
    public string? MetadataBlob { get; set; }

    [JsonPropertyName("profile_url")]
    public string? ProfileUrl { get; set; }

    [JsonPropertyName("media")]
    public Media? Media { get; set; }

    [JsonPropertyName("modfile")]
    public Modfile? Modfile { get; set; }

    [JsonPropertyName("dependencies")]
    public bool Dependencies { get; set; }

    [JsonPropertyName("platforms")]
    public List<Platform>? Platforms { get; set; }

    [JsonPropertyName("metadata_kvp")]
    public List<MetadataKvp>? MetadataKvp { get; set; }

    [JsonPropertyName("tags")]
    public List<Tag>? Tags { get; set; }

    [JsonPropertyName("stats")]
    public Stats? Stats { get; set; }
}

public class Download
{
    [JsonPropertyName("binary_url")]
    public string? BinaryUrl { get; set; }

    [JsonPropertyName("date_expires")]
    public int DateExpires { get; set; }
}

public class Filehash
{
    [JsonPropertyName("md5")]
    public string? Md5 { get; set; }
}

public class Image
{
    [JsonPropertyName("filename")]
    public string? Filename { get; set; }

    [JsonPropertyName("original")]
    public string? Original { get; set; }

    [JsonPropertyName("thumb_320x180")]
    public string? Thumb320x180 { get; set; }

    [JsonPropertyName("thumb_1280x720")]
    public string? Thumb1280x720 { get; set; }
}

public class Logo
{
    [JsonPropertyName("filename")]
    public string? Filename { get; set; }

    [JsonPropertyName("original")]
    public string? Original { get; set; }

    [JsonPropertyName("thumb_320x180")]
    public string? Thumb320x180 { get; set; }

    [JsonPropertyName("thumb_640x360")]
    public string? Thumb640x360 { get; set; }

    [JsonPropertyName("thumb_1280x720")]
    public string? Thumb1280x720 { get; set; }
}

public class Media
{
    [JsonPropertyName("youtube")]
    public List<string>? Youtube { get; set; }

    [JsonPropertyName("sketchfab")]
    public List<string>? Sketchfab { get; set; }

    [JsonPropertyName("images")]
    public List<Image>? Images { get; set; }
}

public class MetadataKvp
{
    [JsonPropertyName("metakey")]
    public string? Metakey { get; set; }

    [JsonPropertyName("metavalue")]
    public string? Metavalue { get; set; }
}

public class Modfile
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("mod_id")]
    public int ModId { get; set; }

    [JsonPropertyName("date_added")]
    public int DateAdded { get; set; }

    [JsonPropertyName("date_updated")]
    public int DateUpdated { get; set; }

    [JsonPropertyName("date_scanned")]
    public int DateScanned { get; set; }

    [JsonPropertyName("virus_status")]
    public int VirusStatus { get; set; }

    [JsonPropertyName("virus_positive")]
    public int VirusPositive { get; set; }

    [JsonPropertyName("virustotal_hash")]
    public string? VirustotalHash { get; set; }

    [JsonPropertyName("filesize")]
    public int Filesize { get; set; }

    [JsonPropertyName("filesize_uncompressed")]
    public int FilesizeUncompressed { get; set; }

    [JsonPropertyName("filehash")]
    public Filehash? Filehash { get; set; }

    [JsonPropertyName("filename")]
    public string? Filename { get; set; }

    [JsonPropertyName("version")]
    public string? Version { get; set; }

    [JsonPropertyName("changelog")]
    public string? Changelog { get; set; }

    [JsonPropertyName("metadata_blob")]
    public string? MetadataBlob { get; set; }

    [JsonPropertyName("download")]
    public Download? Download { get; set; }

    [JsonPropertyName("platforms")]
    public List<Platform> Platforms { get; set; } = [];
}

public class Platform
{
    [JsonPropertyName("platform")]
    public string? PlatformName { get; set; }

    [JsonPropertyName("status")]
    public int Status { get; set; }

    [JsonPropertyName("modfile_live")]
    public int ModfileLive { get; set; }
}

public class Stats
{
    [JsonPropertyName("mod_id")]
    public int ModId { get; set; }

    [JsonPropertyName("popularity_rank_position")]
    public int PopularityRankPosition { get; set; }

    [JsonPropertyName("popularity_rank_total_mods")]
    public int PopularityRankTotalMods { get; set; }

    [JsonPropertyName("downloads_today")]
    public int DownloadsToday { get; set; }

    [JsonPropertyName("downloads_total")]
    public int DownloadsTotal { get; set; }

    [JsonPropertyName("subscribers_total")]
    public int SubscribersTotal { get; set; }

    [JsonPropertyName("ratings_total")]
    public int RatingsTotal { get; set; }

    [JsonPropertyName("ratings_positive")]
    public int RatingsPositive { get; set; }

    [JsonPropertyName("ratings_negative")]
    public int RatingsNegative { get; set; }

    [JsonPropertyName("ratings_percentage_positive")]
    public int RatingsPercentagePositive { get; set; }

    [JsonPropertyName("ratings_weighted_aggregate")]
    public double RatingsWeightedAggregate { get; set; }

    [JsonPropertyName("ratings_display_text")]
    public string? RatingsDisplayText { get; set; }

    [JsonPropertyName("date_expires")]
    public int DateExpires { get; set; }
}

public class UserProfile
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name_id")]
    public string? NameId { get; set; }

    [JsonPropertyName("username")]
    public string? Username { get; set; }

    [JsonPropertyName("display_name_portal")]
    public string? DisplayNamePortal { get; set; }

    [JsonPropertyName("date_online")]
    public int DateOnline { get; set; }

    [JsonPropertyName("date_joined")]
    public int DateJoined { get; set; }

    [JsonPropertyName("avatar")]
    public Avatar? Avatar { get; set; }

    [JsonPropertyName("timezone")]
    public string? Timezone { get; set; }

    [JsonPropertyName("language")]
    public string? Language { get; set; }

    [JsonPropertyName("profile_url")]
    public string? ProfileUrl { get; set; }
}

public class Tag
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("name_localized")]
    public string? NameLocalized { get; set; }

    [JsonPropertyName("date_added")]
    public int DateAdded { get; set; }
}
