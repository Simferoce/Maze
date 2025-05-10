using System;

namespace Game.Core
{
    public class WallDefinition : EntityDefinition
    {
        public WallDefinition(Guid id) : base(id)
        {
        }

        public override Entity Instantiate(GameManager gameManager)
        {
            return new Wall(gameManager, this);
        }
    }
}
