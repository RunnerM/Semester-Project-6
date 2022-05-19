using System;
using System.Linq;
using System.Threading.Tasks;
using Data.Domain;
using EFCore.Utils;
using Microsoft.EntityFrameworkCore;
using WebApp.DataServices.Services.User;
using Xunit;

namespace Test;

[Collection("database")]
public class UserService_Test
{
    private readonly UserService _userService;
    private Context _context = new();

    public UserService_Test()
    {
        _userService = new UserService();
        Assert.NotNull(_userService);
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
    }

    private void seed()
    {
        _context.Set<User>().Add(new User()
        {
            Name = "Test",
            Email = "test",
            GoogleExternalId = "externalId"
        });
        _context.SaveChanges();
    }

    [Fact]
    public async Task GetUser_Test()
    {
        seed();
        var user = await _userService.GetUserByEmailAsync("test");
        Assert.NotNull(user);
        Assert.Equal("Test", user.Name);
        var user2 = await _userService.GetUserByIdAsync(user.Id);
        Assert.NotNull(user);
        Assert.Equal("Test", user.Name);
    }

    [Fact]
    public async Task GetUser_NotFound_Test()
    {
        var e = await Assert.ThrowsAsync<Exception>(async () =>
            await _userService.GetUserByEmailAsync("test@test.com"));
        Assert.Equal("User not found", e.Message);
        e = await Assert.ThrowsAsync<Exception>(async () => await _userService.GetUserByIdAsync(Guid.NewGuid()));
        Assert.Equal("User not found", e.Message);
    }

    [Fact]
    public async Task CreateUser_Test()
    {
        var user = await _userService.CreateUserAsync(
            new IUserService.CreateUserRequest("test@test.com", "Test", "test"));
        Assert.NotNull(user);
        var cuser = _context.Set<User>().SingleOrDefault(u => u.Email == "test@test.com");
        Assert.NotNull(cuser);
        Assert.Equal("Test", cuser.Name);
    }
    [Fact]
    public async Task CreateUser_InvalidRequest_Test()
    {
        var e = await Assert.ThrowsAnyAsync<Exception>(async () => await _userService.CreateUserAsync(
            new IUserService.CreateUserRequest("test@test.com", null, null)));
        Assert.Equal("Invalid request", e.Message);
    }

    [Fact]
    public async Task UpdateUser_Test()
    {
        seed();
        var userToUpdate = await _context.Set<User>().FirstAsync();
        var user = await _userService.UpdateUserAsync(new IUserService.UpdateUserRequest("test@test.com","Test2", "test", userToUpdate.Id));
        Assert.NotNull(user);
        Assert.Equal("Test2", user.Name);
        await using var context = new Context();
        var userRefetch= context.Set<User>().First();
        Assert.Equal("Test2", userRefetch.Name);
        Assert.Equal("test@test.com", userRefetch.Email);
    }

    [Fact]
    public async Task UpdateUser_InvalidRequest_Test()
    {
        seed();
        var userToUpdate = await _context.Set<User>().FirstAsync();
        var e = await Assert.ThrowsAnyAsync<Exception>( async ()=> await _userService.UpdateUserAsync(
            new IUserService.UpdateUserRequest(null, null, "test", userToUpdate.Id)));
        Assert.Equal("Email or Name required to update user", e.Message);
        e = await Assert.ThrowsAnyAsync<Exception>( async ()=> await _userService.UpdateUserAsync(
            new IUserService.UpdateUserRequest("test", "Test2", "test", Guid.Empty)));
        Assert.Equal("Id is required", e.Message);
    }

}