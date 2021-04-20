using System.Threading.Tasks;
using Demo.NServiceBus.Message.Commands;
using NServiceBus;
using NServiceBus.Logging;

namespace Demo.NServiceBus.ShippingManagement.Handlers
{
    public class ShippingHandler : IHandleMessages<ShipOrder>
    {
        /* Because LogManager.GetLogger(..); is an expensive call,
         it's important to implement loggers as static members. */
        static readonly ILog Log = LogManager.GetLogger<ShippingHandler>();

        public Task Handle(ShipOrder message, IMessageHandlerContext context)
        {
            Log.Info($"Received {nameof(ShipOrder)}, OrderId = {message.OrderId}, created by {message.CreatedBy} on {message.CreatedOn}");
            return Task.CompletedTask;
        }
    }
}
