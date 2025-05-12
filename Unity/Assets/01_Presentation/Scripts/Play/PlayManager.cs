using Game.Core;
using System;
using UnityEngine;

namespace Game.Presentation
{
    public class PlayManager : MonoBehaviour
    {
        [SerializeField] private PresentationRegistry presentationRegistry;
        [SerializeField] private EntityVisualHandler entityVisualHandler;
        [SerializeField] private PlayCamera playCamera;
        [SerializeField] private InputManager inputManager;

        private UnityLogger unityLogger;
        private GameManager gameManager;
        private RecorderManager recorderManager;
        private PlatformManager platformManager;
        private IRecordSessionRepository recordSessionRepository;
        private bool synchronize = false;

        private void Awake()
        {
            unityLogger = new UnityLogger();
            gameManager = new Core.GameManager(unityLogger);
            platformManager = new PlatformManager();
            recordSessionRepository = new RecordSessionRepositoryWeb();
            recorderManager = new RecorderManager(recordSessionRepository, gameManager);

            Registry registry = presentationRegistry.GenerateGameRegistry();
            gameManager.Initialize(registry);
            inputManager.Initialize(gameManager);
        }

        public void Play(Guid worldDefinition, Guid playerEntityDefinitionId, Guid playerDefinitionId, int seed, bool record)
        {
            if (record)
                recorderManager.Start();

            gameManager.Start(new Game.Core.GameModeParameter() { WorldDefinition = worldDefinition, PlayerCharacterDefinition = playerEntityDefinitionId, PlayerDefinition = playerDefinitionId, Seed = seed });
            entityVisualHandler.Refresh(presentationRegistry, gameManager);
            playCamera.Refresh(gameManager);
        }

        private void Update()
        {
            if (synchronize)
            {
                entityVisualHandler.Synchronize();

                synchronize = false;
            }

            if (!gameManager.IsStarted)
                return;
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