namespace Game.Core
{
    public struct DynamicObject
    {
        public static DynamicObject Undefined = default;

        public CollisionHandle CollisionHandle;

        public DynamicObject(CollisionHandle collisionHandle)
        {
            CollisionHandle = collisionHandle;
        }
    }
}
