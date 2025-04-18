namespace Game.Core
{
    public struct Input
    {
        public int Tick { get; set; }
        public InputButtonType Button { get; set; }
        public InputType Type { get; set; }

        public Input(int tick, InputType inputType, InputButtonType inputButton)
        {
            Tick = tick;
            Type = inputType;
            Button = inputButton;
        }
    }
}
