namespace Game.Core
{
    public struct InputAxisState
    {
        public Fixed64 Value { get; private set; }

        public InputAxisState(Fixed64 value)
        {
            Value = value;
        }

        public InputAxisState Set(Fixed64 value)
        {
            Value = Math.Clamp01(value);
            return this;
        }
    }
}
