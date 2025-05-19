using Game.Core;
using System;
using UnityEngine;

namespace Game.Presentation
{
    public abstract class EntityVisual : MonoBehaviour
    {
        protected ServiceRegistry serviceRegistry;
        protected PresentationRegistry presentationRegistry;
        protected float lastTime;
        protected long currentTick = -1;
        protected long lastTick;
        protected Vector3 currentPosition;
        protected Vector3 lastPosition;
        protected Quaternion currentRotation;
        protected Quaternion lastRotation;

        public Guid EntityId { get; private set; }

        public virtual void Initialize(ServiceRegistry serviceRegistry, Guid entityId)
        {
            this.serviceRegistry = serviceRegistry;
            EntityId = entityId;

            SynchronizeTransform();
        }

        protected virtual void Update()
        {
            SynchronizeTransform();
        }

        protected virtual void SynchronizeTransform()
        {
            GameManager gameManager = serviceRegistry.Get<GameProvider>().GameManager;
            Core.Entity entity = serviceRegistry.Get<GameProvider>().GameManager.WorldManager.GetEntityById(EntityId);
            long frameTick = gameManager.TimeManager.CurrentTick;
            if (frameTick > currentTick)
            {
                lastTime = Time.time;
                lastTick = currentTick;
                currentTick = frameTick;
                lastPosition = currentPosition;
                lastRotation = currentRotation;
                currentPosition = new Vector3(entity.LocalPosition.X.ToFloat(), entity.LocalPosition.Y.ToFloat(), 0f) / serviceRegistry.Get<PresentationConstant>().Scale;
                currentRotation = Quaternion.Euler(0f, 0f, entity.LocalRotation.ToFloat() * Mathf.Rad2Deg);
            }

            float alpha = lastTick != -1 ? (Time.time - Time.fixedTime) / Time.fixedDeltaTime : 1;
            this.transform.position = Vector3.LerpUnclamped(lastPosition, currentPosition, alpha);
            //this.transform.rotation = Quaternion.SlerpUnclamped(lastRotation, currentRotation, alpha);
            this.transform.rotation = currentRotation;
        }
    }
}

