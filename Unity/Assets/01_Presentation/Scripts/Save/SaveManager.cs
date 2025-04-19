using UnityEngine;

namespace Game.Presentation
{
    public class SaveManager
    {
        public SaveCollection<RecordSessionSave> Sessions { get; private set; }

        private PresentationManager presentationManager;

        public SaveManager(PresentationManager presentationManager)
        {
            this.presentationManager = presentationManager;
            Sessions = new SaveCollection<RecordSessionSave>(presentationManager, $"{Application.persistentDataPath}/Sessions");
        }

        public void Load()
        {
            Sessions.Load();
        }
    }
}
