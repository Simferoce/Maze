using Game.Core;

namespace Game.Presentation
{
    public class GameProvider : IService
    {
        public GameManager GameManager { get; private set; }

        public GameProvider(GameManager gameManager)
        {
            this.GameManager = gameManager;
        }
    }
}
