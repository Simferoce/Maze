using Game.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Core
{
    public class PresentationRegistry : MonoBehaviour
    {
        [SerializeField] private List<EntityPresentationDefinition> definitions;
        [SerializeField] private EntityPresentationDefinition playerDefinition;

        public List<EntityPresentationDefinition> Definitions { get => definitions; set => definitions = value; }
        public EntityPresentationDefinition PlayerDefinition { get => playerDefinition; set => playerDefinition = value; }

        public T Get<T>(Guid id)
            where T : PresentationDefinition
        {
            PresentationDefinition presentationDefinition = Definitions.FirstOrDefault(x => x.Id == id.ToString());
            return presentationDefinition as T;
        }
    }
}
