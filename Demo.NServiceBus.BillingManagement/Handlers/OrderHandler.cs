using System;
using System.Threading.Tasks;
using Demo.NServiceBus.Message.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace Demo.NServiceBus.BillingManagement.Handlers
{
    public class OrderHandler : IHandleMessages<OrderCreated>
    {
        /* Because LogManager.GetLogger(..); is an expensive call,
            it's important to implement loggers as static members. */
        static readonly ILog Log = LogManager.GetLogger<OrderHandler>();

        // EVENT handler, not COMMAND handler - implementation-wise there's no difference.
        public async Task Handle(OrderCreated message, IMessageHandlerContext context)
        {
            Log.Info($"Received {nameof(OrderCreated)}, OrderId = {message.OrderId} - preparing for billing activities.");

            try
            {
                var billingRecord = new BillingRecordCreated
                {
                    BillingRecordId = Guid.NewGuid().ToString(),
                    OrderId = message.OrderId,
                    CreatedOn = message.CreatedOn,
                    CreatedBy = message.CreatedBy
                };

                await context.Publish(billingRecord);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
