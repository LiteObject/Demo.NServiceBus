using System;
using System.Threading.Tasks;
using Demo.NServiceBus.Message.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace Demo.NServiceBus.ShippingManagement.Handlers
{
    public class OrderHandler : IHandleMessages<OrderCreated>
    {
        /* Because LogManager.GetLogger(..); is an expensive call,
            it's important to implement loggers as static members. */
        static readonly ILog Log = LogManager.GetLogger<OrderHandler>();

        // EVENT handler, not COMMAND handler - implementation-wise there's no difference.
        public Task Handle(OrderCreated message, IMessageHandlerContext context)
        {
            Log.Info($"Received {nameof(OrderCreated)}, OrderId = {message.OrderId} - preparing for shipment.");
            return Task.CompletedTask;
        }
    }
}
