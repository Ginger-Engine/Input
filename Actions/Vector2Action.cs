using System.Numerics;

namespace Engine.Input.Actions;

public class Vector2Action : InputAction<Vector2>
{
    public event Action<Vector2>? OnTriggered;
    
    public override void Update(float dt)
    {
        var total = Vector2.Zero;
        foreach (var b in Bindings)
            total += b.Evaluate(dt);

        OnTriggered?.Invoke(total);
    }
}