using YamlDotNet.Serialization;

namespace Engine.Input.Config;

public class ConfigLoader(BindingTypeResolver bindingTypeResolver)
{
    public ConfigRoot Load(string yaml)
    {
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(YamlDotNet.Serialization.NamingConventions.CamelCaseNamingConvention.Instance)
            .WithNodeTypeResolver(bindingTypeResolver)
            .Build();

        return deserializer.Deserialize<ConfigRoot>(yaml);
    }
}
