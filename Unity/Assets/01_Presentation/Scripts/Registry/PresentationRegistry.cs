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
        [SerializeField] private List<EntityPresentationDefinition> definitions;

        public List<EntityPresentationDefinition> Definitions { get => definitions; set => definitions = value; }

        public T Get<T>(Guid id)
            where T : PresentationDefinition
        {
            PresentationDefinition presentationDefinition = Definitions.FirstOrDefault(x => x.Id == id.ToString());
            return presentationDefinition as T;
        }
    }
}
