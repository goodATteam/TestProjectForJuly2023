using System.Text.Json.Serialization;

namespace TestProjectSfApi.Domain.Entities
{
    public class SystemDataLoadLog : BaseEntity
    {
        [JsonPropertyName("FIN_Process__c")]
        public DataGroup? DataGroup { get; set; }

        [JsonPropertyName("FIN_ProcessDate__c")]
        public DateOnly? ProcessDate { get; set; }

        [JsonPropertyName("AWS_ActualRawTotalRecordCount__c")]
        public double? ActualRawTotalRecordCount { get; set; }

        [JsonPropertyName("AWS_ActualRawTotalAmount__c")]
        public decimal? ActualRawTotalAmount { get; set; }

        private bool _done;
        public bool Done
        {
            get => _done;
            set
            {
                if (value && !_done)
                {
                    AddDomainEvent(new SystemDataLoadLogCompletedEvent(this));
                }

                _done = value;
            }
        }
    }
}