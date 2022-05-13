using System.Text.Json.Serialization;

namespace Data.TMDBDomain;

public class TMDBPersonInList
{
    [JsonPropertyName("profile_path")]
    public string ProfilePath { get; set; }
    [JsonPropertyName("adult")]
    public bool Adult { get; set; }
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("known_for")]
    public object KnownFor { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("popularity")]
    public double Popularity { get; set; }
    

}