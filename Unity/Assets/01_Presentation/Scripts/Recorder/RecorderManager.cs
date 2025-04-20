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
            gameManager.InputManager.OnInputAction += OnInputAction;
        }

        private void OnInputAction(InputAction inputAction)
        {
            currentSession.Register(inputAction);
        }

        private void OnGameFinished()
        {
            gameManager.InputManager.OnInputAction -= OnInputAction;
            RecordSessionSave recordSessionSave = new RecordSessionSave();
            recordSessionSave.GameModeParameter = JsonConvert.SerializeObject(currentSession.GameModeParameter);
            recordSessionSave.Inputs = currentSession.InputActions.Select(x => new RecordSessionSave.InputActionSave() { InputType = x.InputType, Tick = x.Tick, Value = x.Value }).ToList();
            saveManager.Sessions.Add(recordSessionSave, $"{DateTime.Now.Ticks}");
            saveManager.Sessions.Flush();
        }
    }
}
