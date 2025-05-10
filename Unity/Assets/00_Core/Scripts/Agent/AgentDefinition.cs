using System;

namespace Game.Core
{
    public abstract class AgentDefinition : EntityDefinition
    {
        protected AgentDefinition(Guid id) : base(id)
        {
        }
    }
}
