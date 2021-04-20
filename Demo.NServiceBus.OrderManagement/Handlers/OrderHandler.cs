using System;
using System.Threading.Tasks;
using Demo.NServiceBus.Message.Commands;
using Demo.NServiceBus.Message.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace Demo.NServiceBus.OrderManagement.Handlers
{
    public class OrderHandler : IHandleMessages<CreateOrder>
    {
        /* Because LogManager.GetLogger(..); is an expensive call,
         it's important to implement loggers as static members. */
        static readonly ILog Log = LogManager.GetLogger<OrderHandler>();

        public async Task Handle(CreateOrder message, IMessageHandlerContext context)
        {
            Log.Info($"Received {nameof(CreateOrder)}, OrderId = {message.OrderId}, created by {message.CreatedBy} on {message.CreatedOn}");

            /*
            var command = new ShipOrder
            {
                OrderId = Guid.NewGuid().ToString(),
                CreatedBy = "Mohammed",
                CreatedOn = DateTime.Now
            };
            
            await context.Send(command).ConfigureAwait(false);*/

            // Perform task(s) to create order and publish a "created" event.

            var orderCreatedEvent = new OrderCreated
            {
                OrderId = Guid.NewGuid().ToString(),
                CreatedBy = "Mohammed",
                CreatedOn = DateTime.Now
            };

            await context.Publish(orderCreatedEvent);

            // return Task.CompletedTask;
        }
    }
}
