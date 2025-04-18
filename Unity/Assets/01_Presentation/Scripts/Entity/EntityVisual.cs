using System;
using UnityEngine;

namespace Game.Presentation
{
    public class EntityVisual : MonoBehaviour
    {
        private PresentationManager presentationManager;

        public Guid EntityId { get; private set; }

        public void Initialize(PresentationManager presentationManager, Guid entityId)
        {
            this.presentationManager = presentationManager;
            EntityId = entityId;
        }

        private void Update()
        {
            Core.Entity entity = presentationManager.GameManager.WorldManager.GetEntityById(EntityId);
            this.transform.position = new Vector3(entity.Transform.LocalPosition.x.ToFloat(), entity.Transform.LocalPosition.y.ToFloat(), 0f);
        }
    }
}

