namespace Game.Core
{
    public struct CollisionHandle
    {
        public CollisionType Type;
        public int Index;

        public CollisionHandle(CollisionType type, int index)
        {
            Type = type;
            Index = index;
        }
    }
}
