namespace Data;

public class Rating
{
    public int MovieId { get; set; }
    public float RatingValue { get; set; }
    public int Votes { get; set; }
    //navigation properties
    public Movie Movie { get; set; }
}