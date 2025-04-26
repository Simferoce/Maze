using Game.Core;
using Newtonsoft.Json;

namespace Game.Presentation
{
    public struct RecordSessionHeaderDTO
    {
        public string Name { get; set; }
        public string GameModeParameter { get; set; }
        public long Date { get; set; }

        public static RecordSessionHeader Convert(RecordSessionHeaderDTO recordSessionHeaderDTO)
        {
            return new RecordSessionHeader()
            {
                Name = recordSessionHeaderDTO.Name,
                GameModeParameter = JsonConvert.DeserializeObject<GameModeParameter>(recordSessionHeaderDTO.GameModeParameter),
                Date = recordSessionHeaderDTO.Date,
            };
        }

        public static RecordSessionHeaderDTO Convert(RecordSessionHeader recordSessionHeader)
        {
            return new RecordSessionHeaderDTO()
            {
                Name = recordSessionHeader.Name,
                GameModeParameter = JsonConvert.SerializeObject(recordSessionHeader.GameModeParameter),
                Date = recordSessionHeader.Date,
            };
        }
    }
}
