using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Domain;
using EFCore.Config;
using EFCore.Utils;
using Xunit;

namespace Test;

[Collection("database")]
public class UnitTest1
{
    [Fact]
    public async Task Test()
    {
        Assert.True(Config.UnitTests);
        await using var context = new Context();
        Assert.Equal("Microsoft.EntityFrameworkCore.InMemory",context.Database.ProviderName);
    }

    [Fact]
    public async Task TestAutoIncludes()
    {
        await using var context = new Context();
        context.Add(new User()
        {
            Id = Guid.NewGuid(),
            Email = "test@test.com",
            GoogleExternalId = "id",
            Name = "Marton",
            TopLists = new List<UserToplists>()
            {
                new UserToplists()
                {
                    Movie = new Movie()
                    {
                        Id = Guid.NewGuid(),
                        Title = "title",
                        TMDBExternalId = "id",
                        Year = 2000
                    }
                }
            }
        });
        await context.SaveChangesAsync();
        var user = context.Set<User>().First();
        Assert.NotNull(user.TopLists);
    }
}