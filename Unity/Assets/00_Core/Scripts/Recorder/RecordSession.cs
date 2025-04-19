using System.Collections.Generic;

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
            InputActions.Add(inputAction);
        }
    }
}
