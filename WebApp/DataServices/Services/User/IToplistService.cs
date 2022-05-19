namespace WebApp.DataServices.Services.User;

public interface IToplistService
{
    public Task<bool> InitToplist(string userExternalId);
    public Task<Data.Domain.User> AddToToplistAsync(string externalMovieId, Guid userId);
    public Task<Data.Domain.User> RemoveFromToplistAsync(string externalMovieId, Guid userId);
    public Task<Data.Domain.User> MoveUpInToplistAsync(string externalMovieId, Guid userId);
    public Task<Data.Domain.User> MoveDownInToplistAsync(string externalMovieId, Guid userId);

}