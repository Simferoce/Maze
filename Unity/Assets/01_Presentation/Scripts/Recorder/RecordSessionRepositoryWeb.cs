using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using UnityEngine;

namespace Game.Presentation
{
    public class RecordSessionRepositoryWeb : IRecordSessionRepository
    {
        private readonly HttpClient httpClient;
        private readonly string baseUrl = "https://localhost:7205/api";

        public RecordSessionRepositoryWeb()
        {
            httpClient = new HttpClient();
        }

        public async Awaitable<List<RecordSessionHeader>> GetRecordSessionHeadersAsync()
        {
            var response = await httpClient.GetAsync($"{baseUrl}/RecordSessionHeaders");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            List<RecordSessionHeaderDTO> recordSessionHeaderDTOs = JsonConvert.DeserializeObject<List<RecordSessionHeaderDTO>>(json);
            List<RecordSessionHeader> result = recordSessionHeaderDTOs.Select(x => RecordSessionHeaderDTO.Convert(x)).ToList();
            return result;
        }

        public async Awaitable<RecordSessionHeader> GetRecordSessionHeaderAsync(long id)
        {
            var response = await httpClient.GetAsync($"{baseUrl}/RecordSessionHeaders/{id}");
            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            RecordSessionHeaderDTO recordSessionHeaderDTO = JsonConvert.DeserializeObject<RecordSessionHeaderDTO>(json);
            RecordSessionHeader recordSessionHeader = RecordSessionHeaderDTO.Convert(recordSessionHeaderDTO);
            return recordSessionHeader;
        }

        public async Awaitable AddRecordSessionHeaderAsync(RecordSessionHeader recordSessionHeader, RecordSessionBody recordSessionBody)
        {
            RecordSessionHeaderDTO recordSessionHeaderDTO = RecordSessionHeaderDTO.Convert(recordSessionHeader);
            RecordSessionBodyDTO recordSessionBodyDTO = RecordSessionBodyDTO.Convert(recordSessionBody);

            var jsonContent = new StringContent(JsonConvert.SerializeObject(recordSessionHeaderDTO), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{baseUrl}/RecordSessionHeaders", jsonContent);
            response.EnsureSuccessStatusCode();

            string bodyJson = JsonConvert.SerializeObject(recordSessionBodyDTO);
            byte[] bodyData = Encoding.UTF8.GetBytes(bodyJson);
            var content = new MultipartFormDataContent();
            var fileContent = new ByteArrayContent(bodyData);
            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
            content.Add(fileContent, "file", recordSessionHeader.Id.ToString());

            var responseBody = await httpClient.PostAsync($"{baseUrl}/RecordSessionBodies/upload", content);
            responseBody.EnsureSuccessStatusCode();
        }

        public async Awaitable<RecordSessionBody> GetRecordSessionBodyAsync(long id)
        {
            var response = await httpClient.GetAsync($"{baseUrl}/RecordSessionBodies/download/{id}");
            if (!response.IsSuccessStatusCode)
                return null;

            var stream = await response.Content.ReadAsStreamAsync();
            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            var bodyJson = Encoding.UTF8.GetString(memoryStream.ToArray());
            RecordSessionBodyDTO recordSessionBodyDTO = JsonConvert.DeserializeObject<RecordSessionBodyDTO>(bodyJson);
            RecordSessionBody recordSessionBody = RecordSessionBodyDTO.Convert(recordSessionBodyDTO);
            return recordSessionBody;
        }
    }
}