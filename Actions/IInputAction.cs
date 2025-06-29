using Engine.Input.Bindings;

namespace Engine.Input.Actions;

public interface IInputAction
{
    string Id { get; set; }
    void Update(float dt);
}

public interface IInputAction<T> : IInputAction
{
    public IList<IBinding<T>> Bindings { get; }
    
    public IInputAction<T> AddBinding(IBinding<T> binding)
    {
        Bindings.Add(binding);
        return this;
    }
}