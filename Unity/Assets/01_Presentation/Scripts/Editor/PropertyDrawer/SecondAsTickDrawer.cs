using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.Presentation
{
    [CustomPropertyDrawer(typeof(SecondAsTickAttribute))]
    public class SecondAsTickDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            float fixedDt = Time.fixedDeltaTime;

            int storedTicks = property.intValue;
            float seconds = storedTicks * fixedDt;

            var floatField = new FloatField(property.displayName) { value = seconds };
            floatField.AddToClassList(BaseField<float>.alignedFieldUssClassName);

            floatField.RegisterValueChangedCallback(evt =>
            {
                float newSeconds = evt.newValue;
                float asTicks = newSeconds / fixedDt;

                property.intValue = (int)asTicks;
                property.serializedObject.ApplyModifiedProperties();
            });

            return floatField;
        }
    }
}