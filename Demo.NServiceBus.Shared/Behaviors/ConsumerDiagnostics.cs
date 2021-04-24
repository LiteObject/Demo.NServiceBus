using NServiceBus.Pipeline;

namespace Demo.NServiceBus.Shared.Behaviors
{
    using System;
    using System.Threading.Tasks;
    using System.Diagnostics;

    /***********************************************************************
     * Original Source: https://jimmybogard.com/building-end-to-end-diagnostics-and-tracing-a-primer-trace-context/
     *
     * Behaviors in NServiceBus are similar to ASP.NET Core middleware.
     ***********************************************************************/

    public class ConsumerDiagnostics
        : Behavior<IIncomingPhysicalMessageContext>
    {
        /*************************************************************************
         * Two parameters - the first being the context of the operation performed,
         * and the second delegate to perform the next action in the chain.
         *************************************************************************/

        public override async Task Invoke(IIncomingPhysicalMessageContext context, Func<Task> next)
        {
            var activity = StartActivity(context);

            try
            {
                await next().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await Console.Error.WriteLineAsync($"Exception thrown in ${nameof(ConsumerDiagnostics)}:\n${e.StackTrace}\n\n");
            }
            finally
            {
                StopActivity(activity, context);
            }
        }

        private static Activity StartActivity(IIncomingPhysicalMessageContext context)
        {
            /*******************************************************************************
             * System.Diagnostics.Activity class represents an operation with context to be
             * used for logging.
             *
             * An Activity has an operation name, an ID, a start time and duration, tags,
             * and baggage.
             *******************************************************************************/

            var activity = new Activity(Constants.ConsumerActivityName);

            if (!context.MessageHeaders.TryGetValue(Constants.TraceParentHeaderName, out var requestId))
            {
                context.MessageHeaders.TryGetValue(Constants.RequestIdHeaderName, out requestId);
            }

            if (!string.IsNullOrEmpty(requestId))
            {
                activity.SetParentId(requestId);

                if (context.MessageHeaders.TryGetValue(Constants.TraceStateHeaderName, out var traceState))
                {
                    activity.TraceStateString = traceState;
                }
            }

            // The current activity gets an ID with the W3C format
            activity.Start();

            return activity;
        }

        private static void StopActivity(Activity activity, IIncomingPhysicalMessageContext context)
        {
            if (activity.Duration == TimeSpan.Zero)
            {
                activity.SetEndTime(DateTime.UtcNow);
            }

            activity.Stop();
        }
    }
}
