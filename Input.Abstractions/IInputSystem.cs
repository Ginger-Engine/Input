namespace Input.Abstractions;

public interface IInputSystem
{
    void Update();
    IInputAction<T>? Get<T>(string actionId);
}
