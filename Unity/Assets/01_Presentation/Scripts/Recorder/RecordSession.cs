using System.Collections.Generic;
using System.Linq;

namespace Game.Core
{
    public class RecordSession
    {
        public GameModeParameter GameModeParameter { get; private set; }
        public List<InputAction> InputActions { get; private set; } = new List<InputAction>();

        public RecordSession(GameModeParameter gameModeParameter)
        {
            GameModeParameter = gameModeParameter;
        }

        public void Register(InputAction inputAction)
        {
            if (inputAction.InputType == InputType.HorizontalAxis
                || inputAction.InputType == InputType.VerticalAxis)
            {
                InputAction lastAction = InputActions.LastOrDefault(x => x.InputType == inputAction.InputType);
                if (lastAction.InputType == InputType.Undefined || lastAction.Value != inputAction.Value)
                {
                    InputActions.Add(inputAction);
                }
            }
            else
            {
                InputActions.Add(inputAction);
            }
        }
    }
}
