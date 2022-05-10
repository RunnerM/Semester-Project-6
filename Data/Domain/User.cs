using System.ComponentModel.DataAnnotations;

namespace Data.Domain;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string email { get; set; }
    public string password { get; set; }
}