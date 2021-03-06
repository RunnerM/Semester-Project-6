using System.Text.Json.Serialization;

namespace Data.TMDBDomain;

public class TMDBPerson
{
    [JsonPropertyName("birthday")]
    public string? Birthday { get; set; }
    
    [JsonPropertyName("known_for_department")]
    public string? KnownForDepartment { get; set; }
    
    [JsonPropertyName("deathday")]
    public string? Deathday { get; set; }
    
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    
    [JsonPropertyName("also_known_as")]
    public List<string?>? AlsoKnownAs { get; set; }
    
    [JsonPropertyName("gender")]
    public int Gender { get; set; }
    
    [JsonPropertyName("biography")]
    public string? Biography { get; set; }
    
    [JsonPropertyName("popularity")]
    public double Popularity { get; set; }
    
    [JsonPropertyName("place_of_birth")]
    public string? PlaceOfBirth { get; set; }
    
    [JsonPropertyName("profile_path")]
    public string? ProfilePath { get; set; }
    
    [JsonPropertyName("adult")]
    public bool Adult { get; set; }
    
    [JsonPropertyName("imdb_id")]
    public string? ImdbId { get; set; }
    
    [JsonPropertyName("homepage")]
    public string? Homepage { get; set; }
    
    [JsonPropertyName("job")]
    public string? Job { get; set; }
    
    [JsonPropertyName("original_name")]
    public string? OriginalName { get; set; }

}