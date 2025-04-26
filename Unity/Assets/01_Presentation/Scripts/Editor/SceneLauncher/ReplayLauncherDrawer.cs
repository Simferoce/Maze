using Game.SceneLauncher;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine.UIElements;

namespace Game.Presentation
{
    [LauncherDrawer(typeof(ReplayLauncher))]
    public class ReplayLauncherDrawer : LauncherDrawer<ReplayLauncher>
    {
        private struct SessionChoice : IPopupFieldData
        {
            public string Id { get; set; }
            public string Name { get; set; }

            public SessionChoice(string id, string name)
            {
                Id = id;
                Name = name;
            }
        }

        public ReplayLauncherDrawer(ReplayLauncher launcher) : base(launcher)
        {
        }

        public override void Initialize(VisualElement root)
        {
            List<SessionChoice> sessionChoices = Directory.EnumerateDirectories(RecordSessionRepository.SESSION_PATH)
                .Select(x => new SessionChoice(Path.GetFileNameWithoutExtension(x), Path.GetFileNameWithoutExtension(x)))
                .Prepend(new SessionChoice("", "None")).ToList();

            PopupField<SessionChoice> popupField = CreatePreferencePopField<SessionChoice>(ReplayLauncher.SESSION_NAME_KEY, "Session", sessionChoices, x => x.Name);
            root.Add(popupField);
        }
    }
}
