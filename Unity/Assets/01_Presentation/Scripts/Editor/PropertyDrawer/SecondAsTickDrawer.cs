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
            // -------- get the current project‑wide fixed‑timestep --------
            float fixedDt = Time.fixedDeltaTime;          // e.g. 0.02 s

            // -------- read: ticks → seconds -----------------------------
            int storedTicks = property.intValue;       // raw fixed‑point “ticks”
            float seconds = storedTicks * fixedDt;

            var floatField = new FloatField(property.displayName) { value = seconds };
            floatField.AddToClassList(BaseField<float>.alignedFieldUssClassName);

            // -------- write: seconds → ticks ----------------------------
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