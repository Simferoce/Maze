namespace Game.Core
{
    public class Transform
    {
        public Fixed64 LocalRotation { get => localRotation; private set => localRotation = value; }
        public Vector2 LocalPosition { get => localPosition; private set => localPosition = value; }

        private Vector2 localPosition;
        private Fixed64 localRotation;

        public void Translate(Vector2 translation)
        {
            LocalPosition += translation;
        }

        public void SetPosition(Vector2 position)
        {
            LocalPosition = position;
        }

        public void SetRotation(Fixed64 rotation)
        {
            LocalRotation = rotation;
        }

        public void LookAt(Vector2 point)
        {
            Vector2 delta = point - LocalPosition;
            if (delta == Vector2.Zero)
                return;

            Fixed64 angle = Math.ATan2(delta.Y, delta.X);
            LocalRotation = angle;
        }

        public void LookIn(Vector2 direction)
        {
            direction = direction.Normalized;
            if (direction == Vector2.Zero)
                return;

            Fixed64 angle = Math.ATan2(direction.Y, direction.X);
            LocalRotation = angle;
        }
    }
}