using Game.Core;
using System.Collections.Generic;
using System.Linq;

namespace Game.Presentation
{
    public class RecordSessionBody
    {
        public List<Command> Commands { get; set; } = new List<Command>();

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
