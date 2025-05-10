#if UNITY_EDITOR
using Game.Core;
using System;
using UnityEditor;
#endif
using UnityEngine;

namespace Game.Presentation
{
    public abstract class PresentationDefinition : ScriptableObject
    {
        [SerializeField, HideInInspector] private byte[] id;

        public Guid Id { get => new Guid(id); set => id = value.ToByteArray(); }

        public abstract Definition Create();
        public abstract void Initialize(Registry registry, Definition definition);

#if UNITY_EDITOR
        private void OnEnable()
        {
            if (AssetDatabase.TryGetGUIDAndLocalFileIdentifier(this, out string guid, out _)
                && id != new Guid(guid).ToByteArray())
            {
                id = new Guid(guid).ToByteArray();
                EditorUtility.SetDirty(this);
            }
        }

        private void OnValidate()
        {
            if (AssetDatabase.TryGetGUIDAndLocalFileIdentifier(this, out string guid, out _)
                && id != new Guid(guid).ToByteArray())
            {
                id = new Guid(guid).ToByteArray();
                EditorUtility.SetDirty(this);
            }
        }
#endif
    }
}
