using Game.Core;
using UnityEngine;

namespace Game.Presentation
{
    [CreateAssetMenu(fileName = "CharacterPresentationDefinition", menuName = "Definitions/CharacterPresentationDefinition")]
    public class CharacterPresentationDefinition : EntityPresentationDefinition
    {
        [SerializeField] private SerializedAttributeHandler attributeHandler;
        [SerializeField, LongAsFixed64Float] private long radius;
        [SerializeField] private CharacterVisual prefab;

        public CharacterVisual Prefab { get => prefab; set => prefab = value; }

        public override Definition Create()
        {
            CharacterDefinition entityDefinition = new Game.Core.CharacterDefinition(this.Id);
            return entityDefinition;
        }

        public override bool HasIndependentVisual()
        {
            return true;
        }

        public override void Initialize(Registry registry, Definition definition)
        {
            CharacterDefinition characterDefinition = definition as CharacterDefinition;
            characterDefinition.AttributeHandler = attributeHandler.Convert();
            characterDefinition.Radius = new Fixed64(radius);
        }

        public override EntityVisual InstantiateVisual(Entity entity)
        {
            CharacterVisual characterVisual = GameObject.Instantiate(prefab);
            return characterVisual;
        }
    }
}
