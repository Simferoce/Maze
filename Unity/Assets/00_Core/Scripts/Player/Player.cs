namespace Game.Core
{
    public class Player : Agent
    {
        public Entity Avatar { get; private set; }

        private Vector2 direction = Vector2.Zero;

        public Player(GameManager gameManager) : base(gameManager)
        {
            gameManager.InputManager.OnInputAction += InputManager_OnInputAction;
        }

        public override void Dispose()
        {
            base.Dispose();
            gameManager.InputManager.OnInputAction -= InputManager_OnInputAction;
        }

        public void Assign(Entity playerEntity)
        {
            Avatar = playerEntity;
        }

        private void InputManager_OnInputAction(InputAction inputAction)
        {
            if (inputAction.InputType == InputType.HorizontalAxis)
            {
                direction = new Vector2(inputAction.Value, direction.y);
            }
            else if (inputAction.InputType == InputType.VerticalAxis)
            {
                direction = new Vector2(direction.x, inputAction.Value);
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