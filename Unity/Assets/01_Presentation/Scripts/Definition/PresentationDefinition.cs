#if UNITY_EDITOR
using System;
using UnityEditor;
#endif
using UnityEngine;

namespace Game.Presentation
{
    public abstract class PresentationDefinition : ScriptableObject
    {
        [SerializeField] private string id;

        public string Id { get => id; set => id = value; }

#if UNITY_EDITOR
        private void OnEnable()
        {
            if (AssetDatabase.TryGetGUIDAndLocalFileIdentifier(this, out string guid, out _)
                && id != new Guid(guid).ToString())
            {
                id = new Guid(guid).ToString();
                EditorUtility.SetDirty(this);
            }
        }

        private void OnValidate()
        {
            if (AssetDatabase.TryGetGUIDAndLocalFileIdentifier(this, out string guid, out _)
                && id != new Guid(guid).ToString())
            {
                id = new Guid(guid).ToString();
                EditorUtility.SetDirty(this);
            }
        }
#endif
    }
}
