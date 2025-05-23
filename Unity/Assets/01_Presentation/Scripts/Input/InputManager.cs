﻿using Game.Core;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Presentation
{
    public class InputManager : MonoBehaviour
    {
        private Dictionary<Core.ComandType, Game.Core.Command> axisCommands = new Dictionary<Core.ComandType, Game.Core.Command>();
        private Dictionary<Core.ComandType, Game.Core.Command> lastAxisCommands = new Dictionary<Core.ComandType, Game.Core.Command>();
        private Dictionary<Core.ComandType, Game.Core.Command> actionCommands = new Dictionary<Core.ComandType, Game.Core.Command>();
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

        public void OnAbilityUse1(InputAction.CallbackContext context)
        {
            GameManager gameManager = serviceRegistry.Get<GameProvider>().GameManager;
            if (context.performed)
                actionCommands[Core.ComandType.UseAbility] = new Command() { CommandType = ComandType.UseAbility, Value = Fixed64.FromInt(0), Tick = gameManager.TimeManager.CurrentTick };
        }

        public void Flush()
        {
            GameManager gameManager = serviceRegistry.Get<GameProvider>().GameManager;
            UnityEngine.Vector2 lookAtDirection = serviceRegistry.Get<PlayCamera>().GetViewportMousePosition();
            lookAtDirection *= 2f;
            lookAtDirection -= UnityEngine.Vector2.one;

            axisCommands[Core.ComandType.LookAtPointX] = new Command() { CommandType = Core.ComandType.LookAtPointX, Value = Fixed64.FromFloat(lookAtDirection.x), Tick = gameManager.TimeManager.CurrentTick };
            axisCommands[Core.ComandType.LookAtPointY] = new Command() { CommandType = Core.ComandType.LookAtPointY, Value = Fixed64.FromFloat(lookAtDirection.y), Tick = gameManager.TimeManager.CurrentTick };

            foreach (KeyValuePair<Core.ComandType, Game.Core.Command> command in axisCommands)
            {
                if (lastAxisCommands.ContainsKey(command.Key)
                    && lastAxisCommands[command.Key].CommandType == command.Value.CommandType
                    && lastAxisCommands[command.Key].Value == command.Value.Value)
                    continue;

                gameManager.CommandManager.Add(command.Value);
                lastAxisCommands[command.Key] = command.Value;
            }

            foreach (KeyValuePair<Core.ComandType, Game.Core.Command> command in actionCommands)
                gameManager.CommandManager.Add(command.Value);

            actionCommands.Clear();
            axisCommands.Clear();
        }
    }
}
