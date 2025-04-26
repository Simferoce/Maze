using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace Game.Presentation
{
    public class RecordSessionRepository
    {
        public static string SESSION_PATH => $"{Application.persistentDataPath}/Sessions";
        public static string HEADER_FILE => "header.json";
        public static string BODDY_FILE => "body.json";

        private PlatformManager platformManager;

        public RecordSessionRepository(PlatformManager platformManager)
        {
            this.platformManager = platformManager;
        }

        public void Add(RecordSessionHeader recordSessionHeader, RecordSessionBody recordSession)
        {
            if (!platformManager.DirectoryExists(SESSION_PATH))
                platformManager.CreateDirectory(SESSION_PATH);

            string folderPath = Path.Combine(SESSION_PATH, recordSessionHeader.Name);
            if (!platformManager.DirectoryExists(folderPath))
                platformManager.CreateDirectory(folderPath);

            string headerPath = Path.Combine(folderPath, HEADER_FILE);
            string bodyPath = Path.Combine(folderPath, BODDY_FILE);

            RecordSessionHeaderDTO recordSessionHeaderDTO = RecordSessionHeaderDTO.Convert(recordSessionHeader);
            string headerJson = JsonConvert.SerializeObject(recordSessionHeaderDTO);
            byte[] headerData = Encoding.UTF8.GetBytes(headerJson);
            platformManager.Save(headerPath, headerData);

            RecordSessionDTO recordSessionDTO = RecordSessionDTO.Convert(recordSession);
            string bodyJson = JsonConvert.SerializeObject(recordSessionDTO);
            byte[] bodyData = Encoding.UTF8.GetBytes(bodyJson);
            platformManager.Save(bodyPath, bodyData);
        }

        public List<RecordSessionHeader> GetAllHeader()
        {
            List<RecordSessionHeader> results = new List<RecordSessionHeader>();

            if (!platformManager.DirectoryExists(SESSION_PATH))
                return results;

            foreach (string path in platformManager.EnumerateFiles(SESSION_PATH))
            {
                if (!platformManager.DirectoryExists(path))
                    continue;

                string headerPath = Path.Combine(path, HEADER_FILE);
                if (!platformManager.FileExists(headerPath))
                    continue;

                byte[] headerData = platformManager.Load(headerPath);
                string json = Encoding.UTF8.GetString(headerData);
                RecordSessionHeaderDTO recordSessionHeaderDTO = JsonConvert.DeserializeObject<RecordSessionHeaderDTO>(json);
                RecordSessionHeader recordSessionHeader = RecordSessionHeaderDTO.Convert(recordSessionHeaderDTO);
                results.Add(recordSessionHeader);
            }

            return results;
        }

        public RecordSessionBody GetRecordSessionBody(string name)
        {
            string folderPath = Path.Combine(SESSION_PATH, name);
            if (!platformManager.DirectoryExists(folderPath))
            {
                Debug.LogError($"Could not find the directory of the given header \"{name}\"");
                return null;
            }

            string bodyPath = Path.Combine(folderPath, BODDY_FILE);
            if (!platformManager.FileExists(bodyPath))
            {
                Debug.LogError($"Could not find the body of the given header \"{name}\"");
                return null;
            }

            byte[] bodyData = platformManager.Load(bodyPath);
            string json = Encoding.UTF8.GetString(bodyData);
            RecordSessionDTO recordSessionDTO = JsonConvert.DeserializeObject<RecordSessionDTO>(json);
            RecordSessionBody recordSessionBody = RecordSessionDTO.Convert(recordSessionDTO);
            return recordSessionBody;
        }

        public RecordSessionHeader GetRecordSessionHeader(string name)
        {
            string folderPath = Path.Combine(SESSION_PATH, name);
            if (!platformManager.DirectoryExists(folderPath))
            {
                Debug.LogError($"Could not find the directory of the given header \"{name}\"");
                return null;
            }

            string headerPath = Path.Combine(folderPath, HEADER_FILE);
            if (!platformManager.FileExists(headerPath))
            {
                Debug.LogError($"Could not find the header of the given header \"{name}\"");
                return null;
            }

            byte[] headerData = platformManager.Load(headerPath);
            string json = Encoding.UTF8.GetString(headerData);
            RecordSessionHeaderDTO recordSessionHeaderDTO = JsonConvert.DeserializeObject<RecordSessionHeaderDTO>(json);
            RecordSessionHeader recordSessionHeader = RecordSessionHeaderDTO.Convert(recordSessionHeaderDTO);
            return recordSessionHeader;
        }
    }
}
