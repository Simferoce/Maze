using Game.Core;

namespace Game.Presentation
{
    public abstract class EntityPresentationDefinition : PresentationDefinition
    {
        public abstract Definition Convert();
        public abstract bool PresentationOf(Entity entity);
        public abstract EntityVisual InstantiateVisual(Entity entity);
    }
}
