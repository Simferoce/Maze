using Game.Core;
using System;
using UnityEngine;

namespace Game.Presentation
{
    public class EntityVisual : MonoBehaviour
    {
        private GameManager gameManager;

        public Guid EntityId { get; private set; }

        public void Initialize(GameManager gameManager, Guid entityId)
        {
            this.gameManager = gameManager;
            EntityId = entityId;
        }

        private void Update()
        {
            Core.Entity entity = gameManager.WorldManager.GetEntityById(EntityId);
            this.transform.position = new Vector3(entity.Transform.LocalPosition.x.ToFloat(), entity.Transform.LocalPosition.y.ToFloat(), 0f);
        }
    }
}

