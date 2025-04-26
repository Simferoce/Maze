using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using UnityEngine;

namespace Game.Presentation
{
    public class RecordSessionRepositoryWeb : IRecordSessionRepository
    {
        private readonly HttpClient httpClient;

        public RecordSessionRepositoryWeb()
        {
            httpClient = new HttpClient();
        }

        public async Awaitable<List<RecordSessionHeader>> GetRecordSessionHeadersAsync()
        {
            List<RecordSessionHeaderDTO> recordSessionHeaderDTOs = await RecordSessionWebRequest.GetRecordSessionHeadersAsync(httpClient);
            List<RecordSessionHeader> result = recordSessionHeaderDTOs.Select(x => RecordSessionHeaderDTO.Convert(x)).ToList();
            return result;
        }

        public async Awaitable<RecordSessionHeader> GetRecordSessionHeaderAsync(long id)
        {
            RecordSessionHeaderDTO recordSessionHeaderDTO = await RecordSessionWebRequest.GetRecordSessionHeaderAsync(httpClient, id);
            RecordSessionHeader recordSessionHeader = RecordSessionHeaderDTO.Convert(recordSessionHeaderDTO);
            return recordSessionHeader;
        }

        public async Awaitable AddRecordSessionHeaderAsync(RecordSessionHeader recordSessionHeader, RecordSessionBody recordSessionBody)
        {
            RecordSessionHeaderDTO recordSessionHeaderDTO = RecordSessionHeaderDTO.Convert(recordSessionHeader);
            RecordSessionBodyDTO recordSessionBodyDTO = RecordSessionBodyDTO.Convert(recordSessionBody);
            await RecordSessionWebRequest.AddRecordSessionHeaderAsync(httpClient, recordSessionHeaderDTO, recordSessionBodyDTO);
        }

        public async Awaitable<RecordSessionBody> GetRecordSessionBodyAsync(long id)
        {
            RecordSessionBodyDTO recordSessionBodyDTO = await RecordSessionWebRequest.GetRecordSessionBodyAsync(httpClient, id);
            RecordSessionBody recordSessionBody = RecordSessionBodyDTO.Convert(recordSessionBodyDTO);
            return recordSessionBody;
        }
    }
}