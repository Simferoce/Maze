namespace Game.Core
{
    public struct Attribute
    {
        public static readonly Attribute Default = new Attribute(AttributeType.Undefined, new Fixed64(0));

        public AttributeType Type { get; set; }
        public Fixed64 Value { get; set; }

        public Attribute(AttributeType type, Fixed64 value)
        {
            Type = type;
            Value = value;
        }
    }
}
