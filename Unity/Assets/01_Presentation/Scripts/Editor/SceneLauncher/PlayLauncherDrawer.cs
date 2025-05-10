using Game.SceneLauncher;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Game.Presentation
{
    [LauncherDrawer(typeof(PlayLauncher))]
    public class PlayLauncherDrawer : LauncherDrawer<PlayLauncher>
    {
        private ObjectField playerCharacterDefinitionUIElement;
        private ObjectField worldDefinitionUIElement;
        private Toggle recordUIElement;
        private IntegerField seedUIElement;

        public PlayLauncherDrawer(PlayLauncher launcher) : base(launcher)
        {
        }

        public override void Initialize(VisualElement root)
        {
            worldDefinitionUIElement = CreatePreferenceObjectField<WorldPresentationDefinition>("WorldManager Definition", PlayLauncher.WORLD_DEFINITION_KEY);
            root.Add(worldDefinitionUIElement);

            playerCharacterDefinitionUIElement = CreatePreferenceObjectField<CharacterPresentationDefinition>("Player Definition", PlayLauncher.PLAYER_CHARACTER_DEFINITION_KEY);
            root.Add(playerCharacterDefinitionUIElement);

            seedUIElement = CreateIntegerPreferenceField("Seed", PlayLauncher.SEED_KEY);
            root.Add(seedUIElement);

            recordUIElement = CreateTogglePreferenceField("Record", PlayLauncher.RECORD_KEY);
            root.Add(recordUIElement);
        }
    }
}
