using Game.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Core
{
    [CreateAssetMenu(fileName = "PresentationRegistry", menuName = "Definitions/PresentationRegistry")]
    public class PresentationRegistry : ScriptableObject
    {
        [SerializeField] private List<PresentationDefinition> definitions;

        private Dictionary<Guid, PresentationDefinition> maps;
        private Dictionary<Guid, PresentationDefinition> Maps { get { return maps ??= definitions.ToDictionary(x => x.Id, x => x); } }

        public T Get<T>(Guid id)
            where T : PresentationDefinition
        {
            PresentationDefinition presentationDefinition = Maps[id];
            return (T)presentationDefinition;
        }

        public Registry GenerateGameRegistry()
        {
            Registry registry = new Registry();
            List<Definition> definitions = new List<Definition>();
            for (int i = 0; i < this.definitions.Count; i++)
            {
                PresentationDefinition presentationDefinition = this.definitions[i];
                Definition definition = presentationDefinition.Create();
                definitions.Add(definition);
                registry.Add(definition);
            }

            for (int i = 0; i < definitions.Count; i++)
            {
                Definition definition = definitions[i];
                PresentationDefinition presentationDefinition = this.definitions[i];
                presentationDefinition.Initialize(registry, definition);
            }

            return registry;
        }
    }
}
