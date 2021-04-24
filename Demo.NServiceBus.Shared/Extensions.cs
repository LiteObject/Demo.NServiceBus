using System;
using Npgsql;
using NpgsqlTypes;
using NServiceBus;

namespace Demo.NServiceBus.Shared
{
    public static class Extensions
    {
        public static void ConfigureCommonSettings(this EndpointConfiguration endpointConfiguration, out TransportExtensions<LearningTransport> transport)
        {
            endpointConfiguration.EnableInstallers();

            /*
           * This setting defines the transport that NServiceBus will use to send and receive messages.
           * We are using the Learning transport, which is bundled in the NServiceBus core library as a
           * starter transport for learning how to use NServiceBus without additional dependencies.
           */
            transport = endpointConfiguration.UseTransport<LearningTransport>();

            // Configure immediately retry for transient exceptions
            var recoverability = endpointConfiguration.Recoverability();
            recoverability.Immediate(immediateRetriesSettings =>
            {
                // Number of times Immediate Retries are performed. Default: 5.
                immediateRetriesSettings.NumberOfRetries(3);
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

            /* SAGA PERSISTENCE:
             * LearningPersistence which is designed for testing and development.
             * It stores data on the disk in a folder in the executable path.
             * In production use one of our production-level persistence options.
             *
             * For more info: https://docs.particular.net/persistence/#supported-persisters
             */
            /* var persistence = endpointConfiguration.UsePersistence<LearningPersistence>();
            // persistence.SagaStorageDirectory("PathToStoreSagas"); */

            // Ensure an instance of PostgreSQL (Version 10 or later) is installed and accessible on localhost.
            var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
            
            var connection = "Host=localhost;Username=postgres;Password=Demo.01;Database=postgres";

            var dialect = persistence.SqlDialect<SqlDialect.PostgreSql>();
            dialect.JsonBParameterModifier(
                modifier: parameter =>
                {
                    var npgsqlParameter = (NpgsqlParameter)parameter;
                    npgsqlParameter.NpgsqlDbType = NpgsqlDbType.Jsonb;
                });
            persistence.ConnectionBuilder(
                connectionBuilder: () => new NpgsqlConnection(connection));

            var subscriptions = persistence.SubscriptionSettings();
            subscriptions.CacheFor(TimeSpan.FromMinutes(1));
            
            /***************************************************************************************
             * Enabling auditing on all endpoints results in doubling the global message throughput.
             * That can sometimes be troublesome in high message volume environments.
             ***************************************************************************************/
            endpointConfiguration.AuditProcessedMessagesTo("targetAuditQueue");
        }
    }
}
