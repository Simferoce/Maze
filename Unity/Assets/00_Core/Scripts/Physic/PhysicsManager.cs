using System.Collections.Generic;

namespace Game.Core
{
    public class PhysicsManager
    {
        private GameManager gameManager;
        private List<DynamicObject> dynamicObjects = new List<DynamicObject>();
        private List<Circle> circles = new List<Circle>();
        private List<AABB> axisAlignedBoundingBoxes = new List<AABB>();
        private int id;

        public PhysicsManager(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        public void Update()
        {
            for (int i = 0; i < dynamicObjects.Count; ++i)
            {
                CollisionHandle collisionHandle = dynamicObjects[i].CollisionHandle;
                for (int j = 0; j < axisAlignedBoundingBoxes.Count; ++j)
                {
                    if (axisAlignedBoundingBoxes[j] == AABB.Undefined)
                        continue;

                    if (Overlap(collisionHandle, new CollisionHandle(CollisionType.AABB, j)))
                    {
                        gameManager.Logger.Log("Collision !", ILogger.LogLevel.Debug);
                    }
                }
            }
        }

        public bool Overlap(CollisionHandle collisionHandleA, CollisionHandle collisionHandleB)
        {
            Assertion.IsTrue(collisionHandleA.Type != CollisionType.Undefined, "The collision of the handle A is undefined.");
            Assertion.IsTrue(collisionHandleB.Type != CollisionType.Undefined, "The collision of the handle B is undefined.");

            if (collisionHandleA.Type == CollisionType.Circle && collisionHandleB.Type == CollisionType.AABB)
            {
                return Physics.Overlap(circles[collisionHandleA.Index], axisAlignedBoundingBoxes[collisionHandleB.Index]);
            }
            else
            {
                throw new System.NotImplementedException();
            }
        }

        #region Update Collider
        public void UpdateCircle(CollisionHandle collisionHandle, Vector2 center, Fixed64 radius)
        {
            Assertion.IsTrue(collisionHandle.Type == CollisionType.Circle, $"Expecting the type of collision to be circle by was \"{collisionHandle.Type}\".");
            circles[collisionHandle.Index] = new Circle(center, radius, circles[collisionHandle.Index].Id);
        }

        public void UpdateAABB(CollisionHandle collisionHandle, Vector2 min, Vector2 max)
        {
            Assertion.IsTrue(collisionHandle.Type == CollisionType.AABB, $"Expecting the type of collision to be aabb by was \"{collisionHandle.Type}\".");
            axisAlignedBoundingBoxes[collisionHandle.Index] = new AABB(min, max, axisAlignedBoundingBoxes[collisionHandle.Index].Id);
        }
        #endregion

        #region Registration
        public CollisionHandle RegisterCircle(Vector2 center, Fixed64 radius)
        {
            int free = circles.IndexOf(Circle.Undefined);
            if (free == -1)
            {
                free = circles.Count;
                circles.Add(Circle.Undefined);
            }

            circles[free] = new Circle(center, radius, id++);
            return new CollisionHandle(CollisionType.Circle, free);
        }

        public CollisionHandle RegisterAABB(Vector2 min, Vector2 max)
        {
            int free = axisAlignedBoundingBoxes.IndexOf(AABB.Undefined);
            if (free == -1)
            {
                free = axisAlignedBoundingBoxes.Count;
                axisAlignedBoundingBoxes.Add(AABB.Undefined);
            }

            axisAlignedBoundingBoxes[free] = new AABB(min, max, id++);
            return new CollisionHandle(CollisionType.AABB, free);
        }

        public DynamicObjectHandle RegisterDynamicObject(CollisionHandle collisionHandle)
        {
            int free = dynamicObjects.IndexOf(DynamicObject.Undefined);
            if (free == -1)
            {
                free = dynamicObjects.Count;
                dynamicObjects.Add(DynamicObject.Undefined);
            }

            dynamicObjects[free] = new DynamicObject(collisionHandle);
            return new DynamicObjectHandle(free);
        }

        public void Unregister(CollisionHandle collisionHandle)
        {
            switch (collisionHandle.Type)
            {
                case CollisionType.Circle:
                    circles[collisionHandle.Index] = Circle.Undefined;
                    break;
                case CollisionType.AABB:
                    axisAlignedBoundingBoxes[collisionHandle.Index] = AABB.Undefined;
                    break;
                default:
                    throw new System.NotImplementedException();
            }
        }

        public void UnregisterDynamicObject(DynamicObjectHandle dynamicObjectHandle)
        {
            dynamicObjects[dynamicObjectHandle.Index] = DynamicObject.Undefined;
        }
        #endregion
    }
}
