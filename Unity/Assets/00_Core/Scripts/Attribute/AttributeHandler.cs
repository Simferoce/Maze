using System.Collections.Generic;

namespace Game.Core
{
    public class AttributeHandler
    {
        private Dictionary<AttributeType, Attribute> attributes = new Dictionary<AttributeType, Attribute>();

        public void Add(AttributeType attributeType, Fixed64 value)
        {
            Assertion.IsTrue(!attributes.ContainsKey(attributeType), $"Could not add the attribute with the type \"{attributeType}\" because there is already an attribute with this type in the handler.");

            attributes[attributeType] = new Attribute(attributeType, value);
        }

        public void Set(AttributeType attributeType, Fixed64 value)
        {
            Assertion.IsTrue(attributes.ContainsKey(attributeType), $"Could not update the attribute with the type \"{attributeType}\" because it is not present in the handler.");

            attributes[attributeType] = new Attribute(attributeType, value);
        }

        public void Multiple(AttributeType attributeType, Fixed64 value)
        {
            Assertion.IsTrue(attributes.ContainsKey(attributeType), $"Could not update the attribute with the type \"{attributeType}\" because it is not present in the handler.");

            attributes[attributeType] = new Attribute(attributeType, attributes[attributeType].Value * value);
        }

        public Attribute Get(AttributeType attributeType)
        {
            Assertion.IsTrue(attributes.ContainsKey(attributeType), $"Could not get the attribute with the type \"{attributeType}\" because it is not present in the handler.");

            return attributes[attributeType];
        }

        public bool TryGet(AttributeType attributeType, out Attribute attribute)
        {
            if (attributes.ContainsKey(attributeType))
            {
                attribute = attributes[attributeType];
                return true;
            }

            attribute = Attribute.Default;
            return false;
        }

        public AttributeHandler Clone()
        {
            AttributeHandler handler = new AttributeHandler();
            handler.attributes = new Dictionary<AttributeType, Attribute>(attributes);
            return handler;
        }
    }
}
