using Game.SceneLauncher;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Game.Presentation
{
    [Launcher(1)]
    public class ReplayLauncher : Launcher
    {
        public const string SESSION_NAME_KEY = "SceneLauncher_SessionName";

        public override string GetDescription()
        {
            return $"Replay - {GetDisplayName(GetString(SESSION_NAME_KEY))}";
        }

        public override void Initialize()
        {
            base.Initialize();

            string[] guids = AssetDatabase.FindAssets("t:scene 00-Replay");
            string assetPath = AssetDatabase.GUIDToAssetPath(guids[0]);

            EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(assetPath);
        }

        public override void Launch()
        {
            ReplayManager replayManager = GameObject.FindFirstObjectByType<ReplayManager>();
            replayManager.Play(long.Parse(GetString(SESSION_NAME_KEY)));
        }

        public override void Load()
        {
            SetString(SESSION_NAME_KEY, LoadString(SESSION_NAME_KEY));
        }

        public static string GetDisplayName(string value)
        {
            return string.IsNullOrEmpty(value) ? "Undefined" : value;
        }
    }
}
