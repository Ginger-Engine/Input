namespace Engine.Input.Abstractions;

public class ButtonAction : InputAction<bool>, IButtonAction
{
    public ButtonAction() : base(InputActionType.Button, v => v) { }
}
