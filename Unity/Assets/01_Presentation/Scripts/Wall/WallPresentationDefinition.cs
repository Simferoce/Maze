using Game.Core;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.Presentation
{
    [CreateAssetMenu(fileName = "WallPresentationDefinition", menuName = "Definitions/WallPresentationDefinition")]
    public class WallPresentationDefinition : EntityPresentationDefinition
    {
        [SerializeField] private Tile tile;

        public Tile Tile { get => tile; set => tile = value; }

        public override Definition Create()
        {
            WallDefinition definition = new Game.Core.WallDefinition(this.Id);
            return definition;
        }

        public override bool HasIndependentVisual()
        {
            return false;
        }

        public override void Initialize(Registry registry, Definition definition)
        {
        }

        public override EntityVisual InstantiateVisual(Entity entity)
        {
            throw new NotImplementedException();
        }
    }
}
