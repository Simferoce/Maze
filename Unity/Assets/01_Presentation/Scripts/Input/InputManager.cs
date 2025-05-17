using Game.Core;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Presentation
{
    public class InputManager : MonoBehaviour
    {
        private Dictionary<Core.ComandType, Game.Core.Command> axisCommands = new Dictionary<Core.ComandType, Game.Core.Command>();
        private Dictionary<Core.ComandType, Game.Core.Command> lastAxisCommands = new Dictionary<Core.ComandType, Game.Core.Command>();
        private ServiceRegistry serviceRegistry;

        public void Initialize(ServiceRegistry serviceRegistry)
        {
            this.serviceRegistry = serviceRegistry;
        }

        public void OnHorizontal(InputAction.CallbackContext context)
        {
            GameManager gameManager = serviceRegistry.Get<GameProvider>().GameManager;

            float value = context.ReadValue<float>();
            axisCommands[Core.ComandType.HorizontalAxis] = new Core.Command() { CommandType = Core.ComandType.HorizontalAxis, Value = Core.Fixed64.FromFloat(value), Tick = gameManager.TimeManager.CurrentTick };
        }

        public void OnVertical(InputAction.CallbackContext context)
        {
            GameManager gameManager = serviceRegistry.Get<GameProvider>().GameManager;

            float value = context.ReadValue<float>();
            axisCommands[Core.ComandType.VerticalAxis] = new Core.Command() { CommandType = Core.ComandType.VerticalAxis, Value = Core.Fixed64.FromFloat(value), Tick = gameManager.TimeManager.CurrentTick };
        }

        public void Flush()
        {
            GameManager gameManager = serviceRegistry.Get<GameProvider>().GameManager;
            Core.Vector2 mouseGamePosition = serviceRegistry.Get<PlayCamera>().GetGameWorldPosition();
            axisCommands[Core.ComandType.LookAtPointX] = new Command() { CommandType = Core.ComandType.LookAtPointX, Value = mouseGamePosition.X, Tick = gameManager.TimeManager.CurrentTick };
            axisCommands[Core.ComandType.LookAtPointY] = new Command() { CommandType = Core.ComandType.LookAtPointY, Value = mouseGamePosition.Y, Tick = gameManager.TimeManager.CurrentTick };

            foreach (KeyValuePair<Core.ComandType, Game.Core.Command> command in axisCommands)
            {
                if (lastAxisCommands.ContainsKey(command.Key)
                    && lastAxisCommands[command.Key].CommandType == command.Value.CommandType
                    && lastAxisCommands[command.Key].Value == command.Value.Value)
                    continue;

                gameManager.CommandManager.Execute(command.Value);
                lastAxisCommands[command.Key] = command.Value;
            }

            axisCommands.Clear();
        }
    }
}
