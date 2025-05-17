using Game.Core;
using UnityEngine;

namespace Game.Presentation
{
    public class ReplayManager : MonoBehaviour
    {
        [SerializeField] private PresentationRegistry presentationRegistry;
        [SerializeField] private PresentationConstant presentationConstant;

        private ServiceRegistry serviceRegistry;
        private GameManager gameManager;
        private EntityVisualHandler entityVisualHandler;
        private PlatformManager platformManager;
        private IRecordSessionRepository recordSessionRepository;
        private UnityLogger logger;
        private int commandIndex = 0;
        private RecordSessionHeader currentSessionHeader;
        private RecordSessionBody currentSessionBody;
        private bool synchronize = false;

        public void Awake()
        {
            logger = new UnityLogger();
            gameManager = new GameManager(logger);

            serviceRegistry = new ServiceRegistry();
            platformManager = new PlatformManager();
            recordSessionRepository = new RecordSessionRepositoryWeb();
            entityVisualHandler = new EntityVisualHandler();

            serviceRegistry.Register(platformManager);
            serviceRegistry.Register(recordSessionRepository);
            serviceRegistry.Register(presentationConstant);

            Registry registry = presentationRegistry.GenerateGameRegistry();
            gameManager.Initialize(registry);
            entityVisualHandler.Initialize(serviceRegistry);
        }

        public async void Play(long id)
        {
            currentSessionHeader = await recordSessionRepository.GetRecordSessionHeaderAsync(id);
            currentSessionBody = await recordSessionRepository.GetRecordSessionBodyAsync(id);
            gameManager.Start(currentSessionHeader.GameModeParameter);
        }

        private void Update()
        {
            if (synchronize)
            {
                entityVisualHandler.Synchronize();

                synchronize = false;
            }
        }

        public void FixedUpdate()
        {
            if (!gameManager.IsStarted)
                return;

            while (currentSessionBody.Commands.Count > commandIndex && currentSessionBody.Commands[commandIndex].Tick == gameManager.TimeManager.CurrentTick)
            {
                gameManager.CommandManager.Execute(currentSessionBody.Commands[commandIndex]);

                commandIndex++;
            }

            gameManager.Update();
            synchronize = true;
        }
    }
}
