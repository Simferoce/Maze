using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.SceneLauncher
{
    public class SceneLauncherEditorWindow : EditorWindow
    {
        public static List<Type> Options => options ??= SceneLauncherUtility.GetSceneLauncherTypes();
        public static Dictionary<Type, Type> LauncherDrawers => launcherDrawers ??= SceneLauncherUtility.GetSceneLauncherDrawer();

        private static List<Type> options = null;
        private static Dictionary<Type, Type> launcherDrawers = null;

        private LauncherDrawer drawer = null;
        private DropdownField launcherChoiceField;
        private VisualElement launcherRoot;

        [MenuItem("Tools/SceneLauncher")]
        public static void ShowSceneLauncher()
        {
            SceneLauncherEditorWindow wnd = GetWindow<SceneLauncherEditorWindow>();
            wnd.titleContent = new GUIContent("SceneLauncher");
        }

        public void CreateGUI()
        {
            launcherChoiceField = new DropdownField(Options.Select(x => x.ToString()).ToList(), Options.IndexOf(Bootstrap.CurrentLauncher.GetType()));
            launcherChoiceField.RegisterValueChangedCallback(OnLauncherOptionChange);
            rootVisualElement.Add(launcherChoiceField);

            launcherRoot = new VisualElement();
            rootVisualElement.Add(launcherRoot);

            RefreshLauncher();

            void OnLauncherOptionChange(ChangeEvent<string> evt)
            {
                Type launcherType = Options[launcherChoiceField.index];
                Bootstrap.ChangeLauncher(launcherType);
                RefreshLauncher();
            }
        }

        private void RefreshLauncher()
        {
            if (drawer == null || Bootstrap.CurrentLauncher.GetType() != drawer.GetLauncherType())
            {
                launcherRoot.Clear();

                LauncherDrawer drawer = (LauncherDrawer)Activator.CreateInstance(LauncherDrawers[Bootstrap.CurrentLauncher.GetType()], new object[] { Bootstrap.CurrentLauncher });
                this.drawer = drawer;

                drawer.Initialize(launcherRoot);
            }
        }
    }
}
