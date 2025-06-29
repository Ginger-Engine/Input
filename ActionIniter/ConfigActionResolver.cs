using System.Numerics;
using Engine.Input.Actions;
using Engine.Input.Config;
using Engine.Input.Devices;

namespace Engine.Input.ActionIniter;

public class ConfigActionResolver : IInputActionResolver
{
    private readonly ConfigRootProvider _configRootProvider;
    private readonly BindingTypeRegistry _bindingTypeRegistry;

    public ConfigActionResolver(ConfigRootProvider configRootProvider, BindingTypeRegistry bindingTypeRegistry)
    {
        _configRootProvider = configRootProvider;
        _bindingTypeRegistry = bindingTypeRegistry;
    }

    public void ResolveBindings<T>(T inputAction) where T : class, IInputAction
    {
        var configBindings = _configRootProvider.Value.FindBindingDefinitions(inputAction.Id);

        foreach (var def in configBindings)
        {
            string typeName = def.GetType().Name.Replace("BindingDefinition", ""); // или !Button из YAML

            if (inputAction is IInputAction<Vector2> vec2)
            {
                var binding = _bindingTypeRegistry.CreateBinding<Vector2>(typeName, def);
                if (binding != null) vec2.AddBinding(binding);
            }
            else if (inputAction is IInputAction<float> axis)
            {
                var binding = _bindingTypeRegistry.CreateBinding<float>(typeName, def);
                if (binding != null) axis.AddBinding(binding);
            }
            else if (inputAction is IInputAction<bool> btn)
            {
                var binding = _bindingTypeRegistry.CreateBinding<bool>(typeName, def);
                if (binding != null) btn.AddBinding(binding);
            }
        }
    }
}
