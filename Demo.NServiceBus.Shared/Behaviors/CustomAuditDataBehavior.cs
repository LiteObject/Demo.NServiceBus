using NServiceBus.Pipeline;

namespace Demo.NServiceBus.Shared.Behaviors
{
    using System;
    using System.Threading.Tasks;

    public class CustomAuditDataBehavior : Behavior<IAuditContext>
    {
        public override Task Invoke(IAuditContext context, Func<Task> next)
        {
            context.AddAuditData("myKey", "myValue");
            return next();
        }
    }
}
