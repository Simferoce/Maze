using System;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Game.SceneLauncher
{
    public class BoostrapReadyNotification : AssetPostprocessor
    {
        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths, bool didDomainReload)
        {
            Bootstrap.Initialize();
        }
    }

    [InitializeOnLoad]
    public class Bootstrap
    {
        public static Launcher CurrentLauncher { get; private set; }
        public static event System.Action OnModified;

        private static bool HasBeenInitialized = false;

        public static void Initialize()
        {
            if (HasBeenInitialized)
                return;

            HasBeenInitialized = true;

            string[] guids = AssetDatabase.FindAssets("t:scene 00-Empty");
            string assetPath = AssetDatabase.GUIDToAssetPath(guids[0]);

            EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(assetPath);
            EditorApplication.playModeStateChanged += EditorApplication_playModeStateChanged;

            Type launcherType = EditorPrefs.HasKey("SceneLancher_LauncherType") ? Type.GetType(EditorPrefs.GetString("SceneLancher_LauncherType")) : typeof(NoneLauncher);
            if (launcherType == null)
                launcherType = typeof(NoneLauncher);

            ChangeLauncher(launcherType);
        }


        private static void EditorApplication_playModeStateChanged(PlayModeStateChange obj)
        {
            if (obj == PlayModeStateChange.EnteredPlayMode)
            {
                CurrentLauncher.Launch();
            }
        }

        public static void ChangeLauncher(Type type)
        {
            if (CurrentLauncher != null)
                CurrentLauncher.OnModified -= CurrentLauncher_OnModified;

            CurrentLauncher = (Launcher)Activator.CreateInstance(type);
            CurrentLauncher.OnModified += CurrentLauncher_OnModified;

            CurrentLauncher.Load();
            EditorPrefs.SetString("SceneLancher_LauncherType", type.FullName);
            OnModified?.Invoke();
        }

        private static void CurrentLauncher_OnModified()
        {
            OnModified?.Invoke();
        }
    }
}