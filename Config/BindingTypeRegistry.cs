using Engine.Input.Bindings;
using Engine.Input.Devices;

namespace Engine.Input.Config;

public class BindingTypeRegistry(IEnumerable<IInputDevice> devices)
{
    private record Entry(Type DefinitionType, Delegate Factory);

    private readonly Dictionary<string, Entry> _bindingTypes = new();

    public void Register<T>(string name, Type definitionType, Func<IInputDevice, IBindingDefinition, IBinding<T>> factory)
    {
        _bindingTypes[name] = new Entry(definitionType, factory);
    }

    public Type? GetDefinitionType(string name)
    {
        return _bindingTypes.TryGetValue(name, out var entry) ? entry.DefinitionType : null;
    }

    public IBinding<T>? CreateBinding<T>(string name, IBindingDefinition def)
    {
        var device = devices.FirstOrDefault(d => d.Id == def.Device);
        if (_bindingTypes.TryGetValue(name, out var entry) &&
            entry.Factory is Func<IInputDevice, IBindingDefinition, IBinding<T>> factory)
        {
            return factory(device, def);
        }

        return default;
    }
}