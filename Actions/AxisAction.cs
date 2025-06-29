namespace Engine.Input.Actions;

public class AxisAction : InputAction<float>
{
    public event Action<float>? OnTriggered;

    public override void Update(float dt)
    {
        var value = 0f;
        foreach (var binding in Bindings)
            value += binding.Evaluate(dt);

        OnTriggered?.Invoke(value);
    }
}