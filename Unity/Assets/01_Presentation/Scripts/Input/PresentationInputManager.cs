using System.Collections.Generic;
using UnityEngine;

namespace Game.Presentation
{
    public class PresentationInputManager
    {
        private Dictionary<(Core.InputType, Core.InputButtonType), Game.Core.InputAction> buttonInputActions = new Dictionary<(Core.InputType, Core.InputButtonType), Game.Core.InputAction>();
        private Dictionary<Core.InputAxisType, Game.Core.InputAction> axisInputActions = new Dictionary<Core.InputAxisType, Game.Core.InputAction>();
        private PresentationManager presentationManager;

        public PresentationInputManager(PresentationManager presentationManager)
        {
            this.presentationManager = presentationManager;
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.D))
                buttonInputActions[(Core.InputType.ButtonDown, Core.InputButtonType.Right)] = Core.InputAction.BuildInputActionButtonDown(Core.InputButtonType.Right);
            else if (Input.GetKeyDown(KeyCode.S))
                buttonInputActions[(Core.InputType.ButtonDown, Core.InputButtonType.Right)] = Core.InputAction.BuildInputActionButtonDown(Core.InputButtonType.Down);
            else if (Input.GetKeyDown(KeyCode.A))
                buttonInputActions[(Core.InputType.ButtonDown, Core.InputButtonType.Right)] = Core.InputAction.BuildInputActionButtonDown(Core.InputButtonType.Left);
            else if (Input.GetKeyDown(KeyCode.W))
                buttonInputActions[(Core.InputType.ButtonDown, Core.InputButtonType.Right)] = Core.InputAction.BuildInputActionButtonDown(Core.InputButtonType.Up);

            if (Input.GetKeyUp(KeyCode.D))
                buttonInputActions[(Core.InputType.ButtonUp, Core.InputButtonType.Right)] = Core.InputAction.BuildInputActionButtonUp(Core.InputButtonType.Right);
            else if (Input.GetKeyUp(KeyCode.S))
                buttonInputActions[(Core.InputType.ButtonUp, Core.InputButtonType.Right)] = Core.InputAction.BuildInputActionButtonUp(Core.InputButtonType.Down);
            else if (Input.GetKeyUp(KeyCode.A))
                buttonInputActions[(Core.InputType.ButtonUp, Core.InputButtonType.Right)] = Core.InputAction.BuildInputActionButtonUp(Core.InputButtonType.Left);
            else if (Input.GetKeyUp(KeyCode.W))
                buttonInputActions[(Core.InputType.ButtonUp, Core.InputButtonType.Right)] = Core.InputAction.BuildInputActionButtonUp(Core.InputButtonType.Up);

            axisInputActions[Core.InputAxisType.Horizontal] = (Core.InputAction.BuildInputActionAxisSet(Core.InputAxisType.Horizontal, Core.Fixed64.FromFloat(Input.GetAxis("Horizontal"))));
            axisInputActions[Core.InputAxisType.Vertical] = Core.InputAction.BuildInputActionAxisSet(Core.InputAxisType.Vertical, Core.Fixed64.FromFloat(Input.GetAxis("Vertical")));
        }

        public void Flush()
        {
            foreach (KeyValuePair<(Core.InputType, Core.InputButtonType), Core.InputAction> buttonInputAction in buttonInputActions)
                presentationManager.GameManager.InputManager.Write(buttonInputAction.Value);

            foreach (KeyValuePair<Core.InputAxisType, Core.InputAction> axisInputAction in axisInputActions)
            {
                if (presentationManager.GameManager.InputManager.GetAxis(axisInputAction.Value.InputAxisType) == axisInputAction.Value.Value)
                    continue;

                presentationManager.GameManager.InputManager.Write(axisInputAction.Value);
            }

            buttonInputActions.Clear();
            axisInputActions.Clear();
        }
    }
}
