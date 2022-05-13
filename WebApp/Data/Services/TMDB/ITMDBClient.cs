using Data.TMDBDomain;

namespace WebApp.Data.Services.TMDB;

public interface ITMDBClient
{
    public Task<List<TMDBMovie>> GetTopRatedMoviesAsync();
    public Task<List<TMDBPersonInList>> GetPopularPeopleAsync();
    public Task<TMDBMovie> GetMovieByIdAsync(int MovieId);
    public Task<TMDBPerson> GetPersonByIdAsync(int PersonId);
    
    public Task<TMDBCast> GetCastByMovieIdAsync(int MovieId);
}