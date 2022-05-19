using Data.TMDBDomain;
using EFCore.Utils;

namespace WebApp.DataServices.Services.TMDB;

public class DataService : IDataService
{
    private readonly ITMDBClient _tmdbClient;
    private readonly Context _context;

    private const string DummyImageUrl =
        "https://e7.pngegg.com/pngimages/923/367/png-clipart-man-white-and-black-with-eyeglasses-art-beard-art-face-logo-beard-face-people.png";

    private const string ImageBaseUrl = "https://image.tmdb.org/t/p/original/";


    public DataService(ITMDBClient tmdbClient)
    {
        _tmdbClient = tmdbClient;
        _context = new Context();
    }

    public async Task<List<TMDBMovie>> GetTopRatedMoviesAsync()
    {
        var movies = await _tmdbClient.GetTopRatedMoviesAsync();
        foreach (var movie in movies)
            movie.PosterPath = ImageBaseUrl + movie.PosterPath;
        return movies;
    }

    public async Task<List<TMDBPerson>> GetPopularPersonalAsync()
    {
        var people = await _tmdbClient.GetPopularPeopleAsync();
        people.ForEach(x =>
        {
            if (x.ProfilePath == null)
                x.ProfilePath = DummyImageUrl;
            else
                x.ProfilePath = ImageBaseUrl + x.ProfilePath;
        });
        return people;
    }

    public async Task<TMDBMovie> GetMovieByIdAsync(int MovieId)
    {
        var movie = await _tmdbClient.GetMovieByIdAsync(MovieId);
        movie.PosterPath = ImageBaseUrl + movie.PosterPath;
        movie.BackdropPath = ImageBaseUrl + movie.BackdropPath;
        return movie;
    }

    public async Task<TMDBPerson> GetPersonByIdAsync(int PersonId)
    {
        var person = await _tmdbClient.GetPersonByIdAsync(PersonId);
        if (person.ProfilePath == null)
            person.ProfilePath = DummyImageUrl;
        else
            person.ProfilePath = ImageBaseUrl + person.ProfilePath;
        return person;
    }

    public async Task<TMDBCast> GetCastByMovieIdAsync(int MovieId)
    {
        var cast = await _tmdbClient.GetCastByMovieIdAsync(MovieId);
        
        cast.cast = cast.cast.DistinctBy(x => x.Name).ToList();
        cast.crew = cast.crew.DistinctBy(x => x.Name).ToList();
        cast.directors = new List<TMDBPerson>().ToList();
       
        cast.cast.ToList().ForEach(x =>
        {
            if (x.ProfilePath == null)
                x.ProfilePath = DummyImageUrl;
            else
                x.ProfilePath = ImageBaseUrl + x.ProfilePath;
        });
        cast.crew.ToList().ForEach(x =>
        {
            if (x.ProfilePath == null)
                x.ProfilePath = DummyImageUrl;
            else
                x.ProfilePath = ImageBaseUrl + x.ProfilePath;

            if (x.Job.Contains("Director"))
            {
                cast.directors.Add(x);
                cast.crew.Remove(x);
            }
        });

        cast.directors = cast.directors.DistinctBy(x => x.Name).ToList();
        return cast;
    }

    public async Task<List<TMDBMovie>> SearchMovieByTermAsync(string SearchTerm)
    {
        var movies = await _tmdbClient.SearchMovieByTermAsync(SearchTerm);
        movies.Results.ForEach(x =>
        {
            x.PosterPath = ImageBaseUrl + x.PosterPath;
            x.BackdropPath = ImageBaseUrl + x.BackdropPath;
        });
        return movies.Results;
    }

    public async Task<List<TMDBPerson>> SearchPeopleByTermAsync(string SearchTerm)
    {
        var people = await _tmdbClient.SearchPeopleByTermAsync(SearchTerm);
        people.Results.ForEach(x =>
        {
            if (x.ProfilePath == null)
                x.ProfilePath = DummyImageUrl;
            else
                x.ProfilePath = ImageBaseUrl + x.ProfilePath;
        });
        return people.Results;
    }

    public async Task<List<TMDBMovie>> GetTrendingMovies()
    {
        var movies = await _tmdbClient.GetTrendingWeeklyMovies();
        foreach (var movie in movies)
        {
            movie.PosterPath = "https://image.tmdb.org/t/p/original/" + movie.PosterPath;
            movie.BackdropPath = "https://image.tmdb.org/t/p/original/" + movie.BackdropPath;
        }

        return movies;
    }

    public async Task<List<TMDBMovie>> GetCreditsForPersonAsync(int personId)
    {
        var m = await _tmdbClient.GetCreditsForPersonAsync(personId);
        m.ForEach(x =>
            {
                x.PosterPath = ImageBaseUrl + x.PosterPath;
                x.BackdropPath = ImageBaseUrl + x.BackdropPath;
            }
        );
        return m;
    }


    public async Task<List<TMDBMovie>> GetNowPlayingMovies() 
    {
        var movies = await _tmdbClient.GetNowPlayingMoviesAsync();
        foreach (var movie in movies)
            movie.PosterPath = ImageBaseUrl + movie.PosterPath;
        return movies;
    }
    public async Task<List<TMDBMovie>> GetPopularMovies()
    {
        var movies = await _tmdbClient.GetPopularMoviesAsync();
        foreach (var movie in movies)
            movie.PosterPath = ImageBaseUrl + movie.PosterPath;
        return movies;
    } 
    public async Task<List<TMDBMovie>> GetUpcomingMovies()
    {
    var movies = await _tmdbClient.GetUpcomingMoviesAsync();
    foreach (var movie in movies)
        movie.PosterPath = ImageBaseUrl + movie.PosterPath;
    return movies;
    } 
}