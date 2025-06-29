using Engine.Input.Bindings;

namespace Engine.Input.Actions;

public abstract class InputAction<T> : IInputAction<T>
{
    public List<IBinding<T>> Bindings { get; protected set; } = [];
    IList<IBinding<T>> IInputAction<T>.Bindings => Bindings;

    public string Id { get; set; } = null!;
    public abstract void Update(float dt);
}