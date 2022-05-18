using System.Text.Json;
using Data.TMDBDomain;

namespace WebApp.DataServices.Services.TMDB;

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

    public async Task<List<TMDBPerson>> GetPopularPeopleAsync()
    {
        var result = await _client.GetAsync(BaseUrl + "person/popular?" + ApiKey);
        result.EnsureSuccessStatusCode();
        var json = await result.Content.ReadAsStringAsync();
        var movies = JsonSerializer.Deserialize<TMDBPage<TMDBPerson>>(json);
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
        var result = await _client.GetAsync(BaseUrl + "search/movie?query=" + SearchTerm +"&"+ ApiKey);
        result.EnsureSuccessStatusCode();
        var json = await result.Content.ReadAsStringAsync();
        var movies = JsonSerializer.Deserialize<TMDBPage<TMDBMovie>>(json);
        return movies;
    }

    public async Task<TMDBPage<TMDBPerson>> SearchPeopleByTermAsync(string SearchTerm)
    {
        var result = await _client.GetAsync(BaseUrl + "search/person?query=" + SearchTerm +"&"+ ApiKey);
        result.EnsureSuccessStatusCode();
        var json = await result.Content.ReadAsStringAsync();
        var people = JsonSerializer.Deserialize<TMDBPage<TMDBPerson>>(json);
        return people;
    }
    public async Task<List<TMDBMovie>> GetTrendingWeeklyMovies()
    {
        var result = await _client.GetAsync(BaseUrl + "trending/movie/week?" + ApiKey);
        result.EnsureSuccessStatusCode();
        var json = await result.Content.ReadAsStringAsync();
        var movies = JsonSerializer.Deserialize<TMDBPage<TMDBMovie>>(json);
        return movies.Results.ToList();

    }

    public async Task<List<TMDBMovie>> GetCreditsForPersonAsync(int personId)
    {
        var result = await _client.GetAsync(BaseUrl + "person/"+personId+"/movie_credits?" + ApiKey);
        result.EnsureSuccessStatusCode();
        var json = await result.Content.ReadAsStringAsync();
        var movies = JsonSerializer.Deserialize<TMDBCredit>(json);
        var all = movies.cast;
        all.AddRange(movies.crew);
        return all;
    }
}