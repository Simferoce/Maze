using Game.Core;
using System;
using UnityEngine;

namespace Game.Presentation
{
    public abstract class EntityVisual : MonoBehaviour
    {
        protected GameManager gameManager;
        protected PresentationRegistry presentationRegistry;

        public Guid EntityId { get; private set; }

        public void Initialize(GameManager gameManager, PresentationRegistry presentationRegistry, Guid entityId)
        {
            this.gameManager = gameManager;
            this.presentationRegistry = presentationRegistry;
            EntityId = entityId;
        }

        protected virtual void Update()
        {
            Core.Entity entity = gameManager.WorldManager.GetEntityById(EntityId);
            this.transform.position = new Vector3(entity.Transform.LocalPosition.X.ToFloat(), entity.Transform.LocalPosition.Y.ToFloat(), 0f) / 100f;
        }
    }
}

