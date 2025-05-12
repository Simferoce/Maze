namespace Game.Core
{
    public struct Bounds
    {
        public Vector2 Min;
        public Vector2 Max;

        public Bounds(Vector2 min, Vector2 max)
        {
            Min = min;
            Max = max;
        }

        public bool Overlaps(Bounds other)
        {
            return !(Max.X < other.Min.X || Min.X > other.Max.X ||
                     Max.Y < other.Min.Y || Min.Y > other.Max.Y);
        }
    }
}
