using System;
using System.Collections.Generic;

namespace Game.Core
{
    public class Registry
    {
        private Dictionary<Guid, Definition> definitions = new Dictionary<Guid, Definition>();
        private GameManager gameManager;
        private bool isInitialized = false;

        public Registry(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        public void Initialize(List<Definition> definitions)
        {
            isInitialized = true;
            foreach (Definition definition in definitions)
                Add(definition);
        }

        public void Add(Definition definition)
        {
            Assertion.IsTrue(isInitialized, "The registry is not initialized yet.");
            Assertion.IsTrue(!definitions.ContainsKey(definition.Id), $"There is already a definition with id \"{definition.Id}\" in the registry.");

            definitions[definition.Id] = definition;
        }

        public T Get<T>(Guid id)
            where T : Definition
        {
            Assertion.IsTrue(isInitialized, "The registry is not initialized yet.");
            Assertion.IsTrue(definitions.ContainsKey(id), $"There is no definition with the id \"{id}\" in the registry.");
            Assertion.IsTrue(definitions[id] is T, $"The definition with the id \"{id}\" is not of type \"{nameof(T)}\".");

            return (T)definitions[id];
        }
    }
}
