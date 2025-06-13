using Engine.Core;
using Engine.Input.Config;
using GignerEngine.DiContainer;

namespace Engine.Input;

public class InputBundle : IBundle
{
    public void InstallBindings(DiBuilder builder)
    {
        builder.Bind<InputStage>();
    }

    public void Configure(string c, IReadonlyDiContainer diContainer)
    {
        var config = InputConfig.Load(c);
    }
}