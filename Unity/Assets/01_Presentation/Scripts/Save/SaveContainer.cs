using Newtonsoft.Json;
using System.Text;

namespace Game.Presentation
{
    public class SaveContainer<T>
    {
        public bool IsLoaded { get; private set; }
        public bool IsDirty { get; private set; } = false;

        private PlatformManager platformManager;
        private string location;
        private T data;

        public SaveContainer(PlatformManager platformManager, string location)
        {
            this.platformManager = platformManager;
            this.location = location;
        }

        public void Flush()
        {
            if (!IsDirty)
                return;

            string json = JsonConvert.SerializeObject(this.data, Formatting.Indented);
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            platformManager.Save(location, bytes);
            IsDirty = false;
        }

        public void Save(T data)
        {
            this.data = data;
            IsDirty = true;
        }

        public void Load()
        {
            if (!platformManager.FileExists(location))
                return;

            byte[] bytes = platformManager.Load(location);
            string json = Encoding.UTF8.GetString(bytes);
            data = JsonConvert.DeserializeObject<T>(json);
            IsLoaded = true;
        }

        public T GetData()
        {
            return data;
        }
    }
}
