using System;

namespace Game.Core
{
    public abstract class Definition
    {
        public Guid Id { get; private set; }

        protected Definition(Guid id)
        {
            Id = id;
        }
    }
}
