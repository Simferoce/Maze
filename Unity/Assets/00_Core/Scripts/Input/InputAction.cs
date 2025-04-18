namespace Game.Core
{
    public struct InputAction
    {
        public InputType InputType { get; set; }
        public InputAxisType InputAxisType { get; set; }
        public InputButtonType InputButtonType { get; set; }
        public long Tick { get; set; }
        public Fixed64 Value { get; set; }

        public static InputAction BuildInputActionAxisSet(InputAxisType inputAxisType, Fixed64 value)
        {
            return new InputAction { InputType = InputType.SetAxis, InputAxisType = inputAxisType, Value = value };
        }

        public static InputAction BuildInputActionButtonDown(InputButtonType inputButtonType)
        {
            return new InputAction { InputType = InputType.ButtonDown, InputButtonType = inputButtonType };
        }

        public static InputAction BuildInputActionButtonUp(InputButtonType inputButtonType)
        {
            return new InputAction { InputType = InputType.ButtonUp, InputButtonType = inputButtonType };
        }
    }
}
