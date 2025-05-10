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
        public WorldManager WorldManager { get; set; }
        public bool IsStarted { get; private set; } = false;
        public bool IsInitialized { get; private set; } = false;

        public GameManager(ILogger logger)
        {
            Logger = logger;

            WorldManager = new WorldManager(this);
            CommandManager = new CommandManager(this);
            TimeManager = new TimeManager(this);
        }

        public void Initialize(Registry registry)
        {
            IsInitialized = true;
            Registry = registry;
        }

        public void Start(GameModeParameter gameModeParameter)
        {
            Assertion.IsTrue(IsInitialized, "The game has not been initialized yet.");
            IsStarted = true;

            WorldDefinition worldDefinition = Registry.Get<WorldDefinition>(gameModeParameter.WorldDefinition);
            Assertion.IsNotNull(worldDefinition, $"Could not find the {nameof(WorldDefinition)} with the id \"{gameModeParameter.WorldDefinition}\".");

            World world = worldDefinition.Instantiate(this) as World;
            world.Generate(gameModeParameter.Seed);

            CharacterDefinition characterDefinition = Registry.Get<CharacterDefinition>(gameModeParameter.PlayerCharacterDefinition);
            Assertion.IsNotNull(worldDefinition, $"Could not find the {nameof(CharacterDefinition)} with the id \"{gameModeParameter.PlayerCharacterDefinition}\".");

            Character playerAvatar = characterDefinition.Instantiate(this) as Character;
            Player player = new Player(this);
            player.Assign(playerAvatar);
            playerAvatar.SetPosition(world.SpawnPoint);

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