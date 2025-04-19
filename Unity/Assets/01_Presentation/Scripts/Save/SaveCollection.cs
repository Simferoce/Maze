using System.Collections.Generic;

namespace Game.Presentation
{
    public class SaveCollection<T>
    {
        private List<SaveContainer<T>> entries = new List<SaveContainer<T>>();
        private PresentationManager presentationManager;
        private string location;

        public SaveCollection(PresentationManager presentationManager, string location)
        {
            this.presentationManager = presentationManager;
            this.location = location;
        }

        public void Load()
        {
            if (!presentationManager.PlatformManager.DirectoryExists(location))
                return;

            foreach (string path in presentationManager.PlatformManager.EnumerateFiles(location))
            {
                SaveContainer<T> saveContainer = new SaveContainer<T>(presentationManager, path);
                saveContainer.Load();
                entries.Add(saveContainer);
            }
        }

        public void Add(T entry, string name)
        {
            SaveContainer<T> saveContainer = new SaveContainer<T>(presentationManager, $"{location}/{name}");
            saveContainer.Save(entry);
            entries.Add(saveContainer);
        }

        public void Flush()
        {
            if (!presentationManager.PlatformManager.DirectoryExists(location))
                presentationManager.PlatformManager.CreateDirectory(location);

            foreach (SaveContainer<T> entry in entries)
                entry.Flush();
        }
    }
}
