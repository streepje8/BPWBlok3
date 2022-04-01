// ----- AUTO GENERATED CODE ----- //
using System;
using UnityEngine;
namespace Openverse.Variables
{
    [Serializable]
    public class Vector2IntReference
    {
        public bool UseConstant = true;
        public Vector2Int ConstantValue;
        public Vector2IntVariable Variable;
        public Vector2IntReference(){}
        public Vector2IntReference(Vector2Int value)
        {
            UseConstant = true;
            ConstantValue = value;
        }
        public Vector2Int Value
        {
            get { return UseConstant ? ConstantValue : Variable.Value; }
            set { if (UseConstant) { ConstantValue = value; } else { Variable.Value = value; } }    
        }
        public static implicit operator Vector2Int(Vector2IntReference reference)
        {
            return reference.Value;
        }
    }
}

