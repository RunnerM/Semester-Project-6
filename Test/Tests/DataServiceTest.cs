using System.Threading.Tasks;
using Data.TMDBDomain;
using Moq;
using WebApp.DataServices.Services.TMDB;
using Xunit;

namespace Test;

[Collection("database")]
public class DataServiceTest
{
    private readonly Mock<ITMDBClient> clientMock = new Mock<ITMDBClient>();

    private IDataService BuildService()
    {
        clientMock.Setup(x => x.GetMovieByIdAsync(It.IsAny<int>())).ReturnsAsync(new TMDBMovie()
        {
            Adult = false,
            Id = 01,
            OriginalLanguage = "eng",
            BackdropPath = "/something.png"
        });
        return new DataService(clientMock.Object);
    }

    [Fact]
    public async Task GetMoviesTest()
    {
        var service = BuildService();
        var res=await service.GetMovieByIdAsync(01);
        Assert.Contains("https://image.tmdb.org/t/p/original", res.BackdropPath);
    }
}