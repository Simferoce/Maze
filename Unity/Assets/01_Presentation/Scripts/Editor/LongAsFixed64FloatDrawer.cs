using Game.Core;
using UnityEditor;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(LongAsFixed64FloatAttribute))]
public class LongAsFixed64FloatDrawer : PropertyDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        var attr = (LongAsFixed64FloatAttribute)attribute;

        float valueAsFloat = new Fixed64(property.longValue).ToFloat();

        var floatField = new FloatField(property.displayName)
        {
            value = valueAsFloat
        };
        floatField.AddToClassList(BaseField<float>.alignedFieldUssClassName);
        floatField.RegisterValueChangedCallback(evt =>
        {
            long fixedRaw = Fixed64.FromFloat(evt.newValue).RawValue;
            property.longValue = fixedRaw;
            property.serializedObject.ApplyModifiedProperties();
        });

        return floatField;
    }
}
