namespace Game.Core
{
    public struct Command
    {
        public long Tick { get; set; }
        public ComandType CommandType { get; set; }
        public Fixed64 Value { get; set; }
    }
}
