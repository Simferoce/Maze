namespace Game.Core
{
    public class StandardCharacterAbility : CharacterAbility<StandardCharacterAbilityDefinition>
    {
        public StandardCharacterAbility(Character character, StandardCharacterAbilityDefinition definition) : base(character, definition)
        {
        }

        public override void Use()
        {
            Character.StateMachine.Change(new StandardCharacterAbilityCharacterState(this, Character.StateMachine));
        }
    }
}
