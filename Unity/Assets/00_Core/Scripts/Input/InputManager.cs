namespace Game.Core
{
    public class InputManager
    {
        public delegate void OnInputActionDelegate(InputAction inputAction);
        public event OnInputActionDelegate OnInputAction;

        private GameManager gameManager;

        public InputManager(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        public void Execute(InputAction inputAction)
        {
            OnInputAction?.Invoke(inputAction);
        }
    }
}
