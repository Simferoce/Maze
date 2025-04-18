using System.Collections.Generic;

namespace Game.Core
{
    public class RecordSession
    {
        public GameModeParameter GameModeParameter { get; private set; }
        private List<InputAction> inputActions = new List<InputAction>();

        public RecordSession(GameModeParameter gameModeParameter)
        {
            GameModeParameter = gameModeParameter;
        }

        public void Register(InputAction inputAction)
        {
            inputActions.Add(inputAction);
        }
    }
}
