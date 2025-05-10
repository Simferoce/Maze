namespace Game.Core
{
    public class Character : Entity<CharacterDefinition>
    {
        public AttributeHandler AttributeHandler { get; private set; }

        public Character(GameManager gameManager, CharacterDefinition definition) : base(gameManager, definition)
        {
            this.AttributeHandler = definition.AttributeHandler.Clone();
        }
    }
}
