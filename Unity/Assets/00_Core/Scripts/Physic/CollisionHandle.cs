using System;

namespace Game.Core
{
    public struct CollisionHandle
    {
        public static CollisionHandle Undefined = new CollisionHandle(CollisionType.Undefined, 0);

        public CollisionType Type;
        public int Index;

        public CollisionHandle(CollisionType type, int index)
        {
            Type = type;
            Index = index;
        }

        public static bool operator ==(CollisionHandle a, CollisionHandle b) => a.Type == b.Type && a.Index == b.Index;
        public static bool operator !=(CollisionHandle a, CollisionHandle b) => a.Type != b.Type || a.Index != b.Index;

        public override bool Equals(object obj)
        {
            return obj is CollisionHandle handle && handle == this;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type, Index);
        }
    }
}
