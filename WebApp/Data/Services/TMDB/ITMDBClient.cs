using Data.TMDBDomain;

namespace WebApp.Data.Services.TMDB;

public interface ITMDBClient
{
    public Task<List<TMDBMovie>> GetTopRatedMovies();

}