namespace Engine.Input.Actions;

public class ButtonAction : InputAction<bool>
{
    public event Action? OnDown;
    public event Action? OnUp;
    public event Action? OnPerformed;
    
    private bool _previousState = false;

    public override void Update(float dt)
    {
        var currentState = false;

        foreach (var binding in Bindings)
        {
            if (!binding.Evaluate(dt)) continue;
            
            currentState = true;
            break;
        }

        if (currentState && !_previousState)
            OnDown?.Invoke();
        else if (!currentState && _previousState)
            OnUp?.Invoke();
        if (currentState)
            OnPerformed?.Invoke();

        _previousState = currentState;
    }
}