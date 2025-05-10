using Game.Core;

namespace Game.Presentation
{
    public abstract class EntityPresentationDefinition : PresentationDefinition
    {
        public abstract bool HasIndependentVisual();
        public abstract EntityVisual InstantiateVisual(Entity entity);
    }
}
