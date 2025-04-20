using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Game.SceneLauncher
{
    public static class SceneLauncherUtility
    {
        public static List<Type> GetSceneLauncherTypes()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => !x.IsAbstract && typeof(Launcher).IsAssignableFrom(x))
                .ToList();
        }

        public static Dictionary<Type, Type> GetSceneLauncherDrawer()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => !x.IsAbstract && typeof(LauncherDrawer).IsAssignableFrom(x) && x.GetCustomAttribute<LauncherDrawerAttribute>() != null)
                .ToDictionary(x => x.GetCustomAttribute<LauncherDrawerAttribute>().Type, x => x);
        }
    }
}
