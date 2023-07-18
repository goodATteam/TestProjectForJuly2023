using System.Text.Json.Serialization;
using TestProjectSfApi.Application.Common.Helpers;
using TestProjectSfApi.Domain.Common;
using TestProjectSfApi.Domain.Entities;
using TestProjectSfApi.Domain.Events;

namespace TestProjectSfApi.Application.SystemDataLoadLogItems.Queries
{
    public class QueryResponseDTO : QueryResponse<SystemDataLoadLog>
    {
        [JsonPropertyName("records")]
        public List<SystemDataLoadLog> Records { get; set; }
    }
}