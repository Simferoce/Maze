using Game.SceneLauncher;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Game.Presentation
{
    [LauncherDrawer(typeof(PlayLauncher))]
    public class PlayLauncherDrawer : LauncherDrawer<PlayLauncher>
    {
        private ObjectField playerCharacterDefinitionUIElement;
        private Toggle recordUIElement;

        public PlayLauncherDrawer(PlayLauncher launcher) : base(launcher)
        {
        }

        public override void Initialize(VisualElement root)
        {
            playerCharacterDefinitionUIElement = CreatePreferenceObjectField<CharacterPresentationDefinition>("Player Definition", PlayLauncher.PLAYER_CHARACTER_DEFINITION_KEY);
            root.Add(playerCharacterDefinitionUIElement);

            recordUIElement = CreateTogglePreferenceField("Record", PlayLauncher.RECORD_KEY);
            root.Add(recordUIElement);
        }
    }
}
