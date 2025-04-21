namespace Game.SceneLauncher
{
    [Launcher(-1)]
    public class NoneLauncher : Launcher
    {
        public override void Initialize()
        {
        }

        public override string GetDescription()
        {
            return "None";
        }

        public override void Launch()
        {
        }

        public override void Load()
        {
        }
    }
}
