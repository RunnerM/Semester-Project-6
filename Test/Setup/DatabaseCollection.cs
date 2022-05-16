using Xunit;

namespace Test.Setup;

[CollectionDefinition("database")]
public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
{
    
}