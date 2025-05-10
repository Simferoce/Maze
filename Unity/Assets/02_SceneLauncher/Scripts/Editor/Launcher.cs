using System;
using System.Collections.Generic;
using UnityEditor;

namespace Game.SceneLauncher
{
    public abstract class Launcher
    {
        public event Action OnModified;

        private Dictionary<string, object> fields = new Dictionary<string, object>();

        public virtual void Initialize()
        {
        }

        public abstract void Launch();
        public abstract void Load();

        protected T LoadObject<T>(string key)
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

        protected string LoadString(string key)
        {
            if (EditorPrefs.HasKey(key))
                return EditorPrefs.GetString(key);

            return string.Empty;
        }

        protected bool LoadBool(string key)
        {
            if (EditorPrefs.HasKey(key))
                return EditorPrefs.GetBool(key);

            return false;
        }

        public virtual void SetObject<T>(string key, T data)
            where T : UnityEngine.Object
        {
            EditorPrefs.SetString(key, data != null ? AssetDatabase.GetAssetPath(data) : string.Empty);
            fields[key] = data;
            OnModified?.Invoke();
        }

        public virtual void SetString(string key, string value)
        {
            EditorPrefs.SetString(key, value);
            fields[key] = value;
            OnModified?.Invoke();
        }

        public virtual void SetBool(string key, bool value)
        {
            EditorPrefs.SetBool(key, value);
            fields[key] = value;
            OnModified?.Invoke();
        }

        public T GetObject<T>(string key)
        {
            if (!fields.ContainsKey(key))
                return default;

            return (T)fields[key];
        }

        public string GetString(string key)
        {
            if (!fields.ContainsKey(key))
                return string.Empty;

            return (string)fields[key];
        }

        public bool GetBool(string key)
        {
            if (!fields.ContainsKey(key))
                return false;

            return (bool)fields[key];
        }

        public abstract string GetDescription();
    }
}
