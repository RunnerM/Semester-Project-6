using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Domain;
using Data.TMDBDomain;
using EFCore.Utils;
using Moq;
using WebApp.DataServices.Services.TMDB;
using WebApp.DataServices.Services.User;
using Xunit;

namespace Test;
[Collection("database")]
public class ToplistService_Test
{
    private Context _context = new();
    private ToplistService _toplistService;
    private Mock<IDataService> _dataServiceMock;

    public ToplistService_Test()
    {
        _dataServiceMock = new Mock<IDataService>();
        _dataServiceMock.Setup(x=>x.GetMovieByIdAsync(It.IsAny<int>())).ReturnsAsync(new TMDBMovie()
        {
            Title = "TestTitle",
            PosterPath = "TestPosterPath",
            ReleaseDate = "TestReleaseDate",
            Id = 111
        });
        _toplistService = new ToplistService(_dataServiceMock.Object); 
        Assert.NotNull(_toplistService);
    }

    private void seed()
    {
        _context.Set<User>().Add(new User()
        {
            Name = "TestUser",
            Email = "TestEmail",
            GoogleExternalId = "TestGoogleId",
            TopLists = new List<UserToplists>()
            {
                new UserToplists()
                {
                    Movie = new Movie()
                    {
                        TMDBExternalId = "01",
                        ReleaseDate = "2000-01-01",
                        Title = "TestMovie",
                    },
                    TopListIndex = 1
                },
                new UserToplists()
                {
                    Movie = new Movie()
                    {
                        TMDBExternalId = "02",
                        ReleaseDate = "2000-01-02",
                        Title = "TestMovie2",
                    },
                    TopListIndex = 2
                },
                new UserToplists()
                {
                    Movie = new Movie()
                    {
                        TMDBExternalId = "03",
                        ReleaseDate = "2000-01-03",
                        Title = "TestMovie3",
                    },
                    TopListIndex = 3
                }
            }
        });
        _context.SaveChanges();
    }

    [Fact]
    public async Task InitToplist_Test()
    {
        seed();
        
        var user= await _toplistService.InitToplist("TestGoogleId");
        Assert.True(user);
    }

    [Fact]
    public async Task AddToToplist_Test()
    {
        seed();
        var user = _context.Set<User>().First();
        user = await _toplistService.AddToToplistAsync("111", user.Id);
        Assert.Equal(4, user.TopLists.Count);
    }

    [Fact]
    public async Task RemoveFromToplist_Test()
    {
        seed();
        var user = _context.Set<User>().First();
        user = await _toplistService.RemoveFromToplistAsync("03", user.Id);
        Assert.Equal(2, user.TopLists.Count);
    }

    [Fact]
    public async Task MoveUpInToplist_Test()
    {
        seed();
        var user = _context.Set<User>().First();
        user = await _toplistService.MoveUpInToplistAsync("03", user.Id);
        Assert.Equal(3, user.TopLists.Count);
    }

    [Fact]
    public async Task MoveDownInToplist_Test()
    {
        seed();
        var user = _context.Set<User>().First();
        user = await _toplistService.MoveDownInToplistAsync("01", user.Id);
        Assert.Equal(3, user.TopLists.Count);
    }
}