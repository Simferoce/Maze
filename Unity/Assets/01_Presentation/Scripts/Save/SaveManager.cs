using UnityEngine;

namespace Game.Presentation
{
    public class SaveManager
    {
        public static string SESSION_PATH => $"{Application.persistentDataPath}/Sessions";
        public SaveCollection<RecordSessionSave> Sessions { get; private set; }

        private PlatformManager platformManager;

        public SaveManager(PlatformManager platformManager)
        {
            this.platformManager = platformManager;
            Sessions = new SaveCollection<RecordSessionSave>(platformManager, SESSION_PATH);
        }

        public void Load()
        {
            Sessions.Load();
        }
    }
}
