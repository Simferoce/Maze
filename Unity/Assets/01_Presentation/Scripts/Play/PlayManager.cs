using Game.Core;
using System;
using System.Linq;
using UnityEngine;

namespace Game.Presentation
{
    public class PlayManager : MonoBehaviour
    {
        [SerializeField] private PresentationRegistry presentationRegistry;

        private UnityLogger unityLogger;
        private EntityVisualHandler entityVisualHandler;
        private GameManager gameManager;
        private RecorderManager recorderManager;
        private PlatformManager platformManager;
        private SaveManager saveManager;
        private PresentationInputManager presentationInputManager;

        private void Awake()
        {
            unityLogger = new UnityLogger();
            gameManager = new Core.GameManager(unityLogger);
            entityVisualHandler = new EntityVisualHandler(presentationRegistry, gameManager);
            presentationInputManager = new PresentationInputManager(gameManager);
            platformManager = new PlatformManager();
            saveManager = new SaveManager(platformManager);
            recorderManager = new RecorderManager(saveManager, gameManager);

            gameManager.Initialize(presentationRegistry.Definitions.Select(x => x.Convert()).ToList());
            gameManager.Start(new Game.Core.GameModeParameter() { PlayerEntityDefinition = new Guid(presentationRegistry.PlayerDefinition.Id) });
        }

        private void Update()
        {
            presentationInputManager.Update();
        }

        private void FixedUpdate()
        {
            presentationInputManager.Flush();
            gameManager.Update();
            entityVisualHandler.Update();
        }

        private void OnApplicationQuit()
        {
            gameManager.Finish();
        }
    }
}