namespace Engine.Input.Bindings;

using Devices;

public class AxisBinding : IBinding<float>
{
    public required IInputDevice Device { get; init; }
    public string? Positive { get; init; }
    public string? Negative { get; init; }

    public float Evaluate(float dt)
    {
        var value = 0f;

        if (Positive != null && Device.ReadBool(Positive))
            value += dt;

        if (Negative != null && Device.ReadBool(Negative))
            value -= dt;

        return value;
    }
}
