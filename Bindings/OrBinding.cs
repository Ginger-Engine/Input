namespace Engine.Input.Bindings;

public class OrBinding : IBinding<bool>
{
    public List<IBinding<bool>> Bindings = [];

    public bool Evaluate(float dt) => Bindings.Any(b => b.Evaluate(dt));
}
