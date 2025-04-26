using Game.Core;
using System.Linq;
using UnityEngine;

namespace Game.Presentation
{
    public class ReplayManager : MonoBehaviour
    {
        [SerializeField] private PresentationRegistry presentationRegistry;

        private GameManager gameManager;
        private EntityVisualHandler entityVisualHandler;
        private PlatformManager platformManager;
        private IRecordSessionRepository recordSessionRepository;
        private UnityLogger logger;
        private int commandIndex = 0;
        private RecordSessionHeader currentSessionHeader;
        private RecordSessionBody currentSessionBody;

        public void Awake()
        {
            logger = new UnityLogger();
            gameManager = new GameManager(logger);
            platformManager = new PlatformManager();
            recordSessionRepository = new RecordSessionRepositoryWeb();
            entityVisualHandler = new EntityVisualHandler(presentationRegistry, gameManager);

            gameManager.Initialize(presentationRegistry.Definitions.Select(x => x.Convert()).ToList());
        }

        public async void Play(long id)
        {
            currentSessionHeader = await recordSessionRepository.GetRecordSessionHeaderAsync(id);
            currentSessionBody = await recordSessionRepository.GetRecordSessionBodyAsync(id);
            gameManager.Start(currentSessionHeader.GameModeParameter);
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
            entityVisualHandler.Synchronize();
        }
    }
}
