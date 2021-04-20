using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;

namespace Demo.NServiceBus.Message.Commands
{
    public class DoSomethingElse : ICommand
    {
        public string SomeProperty { get; set; }
    }
}
