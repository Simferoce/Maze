using System;
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

        protected ObjectField CreatePreferenceObjectField<U>(string label, string key)
                where U : UnityEngine.Object
        {
            ObjectField objectField = new ObjectField(label);
            objectField.objectType = typeof(U);

            objectField.value = launcher.Get<U>(key);
            objectField.RegisterValueChangedCallback(OnFieldUpdated);

            void OnFieldUpdated(ChangeEvent<UnityEngine.Object> evt)
            {
                launcher.Set<U>(key, (U)evt.newValue);
            }

            return objectField;
        }
    }
}