using Game.Core;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Presentation
{
    public class InputManager
    {
        private Dictionary<Core.ComandType, Game.Core.Command> commands = new Dictionary<Core.ComandType, Game.Core.Command>();
        private GameManager gameManager;

        public InputManager(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        public void Update()
        {
            commands[Core.ComandType.HorizontalAxis] = new Core.Command() { CommandType = Core.ComandType.HorizontalAxis, Value = Core.Fixed64.FromFloat(Input.GetAxis("Horizontal")), Tick = gameManager.TimeManager.CurrentTick };
            commands[Core.ComandType.VerticalAxis] = new Core.Command() { CommandType = Core.ComandType.VerticalAxis, Value = Core.Fixed64.FromFloat(Input.GetAxis("Vertical")), Tick = gameManager.TimeManager.CurrentTick };
        }

        public void Flush()
        {
            foreach (KeyValuePair<Core.ComandType, Game.Core.Command> command in commands)
                gameManager.CommandManager.Execute(command.Value);

            commands.Clear();
        }
    }
}
