using System;

namespace Input.Abstractions;

public interface IInputAction
{
    InputActionType ActionType { get; }
    event Action? OnPressed;
    event Action? OnReleased;
}

public interface IInputAction<T> : IInputAction
{
    T Value { get; }
    event Action<T>? OnChanged;
}

public interface IButtonAction : IInputAction<bool> { }
public interface IAxisAction : IInputAction<float> { }
public interface IVector2Action : IInputAction<System.Numerics.Vector2> { }
