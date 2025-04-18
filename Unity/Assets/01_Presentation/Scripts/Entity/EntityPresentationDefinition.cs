using Game.Core;
using System;
using UnityEngine;

namespace Game.Presentation
{
    [CreateAssetMenu(fileName = "EntityPresentationDefinition", menuName = "Definitions/EntityPresentationDefinition")]
    public class EntityPresentationDefinition : PresentationDefinition
    {
        [SerializeField] private SerializedAttributeHandler attributeHandler;
        [SerializeField] private EntityVisual prefab;

        public EntityVisual Prefab { get => prefab; set => prefab = value; }

        public Definition Convert()
        {
            EntityDefinition entityDefinition = new Game.Core.EntityDefinition(new Guid(this.Id));
            entityDefinition.AttributeHandler = attributeHandler.Convert();
            return entityDefinition;
        }
    }
}
