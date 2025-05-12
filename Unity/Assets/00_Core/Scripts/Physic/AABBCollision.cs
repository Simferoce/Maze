using System;

namespace Game.Core
{
    public struct AABBCollision
    {
        public static AABBCollision Undefined = new AABBCollision(Vector2.Zero, Vector2.Zero, -1);

        public Vector2 Min;
        public Vector2 Max;
        public int Id;

        public AABBCollision(Vector2 min, Vector2 max, int id)
        {
            Min = min;
            Max = max;
            Id = id;
        }

        public static bool operator ==(AABBCollision a, AABBCollision b) => a.Min == b.Min && a.Max == b.Max;
        public static bool operator !=(AABBCollision a, AABBCollision b) => a.Min != b.Min || a.Max != b.Max;

        public override bool Equals(object other)
        {
            return other is AABBCollision aabb && aabb == this;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Min, Max);
        }
    }
}
