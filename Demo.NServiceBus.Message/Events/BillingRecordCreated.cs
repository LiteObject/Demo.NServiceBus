using System;

namespace Demo.NServiceBus.Message.Events
{
    public class BillingRecordCreated
    {
        public  string BillingRecordId { get; set; }

        public string OrderId { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }
    }
}
