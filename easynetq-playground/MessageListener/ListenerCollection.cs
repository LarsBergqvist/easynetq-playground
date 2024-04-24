namespace MessageListener;

public class ListenerCollection(List<IListener> listeners)
{
    public List<IListener> Listeners { get; set; } = listeners;
}