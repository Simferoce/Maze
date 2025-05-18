namespace Game.Core
{
    public class CharacterStateMachine
    {
        public CharacterState CurrentState { get; private set; }
        public CharacterState NextState { get; private set; }
        public Character Character { get; private set; }

        public CharacterStateMachine(Character character)
        {
            Character = character;
        }

        public void Initialize(CharacterState initialState)
        {
            CurrentState = initialState;
            CurrentState.Enter();
        }

        public void Update()
        {
            Assertion.IsNotNull(CurrentState, "Attempting to update the statemachine while it does not have a state.");

            if (NextState != null)
            {
                CurrentState.Exit();
                CurrentState = NextState;
                CurrentState.Enter();
                NextState = null;
            }

            CurrentState.Update();
        }

        public void Change(CharacterState nextState)
        {
            if (this.NextState != null)
                Character.GameManager.Logger.Log($"Overriding the next state \"{this.NextState}\" with {nextState}.", ILogger.LogLevel.Error);

            this.NextState = nextState;
        }
    }
}
