using Game.Core;
using System;

namespace Game.Presentation
{
    public class RecorderManager
    {
        private GameManager gameManager;
        private RecordSessionRepository recordSessionRepository;
        private RecordSessionHeader currentSessionHeader;
        private RecordSessionBody currentSessionBody;

        public RecorderManager(RecordSessionRepository recordSessionRepository, GameManager gameManager)
        {
            this.recordSessionRepository = recordSessionRepository;
            this.gameManager = gameManager;
            gameManager.OnGameStarted += OnGameStarted;
            gameManager.OnGameFinished += OnGameFinished;
        }

        private void OnGameStarted(Core.GameModeParameter gameModeParameter)
        {
            currentSessionHeader = new RecordSessionHeader() { GameModeParameter = gameModeParameter };
            currentSessionBody = new RecordSessionBody();
            gameManager.CommandManager.OnCommandReceived += OnCommandReceived;
        }

        private void OnCommandReceived(Command command)
        {
            currentSessionBody.Register(command);
        }

        private void OnGameFinished()
        {
            gameManager.CommandManager.OnCommandReceived -= OnCommandReceived;
            currentSessionHeader.Date = DateTime.Now.Ticks;
            currentSessionHeader.Name = currentSessionHeader.Date.ToString();
            recordSessionRepository.Add(currentSessionHeader, currentSessionBody);
        }
    }
}
