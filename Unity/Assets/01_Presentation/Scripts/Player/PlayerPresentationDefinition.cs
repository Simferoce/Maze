using Game.Core;
using UnityEngine;

namespace Game.Presentation
{
    [CreateAssetMenu(fileName = "PlayerPresentationDefinition", menuName = "Definitions/PlayerPresentationDefinition")]
    public class PlayerPresentationDefinition : AgentPresentationDefinition
    {
        public override Definition Create()
        {
            return new PlayerDefinition(Id);
        }

        public override void Initialize(Registry registry, Definition definition)
        {
        }
    }
}
