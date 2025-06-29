using Engine.Input.Devices;

namespace Engine.Input.Bindings;

public interface IBinding<out T>
{
    T Evaluate(float dt);
}