using System.Collections.Generic;

namespace Game.Core
{
    public class InputManager
    {
        public delegate void OnInputActionDelegate(InputAction inputAction);
        public event OnInputActionDelegate OnInputAction;

        private Dictionary<InputButtonType, InputButtonState> inputButtonStates = new Dictionary<InputButtonType, InputButtonState>()
        {
            { InputButtonType.Right, new InputButtonState(InputButtonType.Right) },
            { InputButtonType.Down, new InputButtonState(InputButtonType.Down) },
            { InputButtonType.Left, new InputButtonState(InputButtonType.Left) },
            { InputButtonType.Up, new InputButtonState(InputButtonType.Up) },
        };

        private Dictionary<InputAxisType, InputAxisState> inputAxisStates = new Dictionary<InputAxisType, InputAxisState>()
        {
            {InputAxisType.Horizontal, new InputAxisState(Fixed64.Zero) },
            {InputAxisType.Vertical, new InputAxisState(Fixed64.Zero) },
        };

        private GameManager gameManager;

        public InputManager(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        #region Write
        public void Write(InputAction inputAction)
        {
            inputAction.Tick = gameManager.TimeManager.CurrentTick;

            switch (inputAction.InputType)
            {
                case InputType.SetAxis:
                    SetAxis(inputAction.InputAxisType, inputAction.Value);
                    break;
                case InputType.ButtonDown:
                    SetButtonDown(inputAction.InputButtonType);
                    break;
                case InputType.ButtonUp:
                    SetButtonUp(inputAction.InputButtonType);
                    break;
            }

            OnInputAction?.Invoke(inputAction);
        }

        private void SetButtonDown(InputButtonType inputButton)
        {
            Assertion.IsTrue(inputButtonStates.ContainsKey(inputButton), $"The input button \"{inputButton}\" is missing from the preallocated array.");

            inputButtonStates[inputButton] = inputButtonStates[inputButton].SetButtonDown();

        }

        private void SetButtonUp(InputButtonType inputButton)
        {
            Assertion.IsTrue(inputButtonStates.ContainsKey(inputButton), $"The input button \"{inputButton}\" is missing from the preallocated array.");

            inputButtonStates[inputButton] = inputButtonStates[inputButton].SetButtonUp();
        }

        private void SetAxis(InputAxisType inputAxisType, Fixed64 value)
        {
            Assertion.IsTrue(inputAxisStates.ContainsKey(inputAxisType), $"The input axis \"{inputAxisType}\" is missing from the preallocated array.");

            inputAxisStates[inputAxisType] = inputAxisStates[inputAxisType].Set(value);
        }
        #endregion

        #region Read
        public bool IsPressed(InputButtonType inputButton)
        {
            Assertion.IsTrue(inputButtonStates.ContainsKey(inputButton), $"The input button \"{inputButton}\" is missing from the preallocated array.");

            return inputButtonStates[inputButton].IsPress;
        }
        public Fixed64 GetAxis(InputAxisType inputAxisType)
        {
            Assertion.IsTrue(inputAxisStates.ContainsKey(inputAxisType), $"The input axis \"{inputAxisType}\" is missing from the preallocated array.");

            return inputAxisStates[inputAxisType].Value;
        }
        #endregion
    }
}
