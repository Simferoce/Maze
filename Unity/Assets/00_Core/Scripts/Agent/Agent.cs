using System;

namespace Game.Core
{
    public abstract class Agent : IDisposable
    {
        protected GameManager gameManager;

        public Agent(GameManager gameManager)
        {
            this.gameManager = gameManager;

            gameManager.WorldManager.Register(this);
        }

        public abstract void Update();

        public virtual void Dispose()
        {
        }
    }
}
