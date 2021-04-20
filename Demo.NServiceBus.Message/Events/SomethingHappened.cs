using NServiceBus;

namespace Demo.NServiceBus.Message.Events
{
    public class SomethingHappened : IEvent
    {
        public string SomeProperty { get; set; }
    }
}
