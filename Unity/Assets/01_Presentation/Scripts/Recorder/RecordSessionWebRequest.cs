using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using UnityEngine;

namespace Game.Presentation
{
    public static class RecordSessionWebRequest
    {
        public static readonly string BASE_URL = "https://localhost:7205/api";

        public static async Awaitable<List<RecordSessionHeaderDTO>> GetRecordSessionHeadersAsync(HttpClient httpClient)
        {
            HttpResponseMessage response = await httpClient.GetAsync($"{BASE_URL}/RecordSessionHeaders");
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            List<RecordSessionHeaderDTO> recordSessionHeaderDTOs = JsonConvert.DeserializeObject<List<RecordSessionHeaderDTO>>(json);
            return recordSessionHeaderDTOs;
        }

        public static async Awaitable<RecordSessionHeaderDTO> GetRecordSessionHeaderAsync(HttpClient httpClient, long id)
        {
            HttpResponseMessage response = await httpClient.GetAsync($"{BASE_URL}/RecordSessionHeaders/{id}");
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            RecordSessionHeaderDTO recordSessionHeaderDTO = JsonConvert.DeserializeObject<RecordSessionHeaderDTO>(json);
            return recordSessionHeaderDTO;
        }

        public static async Awaitable AddRecordSessionHeaderAsync(HttpClient httpClient, RecordSessionHeaderDTO recordSessionHeader, RecordSessionBodyDTO recordSessionBody)
        {
            StringContent jsonContent = new StringContent(JsonConvert.SerializeObject(recordSessionHeader), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync($"{BASE_URL}/RecordSessionHeaders", jsonContent);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            JObject jObject = JObject.Parse(json);

            long id = (long)jObject["id"];
            recordSessionHeader.Id = id;

            string bodyJson = JsonConvert.SerializeObject(recordSessionBody);
            byte[] bodyData = Encoding.UTF8.GetBytes(bodyJson);
            MultipartFormDataContent content = new MultipartFormDataContent();
            ByteArrayContent fileContent = new ByteArrayContent(bodyData);
            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
            content.Add(fileContent, "file", recordSessionHeader.Id.ToString());

            HttpResponseMessage responseBody = await httpClient.PostAsync($"{BASE_URL}/RecordSessionBodies/upload", content);
            responseBody.EnsureSuccessStatusCode();
        }

        public static async Awaitable<RecordSessionBodyDTO> GetRecordSessionBodyAsync(HttpClient httpClient, long id)
        {
            HttpResponseMessage response = await httpClient.GetAsync($"{BASE_URL}/RecordSessionBodies/download/{id}");
            response.EnsureSuccessStatusCode();

            Stream stream = await response.Content.ReadAsStreamAsync();
            using MemoryStream memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            string bodyJson = Encoding.UTF8.GetString(memoryStream.ToArray());
            RecordSessionBodyDTO recordSessionBodyDTO = JsonConvert.DeserializeObject<RecordSessionBodyDTO>(bodyJson);
            return recordSessionBodyDTO;
        }
    }
}
