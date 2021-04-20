using NServiceBus;

namespace Demo.NServiceBus.Message.Commands
{
    /*
     * By implementing this interface we let NServiceBus know that the class is a
     * command so that it can build up some metadata about the message type when
     * the endpoint starts up. Any properties you create within the message
     * constitute the message data.
     */
    public class DoSomething : ICommand
    {
        // do not embed logic within your message classes. In essence, messages
        // should be carriers for data only. By keeping your messages small and
        // giving them clear purpose, your code will be easy to understand and evolve.
        public string SomeProperty { get; set; }
    }
}
