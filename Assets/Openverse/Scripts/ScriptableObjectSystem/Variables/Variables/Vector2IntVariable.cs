// ----- AUTO GENERATED CODE ----- //
using UnityEngine;
namespace Openverse.Variables
{
    [CreateAssetMenu(fileName = "New Vector2Int Variable", menuName = "Openverse/Scriptable Object System/Variable/Vector2Int Variable", order = 100)]
    public class Vector2IntVariable : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
#endif
        public Vector2Int Value;
        public void SetValue(Vector2Int value)
        {
            Value = value;
        }
        public void SetValue(Vector2IntVariable value)
        {
            Value = value.Value;
        }
    }
}

