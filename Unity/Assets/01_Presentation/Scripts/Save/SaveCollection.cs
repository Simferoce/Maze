using System.Collections.Generic;

namespace Game.Presentation
{
    public class SaveCollection<T>
    {
        private List<SaveContainer<T>> entries = new List<SaveContainer<T>>();
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
                saveContainer.Load();
                entries.Add(saveContainer);
            }
        }

        public void Add(T entry, string name)
        {
            SaveContainer<T> saveContainer = new SaveContainer<T>(platformManager, $"{location}/{name}");
            saveContainer.Save(entry);
            entries.Add(saveContainer);
        }

        public void Flush()
        {
            if (!platformManager.DirectoryExists(location))
                platformManager.CreateDirectory(location);

            foreach (SaveContainer<T> entry in entries)
                entry.Flush();
        }
    }
}
