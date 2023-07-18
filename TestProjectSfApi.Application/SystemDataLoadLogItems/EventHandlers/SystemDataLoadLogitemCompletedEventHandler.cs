using MediatR;
using TestProjectSfApi.Domain.Events;

namespace TestProjectSfApi.Application.SystemDataLoadLogItems.EventHandlers;

public class SystemDataLoadLogitemCompletedEventHandler : INotificationHandler<SystemDataLoadLogCompletedEvent>
{
    public Task Handle(SystemDataLoadLogCompletedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}