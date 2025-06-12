using System;

namespace Input.Abstractions;

public class InputAction<T> : IInputAction<T>
{
    public InputActionType ActionType { get; }
    public T Value { get; private set; } = default!;

    public event Action? OnPressed;
    public event Action? OnReleased;
    public event Action<T>? OnChanged;

    private bool _wasPressed;
    private readonly Func<T, bool> _isPressedEvaluator;

    public InputAction(InputActionType type, Func<T, bool> isPressedEvaluator)
    {
        ActionType = type;
        _isPressedEvaluator = isPressedEvaluator;
    }

    public void Update(T value)
    {
        Value = value;
        OnChanged?.Invoke(value);

        bool isPressed = _isPressedEvaluator(value);
        if (isPressed && !_wasPressed)
            OnPressed?.Invoke();
        else if (!isPressed && _wasPressed)
            OnReleased?.Invoke();
        _wasPressed = isPressed;
    }
}
