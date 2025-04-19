using Newtonsoft.Json;
using System.Text;

namespace Game.Presentation
{
    public class SaveContainer<T>
    {
        private PresentationManager presentationManager;
        private string location;
        private bool isDirty = false;
        private T data;

        public SaveContainer(PresentationManager presentationManager, string location)
        {
            this.presentationManager = presentationManager;
            this.location = location;
            this.location += ".json";
        }

        public void Flush()
        {
            if (!isDirty)
                return;

            string json = JsonConvert.SerializeObject(this.data, Formatting.Indented);
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            presentationManager.PlatformManager.Save(location, bytes);
            isDirty = false;
        }

        public void Save(T data)
        {
            this.data = data;
            isDirty = true;
        }

        public void Load()
        {
            if (!presentationManager.PlatformManager.FileExists(location))
                return;

            byte[] bytes = presentationManager.PlatformManager.Load(location);
            string json = Encoding.UTF8.GetString(bytes);
            data = JsonConvert.DeserializeObject<T>(json);
        }

        public T GetData()
        {
            return data;
        }
    }
}
