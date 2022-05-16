namespace EFCore.Config;

public static class Config
{
    private static IEnumerable<KeyValuePair<string, string>> Variables;
        
    public static void Init(IEnumerable<KeyValuePair<string, string>> variables)
    {
        Variables = variables;
    }

    private static string Get(string key) => Variables.SingleOrDefault(v => v.Key == key).Value;
    public static string ConnectionString => Get("ConnectionString");
    public static bool UnitTests => Get("Test") == "true";
}