using Data.TMDBDomain;

namespace WebApp.Data.Services.TMDB;

public class DataService : IDataService
{
    private readonly ITMDBClient _tmdbClient;
    
    public DataService(ITMDBClient tmdbClient)
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
    
    public async Task<List<TMDBPersonInList>> GetPopularPersonal()
    {
        var people = await _tmdbClient.GetPopularPeople();
        foreach (var person in people)
            person.ProfilePath = "https://image.tmdb.org/t/p/original/" + person.ProfilePath;
        return people;
    }

    public Task<TMDBMovie> GetMovieById(int MovieId)
    {
        return _tmdbClient.GetMovieById(MovieId);
    }

    public Task<TMDBPerson> GetPersonById(int PersonId)
    {
        return  _tmdbClient.GetPersonById(PersonId);
    }
}