using Game.Core;
using UnityEngine;

namespace Game.Presentation
{
    [CreateAssetMenu(fileName = "WallPresentationDefinition", menuName = "Definitions/WallPresentationDefinition")]
    public class WallPresentationDefinition : EntityPresentationDefinition
    {
        [SerializeField] private WallVisual prefab;

        public override Definition Create()
        {
            WallDefinition definition = new Game.Core.WallDefinition(this.Id);
            return definition;
        }

        public override bool HasIndependentVisual()
        {
            return true;
        }

        public override void Initialize(Registry registry, Definition definition)
        {
        }

        public override EntityVisual InstantiateVisual(Entity entity)
        {
            WallVisual wallVisual = GameObject.Instantiate(prefab);
            return wallVisual;
        }
    }
}
