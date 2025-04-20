using System;

namespace Game.SceneLauncher
{
    public class LauncherDrawerAttribute : Attribute
    {
        public Type Type { get; private set; }

        public LauncherDrawerAttribute(Type type)
        {
            this.Type = type;
        }
    }
}
