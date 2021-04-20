﻿using System;
using Demo.NServiceBus.Message.Commands;
using NServiceBus;

namespace Demo.NServiceBus.BillingManagement
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            var appName = "BillingManagement";

            Console.Title = appName;

            var endpointConfiguration = new EndpointConfiguration(appName);

            var transport = endpointConfiguration.UseTransport<LearningTransport>();
            var routing = transport.Routing();
            routing.RouteToEndpoint(typeof(ShipOrder), "ShippingManagement");

            // Configure immediately retry for transient exceptions
            var recoverability = endpointConfiguration.Recoverability();
            recoverability.Immediate(immediateRetriesSettings =>
            {
                // Number of times Immediate Retries are performed. Default: 5.
                immediateRetriesSettings.NumberOfRetries(3);

                // TO disable retry:
                // immediateRetriesSettings.NumberOfRetries(0);
            });

            // Configuring delayed retries for semi-transient exceptions
            /* recoverability.Delayed(
                delayed =>
                {
                    // delayed.NumberOfRetries(2);
                    // delayed.TimeIncrease(TimeSpan.FromMinutes(5));

                    // To disable:
                    // delayed.NumberOfRetries(0);
                }); */

            var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();

            await endpointInstance.Stop().ConfigureAwait(false);
        }
    }
}
