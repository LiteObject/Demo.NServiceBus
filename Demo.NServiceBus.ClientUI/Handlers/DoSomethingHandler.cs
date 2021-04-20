using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.NServiceBus.Message.Commands;
using NServiceBus;

namespace Demo.NServiceBus.Saga.Handlers
{
    /*
     * When NServiceBus starts up, it scans the types in all available assemblies,
     * finds all message handler classes, and automatically wires them up, so that
     * they will be invoked when messages arrive. There's no special configuration
     * required - it just works.
     */

    // "DoSomething" -> Command handler because "Do" indicates an action that is needed
    public class DoSomethingHandler :
        IHandleMessages<DoSomething>,
        IHandleMessages<DoSomethingElse>
    {
        /* Each time a message is processed, a new instance of that class is
         * instantiated by the framework.
         *
         * You can't set a private member variable in one message handler and then
         * expect to have that value around when the next message (regardless of
         * type) is processed.
         */

        public Task Handle(DoSomething message, IMessageHandlerContext context)
        {
            Console.WriteLine("Received DoSomething");
            return Task.CompletedTask;
        }

        public Task Handle(DoSomethingElse message, IMessageHandlerContext context)
        {
            Console.WriteLine("Received DoSomethingElse");
            return Task.CompletedTask;
        }
    }
}
