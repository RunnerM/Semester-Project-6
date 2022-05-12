using System.Text.Json;
using Data.TMDBDomain;

namespace WebApp.Data.Services.TMDB;

public class TMDBClient : ITMDBClient
{
    private HttpClient _client;

    private const string BaseUrl = "https://api.themoviedb.org/3/";
    private const string ApiKey = "api_key=53002af11ac265ef6a04fec95b8092b2";

    public TMDBClient()
    {
        _client = new HttpClient();
    }

    public async Task<List<TMDBMovie>> GetTopRatedMovies()
    {
        var result= await _client.GetAsync(BaseUrl+"movie/top_rated?" + ApiKey);
        var json = await result.Content.ReadAsStringAsync();
        var movies = JsonSerializer.Deserialize<TMDBPage<TMDBMovie>>(json);
        return movies.Results.ToList();
    }

}