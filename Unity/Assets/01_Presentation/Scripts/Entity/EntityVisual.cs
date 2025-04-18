using System;
using UnityEngine;

namespace Game.Presentation
{
    public class EntityVisual : MonoBehaviour
    {
        public Guid EntityId { get; private set; }

        public void Initialize(Guid entityId)
        {
            EntityId = entityId;
        }
    }
}

