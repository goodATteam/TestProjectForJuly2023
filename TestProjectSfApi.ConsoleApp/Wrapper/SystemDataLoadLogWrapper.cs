using MediatR;
using Microsoft.Extensions.Hosting;
using TestProjectSfApi.Application.Common.Constants;
using TestProjectSfApi.Application.SystemDataLoadLogItems.Queries;
using TestProjectSfApi.Domain.Enums;

namespace TestProjectSfApi.ConsoleApp.Wrapper
{    
    public class SystemDataLoadLogWrapper :IHostedService
    {
        private const int DefaultQuantity = 3;
        private const decimal DefaultAmount = 12.33m;

        private ISender? _mediator;

        public SystemDataLoadLogWrapper(ISender mediator)
        {          
            _mediator = mediator;
        }

        public async Task UpdateSfSystemDataLoadLog(int quantity, decimal amount)
        {
            var queryFilter = $"FIN_Process__c='{DataGroup.Payment}'+and+" +
                              $"FIN_ProcessDate__c={new DateOnly(2022, 03, 22)}";

            GetRecordsQuery query = new GetRecordsQuery();
            query.QueryFilter = queryFilter;
            query.TableName = SalesforceTables.SalesforceTableName;            

            var dataLoadLog = await _mediator.Send(query);

            var ApiSystemDataLoadLog = dataLoadLog.Records.FirstOrDefault();
            ApiSystemDataLoadLog.ActualRawTotalAmount += amount;
            ApiSystemDataLoadLog.ActualRawTotalRecordCount += quantity;

            await _mediator.Send(ApiSystemDataLoadLog);
            
            Console.WriteLine("Hello, World!");
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await UpdateSfSystemDataLoadLog(DefaultQuantity, DefaultAmount);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}