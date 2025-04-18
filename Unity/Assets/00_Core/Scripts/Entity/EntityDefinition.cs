using System;

namespace Game.Core
{
    public class EntityDefinition : Definition
    {
        public AttributeHandler AttributeHandler { get; set; }

        public EntityDefinition(Guid id) : base(id)
        {
        }
    }
}
