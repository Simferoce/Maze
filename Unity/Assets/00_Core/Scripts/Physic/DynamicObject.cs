namespace Game.Core
{
    public struct DynamicObject
    {
        public CollisionHandle CollisionHandle;
        public bool Active;

        public DynamicObject(CollisionHandle collisionHandle)
        {
            CollisionHandle = collisionHandle;
            Active = true;
        }
    }
}
