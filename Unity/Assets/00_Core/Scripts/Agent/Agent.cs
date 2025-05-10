namespace Game.Core
{
    public abstract class Agent : Entity<AgentDefinition>
    {
        public Agent(GameManager gameManager, AgentDefinition definition) : base(gameManager, definition)
        {
            gameManager.UpdateManager.Register(Update);
        }

        public abstract void Update();
    }
}
