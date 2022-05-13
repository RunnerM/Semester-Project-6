using Data.TMDBDomain;

namespace WebApp.Data.Services.TMDB;

public class DataService : IDataService
{
    private readonly ITMDBClient _tmdbClient;
    
    public DataService(ITMDBClient tmdbClient)
    {
        _tmdbClient = tmdbClient;
    }

    public async Task<List<TMDBMovie>> GetTopRatedMoviesAsync()
    {
        var movies = await _tmdbClient.GetTopRatedMoviesAsync();
        foreach (var movie in movies)
            movie.PosterPath = "https://image.tmdb.org/t/p/original/" + movie.PosterPath;
        return movies;
    }
    
    public async Task<List<TMDBPersonInList>> GetPopularPersonalAsync()
    {
        var people = await _tmdbClient.GetPopularPeopleAsync();
        foreach (var person in people)
            person.ProfilePath = "https://image.tmdb.org/t/p/original/" + person.ProfilePath;
        return people;
    }

    public async Task<TMDBMovie> GetMovieByIdAsync(int MovieId)
    {
        var movie=await _tmdbClient.GetMovieByIdAsync(MovieId);
        movie.PosterPath = "https://image.tmdb.org/t/p/original/" + movie.PosterPath;
        movie.BackdropPath = "https://image.tmdb.org/t/p/original/" + movie.BackdropPath;
        return movie;
    }

    public async Task<TMDBPerson> GetPersonByIdAsync(int PersonId)
    {
        var person = await _tmdbClient.GetPersonByIdAsync(PersonId);
        person.ProfilePath = "https://image.tmdb.org/t/p/original/" + person.ProfilePath;
        return person;
    }
}