using System;
using UnityEngine;

namespace Game.Presentation
{
    [AttributeUsage(AttributeTargets.Field)]
    public class LongAsFixed64FloatAttribute : PropertyAttribute
    {
        public LongAsFixed64FloatAttribute()
        {
        }
    }
}