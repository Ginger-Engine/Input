using Engine.Core.Stages;
using Engine.Input.Devices;

namespace Engine.Input;

public class InputStage(IEnumerable<IInputDevice> devices, ActionsCollection actionsCollection) : IStage
{
    public Type[] Before { get; set; } = [typeof(LogicStage)];
    public Type[] After { get; set; } = [];

    public void Start()
    {
    }

    public void Update(float dt)
    {
        foreach (var inputDevice in devices)
        {
            inputDevice.Update();
        }
        
        foreach (var inputAction in actionsCollection.InputActions)
        {
            inputAction.Update(dt);
        }
    }
}