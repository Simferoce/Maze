using Game.Core;
using System;
using UnityEngine;

namespace Game.Presentation
{
    [Serializable]
    public class SerializedAttribute
    {
        [SerializeField] private Game.Core.AttributeType type;
        [SerializeField, LongAsFixed64Float] private long value;

        public AttributeType Type { get => type; set => type = value; }
        public Fixed64 Value { get => new Fixed64(value); set => this.value = value.RawValue; }
    }
}
