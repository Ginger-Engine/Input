using System.Numerics;

namespace Engine.Input.PointerEvents;

public interface IPointerProvider
{
    Vector2 GetPointerPosition();
}