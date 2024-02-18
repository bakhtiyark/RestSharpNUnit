namespace RestSharpNUnit.Specs;

[TestFixture, Category("API")]
public class ApiTests
{
    private readonly ApiClient apiClient = new ApiClient("https://jsonplaceholder.typicode.com");
    [OneTimeSetUp]
    public void Setup()
    {
        Logger.ConfigureLogging();
    }
    [Test]
    public void TestListUsers()
    {
        Log.Information("Executing TestListUsers...");

        RestRequest request = new RestRequest("/users", Method.Get);
        RestResponse response = apiClient.ExecuteRequest(request);

        Log.Information("Response received: {@Response}", response);

        Assert.That(response.StatusCode == HttpStatusCode.OK);
        Assert.That(response.IsSuccessStatusCode);

        var users = Newtonsoft.Json.JsonConvert.DeserializeObject<User[]>(response.Content);
        Assert.That(users.All(user => user.Id > 0 && !string.IsNullOrEmpty(user.Name)));

        Log.Information("TestListUsers completed successfully.");
    }

    [Test]
    public void TestUserResponseHeader()
    {
        Log.Information("Executing TestUserResponseHeader...");
        RestRequest request = new RestRequest("/users", Method.Get);

        RestResponse response = apiClient.ExecuteRequest(request);
        
        Log.Information("Response received: {@Response}", response);

        Assert.That(response.StatusCode == HttpStatusCode.OK);
        Assert.That(response.IsSuccessful);
        Assert.That(response.ContentType, Is.Not.Null.And.Not.Empty);
        Assert.That(response.ContentType, Is.EqualTo("application/json"));

        Log.Information("TestUserResponseHeader completed successfully.");
    }

    [Test]
    public void TestCreateUser()
    {
        Log.Information("Executing TestCreateUser...");
        
        var newUser = new User { Name = "John Doe", Username = "johndoe" };
        RestRequest request = new RestRequest("/users", Method.Post);
        request.AddJsonBody(newUser);

        RestResponse response = apiClient.ExecuteRequest(request);

        Log.Information("Response received: {@Response}", response);

        Assert.That(response.StatusCode == HttpStatusCode.Created);
        Assert.That(response.IsSuccessful);
        Assert.That(response.Content, Is.Not.Null.And.Not.Empty);

        Assert.That(response.Content.Contains("id"));

        Log.Information("TestCreateUser completed successfully.");
    }

    [Test]
    public void TestInvalidEndpoint()
    {
        Log.Information("Executing TestInvalidEndpoint...");

        RestRequest request = new RestRequest("/invalidendpoint", Method.Get);
        RestResponse response = apiClient.ExecuteRequest(request);

        Log.Information("Response received: {@Response}", response);

        Assert.That(response.StatusCode == HttpStatusCode.NotFound);
        Assert.That(!response.IsSuccessful);

        Log.Information("TestInvalidEndpoint completed successfully.");
    }
}