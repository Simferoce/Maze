using System;

namespace Game.Core
{
    public abstract class EntityDefinition : Definition
    {
        public EntityDefinition(Guid id) : base(id)
        {
        }
    }
}
