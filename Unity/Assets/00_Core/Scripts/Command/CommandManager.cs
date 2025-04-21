namespace Game.Core
{
    public class CommandManager
    {
        public delegate void OnCommandReceivedDelegate(Command command);
        public event OnCommandReceivedDelegate OnCommandReceived;

        private GameManager gameManager;

        public CommandManager(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        public void Execute(Command command)
        {
            OnCommandReceived?.Invoke(command);
        }
    }
}
