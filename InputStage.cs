using Engine.Core.Stages;

namespace Engine.Input;

public class InputStage : IStage
{
    public Type[] Before { get; set; } = [typeof(LogicStage)];
    public Type[] After { get; set; } = [];

    public void Start()
    {
    }

    public void Update(float dt)
    {
    }
}