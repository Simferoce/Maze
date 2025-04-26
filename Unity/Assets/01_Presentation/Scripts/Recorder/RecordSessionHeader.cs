using Game.Core;

namespace Game.Presentation
{
    public class RecordSessionHeader
    {
        public string Name { get; set; }
        public GameModeParameter GameModeParameter { get; set; }
        public long Date { get; set; }
    }
}
