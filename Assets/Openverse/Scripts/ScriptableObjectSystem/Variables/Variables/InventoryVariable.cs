// ----- AUTO GENERATED CODE ----- //
using UnityEngine;
namespace Openverse.Variables
{
    [CreateAssetMenu(fileName = "New Inventory Variable", menuName = "Openverse/Scriptable Object System/Variable/Inventory Variable", order = 100)]
    public class InventoryVariable : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
#endif
        public Inventory Value;
        public void SetValue(Inventory value)
        {
            Value = value;
        }
        public void SetValue(InventoryVariable value)
        {
            Value = value.Value;
        }
    }
}

