namespace Data.TMDBDomain;

public class TMDBCast
{
    public int cast_id { get; set; }
    public List<TMDBPerson> cast { get; set; }
    public List<TMDBPerson> crew { get; set; }
    public List<TMDBPerson> directors { get; set; }
}