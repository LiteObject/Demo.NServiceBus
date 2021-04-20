using System.Threading.Tasks;
using Demo.NServiceBus.Message.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace Demo.NServiceBus.ClientUI.Handlers
{
    // "SomethingHappened" -> Event handler because of past tense "happened"
    public class SomethingHappenedHandler : IHandleMessages<SomethingHappened>
    {
        /* Because LogManager.GetLogger(..); is an expensive call,
         it's important to implement loggers as static members. */
        static readonly ILog Log = LogManager.GetLogger<SomethingHappenedHandler>();

        public Task Handle(SomethingHappened message, IMessageHandlerContext context)
        {
            Log.Info($"Received {nameof(SomethingHappened)}");
            return Task.CompletedTask;
        }
    }
}
