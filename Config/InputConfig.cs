using Engine.Input.Abstractions;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Engine.Input.Config;

public class InputConfig
{
    public Dictionary<string, InputTrait> Traits { get; set; } = new();
    public Dictionary<string, InputBindingSet> BindingSets { get; set; } = new();

    public static InputConfig Load(string yaml)
    {
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();
        return deserializer.Deserialize<InputConfig>(yaml);
    }
}

public class InputTrait
{
    public Dictionary<string, ActionConfig> Actions { get; set; } = new();
}

public class ActionConfig
{
    public InputActionType Type { get; set; }
    public Dictionary<string, InputBinding>? Composite { get; set; }
    public List<InputBinding>? Bindings { get; set; }
}

public class InputBindingSet
{
    public List<string> Traits { get; set; } = new();
    public Dictionary<string, ActionConfig> Actions { get; set; } = new();
}
