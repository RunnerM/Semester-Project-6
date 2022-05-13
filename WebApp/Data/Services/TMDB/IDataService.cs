using Data.TMDBDomain;

namespace WebApp.Data.Services.TMDB;

public interface IDataService
{
    public Task<List<TMDBMovie>> GetTopRatedMoviesAsync();
    public Task<List<TMDBPersonInList>> GetPopularPersonalAsync();
    
    public Task<TMDBMovie> GetMovieByIdAsync(int MovieId);
    public Task<TMDBPerson> GetPersonByIdAsync(int PersonId);

}