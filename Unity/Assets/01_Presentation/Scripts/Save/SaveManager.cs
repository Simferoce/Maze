using UnityEngine;

namespace Game.Presentation
{
    public class SaveManager
    {
        public SaveCollection<RecordSessionSave> Sessions { get; private set; }

        private PlatformManager platformManager;

        public SaveManager(PlatformManager platformManager)
        {
            this.platformManager = platformManager;
            Sessions = new SaveCollection<RecordSessionSave>(platformManager, $"{Application.persistentDataPath}/Sessions");
        }

        public void Load()
        {
            Sessions.Load();
        }
    }
}
