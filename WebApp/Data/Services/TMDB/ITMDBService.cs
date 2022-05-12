using Data.TMDBDomain;

namespace WebApp.Data.Services.TMDB;

public interface ITMDBService
{
    public Task GetMoviesAsync(string searchTerm);
    public Task GetMovieAsync(int id);
    
    public Task<List<TMDBMovie>> GetTopRatedMovies();
    
}