using Game.SceneLauncher;
using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Game.Presentation
{
    public class PlayLauncher : Launcher
    {
        public const string PLAYER_ENTITY_DEFINITION_KEY = "SceneLauncher_PlayerEntityDefinition";

        public override string GetDescription()
        {
            return "Play";
        }

        public override void Initialize()
        {
            base.Initialize();

            string[] guids = AssetDatabase.FindAssets("t:scene 00-Game");
            string assetPath = AssetDatabase.GUIDToAssetPath(guids[0]);

            EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(assetPath);
        }

        public override void Launch()
        {
            PlayManager playManager = GameObject.FindFirstObjectByType<PlayManager>();
            playManager.Play(new Guid(GetObject<EntityPresentationDefinition>(PLAYER_ENTITY_DEFINITION_KEY).Id));
        }

        public override void Load()
        {
            SetObject<EntityPresentationDefinition>(PLAYER_ENTITY_DEFINITION_KEY, LoadObject<EntityPresentationDefinition>(PLAYER_ENTITY_DEFINITION_KEY));
        }
    }
}
