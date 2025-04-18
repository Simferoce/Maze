namespace Game.Core
{
    public struct InputButtonState
    {
        public InputButtonType Button { get; set; }
        public bool IsPress { get; set; }

        public InputButtonState(InputButtonType button)
        {
            Button = button;
            IsPress = false;
        }

        public InputButtonState SetButtonDown()
        {
            IsPress = true;
            return this;
        }

        public InputButtonState SetButtonUp()
        {
            IsPress = false;
            return this;
        }
    }
}
