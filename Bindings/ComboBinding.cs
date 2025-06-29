namespace Engine.Input.Bindings;

public class ComboBinding : IBinding<bool>
{
    public List<IBinding<bool>> Keys;

    public bool Evaluate(float dt) => Keys.All(b => b.Evaluate(dt));
}
