using Game.Core;
using System.Collections.Generic;

namespace Game.Presentation
{
    public struct RecordSessionSave
    {
        public struct InputActionSave
        {
            public InputType InputType { get; set; }
            public long Tick { get; set; }
            public Fixed64 Value { get; set; }
        }

        public string GameModeParameter { get; set; }
        public List<InputActionSave> Inputs { get; set; }
    }
}
