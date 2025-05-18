namespace Game.Core
{
    public abstract class CharacterState
    {
        public CharacterStateMachine StateMachine { get; private set; }
        public virtual bool CanMove => true;
        public virtual bool CanUseAbility => true;

        public CharacterState(CharacterStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        public virtual void Enter() { }
        public virtual void Update() { }
        public virtual void Exit() { }
    }
}
