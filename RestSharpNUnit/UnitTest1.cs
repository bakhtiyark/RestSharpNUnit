using RestSharp;
using RestSharpNUnit.Core;

namespace RestSharpNUnit;

[TestFixture, Category("API")]
public class ApiTests
{
    private readonly ApiClient apiClient = new ApiClient("https://jsonplaceholder.typicode.com");

    [Test]
    public void TestListUsers()
    {
        RestRequest request = new RestRequest("/users", Method.Get);
        RestResponse response = apiClient.ExecuteRequest(request);
        Assert.That(response.StatusCode == HttpStatusCode.OK);
        Assert.That(response.IsSuccessStatusCode);

        var users = Newtonsoft.Json.JsonConvert.DeserializeObject<User[]>(response.Content);
        Assert.That(users.All(u => u.Id > 0 && !string.IsNullOrEmpty(u.Name)));
    }

    [Test]
    public void TestUserResponseHeader()
    {
        RestRequest request = new RestRequest("/users", Method.Get);

        RestResponse response = apiClient.ExecuteRequest(request);

        Assert.That(response.StatusCode == HttpStatusCode.OK);
        Assert.That(response.IsSuccessful);

        Assert.That(response.ContentType, Is.EqualTo("application/json"));
    }
}