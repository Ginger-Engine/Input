namespace Engine.Input.Bindings;

using Devices;

public class ButtonBinding : IBinding<bool>
{
    private bool _isDown;

    private IInputDevice _device;

    public required IInputDevice Device
    {
        get => _device;
        init
        {
            value.OnPressed += k =>
            {
                if (k == Control)
                {
                    _isDown = true;
                    // OnValueChanged?.Invoke(true);
                }
            };

            value.OnReleased += k =>
            {
                if (k == Control)
                {
                    _isDown = false;
                    // OnValueChanged?.Invoke(false);
                }
            };
            
            _device = value;
        }
    }

    public required string Control { get; init; }


    public bool Evaluate(float dt) => _isDown;
}