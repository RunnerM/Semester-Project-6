namespace EFCore.Config;

public static class ConfigVariables
{
    
    private static readonly IDictionary<string, string> Default = new Dictionary<string, string>
    {
        {"Test", "false"},
        {"ConnectionString", @"Server=tcp:sep6-sql-server.database.windows.net,1433;Initial Catalog=movie-db;Persist Security Info=False;User ID=sqladmin;Password=7DaM5vEa!1q550H8#pFtH;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=60;"}
    };
    
    private static readonly IDictionary<string, string> Test = new Dictionary<string, string>
    {
        {"Test", "true"}
    };
    
    
    public static IEnumerable<KeyValuePair<string, string>> Variables(string environment)
    {
        switch (environment)
        {
            case "Default":
                return Default;
            case "Test":
                return Test;
            default:
                throw new ArgumentException($"Invalid environment name: {environment}");
        }
    }
}