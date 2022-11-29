namespace Transaction.Messaging;

public class EventEmitterOptions
{
    public string Section => "EventEmitter";
    public string Host { get; set; }
    public string TopicName { get; set; }
}
