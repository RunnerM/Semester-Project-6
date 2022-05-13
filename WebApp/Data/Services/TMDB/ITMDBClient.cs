using Data.TMDBDomain;

namespace WebApp.Data.Services.TMDB;

public interface ITMDBClient
{
    public Task<List<TMDBMovie>> GetTopRatedMovies();
    public Task<List<TMDBPersonInList>> GetPopularPeople();
    public Task<TMDBMovie> GetMovieById(int MovieId);
    public Task<TMDBPerson> GetPersonById(int PersonId);

    public Task<List<TMDBMovie>> GetTrendingWeeklyMovies();
}