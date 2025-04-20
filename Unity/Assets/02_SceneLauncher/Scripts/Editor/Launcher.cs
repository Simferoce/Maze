using System;
using System.Collections.Generic;
using UnityEditor;

namespace Game.SceneLauncher
{
    public abstract class Launcher
    {
        protected Dictionary<string, object> fields = new Dictionary<string, object>();

        public event Action OnModified;

        public abstract void Launch();
        public abstract void Load();

        protected T Load<T>(string key)
            where T : UnityEngine.Object
        {
            if (EditorPrefs.HasKey(key))
            {
                string levelDefinitionPath = EditorPrefs.GetString(key);

                if (!string.IsNullOrEmpty(levelDefinitionPath))
                    return AssetDatabase.LoadAssetAtPath<T>(levelDefinitionPath);
            }

            return null;
        }

        public virtual void Set<T>(string key, T data)
            where T : UnityEngine.Object
        {
            EditorPrefs.SetString(key, data != null ? AssetDatabase.GetAssetPath(data) : string.Empty);
            fields[key] = data;
            OnModified?.Invoke();
        }

        public T Get<T>(string key)
        {
            return (T)fields[key];
        }

        public abstract string GetDescription();
    }
}
