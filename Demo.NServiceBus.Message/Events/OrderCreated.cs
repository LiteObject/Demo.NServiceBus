using System;
using NServiceBus;

namespace Demo.NServiceBus.Message.Events
{
    public class OrderCreated : IEvent
    {
        public string OrderId { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }
    }
}
