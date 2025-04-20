namespace Game.Core
{
    public struct InputAction
    {
        public long Tick { get; set; }
        public InputType InputType { get; set; }
        public Fixed64 Value { get; set; }
    }
}
