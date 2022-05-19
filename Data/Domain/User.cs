using System.ComponentModel.DataAnnotations;

namespace Data.Domain;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string? GoogleExternalId { get; set; }
    public IList<UserToplists> TopLists { get; set; }
}