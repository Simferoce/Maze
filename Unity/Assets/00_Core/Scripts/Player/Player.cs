namespace Game.Core
{
    public class Player : Agent
    {
        public Entity Avatar { get; private set; }

        public Player(GameManager gameManager) : base(gameManager)
        {
        }

        public void Assign(Entity playerEntity)
        {
            Avatar = playerEntity;
        }

        public override void Update()
        {
            Assertion.IsNotNull(Avatar, "Could not update the player because there is no avatar assigned yet.");

            Vector2 direction = new Vector2(gameManager.InputManager.GetAxis(InputAxisType.Horizontal), gameManager.InputManager.GetAxis(InputAxisType.Vertical));
            Vector2 displacement = direction * Avatar.AttributeHandler.Get(AttributeType.MovementSpeed).Value;

            Avatar.Move(displacement);
        }
    }
}