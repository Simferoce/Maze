using System.Collections.Generic;
using System.Linq;

namespace Game.Core
{
    public class RecordSession
    {
        public GameModeParameter GameModeParameter { get; private set; }
        public List<Command> Commands { get; private set; } = new List<Command>();

        public RecordSession(GameModeParameter gameModeParameter)
        {
            GameModeParameter = gameModeParameter;
        }

        public void Register(Command command)
        {
            if (command.CommandType == ComandType.HorizontalAxis
                || command.CommandType == ComandType.VerticalAxis)
            {
                Command lastAction = Commands.LastOrDefault(x => x.CommandType == command.CommandType);
                if (lastAction.CommandType == ComandType.Undefined || lastAction.Value != command.Value)
                {
                    Commands.Add(command);
                }
            }
            else
            {
                Commands.Add(command);
            }
        }
    }
}
