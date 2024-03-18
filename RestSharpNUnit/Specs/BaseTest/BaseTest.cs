namespace RestSharpNUnit.Specs;

public class BaseTest
{
    [OneTimeSetUp]
    public void Setup()
    {
        ConfigureLogging();
    }
}