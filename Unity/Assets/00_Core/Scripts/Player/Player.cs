namespace Game.Core
{
    public class Player : Agent
    {
        public Character Avatar { get; private set; }
        public new PlayerDefinition Definition { get; private set; }

        private Vector2 direction = Vector2.Zero;
        private Vector2 lookAt;

        public Player(GameManager gameManager, PlayerDefinition definition) : base(gameManager, definition)
        {
            Definition = definition;
            GameManager.CommandManager.OnCommandReceived += OnCommandReceived;
        }

        public override void Dispose()
        {
            base.Dispose();
            GameManager.CommandManager.OnCommandReceived -= OnCommandReceived;
        }

        public void Assign(Character playerEntity)
        {
            Avatar = playerEntity;
            lookAt = playerEntity.LocalPosition + Vector2.Up;
        }

        private void OnCommandReceived(Command command)
        {
            if (command.CommandType == ComandType.HorizontalAxis)
            {
                direction = new Vector2(command.Value, direction.Y);
            }
            else if (command.CommandType == ComandType.VerticalAxis)
            {
                direction = new Vector2(direction.X, command.Value);
            }
            else if (command.CommandType == ComandType.LookAtPointX)
            {
                lookAt = new Vector2(command.Value, lookAt.Y);
            }
            else if (command.CommandType == ComandType.LookAtPointY)
            {
                lookAt = new Vector2(lookAt.X, command.Value);
            }
            else if (command.CommandType == ComandType.UseAbility)
            {
                Avatar.UseAbility(command.Value.ToInt());
            }
        }

        public override void Update()
        {
            Assertion.IsNotNull(Avatar, "Could not update the player because there is no avatar assigned yet.");

            Vector2 displacement = direction.Normalized * Avatar.AttributeHandler.Get(AttributeType.MovementSpeed).Value;

            Avatar.LookIn(lookAt);
            Avatar.Move(displacement);
        }
    }
}