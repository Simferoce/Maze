using System.Collections.Generic;
using UnityEngine;

namespace Game.Presentation
{
    public interface IRecordSessionRepository
    {
        public Awaitable<List<RecordSessionHeader>> GetRecordSessionHeadersAsync();
        public Awaitable<RecordSessionHeader> GetRecordSessionHeaderAsync(long id);
        public Awaitable<RecordSessionBody> GetRecordSessionBodyAsync(long id);
        public Awaitable AddRecordSessionHeaderAsync(RecordSessionHeader recordSessionHeader, RecordSessionBody recordSessionBody);
    }
}
