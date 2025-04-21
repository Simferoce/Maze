using Game.Core;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Game.Presentation
{
    public class RecorderManager
    {
        private GameManager gameManager;
        private SaveManager saveManager;
        private RecordSession currentSession;

        public RecorderManager(SaveManager saveManager, GameManager gameManager)
        {
            this.saveManager = saveManager;
            this.gameManager = gameManager;
            gameManager.OnGameStarted += OnGameStarted;
            gameManager.OnGameFinished += OnGameFinished;
        }

        private void OnGameStarted(Core.GameModeParameter gameModeParameter)
        {
            currentSession = new RecordSession(gameModeParameter);
            gameManager.CommandManager.OnCommandReceived += OnCommandReceived;
        }

        private void OnCommandReceived(Command command)
        {
            currentSession.Register(command);
        }

        private void OnGameFinished()
        {
            gameManager.CommandManager.OnCommandReceived -= OnCommandReceived;
            RecordSessionSave recordSessionSave = new RecordSessionSave();
            recordSessionSave.GameModeParameter = JsonConvert.SerializeObject(currentSession.GameModeParameter);
            recordSessionSave.Commands = currentSession.Commands.Select(x => new RecordSessionSave.CommandSave() { CommandType = x.CommandType, Tick = x.Tick, Value = x.Value }).ToList();
            saveManager.Sessions.Add(recordSessionSave, $"{DateTime.Now.Ticks}");
            saveManager.Sessions.Flush();
        }
    }
}
