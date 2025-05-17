using Game.Core;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Presentation
{
    public class InputManager : MonoBehaviour
    {
        private Dictionary<Core.ComandType, Game.Core.Command> commands = new Dictionary<Core.ComandType, Game.Core.Command>();
        private ServiceRegistry serviceRegistry;

        public void Initialize(ServiceRegistry serviceRegistry)
        {
            this.serviceRegistry = serviceRegistry;
        }

        public void OnHorizontal(InputAction.CallbackContext context)
        {
            GameManager gameManager = serviceRegistry.Get<GameProvider>().GameManager;

            float value = context.ReadValue<float>();
            commands[Core.ComandType.HorizontalAxis] = new Core.Command() { CommandType = Core.ComandType.HorizontalAxis, Value = Core.Fixed64.FromFloat(value), Tick = gameManager.TimeManager.CurrentTick };
        }

        public void OnVertical(InputAction.CallbackContext context)
        {
            GameManager gameManager = serviceRegistry.Get<GameProvider>().GameManager;

            float value = context.ReadValue<float>();
            commands[Core.ComandType.VerticalAxis] = new Core.Command() { CommandType = Core.ComandType.VerticalAxis, Value = Core.Fixed64.FromFloat(value), Tick = gameManager.TimeManager.CurrentTick };
        }

        public void Flush()
        {
            GameManager gameManager = serviceRegistry.Get<GameProvider>().GameManager;
            foreach (KeyValuePair<Core.ComandType, Game.Core.Command> command in commands)
                gameManager.CommandManager.Execute(command.Value);

            commands.Clear();
        }
    }
}
