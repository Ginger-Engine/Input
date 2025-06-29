namespace Engine.Input.Bindings;

using Engine.Input.Devices;
using System.Numerics;

public class Vector2FromButtonsBinding : IBinding<Vector2>
{
    public required IInputDevice Device { get; init; }

    public ButtonBinding? Up { get; init; }
    public ButtonBinding? Down { get; init; }
    public ButtonBinding? Left { get; init; }
    public ButtonBinding? Right { get; init; }

    public Vector2 Evaluate(float dt)
    {
        float x = 0f;
        float y = 0f;

        if (Left != null && Left.Device.ReadBool(Left.Control)) x -= dt;
        if (Right != null && Right.Device.ReadBool(Right.Control)) x += dt;
        if (Down != null && Down.Device.ReadBool(Down.Control)) y += dt;
        if (Up != null && Up.Device.ReadBool(Up.Control)) y -= dt;

        return new Vector2(x, y);
    }
}
