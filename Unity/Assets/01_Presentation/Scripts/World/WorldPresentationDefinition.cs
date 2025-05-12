using Game.Core;
using UnityEngine;

namespace Game.Presentation
{
    [CreateAssetMenu(fileName = "WorldPresentationDefinition", menuName = "Definitions/WorldPresentationDefinition")]
    public class WorldPresentationDefinition : EntityPresentationDefinition
    {
        [SerializeField] private WallPresentationDefinition wallDefinition;
        [SerializeField, LongAsFixed64Float] private long tileSize;
        [SerializeField] private int width;
        [SerializeField] private int height;
        [SerializeField] private int scale;
        [SerializeField] private int spawnX;
        [SerializeField] private int spawnY;

        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }
        public int SpawnX { get => spawnX; set => spawnX = value; }
        public int SpawnY { get => spawnY; set => spawnY = value; }
        public int Scale { get => scale; set => scale = value; }

        public override Definition Create()
        {
            WorldDefinition definition = new Game.Core.WorldDefinition(this.Id);
            return definition;
        }

        public override bool HasIndependentVisual()
        {
            return false;
        }

        public override void Initialize(Registry registry, Definition definition)
        {
            WorldDefinition worldDefinition = definition as WorldDefinition;
            worldDefinition.Width = width;
            worldDefinition.Height = height;
            worldDefinition.Scale = scale;
            worldDefinition.SpawnPoint = new Core.Vector2(spawnX, spawnY);
            worldDefinition.TileSize = new Fixed64(tileSize);
            worldDefinition.WallDefinition = registry.Get<WallDefinition>(wallDefinition.Id);
        }

        public override EntityVisual InstantiateVisual(Entity entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
