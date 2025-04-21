using System.Collections.Generic;

namespace Game.Core
{
    public class GameManager
    {
        public delegate void OnGameStartedDelegate(GameModeParameter gameModeParameter);
        public delegate void OnGameFinishedDelegate();
        public event OnGameStartedDelegate OnGameStarted;
        public event OnGameFinishedDelegate OnGameFinished;

        public CommandManager CommandManager { get; private set; }
        public TimeManager TimeManager { get; private set; }
        public ILogger Logger { get; private set; }
        public Registry Registry { get; private set; }
        public WorldManager WorldManager { get; private set; }
        public bool IsStarted { get; private set; } = false;
        public bool IsInitialized { get; private set; } = false;

        public GameManager(ILogger logger)
        {
            Logger = logger;

            Registry = new Registry(this);
            CommandManager = new CommandManager(this);
            TimeManager = new TimeManager(this);
            WorldManager = new WorldManager(this);
        }

        public void Initialize(List<Definition> definitions)
        {
            IsInitialized = true;
            Registry.Initialize(definitions);
        }

        public void Start(GameModeParameter gameModeParameter)
        {
            Assertion.IsTrue(IsInitialized, "The game has not been initialized yet.");
            IsStarted = true;

            EntityDefinition entityDefinition = Registry.Get<EntityDefinition>(gameModeParameter.PlayerEntityDefinition);
            Entity playerAvatar = new Entity(this, entityDefinition);
            Player player = new Player(this);
            player.Assign(playerAvatar);

            OnGameStarted?.Invoke(gameModeParameter);
        }

        public void Finish()
        {
            OnGameFinished?.Invoke();
        }

        public void Update()
        {
            Assertion.IsTrue(IsInitialized, "The game has not been initialized yet.");
            Assertion.IsTrue(IsStarted, "The game has not been started yet.");

            TimeManager.Update();
            WorldManager.Update();
        }
    }
}