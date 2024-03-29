namespace RestSharpNUnit.Specs;

[TestFixture, Category("API")]
public class ApiTests : BaseTest
{
    private readonly ApiClient _apiClient = new (Constants.Website);
    
    [Test]
    public void TestListUsers()
    {
        Log.Information("Executing TestListUsers...");

        var request = new RestRequest("/users");
        var response = _apiClient.ExecuteRequest(request);

        Log.Information("Response received: {@Response}", response);

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.IsSuccessStatusCode);
            var users = Newtonsoft.Json.JsonConvert.DeserializeObject<User[]>(response.Content!);
            Assert.That(users!.All(user => user.Id > 0 && !string.IsNullOrEmpty(user.Name)));

        });
        Log.Information("TestListUsers completed successfully.");
    }

    [Test]
    public void TestUserResponseHeader()
    {
        Log.Information("Executing TestUserResponseHeader...");
        
        var request = new RestRequest("/users");
        var response = _apiClient.ExecuteRequest(request);
        
        Log.Information("Response received: {@Response}", response);

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.IsSuccessful);
            Assert.That(response.ContentType, Is.Not.Null.And.Not.Empty);
            Assert.That(response.ContentType, Is.EqualTo("application/json"));
        });

        Log.Information("TestUserResponseHeader completed successfully.");
    }

    [Test]
    public void TestCreateUser()
    {
        Log.Information("Executing TestCreateUser...");
        
        var newUser = new User { Name = "John Doe", Username = "johndoe" };
        var request = new RestRequest("/users", Method.Post);
        request.AddJsonBody(newUser);

        var response = _apiClient.ExecuteRequest(request);

        Log.Information("Response received: {@Response}", response);

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(response.IsSuccessful);
            Assert.That(response.Content, Is.Not.Null.And.Not.Empty);
            Assert.That(response.Content != null && response.Content.Contains("id"));
        });

        Log.Information("TestCreateUser completed successfully.");
    }

    [Test]
    public void TestInvalidEndpoint()
    {
        Log.Information("Executing TestInvalidEndpoint...");
        
        var request = new RestRequest("/invalid-endpoint");
        var response = _apiClient.ExecuteRequest(request);

        Log.Information("Response received: {@Response}", response);

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            Assert.That(!response.IsSuccessful);
        });

        Log.Information("TestInvalidEndpoint completed successfully.");
    }
}