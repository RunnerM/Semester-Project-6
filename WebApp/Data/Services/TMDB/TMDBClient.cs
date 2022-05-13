using System.Text.Json;
using Data.Domain;
using Data.TMDBDomain;
using EFCore.Utils;
using Microsoft.EntityFrameworkCore;

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
        result.EnsureSuccessStatusCode();
        var json = await result.Content.ReadAsStringAsync();
        var movies = JsonSerializer.Deserialize<TMDBPage<TMDBMovie>>(json);
        return movies.Results.ToList();
    }

    public async Task<List<TMDBPersonInList>> GetPopularPeople()
    {
        var result = await _client.GetAsync(BaseUrl + "person/popular?" + ApiKey);
        result.EnsureSuccessStatusCode();
        var json = await result.Content.ReadAsStringAsync();
        var movies = JsonSerializer.Deserialize<TMDBPage<TMDBPersonInList>>(json);
        return movies.Results.ToList();
    }

    public async Task<TMDBMovie> GetMovieById(int MovieId)
    {
        var result = await _client.GetAsync(BaseUrl + "movie/"+ MovieId + ApiKey);
        result.EnsureSuccessStatusCode();
        var json = await result.Content.ReadAsStringAsync();
        var movie = JsonSerializer.Deserialize<TMDBMovie>(json);
        return movie;
    }

    public async Task<TMDBPerson> GetPersonById(int PersonId)
    {
        var result = await _client.GetAsync(BaseUrl + "person/"+ PersonId + ApiKey);
        result.EnsureSuccessStatusCode();
        var json = await result.Content.ReadAsStringAsync();
        var person = JsonSerializer.Deserialize<TMDBPerson>(json);
        return person;
    }

    public async Task<List<TMDBMovie>> GetTrendingWeeklyMovies()
    {
        var result = await _client.GetAsync(BaseUrl + "trending/movie/week?" + ApiKey);
        result.EnsureSuccessStatusCode();
        var json = await result.Content.ReadAsStringAsync();
        var movies = JsonSerializer.Deserialize<TMDBPage<TMDBMovie>>(json);
        return movies.Results.ToList();
    }
}