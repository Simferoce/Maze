using System;

namespace Game.Core
{
    public class PlayerDefinition : AgentDefinition
    {
        public PlayerDefinition(Guid id) : base(id)
        {
        }

        public override Entity Instantiate(GameManager gameManager)
        {
            return new Player(gameManager, this);
        }
    }
}
