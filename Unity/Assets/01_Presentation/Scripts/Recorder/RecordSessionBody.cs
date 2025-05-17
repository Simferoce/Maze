using Game.Core;
using System.Collections.Generic;

namespace Game.Presentation
{
    public class RecordSessionBody
    {
        public List<Command> Commands { get; set; } = new List<Command>();

        public void Register(Command command)
        {
            Commands.Add(command);
        }
    }
}
