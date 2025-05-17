using Game.Core;
using System;

namespace Game.Presentation
{
    public class RecorderManager : IService
    {
        private RecordSessionHeader currentSessionHeader;
        private RecordSessionBody currentSessionBody;
        private ServiceRegistry serviceRegistry;

        public RecorderManager(ServiceRegistry serviceRegistry)
        {
            this.serviceRegistry = serviceRegistry;
        }

        public void Start()
        {
            serviceRegistry.Get<GameProvider>().GameManager.OnGameStarted += OnGameStarted;
            serviceRegistry.Get<GameProvider>().GameManager.OnGameFinished += OnGameFinished;
        }

        private void OnGameStarted(Core.GameModeParameter gameModeParameter)
        {
            currentSessionHeader = new RecordSessionHeader() { GameModeParameter = gameModeParameter };
            currentSessionBody = new RecordSessionBody();
            serviceRegistry.Get<GameProvider>().GameManager.CommandManager.OnCommandReceived += OnCommandReceived;
        }

        private void OnCommandReceived(Command command)
        {
            currentSessionBody.Register(command);
        }

        private async void OnGameFinished()
        {
            serviceRegistry.Get<GameProvider>().GameManager.CommandManager.OnCommandReceived -= OnCommandReceived;
            currentSessionHeader.Date = DateTime.Now.Ticks;
            currentSessionHeader.Name = currentSessionHeader.Date.ToString();
            await serviceRegistry.Get<IRecordSessionRepository>().AddRecordSessionHeaderAsync(currentSessionHeader, currentSessionBody);
        }
    }
}
