using YamlDotNet.Serialization;

namespace Engine.Input.Config;

public class ConfigRoot
{
    [YamlMember(Alias = "traits")]
    public Dictionary<string, TraitDefinition> Traits { get; set; } = new();

    [YamlMember(Alias = "bindingSets")]
    public Dictionary<string, BindingSetDefinition> BindingSets { get; set; } = new();

    public IEnumerable<IBindingDefinition> FindBindingDefinitions(string actionId)
    {
        foreach (var (traitId, traitDefinition) in Traits)
        {
            foreach (var (actionDefinitionId, actionDefinition) in traitDefinition.Actions)
            {
                if (traitId + '/' + actionDefinitionId == actionId) return actionDefinition.Bindings;
            }
        }

        foreach (var (setId, bindingSetDefinition) in BindingSets)
        {
            foreach (var (actionDefinitionId, actionDefinition) in bindingSetDefinition.Actions)
            {
                if (setId + '/' + actionDefinitionId == actionId) return actionDefinition.Bindings;
            }
        }

        return [];
    }
}


public class TraitDefinition
{
    public Dictionary<string, ActionDefinition> Actions { get; init; } = new();
}

public class BindingSetDefinition
{
    public List<string> Traits { get; init; } = new();
    public Dictionary<string, ActionDefinition> Actions { get; init; } = new();
}

public class ActionDefinition
{
    public string Type { get; init; } = ""; // "Button", "Axis", "Vector2", etc.
    public List<IBindingDefinition>? Bindings { get; init; }
}

public interface IBindingDefinition
{
    public string Device { get; init; }
}

public abstract class BindingDefinition : IBindingDefinition
{
    public string Device { get; init; } = "";
}

public class ButtonBindingDefinition : BindingDefinition
{
    public string Control { get; init; } = "";
}

public class AxisBindingDefinition : BindingDefinition
{
    public string Positive { get; init; } = "";
    public string Negative { get; init; } = "";
}

public class Vector2FromButtonsBindingDefinition : BindingDefinition
{
    public ButtonBindingDefinition Up { get; init; }
    public ButtonBindingDefinition Down { get; init; }
    public ButtonBindingDefinition Left { get; init; }
    public ButtonBindingDefinition Right { get; init; }
}

public class Vector2FromDeltaBindingDefinition : BindingDefinition
{
    public string Control { get; init; }
}

// другие биндинги: ModifierBindingDefinition, HoldBindingDefinition, ComboBindingDefinition и т.д. — можно добавлять аналогично