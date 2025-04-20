using UnityEngine.UIElements;

namespace Game.SceneLauncher
{
    [LauncherDrawer(typeof(NoneLauncher))]
    public class NoneLauncherDrawer : LauncherDrawer<NoneLauncher>
    {
        public NoneLauncherDrawer(NoneLauncher launcher) : base(launcher)
        {
        }

        public override void Initialize(VisualElement root)
        {
        }
    }
}
