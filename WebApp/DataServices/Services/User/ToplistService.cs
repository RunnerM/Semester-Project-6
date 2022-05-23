using Data.Domain;
using EFCore.Utils;
using Microsoft.EntityFrameworkCore;
using WebApp.DataServices.Services.TMDB;

namespace WebApp.DataServices.Services.User;

public class ToplistService : IToplistService
{
    private Context _context = new();
    private IDataService _dataService;


    public ToplistService(IDataService dataService)
    {
        _dataService = dataService;
    }

    public async Task<bool> InitToplist(string userExternalId)
    {
        return await _context.Set<Data.Domain.User>().AnyAsync(x => x.GoogleExternalId == userExternalId);
    }


    public async Task<Data.Domain.User> AddToToplistAsync(string externalMovieId, Guid userId)
    {
        var movie = await CheckIfMovieExists(externalMovieId);
        var user = await _context.Set<Data.Domain.User>().SingleAsync(x => x.Id == userId);
        if (user.TopLists.All(x => x.MovieId != movie.Id))
        {
            var index = user.TopLists.Count + 1;
            user.TopLists.Add(new UserToplists()
            {
                MovieId = movie.Id,
                UserId = user.Id,
                TopListIndex = index
            });
            await _context.SaveChangesAsync();
        }

        return user;
    }

    public async Task<Data.Domain.User> RemoveFromToplistAsync(string externalMovieId, Guid userId)
    {
        var movie = await CheckIfMovieExists(externalMovieId);
        var user = await _context.Set<Data.Domain.User>().SingleAsync(x => x.Id == userId);
        if (user.TopLists.Any(x => x.MovieId == movie.Id))
        {
            var index = user.TopLists.First(x => x.MovieId == movie.Id).TopListIndex;
            user.TopLists.Remove(user.TopLists.Single(x => x.MovieId == movie.Id));
            for (int i = index+1; i <= user.TopLists.Count+1; i++)
            {
                user.TopLists.Single(x => x.TopListIndex == i).TopListIndex--;
            }

            await _context.SaveChangesAsync();
        }

        return user;
    }

    public async Task<Data.Domain.User> MoveUpInToplistAsync(string externalMovieId, Guid userId)
    {
        var movie = await CheckIfMovieExists(externalMovieId);
        var user = await _context.Set<Data.Domain.User>().SingleAsync(x => x.Id == userId);
        if (user.TopLists.Any(x => x.MovieId == movie.Id))
        {
            var index = user.TopLists.First(x => x.MovieId == movie.Id).TopListIndex;
            if (index > 1)
            {
                int tempIndex = user.TopLists.Count+2;
                int index1= user.TopLists.Single(x => x.TopListIndex == index - 1).TopListIndex;
                int index2= user.TopLists.Single(x => x.TopListIndex == index).TopListIndex; 
                user.TopLists.Single(x => x.TopListIndex == index).TopListIndex = tempIndex;
                user.TopLists.Single(x => x.TopListIndex == index - 1).TopListIndex = index2;
                user.TopLists.Single(x => x.TopListIndex == tempIndex).TopListIndex = index1;
                await _context.SaveChangesAsync();
            }
        }

        return user;
    }

    public async Task<Data.Domain.User> MoveDownInToplistAsync(string externalMovieId, Guid userId)
    {
        var movie = await CheckIfMovieExists(externalMovieId);
        var user = await _context.Set<Data.Domain.User>().SingleAsync(x => x.Id == userId);
        if (user.TopLists.Any(x => x.MovieId == movie.Id))
        {
            var index = user.TopLists.First(x => x.MovieId == movie.Id).TopListIndex;
            if (index < user.TopLists.Count)
            {
                int tempIndex = user.TopLists.Count+2;
                int index1= user.TopLists.Single(x => x.TopListIndex == index + 1).TopListIndex;
                int index2= user.TopLists.Single(x => x.TopListIndex == index).TopListIndex; 
                user.TopLists.Single(x => x.TopListIndex == index).TopListIndex = tempIndex;
                user.TopLists.Single(x => x.TopListIndex == index + 1).TopListIndex = index2;
                user.TopLists.Single(x => x.TopListIndex == tempIndex).TopListIndex = index1;
                await _context.SaveChangesAsync();
            }
        }
        return user;
    }

    private async Task<Movie> CheckIfMovieExists(string externalMovieId)
    {
        if (!await _context.Set<Movie>().AnyAsync(x => x.TMDBExternalId == externalMovieId))
        {
            var movie = await _dataService.GetMovieByIdAsync(Int32.Parse(externalMovieId));
            var domainMovie = new Movie()
            {
                Title = movie.Title,
                TMDBExternalId = movie.Id.ToString(),
                ReleaseDate = movie.ReleaseDate
            };
            _context.Set<Movie>().Add(domainMovie);
            await _context.SaveChangesAsync();
        }

        return await _context.Set<Movie>().SingleAsync(x => x.TMDBExternalId == externalMovieId);
    }
}