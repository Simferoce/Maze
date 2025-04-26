using Game.Core;
using System.Collections.Generic;
using System.Linq;

namespace Game.Presentation
{
    public struct RecordSessionDTO
    {
        public struct CommandDTO
        {
            public ComandType CommandType { get; set; }
            public long Tick { get; set; }
            public Fixed64 Value { get; set; }

            public static CommandDTO Convert(Command command)
            {
                return new CommandDTO()
                {
                    CommandType = command.CommandType,
                    Tick = command.Tick,
                    Value = command.Value
                };
            }

            public static Command Convert(CommandDTO command)
            {
                return new Command()
                {
                    CommandType = command.CommandType,
                    Tick = command.Tick,
                    Value = command.Value
                };
            }
        }

        public List<CommandDTO> Commands { get; set; }

        public static RecordSessionDTO Convert(RecordSessionBody recordSession)
        {
            return new RecordSessionDTO()
            {
                Commands = recordSession.Commands.Select(x => CommandDTO.Convert(x)).ToList()
            };
        }

        public static RecordSessionBody Convert(RecordSessionDTO recordSession)
        {
            return new RecordSessionBody()
            {
                Commands = recordSession.Commands.Select(x => CommandDTO.Convert(x)).ToList()
            };
        }
    }
}
