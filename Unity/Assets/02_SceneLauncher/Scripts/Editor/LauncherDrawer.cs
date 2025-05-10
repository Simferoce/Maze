using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Game.SceneLauncher
{
    public abstract class LauncherDrawer
    {
        public abstract void Initialize(VisualElement root);

        public abstract Type GetLauncherType();
    }

    public abstract class LauncherDrawer<T> : LauncherDrawer
            where T : Launcher
    {
        protected T launcher;

        protected LauncherDrawer(T launcher)
        {
            this.launcher = launcher;
        }

        public override Type GetLauncherType()
        {
            return typeof(T);
        }

        protected Toggle CreateTogglePreferenceField(string label, string key)
        {
            Toggle toggle = new Toggle();
            toggle.label = label;
            toggle.AddToClassList(Toggle.alignedFieldUssClassName);

            toggle.value = launcher.GetBool(key);
            toggle.RegisterValueChangedCallback(OnFieldUpdated);

            void OnFieldUpdated(ChangeEvent<bool> evt)
            {
                launcher.SetBool(key, evt.newValue);
            }

            return toggle;
        }

        protected ObjectField CreatePreferenceObjectField<U>(string label, string key)
                where U : UnityEngine.Object
        {
            ObjectField objectField = new ObjectField(label);
            objectField.AddToClassList(ObjectField.alignedFieldUssClassName);
            objectField.objectType = typeof(U);

            objectField.value = launcher.GetObject<U>(key);
            objectField.RegisterValueChangedCallback(OnFieldUpdated);

            void OnFieldUpdated(ChangeEvent<UnityEngine.Object> evt)
            {
                launcher.SetObject<U>(key, (U)evt.newValue);
            }

            return objectField;
        }

        protected PopupField<U> CreatePreferencePopField<U>(string key, string name, List<U> options, Func<U, string> formatFunc)
            where U : IPopupFieldData
        {
            PopupField<U> popupField = new PopupField<U>() { formatListItemCallback = formatFunc, formatSelectedValueCallback = formatFunc };
            popupField.label = name;
            popupField.choices = options;
            U data = options.FirstOrDefault(x => x.Id == launcher.GetString(key));
            if (data == null)
                data = options.First();

            popupField.index = options.IndexOf(data);
            popupField.RegisterValueChangedCallback(OnFieldUpdated);
            void OnFieldUpdated(ChangeEvent<U> evt)
            {
                launcher.SetString(key, evt.newValue.Id);
            }

            return popupField;
        }
    }
}