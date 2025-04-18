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

        public void Press()
        {
            IsPress = true;
        }

        public void Unpress()
        {
            IsPress = false;
        }
    }
}
