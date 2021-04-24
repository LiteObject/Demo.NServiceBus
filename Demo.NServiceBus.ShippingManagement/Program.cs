using System;
using System.Threading.Tasks;
using Demo.NServiceBus.Shared;
using NServiceBus;

namespace Demo.NServiceBus.ShippingManagement
{
    class Program
    {
        static async Task Main(string[] args)
        {
            const string appName = "ShippingManagement";
            
            Console.Title = appName;

            var endpointConfiguration = new EndpointConfiguration(appName);
            endpointConfiguration.ConfigureCommonSettings(out _);
            
            var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);
            
            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();

            await endpointInstance.Stop().ConfigureAwait(false);
        }
    }
}
