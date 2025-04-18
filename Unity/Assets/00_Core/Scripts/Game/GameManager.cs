using System.Collections.Generic;

namespace Game.Core
{
    public class GameManager
    {
        public InputManager InputManager { get; private set; }
        public TimeManager TimeManager { get; private set; }
        public ILogger Logger { get; private set; }
        public Registry Registry { get; private set; }
        public WorldManager WorldManager { get; private set; }
        public Recorder Recorder { get; private set; }

        private bool isInitialized = false;
        private bool isStarted = false;

        public GameManager(ILogger logger)
        {
            Logger = logger;

            Registry = new Registry(this);
            InputManager = new InputManager(this);
            TimeManager = new TimeManager(this);
            WorldManager = new WorldManager(this);
            Recorder = new Recorder(this);
        }

        public void Initialize(List<Definition> definitions)
        {
            isInitialized = true;
            Registry.Initialize(definitions);
        }

        public void Start(GameModeParameter gameModeParameter)
        {
            Assertion.IsTrue(isInitialized, "The game has not been initialized yet.");
            isStarted = true;

            EntityDefinition entityDefinition = Registry.Get<EntityDefinition>(gameModeParameter.PlayerEntityDefinition);
            Entity playerAvatar = new Entity(this, entityDefinition);
            Player player = new Player(this);
            player.Assign(playerAvatar);

            Recorder.StartSession(gameModeParameter);
        }

        public void Update()
        {
            Assertion.IsTrue(isInitialized, "The game has not been initialized yet.");
            Assertion.IsTrue(isStarted, "The game has not been started yet.");

            TimeManager.Update();
            WorldManager.Update();
        }
    }
}