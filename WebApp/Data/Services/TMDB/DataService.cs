using Data.TMDBDomain;
using EFCore.Utils;

namespace WebApp.Data.Services.TMDB;

public class DataService : IDataService
{
    private readonly ITMDBClient _tmdbClient;
    private readonly Context _context;
    
    public DataService(ITMDBClient tmdbClient)
    {
        _tmdbClient = tmdbClient;
        _context = new Context();
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

    public async Task<TMDBCast> GetCastByMovieIdAsync(int MovieId)
    {
        var cast= await _tmdbClient.GetCastByMovieIdAsync(MovieId);
        cast.cast=cast.cast.DistinctBy(x=>x.Name).ToList();
        cast.crew=cast.crew.DistinctBy(x=>x.Name).ToList();
        cast.cast.ForEach(x =>
        {
            if (x.ProfilePath==null)
                x.ProfilePath = "https://e7.pngegg.com/pngimages/923/367/png-clipart-man-white-and-black-with-eyeglasses-art-beard-art-face-logo-beard-face-people.png";
            else
                x.ProfilePath = "https://image.tmdb.org/t/p/original/" + x.ProfilePath;
        });
        cast.crew.ForEach(x =>
        {
            if (x.ProfilePath==null)
                x.ProfilePath = "https://e7.pngegg.com/pngimages/923/367/png-clipart-man-white-and-black-with-eyeglasses-art-beard-art-face-logo-beard-face-people.png";
            else
                x.ProfilePath = "https://image.tmdb.org/t/p/original/" + x.ProfilePath;
        });
        
        return cast;
    }

    public async Task<TMDBPage<TMDBMovie>> SearchMovieByTermAsync(string SearchTerm)
    {
        var movies = await _tmdbClient.SearchMovieByTermAsync(SearchTerm);
        movies.Results.ForEach(x =>
        {
            x.PosterPath = "https://image.tmdb.org/t/p/original/" + x.PosterPath;
            x.BackdropPath = "https://image.tmdb.org/t/p/original/" + x.BackdropPath;
        });
        return movies;
    }
}