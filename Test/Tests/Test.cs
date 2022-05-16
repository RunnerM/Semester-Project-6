using System.Threading.Tasks;
using EFCore.Config;
using EFCore.Utils;
using Xunit;

namespace Test;

[Collection("database")]
public class Test
{
    [Fact]
    public async Task TestCollection()
    {
        Assert.True(Config.UnitTests);
        await using var context = new Context();
        Assert.Equal("Microsoft.EntityFrameworkCore.InMemory",context.Database.ProviderName);
    }
}