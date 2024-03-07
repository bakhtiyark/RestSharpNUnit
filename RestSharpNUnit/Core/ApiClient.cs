namespace RestSharpNUnit.Core;
internal class ApiClient(string baseUrl)
{
    private readonly RestClient _client = new (baseUrl);

    public RestResponse ExecuteRequest(RestRequest request)
    {
        return _client.Execute(request);
    }
}