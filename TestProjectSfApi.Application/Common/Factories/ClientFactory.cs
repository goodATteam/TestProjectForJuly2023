using RestSharp;
using RestSharp.Serializers.Json;
using TestProjectSfApi.Application.Common.Helpers;

namespace TestProjectSfApi.Application.Common.Factories;

public class ClientFactory : IClientFactory
{
    public RestClient GetRestClient()
    {
        var clientOptions = new RestClientOptions($"https://www.salesforce.com/");

        return new RestClient(clientOptions, configureSerialization: s => s.UseSystemTextJson(JsonInternalSerializerOptions.Default));
    }
}