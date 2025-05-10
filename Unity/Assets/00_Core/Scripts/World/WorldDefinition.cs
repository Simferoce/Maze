using System;

namespace Game.Core
{
    public class WorldDefinition : EntityDefinition
    {
        public WallDefinition WallDefinition { get; set; }
        public Fixed64 TileSize { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Scale { get; set; }
        public Vector2 SpawnPoint { get; set; }

        public WorldDefinition(Guid id) : base(id)
        {
        }

        public override Entity Instantiate(GameManager gameManager)
        {
            return new World(gameManager, this);
        }
    }
}
