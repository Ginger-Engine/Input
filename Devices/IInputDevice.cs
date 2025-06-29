using System.Numerics;

namespace Engine.Input.Devices;

public enum InputValueType
{
    Vector2,
    Float,
    Bool,
}
public interface IInputDevice
{
    public string Id { get; }
    public Dictionary<string, InputValueType> Keys { get; }
    
    public event Action<string>? OnPressed;
    public event Action<string>? OnReleased;

    public void Update();
    object? ReadValue(string key);

    Vector2 ReadVector2(string key) => EnsureTypeAndRead<Vector2>(key);
    float ReadFloat(string key) => EnsureTypeAndRead<float>(key);
    bool ReadBool(string key) => EnsureTypeAndRead<bool>(key);

    protected T EnsureTypeAndRead<T>(string key)
    {
        if (!Keys.ContainsKey(key))
            throw new KeyNotFoundException($"Key '{key}' not found in device '{Id}'.");
        
        var value = ReadValue(key);
        
        if (value is not T t)
            throw new InvalidCastException($"Key '{key}' on device '{Id}' is not of type {typeof(T).Name} (was {value?.GetType().Name ?? "null"}).");

        return t;
    }
}