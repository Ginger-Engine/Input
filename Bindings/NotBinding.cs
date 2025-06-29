namespace Engine.Input.Bindings;

public class NotBinding : IBinding<bool>
{
    public IBinding<bool> InnerBinding;

    public bool Evaluate(float dt) => !InnerBinding.Evaluate(dt);
}