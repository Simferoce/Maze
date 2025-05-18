using System;

namespace Game.Core
{
    public abstract class CharacterAbilityDefinition : Definition
    {
        public AttributeHandler AttributeHandler { get; set; }

        public CharacterAbilityDefinition(Guid id) : base(id)
        {
        }

        public abstract CharacterAbility Instantiate(Character character);
    }
}
