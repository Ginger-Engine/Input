using System.Collections.Generic;
using Raylib_cs;
using System.Numerics;
using Input.Abstractions;

namespace Input.Raylib;

public class RaylibInputSystem : IInputSystem
{
    private readonly Dictionary<string, IInputAction> _actions = new();

    public RaylibInputSystem(Dictionary<string, IInputAction> actions)
    {
        _actions = actions;
    }

    public IInputAction<T>? Get<T>(string actionId)
    {
        return _actions.TryGetValue(actionId, out var action) ? (IInputAction<T>)action : null;
    }

    public void Update()
    {
        foreach (var pair in _actions)
        {
            switch (pair.Value)
            {
                case InputAction<bool> button:
                    bool isDown = Raylib.IsKeyDown((KeyboardKey)System.Enum.Parse(typeof(KeyboardKey), pair.Key));
                    button.Update(isDown);
                    break;
                case InputAction<float> axis:
                    float axisValue = Raylib.GetGamepadAxisMovement(0, GamepadAxis.LEFT_Y);
                    axis.Update(axisValue);
                    break;
                case InputAction<Vector2> vec2:
                    Vector2 v = new(Raylib.GetGamepadAxisMovement(0, GamepadAxis.LEFT_X), Raylib.GetGamepadAxisMovement(0, GamepadAxis.LEFT_Y));
                    vec2.Update(v);
                    break;
            }
        }
    }
}
