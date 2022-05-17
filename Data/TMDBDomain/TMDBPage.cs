using System.Text.Json.Serialization;

namespace Data.TMDBDomain;

public class TMDBPage<TType>
{
    [JsonPropertyName("page")]
    public int Page { get; set; }
    
    [JsonPropertyName("results")]
    public List<TType> Results { get; set; }
    
    [JsonPropertyName("total_results")]
    public int TotalPages { get; set; }
    
    [JsonPropertyName("total_pages")]
    public int TotalResults { get; set; }
    
    
    
    
}