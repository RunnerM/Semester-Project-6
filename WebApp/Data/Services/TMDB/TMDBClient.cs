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

    public async Task<List<TMDBMovie>> GetTopRatedMoviesAsync()
    {
        var result= await _client.GetAsync(BaseUrl+"movie/top_rated?" + ApiKey);
        result.EnsureSuccessStatusCode();
        var json = await result.Content.ReadAsStringAsync();
        var movies = JsonSerializer.Deserialize<TMDBPage<TMDBMovie>>(json);
        return movies.Results.ToList();
    }

    public async Task<List<TMDBPersonInList>> GetPopularPeopleAsync()
    {
        var result = await _client.GetAsync(BaseUrl + "person/popular?" + ApiKey);
        result.EnsureSuccessStatusCode();
        var json = await result.Content.ReadAsStringAsync();
        var movies = JsonSerializer.Deserialize<TMDBPage<TMDBPersonInList>>(json);
        return movies.Results.ToList();
    }

    public async Task<TMDBMovie> GetMovieByIdAsync(int MovieId)
    {
        var result = await _client.GetAsync(BaseUrl + "movie/"+ MovieId +"?"+ ApiKey);
        result.EnsureSuccessStatusCode();
        var json = await result.Content.ReadAsStringAsync();
        var movie = JsonSerializer.Deserialize<TMDBMovie>(json);
        return movie;
    }

    public async Task<TMDBPerson> GetPersonByIdAsync(int PersonId)
    {
        var result = await _client.GetAsync(BaseUrl + "person/"+ PersonId +"?"+ ApiKey);
        result.EnsureSuccessStatusCode();
        var json = await result.Content.ReadAsStringAsync();
        var person = JsonSerializer.Deserialize<TMDBPerson>(json);
        return person;
    }

    public async Task<TMDBCast> GetCastByMovieIdAsync(int MovieId)
    {
        var result = await _client.GetAsync(BaseUrl + "movie/"+ MovieId+"/credits?" + ApiKey);
        result.EnsureSuccessStatusCode();
        var json = await result.Content.ReadAsStringAsync();
        var cast = JsonSerializer.Deserialize<TMDBCast>(json);
        return cast;
    }

    public async Task<TMDBPage<TMDBMovie>> SearchMovieByTermAsync(string SearchTerm)
    {
        var s = BaseUrl + "search/movie?query=" + SearchTerm + ApiKey;
        var result = await _client.GetAsync(BaseUrl + "search/movie?query=" + SearchTerm +"&"+ ApiKey);
        result.EnsureSuccessStatusCode();
        var json = await result.Content.ReadAsStringAsync();
        var movies = JsonSerializer.Deserialize<TMDBPage<TMDBMovie>>(json);
        return movies;
    }
    
}