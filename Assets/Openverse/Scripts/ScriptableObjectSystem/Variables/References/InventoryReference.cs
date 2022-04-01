// ----- AUTO GENERATED CODE ----- //
using System;
using UnityEngine;
namespace Openverse.Variables
{
    [Serializable]
    public class InventoryReference
    {
        public bool UseConstant = true;
        public Inventory ConstantValue;
        public InventoryVariable Variable;
        public InventoryReference(){}
        public InventoryReference(Inventory value)
        {
            UseConstant = true;
            ConstantValue = value;
        }
        public Inventory Value
        {
            get { return UseConstant ? ConstantValue : Variable.Value; }
            set { if (UseConstant) { ConstantValue = value; } else { Variable.Value = value; } }    
        }
        public static implicit operator Inventory(InventoryReference reference)
        {
            return reference.Value;
        }
    }
}

