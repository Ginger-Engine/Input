using Engine.Input.Actions;

namespace Engine.Input.ActionIniter;

public class InputActionIniter
{
    private readonly Dictionary<string, IInputAction> _cache = new();
    private readonly IInputActionResolver _actionResolver;
    private readonly ActionsCollection _actionsCollection;

    public InputActionIniter(IInputActionResolver actionResolver, ActionsCollection actionsCollection)
    {
        _actionResolver = actionResolver;
        _actionsCollection = actionsCollection;
    }

    public T Init<T>(string id) where T : IInputAction, new()
    {
        if (_cache.TryGetValue(id, out var cachedAction))
            return (T)cachedAction;

        var action = new T { Id = id };

        if (action is IInputAction inputAction)
        {
            _actionResolver.ResolveBindings((dynamic)inputAction); // <-- добавляет биндинги
            _cache[id] = inputAction;
        }
        _actionsCollection.InputActions.Add(action);
        return action;
    }
}