using System;
using System.Threading.Tasks;
using Demo.NServiceBus.Message.Commands;
using Demo.NServiceBus.Message.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace Demo.NServiceBus.ShippingManagement.Handlers
{
    /*
     * With the base Saga<TData> class in place, NServiceBus makes the
     * current saga data available inside the saga using this.Data.
     *
     * IAmStartedByMessages<T> - implements the IHandleMessages<T> -
     * tells the saga which message types can start new saga instances.
     */
    public class ShippingPolicy :
        Saga<ShippingPolicyData>,
        IAmStartedByMessages<OrderCreated>,
        IAmStartedByMessages<BillingRecordCreated>
    {
        /* Because LogManager.GetLogger(..); is an expensive call,
         it's important to implement loggers as static members. */
        static readonly ILog Log = LogManager.GetLogger<ShippingPolicy>();

        public Task Handle(OrderCreated message, IMessageHandlerContext context)
        {
            Log.Info($"Received {nameof(OrderCreated)} event/message");
            Data.OrderCreated = true;

            // return Task.CompletedTask;
            return ProcessOrder(context);
        }

        public Task Handle(BillingRecordCreated message, IMessageHandlerContext context)
        {
            Log.Info($"Received {nameof(BillingRecordCreated)} event/message");
            Data.BillingRecordCreated = true;

            // return Task.CompletedTask;
            return ProcessOrder(context);
        }

        /// <summary>
        /// The ConfigureHowToFindSaga method configures mappings between incoming
        /// messages and a saga instances based on message properties.
        /// </summary>
        /// <param name="mapper"></param>
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<ShippingPolicyData> mapper)
        {
            /*
             * In the ToSaga expression, it's required that every mapped message maps to the same
             * saga data property. In other words, it's not valid to have one message type map to
             * sagaData.PropertyA and another message type map to sagaData.PropertyB.
             */

            mapper.ConfigureMapping<OrderCreated>(message => message.OrderId)
                .ToSaga(sagaData => sagaData.OrderId);

            mapper.ConfigureMapping<BillingRecordCreated>(message => message.OrderId)
                .ToSaga(sagaData => sagaData.OrderId);
        }

        private async Task ProcessOrder(IMessageHandlerContext context)
        {
            if (Data.OrderCreated && Data.BillingRecordCreated)
            {
                /*
                 * Here we're using SendLocal() to send the ShipOrder command to the same endpoint that
                 * is processing the saga message. This means we don't have to specify any routing rules
                 * for the ShipOrder command. 
                 */
                await context.SendLocal(new ShipOrder() { OrderId = Data.OrderId });
                MarkAsComplete();
            }
        }
    }
}
