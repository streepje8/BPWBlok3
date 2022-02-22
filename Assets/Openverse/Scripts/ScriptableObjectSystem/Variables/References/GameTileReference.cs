// ----- AUTO GENERATED CODE ----- //
using System;
using UnityEngine;
namespace Openverse.Variables
{
    [Serializable]
    public class GameTileReference
    {
        public bool UseConstant = true;
        public GameTile ConstantValue;
        public GameTileVariable Variable;
        public GameTileReference(){}
        public GameTileReference(GameTile value)
        {
            UseConstant = true;
            ConstantValue = value;
        }
        public GameTile Value
        {
            get { return UseConstant ? ConstantValue : Variable.Value; }
            set { if (UseConstant) { ConstantValue = value; } else { Variable.Value = value; } }
        }
        public static implicit operator GameTile(GameTileReference reference)
        {
            return reference.Value;
        }
    }
}

