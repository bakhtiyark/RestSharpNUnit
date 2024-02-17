namespace RestSharpNUnit.Core;
internal class ApiClient
{
    private readonly RestClient client;

    public ApiClient(string baseUrl)
    {
        client = new RestClient(baseUrl);
    }

    public RestResponse ExecuteRequest(RestRequest request)
    {
        return client.Execute(request);
    }
}