using System;
using NServiceBus;

namespace Demo.NServiceBus.Message.Commands
{
    public class ShipOrder : ICommand
    {
        public string OrderId { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }
    }
}
