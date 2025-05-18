using Game.Core;
using System;
using UnityEngine;

namespace Game.Presentation
{
    public abstract class EntityVisual : MonoBehaviour
    {
        protected ServiceRegistry serviceRegistry;
        protected PresentationRegistry presentationRegistry;
        protected Vector3 velocity;

        public Guid EntityId { get; private set; }

        public virtual void Initialize(ServiceRegistry serviceRegistry, Guid entityId)
        {
            this.serviceRegistry = serviceRegistry;
            EntityId = entityId;

            Core.Entity entity = serviceRegistry.Get<GameProvider>().GameManager.WorldManager.GetEntityById(EntityId);
            Vector3 targetPosition = new Vector3(entity.LocalPosition.X.ToFloat(), entity.LocalPosition.Y.ToFloat(), 0f) / serviceRegistry.Get<PresentationConstant>().Scale;
            this.transform.position = targetPosition;
        }

        protected virtual void SynchronizePosition()
        {
            Core.Entity entity = serviceRegistry.Get<GameProvider>().GameManager.WorldManager.GetEntityById(EntityId);
            Vector3 targetPosition = new Vector3(entity.LocalPosition.X.ToFloat(), entity.LocalPosition.Y.ToFloat(), 0f) / serviceRegistry.Get<PresentationConstant>().Scale;
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, 1 - Mathf.Exp(-serviceRegistry.Get<PresentationConstant>().Damping * Time.deltaTime));
        }

        protected virtual void SynchronizeRotation()
        {
            Core.Entity entity = serviceRegistry.Get<GameProvider>().GameManager.WorldManager.GetEntityById(EntityId);
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, entity.LocalRotation.ToFloat() * Mathf.Rad2Deg);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, 1 - Mathf.Exp(-serviceRegistry.Get<PresentationConstant>().Damping * Time.deltaTime));
        }
    }
}

