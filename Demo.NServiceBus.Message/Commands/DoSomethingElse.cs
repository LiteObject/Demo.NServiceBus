using NServiceBus;

namespace Demo.NServiceBus.Message.Commands
{
    public class DoSomethingElse : ICommand
    {
        public string SomeProperty { get; set; }
    }
}
