namespace Game.Core
{
    public class Transform
    {
        public Fixed64 LocalRotation { get => localRotation; set => localRotation = value; }
        public Vector2 LocalPosition { get => localPosition; set => localPosition = value; }

        private Vector2 localPosition;
        private Fixed64 localRotation;

        public void Translate(Vector2 translation)
        {
            LocalPosition += translation;
        }
    }
}