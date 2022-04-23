namespace Data;

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public int? BirthYear { get; set; }
    public IList<Director> Directors { get; set; }
    public IList<Star> Stars { get; set; }
}