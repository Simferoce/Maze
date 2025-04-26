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
        private IRecordSessionRepository recordSessionRepository;
        private InputManager inputManager;

        private void Awake()
        {
            unityLogger = new UnityLogger();
            gameManager = new Core.GameManager(unityLogger);
            entityVisualHandler = new EntityVisualHandler(presentationRegistry, gameManager);
            inputManager = new InputManager(gameManager);
            platformManager = new PlatformManager();
            recordSessionRepository = new RecordSessionRepositoryWeb();
            recorderManager = new RecorderManager(recordSessionRepository, gameManager);

            gameManager.Initialize(presentationRegistry.Definitions.Select(x => x.Convert()).ToList());
        }

        public void Play(Guid playerEntityDefinitionId)
        {
            gameManager.Start(new Game.Core.GameModeParameter() { PlayerEntityDefinition = playerEntityDefinitionId });
        }

        private void Update()
        {
            if (!gameManager.IsStarted)
                return;

            inputManager.Update();
        }

        private void FixedUpdate()
        {
            if (!gameManager.IsStarted)
                return;

            inputManager.Flush();
            gameManager.Update();
            entityVisualHandler.Synchronize();
        }

        private void OnApplicationQuit()
        {
            if (!gameManager.IsStarted)
                return;

            gameManager.Finish();
        }
    }
}