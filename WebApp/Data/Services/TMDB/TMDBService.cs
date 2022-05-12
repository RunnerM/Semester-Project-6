using Data.TMDBDomain;

namespace WebApp.Data.Services.TMDB;

public class TMDBService : ITMDBService
{
    private readonly ITMDBClient _tmdbClient;
    
    public TMDBService(ITMDBClient tmdbClient)
    {
        _tmdbClient = tmdbClient;
    }
    
    public Task GetMoviesAsync(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public Task GetMovieAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<TMDBMovie>> GetTopRatedMovies()
    {
        var movies = await _tmdbClient.GetTopRatedMovies();
        foreach (var movie in movies)
            movie.PosterPath = "https://image.tmdb.org/t/p/original/" + movie.PosterPath;
        return movies;
    }
}