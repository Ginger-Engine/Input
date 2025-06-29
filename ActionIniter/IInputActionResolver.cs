using Engine.Input.Actions;

namespace Engine.Input.ActionIniter;

public interface IInputActionResolver
{
    public void ResolveBindings<T>(T inputAction) where T : class, IInputAction;
}