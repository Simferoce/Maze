using System.Collections.Generic;

namespace Game.Core
{
    public class InputManager
    {
        private Dictionary<InputButtonType, InputButtonState> inputButtonStates = new Dictionary<InputButtonType, InputButtonState>()
        {
            { InputButtonType.Right, new InputButtonState(InputButtonType.Right) },
            { InputButtonType.Down, new InputButtonState(InputButtonType.Down) },
            { InputButtonType.Left, new InputButtonState(InputButtonType.Left) },
            { InputButtonType.Up, new InputButtonState(InputButtonType.Up) },
        };
        private Dictionary<InputAxisType, InputAxisState> inputAxisStates = new Dictionary<InputAxisType, InputAxisState>()
        {
            {InputAxisType.Horizontal, new InputAxisState(InputButtonType.Right, InputButtonType.Left) },
            {InputAxisType.Vertical, new InputAxisState(InputButtonType.Up, InputButtonType.Down) },
        };

        private GameManager gameManager;

        public InputManager(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        public void Update()
        {
            foreach (KeyValuePair<InputAxisType, InputAxisState> inputAxisState in inputAxisStates)
                inputAxisState.Value.Update(this);
        }

        #region Write
        public void Press(InputButtonType inputButton)
        {
            Assertion.IsTrue(inputButtonStates.ContainsKey(inputButton), $"The input button \"{inputButton}\" is missing from the preallocated array.");

            inputButtonStates[inputButton].Press();
        }

        public void Unpress(InputButtonType inputButton)
        {
            Assertion.IsTrue(inputButtonStates.ContainsKey(inputButton), $"The input button \"{inputButton}\" is missing from the preallocated array.");

            inputButtonStates[inputButton].Unpress();
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
