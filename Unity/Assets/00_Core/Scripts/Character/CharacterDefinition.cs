using System;

namespace Game.Core
{
    public class CharacterDefinition : EntityDefinition
    {
        public AttributeHandler AttributeHandler { get; set; }

        public CharacterDefinition(Guid id) : base(id)
        {
        }
    }
}
