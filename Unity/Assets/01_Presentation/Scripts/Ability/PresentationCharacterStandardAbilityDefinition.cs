using Game.Core;
using UnityEngine;

namespace Game.Presentation
{
    [CreateAssetMenu(fileName = "PresentationCharacterStandardAbilityDefinition", menuName = "Definitions/PresentationCharacterStandardAbilityDefinition")]
    public class PresentationCharacterStandardAbilityDefinition : PresentationCharacterAbilityDefinition
    {
        [SerializeField, SecondAsTick] private int duration;
        [SerializeField] private AnimationClip animation;

        public AnimationClip Animation { get => animation; set => animation = value; }

        public override Definition Create()
        {
            return new StandardCharacterAbilityDefinition(Id);
        }

        public override void Initialize(Registry registry, Definition definition)
        {
            StandardCharacterAbilityDefinition standardCharacterAbilityDefinition = definition as StandardCharacterAbilityDefinition;
            standardCharacterAbilityDefinition.Duration = duration;
        }
    }
}
