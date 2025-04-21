using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Game.Presentation
{
    public class SaveCollection<T>
    {
        private Dictionary<string, SaveContainer<T>> entries = new Dictionary<string, SaveContainer<T>>();
        private PlatformManager platformManager;
        private string location;

        public SaveCollection(PlatformManager platformManager, string location)
        {
            this.platformManager = platformManager;
            this.location = location;
        }

        public void Load()
        {
            if (!platformManager.DirectoryExists(location))
                return;

            foreach (string path in platformManager.EnumerateFiles(location))
            {
                SaveContainer<T> saveContainer = new SaveContainer<T>(platformManager, path);
                entries.Add(Path.GetFileNameWithoutExtension(path), saveContainer);
            }
        }

        public void Add(T entry, string name)
        {
            SaveContainer<T> saveContainer = new SaveContainer<T>(platformManager, $"{location}/{name}.json");
            saveContainer.Save(entry);
            entries.Add(name, saveContainer);
        }

        public void Flush()
        {
            if (!platformManager.DirectoryExists(location))
                platformManager.CreateDirectory(location);

            foreach (var entry in entries.Where(x => x.Value.IsDirty))
                entry.Value.Flush();
        }

        public T GetEntry(string name)
        {
            if (!entries.ContainsKey(name))
            {
                Debug.LogError($"Could not get the entry with the name \"{name}\" because it does not exists in the directory.");
                return default;
            }

            if (!entries[name].IsLoaded)
                entries[name].Load();

            return entries[name].GetData();
        }
    }
}
