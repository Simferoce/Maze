using System;

namespace Game.Core
{
    public class StandardCharacterAbilityDefinition : CharacterAbilityDefinition
    {
        public StandardCharacterAbilityDefinition(Guid id) : base(id)
        {
        }

        public override CharacterAbility Instantiate(Character character)
        {
            return new StandardCharacterAbility(character, this);
        }
    }
}
