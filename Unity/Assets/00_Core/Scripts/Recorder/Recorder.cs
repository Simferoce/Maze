namespace Game.Core
{
    public class Recorder
    {
        public RecordSession CurrentSession { get; private set; }

        private GameManager gameManager;

        public Recorder(GameManager gameManager)
        {
            this.gameManager = gameManager;

            Assertion.IsNotNull(gameManager.InputManager, $"Attempting to initialize recorder before input manager was created.");
            gameManager.InputManager.OnInputAction += OnInputAction;
        }

        public void StartSession(GameModeParameter gameModeParameter)
        {
            CurrentSession = new RecordSession(gameModeParameter);
        }

        private void OnInputAction(InputAction inputAction)
        {
            Assertion.IsNotNull(CurrentSession, $"Received input before the session was started.");

            CurrentSession.Register(inputAction);
        }
    }
}
