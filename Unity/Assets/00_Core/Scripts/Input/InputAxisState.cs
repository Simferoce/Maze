namespace Game.Core
{
    public struct InputAxisState
    {
        private static readonly Fixed64 SPEED = Fixed64.FromInt(1);

        public InputButtonType Positive { get; private set; }
        public InputButtonType Negative { get; private set; }
        public Fixed64 Value { get; private set; }

        public InputAxisState(InputButtonType positive, InputButtonType negative)
        {
            Positive = positive;
            Negative = negative;
            Value = Fixed64.Zero;
        }

        public void Update(InputManager inputManager)
        {
            if (inputManager.IsPressed(Positive) == false && inputManager.IsPressed(Negative) == false)
            {
                if (Value < Fixed64.Zero)
                {
                    Value += SPEED;
                    if (Value > Fixed64.Zero)
                    {
                        Value = Fixed64.Zero;
                    }
                }
                else if (Value > Fixed64.Zero)
                {
                    Value -= SPEED;
                    if (Value < Fixed64.Zero)
                    {
                        Value = Fixed64.Zero;
                    }
                }
            }
            else if (inputManager.IsPressed(Positive))
            {
                Value = Math.Clamp01(Value + SPEED);
            }
            else if (inputManager.IsPressed(Negative))
            {
                Value = Math.Clamp01(Value - SPEED);
            }
        }
    }
}
