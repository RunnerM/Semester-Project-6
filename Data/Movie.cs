namespace Data;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Year { get; set; }
    public Rating Rating { get; set; }
    public IList<Director> Directors { get; set; }
    public IList<Star> Stars { get; set; }
}