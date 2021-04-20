using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;

namespace Demo.NServiceBus.Message.Commands
{
    /// <summary>
    /// commands – one-way messages from a sender to a specific receiver.
    /// </summary>
    public class CreateOrder : ICommand
    {
        public string OrderId { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }
    }
}
