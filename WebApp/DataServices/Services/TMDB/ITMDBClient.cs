using Data.TMDBDomain;

namespace WebApp.DataServices.Services.TMDB;

public interface ITMDBClient
{
    public Task<List<TMDBMovie>> GetTopRatedMoviesAsync();
    public Task<List<TMDBPerson>> GetPopularPeopleAsync();
    public Task<TMDBMovie> GetMovieByIdAsync(int MovieId);
    public Task<TMDBPerson> GetPersonByIdAsync(int PersonId);
    
    public Task<TMDBCast> GetCastByMovieIdAsync(int MovieId);
    
    public Task<TMDBPage<TMDBMovie>> SearchMovieByTermAsync(string SearchTerm);
    public Task<TMDBPage<TMDBPerson>> SearchPeopleByTermAsync(string SearchTerm);
}