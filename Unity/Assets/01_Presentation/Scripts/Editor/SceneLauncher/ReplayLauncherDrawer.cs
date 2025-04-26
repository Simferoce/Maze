using Game.SceneLauncher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using UnityEngine.UIElements;

namespace Game.Presentation
{
    [LauncherDrawer(typeof(ReplayLauncher))]
    public class ReplayLauncherDrawer : LauncherDrawer<ReplayLauncher>
    {
        private HttpClient httpClient;

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
            httpClient = new HttpClient();
        }

        public override void Initialize(VisualElement root)
        {
            VisualElement container = new VisualElement();
            root.Add(container);

            Refresh(container);
        }

        public async void Refresh(VisualElement container)
        {
            container.Clear();

            try
            {
                HelpBox helpBox = new HelpBox("Getting available sessions.", HelpBoxMessageType.Info);
                container.Add(helpBox);

                List<RecordSessionHeaderDTO> recordSessionHeaderDTOs = await RecordSessionWebRequest.GetRecordSessionHeadersAsync(httpClient);
                List<SessionChoice> sessionChoices = recordSessionHeaderDTOs
                    .Select(x => new SessionChoice(x.Id.ToString(), x.Name))
                    .Prepend(new SessionChoice(string.Empty, "None")).ToList();

                container.Clear();
                PopupField<SessionChoice> popupField = CreatePreferencePopField<SessionChoice>(ReplayLauncher.SESSION_NAME_KEY, "Session", sessionChoices, x => x.Name);
                container.Add(popupField);
            }
            catch (Exception e)
            {
                container.Clear();

                HelpBox helpBox = new HelpBox("Error while attempting to get available sessions.", HelpBoxMessageType.Error);
                container.Add(helpBox);

                Button button = new Button();
                button.text = "Retry";
                button.clicked += () => { Refresh(container); };
                container.Add(button);
            }
        }
    }
}
