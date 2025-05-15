using System.Collections.Generic;

namespace Game.Core
{
    public class PhysicsManager
    {
        private const int MAX_ITERATION = 4;

        public event System.Action OnCollisionHandled;

        private GameManager gameManager;
        private List<DynamicObject> dynamicObjects = new List<DynamicObject>();
        private List<CircleCollision> circles = new List<CircleCollision>();
        private List<AABBCollision> axisAlignedBoundingBoxes = new List<AABBCollision>();
        private int id;

        public PhysicsManager(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        public void Update()
        {
            for (int i = 0; i < dynamicObjects.Count; ++i)
            {
                bool moved = false;

                CollisionHandle collisionHandleA = dynamicObjects[i].CollisionHandle;
                for (int iteration = 0; iteration < MAX_ITERATION; ++iteration)
                {
                    for (int j = 0; j < axisAlignedBoundingBoxes.Count; ++j)
                    {
                        if (axisAlignedBoundingBoxes[j] == AABBCollision.Undefined)
                            continue;

                        CollisionHandle collisionHandleB = new CollisionHandle(CollisionType.AABB, j);
                        moved |= Resolve(collisionHandleA, collisionHandleB);
                    }

                    if (!moved)
                        break;
                }
            }

            OnCollisionHandled?.Invoke();
        }

        private bool Resolve(CollisionHandle collisionHandleA, CollisionHandle collisionHandleB)
        {
            if (collisionHandleA == collisionHandleB)
                return false;

            if (Overlap(collisionHandleA, collisionHandleB))
            {
                if (ComputePenetration(collisionHandleA, collisionHandleB, out Vector2 direction, out Fixed64 depth)
                    && depth > new Fixed64(1 << 4))
                {
                    Move(collisionHandleA, direction * depth);
                    return true;
                }
            }

            return false;
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

        public bool ComputePenetration(CollisionHandle collisionHandleA, CollisionHandle collisionHandleB, out Vector2 direction, out Fixed64 depth)
        {
            Assertion.IsTrue(collisionHandleA.Type != CollisionType.Undefined, "The collision of the handle A is undefined.");
            Assertion.IsTrue(collisionHandleB.Type != CollisionType.Undefined, "The collision of the handle B is undefined.");

            if (collisionHandleA.Type == CollisionType.Circle && collisionHandleB.Type == CollisionType.AABB)
            {
                return Physics.ComputePenetration(circles[collisionHandleA.Index], axisAlignedBoundingBoxes[collisionHandleB.Index], out direction, out depth);
            }
            else
            {
                throw new System.NotImplementedException();
            }
        }

        public void Move(CollisionHandle collisionHandleA, Vector2 translation)
        {
            Assertion.IsTrue(collisionHandleA.Type != CollisionType.Undefined, "The collision of the handle A is undefined.");
            if (collisionHandleA.Type == CollisionType.Circle)
            {
                UpdateCircle(collisionHandleA, circles[collisionHandleA.Index].Center + translation, circles[collisionHandleA.Index].Radius);
            }
            else
            {
                throw new System.NotImplementedException();
            }
        }

        public Vector2 GetCenter(CollisionHandle collisionHandleA)
        {
            Assertion.IsTrue(collisionHandleA.Type != CollisionType.Undefined, "The collision of the handle A is undefined.");
            if (collisionHandleA.Type == CollisionType.Circle)
            {
                return circles[collisionHandleA.Index].Center;
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
            circles[collisionHandle.Index] = new CircleCollision(center, radius, circles[collisionHandle.Index].Id);
        }

        public void UpdateAABB(CollisionHandle collisionHandle, Vector2 min, Vector2 max)
        {
            Assertion.IsTrue(collisionHandle.Type == CollisionType.AABB, $"Expecting the type of collision to be aabb by was \"{collisionHandle.Type}\".");
            axisAlignedBoundingBoxes[collisionHandle.Index] = new AABBCollision(min, max, axisAlignedBoundingBoxes[collisionHandle.Index].Id);
        }
        #endregion

        #region Registration
        public CollisionHandle RegisterCircle(Vector2 center, Fixed64 radius)
        {
            int free = circles.IndexOf(CircleCollision.Undefined);
            if (free == -1)
            {
                free = circles.Count;
                circles.Add(CircleCollision.Undefined);
            }

            circles[free] = new CircleCollision(center, radius, id++);
            return new CollisionHandle(CollisionType.Circle, free);
        }

        public CollisionHandle RegisterAABB(Vector2 min, Vector2 max)
        {
            int free = axisAlignedBoundingBoxes.IndexOf(AABBCollision.Undefined);
            if (free == -1)
            {
                free = axisAlignedBoundingBoxes.Count;
                axisAlignedBoundingBoxes.Add(AABBCollision.Undefined);
            }

            axisAlignedBoundingBoxes[free] = new AABBCollision(min, max, id++);
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
                    circles[collisionHandle.Index] = CircleCollision.Undefined;
                    break;
                case CollisionType.AABB:
                    axisAlignedBoundingBoxes[collisionHandle.Index] = AABBCollision.Undefined;
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
