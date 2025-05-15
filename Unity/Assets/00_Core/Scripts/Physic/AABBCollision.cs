using System;

namespace Game.Core
{
    public struct AABBCollision
    {
        public Vector2 Min;
        public Vector2 Max;
        public int Id;
        public bool Active;

        public AABBCollision(Vector2 min, Vector2 max, int id)
        {
            Min = min;
            Max = max;
            Id = id;
            Active = true;
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
