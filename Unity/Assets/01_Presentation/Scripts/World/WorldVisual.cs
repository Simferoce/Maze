using Game.Core;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.Presentation
{
    public class WorldVisual : EntityVisual
    {
        [SerializeField] private Grid grid;
        [SerializeField] private Tilemap tilemap;

        protected override void Update()
        {
            World world = gameManager.WorldManager.GetEntityById(EntityId) as World;
            grid.cellSize = new Vector3(world.Definition.TileSize.ToFloat() / 100, world.Definition.TileSize.ToFloat() / 100, world.Definition.TileSize.ToFloat() / 100);
            for (int x = 0; x < world.Walls.GetLength(0); ++x)
            {
                for (int y = 0; y < world.Walls.GetLength(1); ++y)
                {
                    Wall wall = world.Walls[x, y];
                    TileBase tile = tilemap.GetTile(new Vector3Int(x, y, 0));
                    if (wall != null && tile == null)
                    {
                        WallPresentationDefinition wallPresentationDefinition = presentationRegistry.Get<WallPresentationDefinition>(wall.Definition.Id);
                        if (wallPresentationDefinition == null)
                        {
                            Debug.LogError($"Could not find the visual definition associated with \"{wall.Definition.Id}\".");
                            continue;
                        }

                        tilemap.SetTile(new Vector3Int(x, y, 0), wallPresentationDefinition.Tile);
                    }
                    else if (wall != null && tile != null)
                    {
                        WallPresentationDefinition wallPresentationDefinition = presentationRegistry.Get<WallPresentationDefinition>(wall.Definition.Id);
                        if (wallPresentationDefinition == null)
                        {
                            Debug.LogError($"Could not find the visual definition associated with \"{wall.Definition.Id}\".");
                            continue;
                        }

                        if (tile != wallPresentationDefinition.Tile)
                        {
                            tilemap.SetTile(new Vector3Int(x, y, 0), wallPresentationDefinition.Tile);
                        }
                    }
                    else if (wall == null && tile != null)
                    {
                        tilemap.SetTile(new Vector3Int(x, y, 0), null);
                    }
                }
            }
        }
    }
}

