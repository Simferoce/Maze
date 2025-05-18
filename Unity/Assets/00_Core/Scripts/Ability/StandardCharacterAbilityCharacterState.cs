namespace Game.Core
{
    public class StandardCharacterAbilityCharacterState : CharacterState
    {
        private long startedAt;
        private StandardCharacterAbility ability;

        public override bool CanUseAbility => false;
        public long ElapsedTick => StateMachine.Character.GameManager.TimeManager.CurrentTick - startedAt;
        public long Duration => ability.AttributeHandler.Get(AttributeType.Duration).Value.ToInt();

        public StandardCharacterAbilityCharacterState(StandardCharacterAbility ability, CharacterStateMachine stateMachine) : base(stateMachine)
        {
            this.ability = ability;
            this.AttributeHandler = ability.AttributeHandler.Clone();
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
            if (startedAt + Duration <= StateMachine.Character.GameManager.TimeManager.CurrentTick)
                StateMachine.Change(new MoveCharacterState(StateMachine));
        }

        public override void Exit()
        {
            base.Exit();
            StateMachine.Character.GameManager.Logger.Log($"Ability Exit: {StateMachine.Character.GameManager.TimeManager.CurrentTick}", ILogger.LogLevel.Debug);
        }
    }
}
