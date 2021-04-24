using System;
using System.Threading.Tasks;
using Demo.NServiceBus.Message.Commands;
using Demo.NServiceBus.Shared;
using NServiceBus;

namespace Demo.NServiceBus.OrderManagement
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var appName = "OrderManagement";

            Console.Title = appName;

            var endpointConfiguration = new EndpointConfiguration(appName);
            
            endpointConfiguration.ConfigureCommonSettings(out var transport);

            var routing = transport.Routing();
            routing.RouteToEndpoint(typeof(ShipOrder), "ShippingManagement");
            
            var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();

            await endpointInstance.Stop().ConfigureAwait(false);
        }
    }
}
