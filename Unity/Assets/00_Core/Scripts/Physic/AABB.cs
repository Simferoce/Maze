using System;

namespace Game.Core
{
    public struct AABB
    {
        public static AABB Undefined = new AABB(Vector2.Zero, Vector2.Zero, -1);

        public Vector2 Min;
        public Vector2 Max;
        public int Id;

        public AABB(Vector2 min, Vector2 max, int id)
        {
            Min = min;
            Max = max;
            Id = id;
        }

        public static bool operator ==(AABB a, AABB b) => a.Min == b.Min && a.Max == b.Max;
        public static bool operator !=(AABB a, AABB b) => a.Min != b.Min || a.Max != b.Max;

        public override bool Equals(object other)
        {
            return other is AABB aabb && aabb == this;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Min, Max);
        }
    }
}
