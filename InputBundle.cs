using Engine.Core;
using Engine.Input.ActionIniter;
using Engine.Input.Bindings;
using Engine.Input.Config;
using GignerEngine.DiContainer;

namespace Engine.Input;

public class InputBundle : IBundle
{
    public void InstallBindings(DiBuilder builder)
    {
        builder.Bind<ActionsCollection>();
        builder.Bind<InputStage>();
        builder.Bind<ConfigLoader>();
        builder.Bind<ConfigRootProvider>();
        builder.Bind<BindingTypeRegistry>().Eager();
        builder.Bind<IInputActionResolver>().From<ConfigActionResolver>().Eager();
    }

    public void Configure(string c, IReadonlyDiContainer diContainer)
    {
        var configLoader = diContainer.Resolve<ConfigLoader>();
        var bindingTypeRegistry = diContainer.Resolve<BindingTypeRegistry>();

        // Регистрация типов биндингов
        bindingTypeRegistry.Register(
            "Button",
            typeof(ButtonBindingDefinition),
            (device, def) => new ButtonBinding{Device = device, Control = ((ButtonBindingDefinition)def).Control}
        );

        bindingTypeRegistry.Register(
            "Axis",
            typeof(AxisBindingDefinition),
            (device, def) =>
            {
                var d = (AxisBindingDefinition)def;
                return new AxisBinding{Device = device, Positive = d.Positive, Negative = d.Negative};
            });

        bindingTypeRegistry.Register(
            "Vector2FromButtons",
            typeof(Vector2FromButtonsBindingDefinition),
            (device, def) =>
            {
                var d = (Vector2FromButtonsBindingDefinition)def;
                return new Vector2FromButtonsBinding{
                    Device = device,
                    Up = (ButtonBinding)bindingTypeRegistry.CreateBinding<bool>("Button", d.Up)!,
                    Down = (ButtonBinding)bindingTypeRegistry.CreateBinding<bool>("Button", d.Down)!,
                    Left = (ButtonBinding)bindingTypeRegistry.CreateBinding<bool>("Button", d.Left)!,
                    Right = (ButtonBinding)bindingTypeRegistry.CreateBinding<bool>("Button", d.Right)!
                };
            });

        bindingTypeRegistry.Register(
            "Vector2FromDelta",
            typeof(Vector2FromDeltaBindingDefinition),
            (device, def) =>
            {
                var d = (Vector2FromDeltaBindingDefinition)def;
                return new Vector2FromDeltaBinding
                {
                    Device = device,
                    Binding = new ButtonBinding { Device = device, Control = d.Control }
                };
            }
        );
        
        var config = configLoader.Load(c);
        var provider = diContainer.Resolve<ConfigRootProvider>();
        provider.Value = config;
    }
}
