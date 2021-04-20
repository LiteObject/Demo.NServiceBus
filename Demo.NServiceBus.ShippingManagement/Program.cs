using System;
using System.Threading.Tasks;
using NServiceBus;

namespace Demo.NServiceBus.ShippingManagement
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var appName = "ShippingManagement";

            Console.Title = appName;

            var endpointConfiguration = new EndpointConfiguration(appName);

            var transport = endpointConfiguration.UseTransport<LearningTransport>();
            var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

            /* SAGA PERSISTENCE:
             * LearningPersistence which is designed for testing and development.
             * It stores data on the disk in a folder in the executable path.
             * In production use one of our production-level persistence options.
             *
             * For more info: https://docs.particular.net/persistence/#supported-persisters
             */
            var persistence = endpointConfiguration.UsePersistence<LearningPersistence>();
            // persistence.SagaStorageDirectory("PathToStoreSagas");

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();

            await endpointInstance.Stop().ConfigureAwait(false);
        }
    }
}
