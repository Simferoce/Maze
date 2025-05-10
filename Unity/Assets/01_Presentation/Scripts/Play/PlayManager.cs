using Game.Core;
using System;
using UnityEngine;

namespace Game.Presentation
{
    public class PlayManager : MonoBehaviour
    {
        [SerializeField] private PresentationRegistry presentationRegistry;
        [SerializeField] private PlayCamera playCamera;

        private UnityLogger unityLogger;
        private EntityVisualHandler entityVisualHandler;
        private GameManager gameManager;
        private RecorderManager recorderManager;
        private PlatformManager platformManager;
        private IRecordSessionRepository recordSessionRepository;
        private InputManager inputManager;
        private bool synchronize = false;

        private void Awake()
        {
            unityLogger = new UnityLogger();
            gameManager = new Core.GameManager(unityLogger);
            entityVisualHandler = new EntityVisualHandler(presentationRegistry, gameManager);
            inputManager = new InputManager(gameManager);
            platformManager = new PlatformManager();
            recordSessionRepository = new RecordSessionRepositoryWeb();
            recorderManager = new RecorderManager(recordSessionRepository, gameManager);

            Registry registry = presentationRegistry.GenerateGameRegistry();
            gameManager.Initialize(registry);
        }

        public void Play(Guid worldDefinition, Guid playerEntityDefinitionId, Guid playerDefinitionId, int seed, bool record)
        {
            if (record)
                recorderManager.Start();

            gameManager.Start(new Game.Core.GameModeParameter() { WorldDefinition = worldDefinition, PlayerCharacterDefinition = playerEntityDefinitionId, PlayerDefinition = playerDefinitionId, Seed = seed });
            entityVisualHandler.Refresh(gameManager);
            playCamera.Refresh(gameManager, entityVisualHandler);
        }

        private void Update()
        {
            if (synchronize)
            {
                entityVisualHandler.Synchronize();
                playCamera.Synchronize();

                synchronize = false;
            }

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
            synchronize = true;
        }

        private void OnApplicationQuit()
        {
            if (!gameManager.IsStarted)
                return;

            gameManager.Finish();
        }
    }
}