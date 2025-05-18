namespace Game.Core
{
    public class StandardCharacterAbilityCharacterState : CharacterState
    {
        private long startedAt;
        private StandardCharacterAbility ability;

        public StandardCharacterAbilityCharacterState(StandardCharacterAbility ability, CharacterStateMachine stateMachine) : base(stateMachine)
        {
            this.ability = ability;
        }

        public override void Enter()
        {
            base.Enter();
            startedAt = StateMachine.Character.GameManager.TimeManager.CurrentTick;
            StateMachine.Character.GameManager.Logger.Log($"Ability Enter: {StateMachine.Character.GameManager.TimeManager.CurrentTick}", ILogger.LogLevel.Debug);
        }

        public override void Update()
        {
            base.Update();
            if (startedAt + ability.Definition.Duration <= StateMachine.Character.GameManager.TimeManager.CurrentTick)
                StateMachine.Change(new MoveCharacterState(StateMachine));
        }

        public override void Exit()
        {
            base.Exit();
            StateMachine.Character.GameManager.Logger.Log($"Ability Exit: {StateMachine.Character.GameManager.TimeManager.CurrentTick}", ILogger.LogLevel.Debug);
        }
    }
}
