namespace Game.Core
{
    public class Character : Entity<CharacterDefinition>
    {
        public AttributeHandler AttributeHandler { get; private set; }
        public DynamicObjectHandle DynamicObjectHandle { get; private set; }
        public CollisionHandle CollisionHandle { get; private set; }

        public Character(GameManager gameManager, CharacterDefinition definition) : base(gameManager, definition)
        {
            this.AttributeHandler = definition.AttributeHandler.Clone();
            CollisionHandle = gameManager.PhysicsManager.RegisterCircle(this.Transform.LocalPosition, definition.Radius);
            Bounds = new Bounds(new Vector2(-definition.Radius, -definition.Radius), new Vector2(definition.Radius, definition.Radius));
            DynamicObjectHandle = gameManager.PhysicsManager.RegisterDynamicObject(CollisionHandle);
            gameManager.PhysicsManager.OnCollisionHandled += OnCollisionHandled;
        }

        public override void Dispose()
        {
            base.Dispose();
            GameManager.PhysicsManager.OnCollisionHandled -= OnCollisionHandled;
        }

        private void OnCollisionHandled()
        {
            Transform.SetPosition(GameManager.PhysicsManager.GetCenter(CollisionHandle));
        }

        public override void Move(Vector2 translation)
        {
            base.Move(translation);
            GameManager.PhysicsManager.UpdateCircle(CollisionHandle, Transform.LocalPosition, Definition.Radius);
        }

        public override void SetPosition(Vector2 position)
        {
            base.SetPosition(position);
            GameManager.PhysicsManager.UpdateCircle(CollisionHandle, Transform.LocalPosition, Definition.Radius);
        }
    }
}
