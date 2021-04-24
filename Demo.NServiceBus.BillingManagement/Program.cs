using System;
using System.Diagnostics;
using Demo.NServiceBus.Shared;
using Demo.NServiceBus.ShippingManagement;
using NServiceBus;

namespace Demo.NServiceBus.BillingManagement
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            var appName = "BillingManagement";

            Console.Title = appName;

            Activity.DefaultIdFormat = ActivityIdFormat.W3C;

            var endpointConfiguration = new EndpointConfiguration(appName);

            endpointConfiguration.ConfigureCommonSettings(out _);
            
            var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();

            await endpointInstance.Stop().ConfigureAwait(false);
        }
    }
}
