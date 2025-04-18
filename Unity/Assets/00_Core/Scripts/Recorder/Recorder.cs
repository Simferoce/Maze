namespace Game.Core
{
    public class Recorder
    {
        private GameManager gameManager;

        private RecordSession currentSession;

        public Recorder(GameManager gameManager)
        {
            this.gameManager = gameManager;

            Assertion.IsNotNull(gameManager.InputManager, $"Attempting to initialize recorder before input manager was created.");
            gameManager.InputManager.OnInputAction += OnInputAction;
        }

        public void StartSession(GameModeParameter gameModeParameter)
        {
            currentSession = new RecordSession(gameModeParameter);
        }

        private void OnInputAction(InputAction inputAction)
        {
            Assertion.IsNotNull(currentSession, $"Received input before the session was started.");

            currentSession.Register(inputAction);
        }
    }
}
