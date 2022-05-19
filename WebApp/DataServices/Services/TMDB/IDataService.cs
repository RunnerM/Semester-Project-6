using Data.TMDBDomain;

namespace WebApp.DataServices.Services.TMDB;

public interface IDataService
{
    public Task<List<TMDBMovie>> GetTopRatedMoviesAsync();
    public Task<List<TMDBPerson>> GetPopularPersonalAsync();
    
    public Task<TMDBMovie> GetMovieByIdAsync(int MovieId);
    public Task<TMDBPerson> GetPersonByIdAsync(int PersonId);
    
    public Task<TMDBCast> GetCastByMovieIdAsync(int MovieId);
    
    public Task<List<TMDBMovie>> SearchMovieByTermAsync(string SearchTerm);
    public Task<List<TMDBPerson>> SearchPeopleByTermAsync(string SearchTerm);

    public Task<List<TMDBMovie>> GetTrendingMovies();
    public Task<List<TMDBMovie>> GetCreditsForPersonAsync(int personId);

    //Movies
    public Task<List<TMDBMovie>> GetNowPlayingMovies();
    public Task<List<TMDBMovie>> GetPopularMovies();
    public Task<List<TMDBMovie>> GetUpcomingMovies();
}