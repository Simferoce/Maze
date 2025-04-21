namespace Game.Core
{
    public class Player : Agent
    {
        public Entity Avatar { get; private set; }

        private Vector2 direction = Vector2.Zero;

        public Player(GameManager gameManager) : base(gameManager)
        {
            gameManager.CommandManager.OnCommandReceived += OnCommandReceived;
        }

        public override void Dispose()
        {
            base.Dispose();
            gameManager.CommandManager.OnCommandReceived -= OnCommandReceived;
        }

        public void Assign(Entity playerEntity)
        {
            Avatar = playerEntity;
        }

        private void OnCommandReceived(Command command)
        {
            if (command.CommandType == ComandType.HorizontalAxis)
            {
                direction = new Vector2(command.Value, direction.y);
            }
            else if (command.CommandType == ComandType.VerticalAxis)
            {
                direction = new Vector2(direction.x, command.Value);
            }
        }

        public override void Update()
        {
            Assertion.IsNotNull(Avatar, "Could not update the player because there is no avatar assigned yet.");

            Vector2 displacement = direction * Avatar.AttributeHandler.Get(AttributeType.MovementSpeed).Value;

            Avatar.Move(displacement);
        }
    }
}