using MediatR;
using RestSharp;
using System.Text.Json;
using TestProjectSfApi.Application.Common.Helpers;
using TestProjectSfApi.Application.Common.Factories;
using TestProjectSfApi.Domain.Entities;

namespace TestProjectSfApi.Application.SystemDataLoadLogItems.Commands.UpdateSystemDataLoadLogItem
{
    public record UpdateSystemDataLoadLogItemCommand : IRequest
    {
        public string? Id { get; set; }

        public SystemDataLoadLog SystemDataLoadLogInit { get; set; }

        public string? tableName { get; set; }
    }

    public class UpdateSystemDataLoadLogItemCommandHandler : IRequestHandler<UpdateSystemDataLoadLogItemCommand>
    {
        private readonly RestClient _restClient;

        public UpdateSystemDataLoadLogItemCommandHandler(IClientFactory restClientFactory)
        {
            _restClient = restClientFactory.GetRestClient();
        }


        public async Task Handle(UpdateSystemDataLoadLogItemCommand request, CancellationToken cancellationToken)
        {
            if (request.Id is null)
            {
                throw new ArgumentException($"{nameof(request.Id)} can not be null");
            }

            var id = request.Id;
            request.Id = null;
            var restRequest = new RestRequest($"/sobjects/{request}/{id}", Method.Patch)
                .AddJsonBody(JsonSerializer.Serialize(request.SystemDataLoadLogInit, JsonInternalSerializerOptions.Default), false);
            request.Id = id;

            var result = await _restClient.ExecuteAsync(restRequest, cancellationToken);

            if (!result.IsSuccessStatusCode)
            {
                throw new Exception($"Salesforce returned: {result.StatusCode} for update operation, id: {request.Id}.");
            }
        }
    }
}