using RestSharp;

namespace TestProjectSfApi.Application.Common.Factories;

public interface IClientFactory
{
    RestClient GetRestClient();
}