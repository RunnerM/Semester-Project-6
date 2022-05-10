using System.ComponentModel.DataAnnotations;

namespace Data.Domain;

public class UserToplists
{
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid MovieId { get; set; }
    public Movie Movie { get; set; }
    [Required]
    public int TopListIndex { get; set; }
}