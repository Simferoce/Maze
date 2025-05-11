using System;

namespace Game.Core
{
    public class CharacterDefinition : EntityDefinition
    {
        public AttributeHandler AttributeHandler { get; set; }
        public Fixed64 Radius { get; set; }

        public CharacterDefinition(Guid id) : base(id)
        {
        }

        public override Entity Instantiate(GameManager gameManager)
        {
            return new Character(gameManager, this);
        }
    }
}
