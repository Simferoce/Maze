using Game.Core;
using System;
using UnityEngine;

namespace Game.Presentation
{
    [CreateAssetMenu(fileName = "CharacterPresentationDefinition", menuName = "Definitions/CharacterPresentationDefinition")]
    public class CharacterPresentationDefinition : EntityPresentationDefinition
    {
        [SerializeField] private SerializedAttributeHandler attributeHandler;
        [SerializeField] private CharacterVisual prefab;

        public CharacterVisual Prefab { get => prefab; set => prefab = value; }

        public override Definition Convert()
        {
            CharacterDefinition entityDefinition = new Game.Core.CharacterDefinition(new Guid(this.Id));
            entityDefinition.AttributeHandler = attributeHandler.Convert();
            return entityDefinition;
        }

        public override EntityVisual InstantiateVisual(Entity entity)
        {
            CharacterVisual characterVisual = GameObject.Instantiate(prefab);
            return characterVisual;
        }

        public override bool PresentationOf(Entity entity)
        {
            return entity is Character;
        }
    }
}
