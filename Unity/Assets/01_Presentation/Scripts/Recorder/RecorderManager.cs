using Game.Core;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Game.Presentation
{
    public class RecorderManager
    {
        private PresentationManager presentationManager;
        private RecordSession currentSession;

        public RecorderManager(PresentationManager presentationManager)
        {
            this.presentationManager = presentationManager;
            presentationManager.GameManager.OnGameStarted += OnGameStarted;
            presentationManager.GameManager.OnGameFinished += OnGameFinished;
        }

        private void OnGameStarted(Core.GameModeParameter gameModeParameter)
        {
            currentSession = new RecordSession(gameModeParameter);
            presentationManager.GameManager.InputManager.OnInputAction += OnInputAction;
        }

        private void OnInputAction(InputAction inputAction)
        {
            currentSession.Register(inputAction);
        }

        private void OnGameFinished()
        {
            presentationManager.GameManager.InputManager.OnInputAction -= OnInputAction;
            RecordSessionSave recordSessionSave = new RecordSessionSave();
            recordSessionSave.GameModeParameter = JsonConvert.SerializeObject(currentSession.GameModeParameter);
            recordSessionSave.Inputs = currentSession.InputActions.Select(x => new RecordSessionSave.InputActionSave() { InputType = x.InputType, Tick = x.Tick, Value = x.Value }).ToList();
            presentationManager.SaveManager.Sessions.Add(recordSessionSave, $"{DateTime.Now.Ticks}");
            presentationManager.SaveManager.Sessions.Flush();
        }
    }
}
