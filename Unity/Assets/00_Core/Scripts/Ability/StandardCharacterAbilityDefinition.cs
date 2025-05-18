using System;

namespace Game.Core
{
    public class StandardCharacterAbilityDefinition : CharacterAbilityDefinition
    {
        public int Duration { get; set; }

        public StandardCharacterAbilityDefinition(Guid id) : base(id)
        {
        }

        public override CharacterAbility Instantiate(Character character)
        {
            return new StandardCharacterAbility(character, this);
        }
    }
}
