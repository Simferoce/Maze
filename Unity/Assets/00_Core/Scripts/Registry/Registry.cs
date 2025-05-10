using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Core
{
    public class Registry
    {
        private Dictionary<Guid, Definition> definitions = new Dictionary<Guid, Definition>();

        public void Add(Definition definition)
        {
            Assertion.IsTrue(!definitions.ContainsKey(definition.Id), $"There is already a definition with id \"{definition.Id}\" in the registry.");

            definitions[definition.Id] = definition;
        }

        public T Get<T>(Guid id)
            where T : Definition
        {
            Assertion.IsTrue(definitions.ContainsKey(id), $"There is no definition with the id \"{id}\" in the registry.");
            Assertion.IsTrue(definitions[id] is T, $"The definition with the id \"{id}\" is not of type \"{nameof(T)}\".");

            return (T)definitions[id];
        }

        public IEnumerable<T> GetAll<T>()
        {
            return definitions.Values.OfType<T>();
        }
    }
}
