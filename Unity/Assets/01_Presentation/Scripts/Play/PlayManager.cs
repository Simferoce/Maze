using Game.Core;
using System;
using UnityEngine;

namespace Game.Presentation
{
    public class PlayManager : MonoBehaviour
    {
        [SerializeField] private PresentationConstant presentationConstant;
        [SerializeField] private PresentationRegistry presentationRegistry;
        [SerializeField] private EntityVisualHandler entityVisualHandler;
        [SerializeField] private PlayCamera playCamera;
        [SerializeField] private InputManager inputManager;

        private UnityLogger unityLogger;
        private GameManager gameManager;
        private GameProvider gameProvider;
        private RecorderManager recorderManager;
        private PlatformManager platformManager;
        private ServiceRegistry serviceRegistry;
        private IRecordSessionRepository recordSessionRepository;
        private bool synchronize = false;

        private void Awake()
        {
            serviceRegistry = new ServiceRegistry();
            unityLogger = new UnityLogger();
            gameManager = new Core.GameManager(unityLogger);

            gameProvider = new GameProvider(gameManager);
            platformManager = new PlatformManager();
            recordSessionRepository = new RecordSessionRepositoryWeb();
            recorderManager = new RecorderManager(serviceRegistry);

            serviceRegistry.Register(presentationRegistry);
            serviceRegistry.Register(presentationConstant);
            serviceRegistry.Register(recorderManager);
            serviceRegistry.Register(platformManager);
            serviceRegistry.Register(recordSessionRepository);
            serviceRegistry.Register(gameProvider);
            serviceRegistry.Register(playCamera);
            serviceRegistry.Register(entityVisualHandler);

            Registry registry = presentationRegistry.GenerateGameRegistry();
            gameManager.Initialize(registry);

            playCamera.Initialize(serviceRegistry);
            inputManager.Initialize(serviceRegistry);
            entityVisualHandler.Initialize(serviceRegistry);
        }

        public void Play(Guid worldDefinition, Guid playerEntityDefinitionId, Guid playerDefinitionId, int seed, bool record)
        {
            if (record)
                recorderManager.Start();

            gameManager.Start(new Game.Core.GameModeParameter() { WorldDefinition = worldDefinition, PlayerCharacterDefinition = playerEntityDefinitionId, PlayerDefinition = playerDefinitionId, Seed = seed });
        }

        private void Update()
        {
            if (synchronize)
            {
                entityVisualHandler.Synchronize();

                synchronize = false;
            }
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