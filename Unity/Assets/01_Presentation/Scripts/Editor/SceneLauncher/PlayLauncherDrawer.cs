using Game.SceneLauncher;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Game.Presentation
{
    [LauncherDrawer(typeof(PlayLauncher))]
    public class PlayLauncherDrawer : LauncherDrawer<PlayLauncher>
    {
        private ObjectField playerEntityDefinition;

        public PlayLauncherDrawer(PlayLauncher launcher) : base(launcher)
        {
        }

        public override void Initialize(VisualElement root)
        {
            playerEntityDefinition = CreatePreferenceObjectField<EntityPresentationDefinition>("Player Entity Definition", PlayLauncher.PLAYER_ENTITY_DEFINITION_KEY);
            root.Add(playerEntityDefinition);
        }
    }
}
