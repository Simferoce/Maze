using System.Collections.Generic;
using System.Linq;

namespace Game.Core
{
    public class Character : Entity<CharacterDefinition>
    {
        public AttributeHandler AttributeHandler { get; private set; }
        public DynamicObjectHandle DynamicObjectHandle { get; private set; }
        public CollisionHandle CollisionHandle { get; private set; }
        public CharacterStateMachine StateMachine { get; private set; }
        public List<CharacterAbility> Abilities { get; private set; }

        public Character(GameManager gameManager, CharacterDefinition definition) : base(gameManager, definition)
        {
            this.AttributeHandler = Definition.AttributeHandler.Clone();

            StateMachine = new CharacterStateMachine(this);
            CollisionHandle = GameManager.PhysicsManager.RegisterCircle(this.Transform.LocalPosition, Definition.Radius);
            Bounds = new Bounds(new Vector2(-Definition.Radius, -Definition.Radius), new Vector2(Definition.Radius, Definition.Radius));
            DynamicObjectHandle = GameManager.PhysicsManager.RegisterDynamicObject(CollisionHandle);
            GameManager.PhysicsManager.OnCollisionHandled += OnCollisionHandled;

            Abilities = Definition.AbilityDefinitions.Select(x => x.Instantiate(this)).ToList();
            StateMachine.Initialize(new MoveCharacterState(StateMachine));
            GameManager.UpdateManager.Register(Update);
        }

        public override void Dispose()
        {
            base.Dispose();
            GameManager.PhysicsManager.OnCollisionHandled -= OnCollisionHandled;
            GameManager.UpdateManager.Unregister(Update);
        }

        private void Update()
        {
            StateMachine.Update();
        }

        private void OnCollisionHandled()
        {
            SetPosition(GameManager.PhysicsManager.GetCenter(CollisionHandle));
        }

        public override void Move(Vector2 translation)
        {
            if (!StateMachine.CurrentState.CanMove)
                return;

            base.Move(translation);
            GameManager.PhysicsManager.UpdateCircle(CollisionHandle, Transform.LocalPosition, Definition.Radius);
        }

        public void UseAbility(int index)
        {
            Assertion.IsTrue(index < Abilities.Count, $"Attempting to use an ability that does not exists. (index < Abilities.Count [\"{index} < {Abilities.Count}\"])");

            if (StateMachine.CurrentState.CanUseAbility)
                Abilities[index].Use();
        }

        public override void SetPosition(Vector2 position)
        {
            base.SetPosition(position);
            GameManager.PhysicsManager.UpdateCircle(CollisionHandle, Transform.LocalPosition, Definition.Radius);
        }
    }
}
