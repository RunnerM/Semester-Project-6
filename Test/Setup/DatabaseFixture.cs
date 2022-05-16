using EFCore.Config;

namespace Test.Setup;

public class DatabaseFixture
{
    public DatabaseFixture()
    {
        Config.Init(ConfigVariables.Variables("Test"));
    }

}