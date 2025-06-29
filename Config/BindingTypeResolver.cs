namespace Engine.Input.Config;

using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

public class BindingTypeResolver(BindingTypeRegistry bindingTypeRegistry) : INodeTypeResolver
{
    public bool Resolve(NodeEvent nodeEvent, ref Type currentType)
    {
        if (nodeEvent.Tag.IsEmpty) return false;
        
        var tag = nodeEvent.Tag.Value.TrimStart('!');

        if (bindingTypeRegistry.GetDefinitionType(tag) is { } type)
        {
            currentType = type;
            return true;
        }

        return false;
    }
}
