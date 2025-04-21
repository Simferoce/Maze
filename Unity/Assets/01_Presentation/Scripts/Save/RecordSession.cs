using Game.Core;
using System.Collections.Generic;

namespace Game.Presentation
{
    public struct RecordSessionSave
    {
        public struct CommandSave
        {
            public ComandType CommandType { get; set; }
            public long Tick { get; set; }
            public Fixed64 Value { get; set; }
        }

        public string GameModeParameter { get; set; }
        public List<CommandSave> Commands { get; set; }
    }
}
