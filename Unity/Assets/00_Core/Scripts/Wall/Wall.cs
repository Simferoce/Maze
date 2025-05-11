namespace Game.Core
{
    public class Wall : Entity<WallDefinition>
    {
        public Vector2 Size { get; private set; }
        public CollisionHandle CollisionHandle { get; private set; }

        public Wall(GameManager gameManager, WallDefinition definition) : base(gameManager, definition)
        {

        }

        public void Initialize(Vector2 size)
        {
            Size = size;
            CollisionHandle = GameManager.PhysicsManager.RegisterAABB(this.Transform.LocalPosition - Size / 2, this.Transform.LocalPosition + Size / 2);
        }

        public override void SetPosition(Vector2 position)
        {
            base.SetPosition(position);
            GameManager.PhysicsManager.UpdateAABB(CollisionHandle, this.Transform.LocalPosition - Size / 2, this.Transform.LocalPosition + Size / 2);
        }

        public override void Move(Vector2 translation)
        {
            base.Move(translation);
            GameManager.PhysicsManager.UpdateAABB(CollisionHandle, this.Transform.LocalPosition - Size / 2, this.Transform.LocalPosition + Size / 2);
        }
    }
}
