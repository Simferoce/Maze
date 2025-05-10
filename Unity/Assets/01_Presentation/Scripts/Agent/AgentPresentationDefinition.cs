using Game.Core;

namespace Game.Presentation
{
    public abstract class AgentPresentationDefinition : EntityPresentationDefinition
    {
        public override bool HasIndependentVisual()
        {
            return false;
        }

        public override EntityVisual InstantiateVisual(Entity entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
