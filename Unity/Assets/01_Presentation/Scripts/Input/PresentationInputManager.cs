using Game.Core;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Presentation
{
    public class PresentationInputManager
    {
        private Dictionary<Core.InputType, Game.Core.InputAction> inputActions = new Dictionary<Core.InputType, Game.Core.InputAction>();
        private GameManager gameManager;

        public PresentationInputManager(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        public void Update()
        {
            inputActions[Core.InputType.HorizontalAxis] = new Core.InputAction() { InputType = Core.InputType.HorizontalAxis, Value = Core.Fixed64.FromFloat(Input.GetAxis("Horizontal")), Tick = gameManager.TimeManager.CurrentTick };
            inputActions[Core.InputType.VerticalAxis] = new Core.InputAction() { InputType = Core.InputType.VerticalAxis, Value = Core.Fixed64.FromFloat(Input.GetAxis("Vertical")), Tick = gameManager.TimeManager.CurrentTick };
        }

        public void Flush()
        {
            foreach (KeyValuePair<Core.InputType, Game.Core.InputAction> inputAction in inputActions)
                gameManager.InputManager.Execute(inputAction.Value);

            inputActions.Clear();
        }
    }
}
