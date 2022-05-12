namespace Data.Domain;

public class Movie
{
    public Guid Id {get;set;}
    public string Title {get;set;}
    public int Year {get;set;}
    public string TMDBExternalId {get;set;}
    public IList<UserToplists> TopLists { get; set; }
}