namespace Engine.Input.Bindings;

using Devices;
using System.Numerics;

public class Vector2FromDeltaBinding : IBinding<Vector2>
{
    public required IInputDevice Device { get; init; }
    public required ButtonBinding Binding { get; init; } // "Delta", "DeltaX" + "DeltaY", etc.

    public Vector2 Evaluate(float dt)
    {
        return Device.ReadVector2(Binding.Control);
    }
}
