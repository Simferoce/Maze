namespace Game.Core
{
    public class World : Entity<WorldDefinition>
    {
        private Wall[,] walls;
        private Vector2 spawnPoint;

        public Wall[,] Walls { get => walls; set => walls = value; }
        public Vector2 SpawnPoint { get => spawnPoint; set => spawnPoint = value; }

        public World(GameManager gameManager, WorldDefinition definition) : base(gameManager, definition)
        {
        }

        public void Generate(int seed)
        {
            //Uneven number spawn point wrong most likely because of int / int vs fixed64 / int in the spawn point
            Vector2 wallSize = new Vector2(Definition.TileSize, Definition.TileSize);
            spawnPoint = (Definition.SpawnPoint / Definition.Scale) * Definition.TileSize * Definition.Scale;
            spawnPoint += new Vector2(Definition.TileSize * Definition.Scale, Definition.TileSize * Definition.Scale);
            //spawnPoint += new Vector2(Definition.TileSize / Fixed64.FromInt(2) * Definition.Scale, Definition.TileSize / Fixed64.FromInt(2) * Definition.Scale);
            walls = new Wall[((Definition.Width / Definition.Scale) + 2) * Definition.Scale, ((Definition.Height / Definition.Scale) + 2) * Definition.Scale];

            DepthFirstLayout depthFirstLayout = new DepthFirstLayout();
            bool[,] layout = depthFirstLayout.Generate(seed, Definition.Width / Definition.Scale, Definition.Height / Definition.Scale, Definition.SpawnPoint / Definition.Scale);

            for (int x = 0; x < Definition.Width / Definition.Scale; ++x)
            {
                for (int y = 0; y < Definition.Height / Definition.Scale; ++y)
                {
                    for (int sX = 0; sX < Definition.Scale; sX++)
                    {
                        for (int sY = 0; sY < Definition.Scale; sY++)
                        {
                            if (layout[x, y])
                                continue;

                            GenerateWall(x + 1, y + 1, sX, sY, wallSize);
                        }
                    }
                }
            }

            for (int x = 1; x < Definition.Width / Definition.Scale + 1; ++x)
            {
                for (int sX = 0; sX < Definition.Scale; sX++)
                {
                    for (int sY = 0; sY < Definition.Scale; sY++)
                    {
                        GenerateWall(x, 0, sX, sY, wallSize);
                        GenerateWall(x, (walls.GetLength(1) - 1) / Definition.Scale, sX, sY, wallSize);
                    }
                }
            }

            for (int y = 1; y < Definition.Height / Definition.Scale + 1; ++y)
            {
                for (int sX = 0; sX < Definition.Scale; sX++)
                {
                    for (int sY = 0; sY < Definition.Scale; sY++)
                    {
                        GenerateWall(0, y, sX, sY, wallSize);
                        GenerateWall((walls.GetLength(0) - 1) / Definition.Scale, y, sX, sY, wallSize);
                    }
                }
            }

            for (int sX = 0; sX < Definition.Scale; sX++)
            {
                for (int sY = 0; sY < Definition.Scale; sY++)
                {
                    GenerateWall(0, 0, sX, sY, wallSize);
                    GenerateWall((walls.GetLength(0) - 1) / Definition.Scale, 0, sX, sY, wallSize);
                    GenerateWall((walls.GetLength(0) - 1) / Definition.Scale, (walls.GetLength(1) - 1) / Definition.Scale, sX, sY, wallSize);
                    GenerateWall(0, (walls.GetLength(1) - 1) / Definition.Scale, sX, sY, wallSize);
                }
            }
        }

        private void GenerateWall(int x, int y, int sX, int sY, Vector2 size)
        {
            Wall wall = Definition.WallDefinition.Instantiate(GameManager) as Wall;
            wall.Initialize(size);
            Vector2 worldPosition = new Vector2((x * Definition.Scale + sX) * Definition.TileSize, (y * Definition.Scale + sY) * Definition.TileSize);
            worldPosition += size / 2;
            wall.SetPosition(worldPosition);

            if (walls[x * Definition.Scale + sX, y * Definition.Scale + sY] != null)
            {
                GameManager.Logger.Log($"There is already a wall at [{x * Definition.Scale + sX}, {y * Definition.Scale + sY}]", ILogger.LogLevel.Error);
                return;
            }

            walls[x * Definition.Scale + sX, y * Definition.Scale + sY] = wall;
        }
    }
}
