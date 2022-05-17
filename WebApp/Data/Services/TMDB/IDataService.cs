using Data.TMDBDomain;

namespace WebApp.Data.Services.TMDB;

public interface IDataService
{
    public Task GetMoviesAsync(string searchTerm);
    public Task GetMovieAsync(int id);
    
    public Task<List<TMDBMovie>> GetTopRatedMovies();
    public Task<List<TMDBPersonInList>> GetPopularPersonal();
    
    public Task<TMDBMovie> GetMovieById(int MovieId);
    public Task<TMDBPerson> GetPersonById(int PersonId);
    public Task<List<TMDBMovie>> GetTrendingMovies();

}