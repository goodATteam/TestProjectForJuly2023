using MediatR;
using System.Reflection;
using System.Text.Json.Serialization;
using RestSharp;
using TestProjectSfApi.Application.Common.Factories;
using TestProjectSfApi.Domain.Entities;

namespace TestProjectSfApi.Application.SystemDataLoadLogItems.Queries
{
    public record GetRecordsQuery : IRequest<QueryResponseDTO>
    {
        public string? QueryFilter { get; set; }
        public string? TableName { get; set; }
    }

    public class GetRecordsQueryHandler : IRequestHandler<GetRecordsQuery, QueryResponseDTO>
    {
        private readonly RestClient _restClient;

        public GetRecordsQueryHandler(IClientFactory restClientFactory)
        {
            _restClient = restClientFactory.GetRestClient();
        }

        public async Task<QueryResponseDTO> Handle(GetRecordsQuery request, CancellationToken cancellationToken)
        {
            var sqlQuery = $"{GetBaseSqlStatement<SystemDataLoadLog>(request.TableName)}+WHERE+{request.QueryFilter}";
            var queryResult = await _restClient.GetJsonAsync<QueryResponseDTO>(
                $"/query?q={sqlQuery}",
                cancellationToken);

            return new QueryResponseDTO
            {
                Records = queryResult.Records
            };
        }

        protected string GetBaseSqlStatement<TEnt>(string tableName)
        {
            var fieldNames = string.Join(',',
                typeof(TEnt).GetProperties()
                    .Select(prop => prop.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name ?? prop.Name));
            return $"SELECT+{fieldNames}+FROM+{tableName}";
        }
    }
}