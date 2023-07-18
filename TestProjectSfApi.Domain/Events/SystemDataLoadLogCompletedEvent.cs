namespace TestProjectSfApi.Domain.Events
{
    public class SystemDataLoadLogCompletedEvent : BaseEvent
    {
        public SystemDataLoadLogCompletedEvent(SystemDataLoadLog item)
        {
            Item = item;
        }

        public SystemDataLoadLog Item { get; }
    }
}