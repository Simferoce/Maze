namespace Game.Core
{
    public abstract class CharacterAbility
    {
        public CharacterAbilityDefinition Definition { get; private set; }
        public Character Character { get; private set; }
        public AttributeHandler AttributeHandler { get; private set; }

        public CharacterAbility(Character character, CharacterAbilityDefinition definition)
        {
            this.AttributeHandler = definition.AttributeHandler.Clone();
            Definition = definition;
            Character = character;
        }

        public abstract void Use();
    }

    public abstract class CharacterAbility<T> : CharacterAbility
        where T : CharacterAbilityDefinition
    {
        public new T Definition { get; private set; }

        public CharacterAbility(Character character, T definition) : base(character, definition)
        {
            Definition = definition;
        }
    }
}
