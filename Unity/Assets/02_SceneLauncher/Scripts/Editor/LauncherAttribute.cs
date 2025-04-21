using System;

namespace Game.SceneLauncher
{
    public class LauncherAttribute : Attribute
    {
        public int Order { get; private set; }

        public LauncherAttribute(int order)
        {
            this.Order = order;
        }
    }
}
