using System.Numerics;

namespace Engine.Input.Abstractions;

public class Vector2Action : InputAction<Vector2>, IVector2Action
{
    public Vector2Action() : base(InputActionType.Vector2, v => v.LengthSquared() > 0.0001f) { }
}
