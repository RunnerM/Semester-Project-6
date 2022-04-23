namespace Data;

public class Director
{
    public int MovieId { get; set; }
    public int PersonId { get; set; }
    // navigation properties
    public Movie Movie { get; set; }
    public Person Person { get; set; }
}