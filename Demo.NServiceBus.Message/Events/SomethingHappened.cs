using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;

namespace Demo.NServiceBus.Message.Events
{
    public class SomethingHappened : IEvent
    {
        public string SomeProperty { get; set; }
    }
}
