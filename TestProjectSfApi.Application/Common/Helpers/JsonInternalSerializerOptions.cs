using System.Text.Json;
using System.Text.Json.Serialization;

namespace TestProjectSfApi.Application.Common.Helpers
{
    public static class JsonInternalSerializerOptions
    {
        public static JsonSerializerOptions Default { get; } = new()
        {
            Converters =
            {
                new JsonStringEnumConverter(),
            },
        };
    }
}