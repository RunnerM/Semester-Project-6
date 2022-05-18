using EFCore.Utils;
using Microsoft.EntityFrameworkCore;

namespace WebApp.DataServices.Services.User;

public class UserService : IUserService
{
    private readonly Context _context = new();

    public async Task<Data.Domain.User> GetUserByIdAsync(Guid id)
    {
        if (!await _context.Set<Data.Domain.User>().AnyAsync(u => u.Id == id))
            throw new Exception("User not found");
        return await _context.Set<Data.Domain.User>().SingleAsync(u => u.Id == id);
    }

    public async Task<Data.Domain.User> GetUserByEmailAsync(string email)
    {
        if (!await _context.Set<Data.Domain.User>().AnyAsync(u => u.Email == email))
            throw new Exception("User not found");
        return await _context.Set<Data.Domain.User>().SingleAsync(u => u.Email == email);
    }

    public async Task<bool> DoesUserExistAsync(Guid id)
    {
        return await _context.Set<Data.Domain.User>().AnyAsync(u => u.Id == id);
    }

    public Task<bool> DoesUserExistAsync(string email)
    {
        return _context.Set<Data.Domain.User>().AnyAsync(u => u.Email == email);
    }

    public async Task<Data.Domain.User> CreateUserAsync(IUserService.CreateUserRequest request)
    {
        ValidateCreateRequest(request);
        var user = new Data.Domain.User
        {
            Email = request.Email,
            Name = request.Name,
            GoogleExternalId = request.ExternalId
        };
        _context.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<Data.Domain.User> UpdateUserAsync(IUserService.UpdateUserRequest request)
    {
        ValidateUpdateRequest(request);
        if (!_context.Set<Data.Domain.User>().Any(u => u.Id == request.Id))
            throw new Exception("User not found");

        var user = await _context.Set<Data.Domain.User>().SingleAsync(u => u.Id == request.Id);
        if (!string.IsNullOrEmpty(request.Email))
            user.Email = request.Email;
        if (!string.IsNullOrEmpty(request.Name))
            user.Name = request.Name;
        if (!string.IsNullOrEmpty(request.ExternalId))
            user.GoogleExternalId = request.ExternalId;
        await _context.SaveChangesAsync();
        var random = _context.Set<Data.Domain.User>().ToList();
        return user;
    }

    private void ValidateUpdateRequest(IUserService.UpdateUserRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Email) && string.IsNullOrWhiteSpace(request.Name))
            throw new Exception("Email or Name required to update user");

        if (request.Id == Guid.Empty)
            throw new Exception("Id is required");
    }
    private void ValidateCreateRequest(IUserService.CreateUserRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.ExternalId))
            throw new Exception("Invalid request");
    }
}