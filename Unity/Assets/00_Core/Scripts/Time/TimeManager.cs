namespace Game.Core
{
    public class TimeManager
    {
        public long CurrentTick { get; private set; }

        private GameManager gameManager;

        public TimeManager(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        public void Update()
        {
            CurrentTick++;
        }
    }
}
