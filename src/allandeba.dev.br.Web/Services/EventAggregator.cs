namespace allandeba.dev.br.Web.Services;

public class EventAggregator
{
    private readonly Dictionary<Type, List<object>> _handlers = new();

    public void Subscribe<TEvent>(Action<TEvent> handler)
    {
        var type = typeof(TEvent);
        if (!_handlers.ContainsKey(type))
            _handlers[type] = new List<object>();
        _handlers[type].Add(handler);
    }

    public void Publish<TEvent>(TEvent @event)
    {
        var type = typeof(TEvent);
        if (_handlers.ContainsKey(type))
        {
            foreach (var handler in _handlers[type].Cast<Action<TEvent>>())
            {
                handler(@event);
            }
        }
    }

    public void Unsubscribe<TEvent>(Action<TEvent> handler)
    {
        var type = typeof(TEvent);
        if (_handlers.ContainsKey(type))
            _handlers[type].Remove(handler);
    }
}