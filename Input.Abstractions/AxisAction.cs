using System;

namespace Input.Abstractions;

public class AxisAction : InputAction<float>, IAxisAction
{
    public AxisAction() : base(InputActionType.Axis, v => Math.Abs(v) > 0.01f) { }
}
