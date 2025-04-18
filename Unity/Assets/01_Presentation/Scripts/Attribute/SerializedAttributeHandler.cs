using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Presentation
{
    [Serializable]
    public class SerializedAttributeHandler
    {
        [SerializeField] private List<SerializedAttribute> attributes;

        public Game.Core.AttributeHandler Convert()
        {
            Core.AttributeHandler attributeHandler = new Core.AttributeHandler();
            foreach (SerializedAttribute serializedAttribute in attributes)
            {
                attributeHandler.Add(serializedAttribute.Type, serializedAttribute.Value);
            }

            return attributeHandler;
        }
    }
}
