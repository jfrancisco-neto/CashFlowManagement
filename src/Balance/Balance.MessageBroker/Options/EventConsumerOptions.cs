namespace Balance.MessageBroker.Options;

public class EventConsumerOptions
{
    public string Section => "EventConsumer";
    public string TopicName { get; set; }
    public string Host { get; set; }
}