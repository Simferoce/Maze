using Game.Core;
using UnityEngine;

namespace Game.Presentation
{
    [CreateAssetMenu(fileName = "PresentationCharacterStandardAbilityDefinition", menuName = "Definitions/PresentationCharacterStandardAbilityDefinition")]
    public class PresentationCharacterStandardAbilityDefinition : PresentationCharacterAbilityDefinition
    {
        [SerializeField] private AnimationClip animation;
        [SerializeField] private SerializedAttributeHandler serializedAttributeHandler;

        public AnimationClip Animation { get => animation; set => animation = value; }

        public override Definition Create()
        {
            return new StandardCharacterAbilityDefinition(Id);
        }

        public override void Initialize(Registry registry, Definition definition)
        {
            StandardCharacterAbilityDefinition standardCharacterAbilityDefinition = definition as StandardCharacterAbilityDefinition;
            standardCharacterAbilityDefinition.AttributeHandler = serializedAttributeHandler.Convert();
        }
    }
}
