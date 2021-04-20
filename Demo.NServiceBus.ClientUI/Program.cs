using Demo.NServiceBus.Message.Commands;
using NServiceBus;
using NServiceBus.Logging;

namespace Demo.NServiceBus.Saga
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Main Article: https://docs.particular.net/tutorials/nservicebus-step-by-step/1-getting-started/
    /// </summary>
    class Program
    {
        /*
         * Because LogManager.GetLogger(..); is an expensive call, it's important to implement loggers as static members.
         */
        private static readonly ILog Log = LogManager.GetLogger<Program>();

        static async Task Main(string[] args)
        {
            Console.Title = "ClientUI";

            // The EndpointConfiguration class is where we define all the settings that determine how our endpoint will operate. 
            var endpointConfiguration = new EndpointConfiguration("ClientUI");

            /*
             * This setting defines the transport that NServiceBus will use to send and receive messages.
             * We are using the Learning transport, which is bundled in the NServiceBus core library as a
             * starter transport for learning how to use NServiceBus without additional dependencies.
             */
            var transport = endpointConfiguration.UseTransport<LearningTransport>();

            // Returns a RoutingSettings<LearningTransport>
            var routing = transport.Routing();
            routing.RouteToEndpoint(typeof(CreateOrder), "OrderManagement");

            // Starts the endpoint, keep it running until we press the Enter key, then shut it down.
            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();

            await REPL(endpointInstance).ConfigureAwait(false);

            await endpointInstance.Stop().ConfigureAwait(false);

        }

        /// <summary>
        /// REPL = Run -> Eval -> Print -> Loop
        /// </summary>
        /// <param name="endpointInstance"></param>
        /// <returns></returns>
        static async Task REPL(IEndpointInstance endpointInstance)
        {
            Log.Info("Press 'P' to place an order, or 'Q' to quit.");

            while (true)
            {
                Log.Info("Press 'P' to place an order, or 'Q' to quit.");
                var key = Console.ReadKey();
                Console.WriteLine();

                switch (key.Key)
                {
                    case ConsoleKey.P:
                        // Instantiate the command
                        var command = new CreateOrder
                        {
                            OrderId = Guid.NewGuid().ToString(),
                            CreatedBy = "Mohammed",
                            CreatedOn = DateTime.Now
                        };
                        
                        Log.Info($"Sending {nameof(CreateOrder)} command, OrderId = {command.OrderId}");

                        // SendLocal() to send the message to the same endpoint (app/process).
                        await endpointInstance.Send(command).ConfigureAwait(false);

                        break;

                    case ConsoleKey.Q:
                        return;

                    default:
                        Log.Info("Unknown input. Please try again.");
                        break;
                }
            }
        }
    }
}
