using Game.SceneLauncher;
using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Game.Presentation
{
    public class PlayLauncher : Launcher
    {
        public const string PLAYER_CHARACTER_DEFINITION_KEY = "SceneLauncher_PlayerCharacterDefinition";
        public const string RECORD_KEY = "SceneLauncher_Record";

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
            playManager.Play(new Guid(GetObject<CharacterPresentationDefinition>(PLAYER_CHARACTER_DEFINITION_KEY).Id), GetBool(RECORD_KEY));
        }

        public override void Load()
        {
            SetObject<CharacterPresentationDefinition>(PLAYER_CHARACTER_DEFINITION_KEY, LoadObject<CharacterPresentationDefinition>(PLAYER_CHARACTER_DEFINITION_KEY));
            SetBool(RECORD_KEY, LoadBool(RECORD_KEY));
        }
    }
}
