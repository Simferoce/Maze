namespace Game.Core
{
    public class Wall : Entity<WallDefinition>
    {
        public Wall(GameManager gameManager, WallDefinition definition) : base(gameManager, definition)
        {
        }
    }
}
