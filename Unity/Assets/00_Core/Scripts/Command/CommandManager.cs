using System.Collections.Generic;

namespace Game.Core
{
    public class CommandManager
    {
        public delegate void OnCommandReceivedDelegate(Command command);
        public event OnCommandReceivedDelegate OnCommandReceived;

        private GameManager gameManager;
        private List<Command> commands = new List<Command>();

        public CommandManager(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        public void Add(Command command)
        {
            commands.Add(command);
        }

        public void Update()
        {
            foreach (Command command in commands)
                OnCommandReceived?.Invoke(command);

            commands.Clear();
        }
    }
}
