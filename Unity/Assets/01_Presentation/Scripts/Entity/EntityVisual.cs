using Game.Core;
using System;
using UnityEngine;

namespace Game.Presentation
{
    public abstract class EntityVisual : MonoBehaviour
    {
        [SerializeField] private float damping;

        protected ServiceRegistry serviceRegistry;
        protected PresentationRegistry presentationRegistry;
        protected Vector3 velocity;

        public Guid EntityId { get; private set; }

        public void Initialize(ServiceRegistry serviceRegistry, Guid entityId)
        {
            this.serviceRegistry = serviceRegistry;
            EntityId = entityId;

            Core.Entity entity = serviceRegistry.Get<GameProvider>().GameManager.WorldManager.GetEntityById(EntityId);
            Vector3 targetPosition = new Vector3(entity.Transform.LocalPosition.X.ToFloat(), entity.Transform.LocalPosition.Y.ToFloat(), 0f) / 25f;
            this.transform.position = targetPosition;
        }

        protected virtual void Update()
        {
            Core.Entity entity = serviceRegistry.Get<GameProvider>().GameManager.WorldManager.GetEntityById(EntityId);
            Vector3 targetPosition = new Vector3(entity.Transform.LocalPosition.X.ToFloat(), entity.Transform.LocalPosition.Y.ToFloat(), 0f) / 25f;
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, 1 - Mathf.Exp(-damping * Time.deltaTime));
        }
    }
}

