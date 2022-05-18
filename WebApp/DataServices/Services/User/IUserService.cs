namespace WebApp.DataServices.Services.User;

public interface IUserService
{
    public Task<Data.Domain.User> GetUserByIdAsync(Guid id);
    public Task<Data.Domain.User> GetUserByEmailAsync(string email);
    
    public Task<bool> DoesUserExistAsync(Guid id);
    public Task<bool> DoesUserExistAsync(string email);
    
    public record CreateUserRequest(string Email,  string Name, string ExternalId);
    public Task<Data.Domain.User> CreateUserAsync(CreateUserRequest request);
    
    public record UpdateUserRequest(string Email,  string Name, string ExternalId, Guid Id);
    public Task<Data.Domain.User> UpdateUserAsync(UpdateUserRequest request);
    
    
    
    
    
}