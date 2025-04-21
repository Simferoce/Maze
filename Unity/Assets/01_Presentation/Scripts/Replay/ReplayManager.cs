using Game.Core;
using Newtonsoft.Json;
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
        private SaveManager saveManager;
        private UnityLogger logger;
        private int commandIndex = 0;
        private RecordSession currentSession;

        public void Awake()
        {
            logger = new UnityLogger();
            gameManager = new GameManager(logger);
            platformManager = new PlatformManager();
            saveManager = new SaveManager(platformManager);
            entityVisualHandler = new EntityVisualHandler(presentationRegistry, gameManager);

            saveManager.Load();
            gameManager.Initialize(presentationRegistry.Definitions.Select(x => x.Convert()).ToList());
        }

        public void Play(string recordSessionName)
        {
            RecordSessionSave recordSessionSave = saveManager.Sessions.GetEntry(recordSessionName);
            GameModeParameter gameModeParameter = JsonConvert.DeserializeObject<GameModeParameter>(recordSessionSave.GameModeParameter);
            currentSession = new RecordSession(gameModeParameter);
            currentSession.Commands = recordSessionSave.Commands.Select(x => new Command() { CommandType = x.CommandType, Tick = x.Tick, Value = x.Value }).ToList();

            gameManager.Start(gameModeParameter);
        }

        public void FixedUpdate()
        {
            if (!gameManager.IsStarted)
                return;

            while (currentSession.Commands.Count > commandIndex && currentSession.Commands[commandIndex].Tick == gameManager.TimeManager.CurrentTick)
            {
                gameManager.CommandManager.Execute(currentSession.Commands[commandIndex]);

                commandIndex++;
            }

            gameManager.Update();
            entityVisualHandler.Synchronize();
        }
    }
}
