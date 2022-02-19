// ----- AUTO GENERATED CODE ----- //
using UnityEngine;
namespace Openverse.Variables
{
    [CreateAssetMenu(fileName = "New GameTile Variable", menuName = "Openverse/Scriptable Object System/Variable/GameTile Variable", order = 100)]
    public class GameTileVariable : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
#endif
        public GameTile Value;
        public void SetValue(GameTile value)
        {
            Value = value;
        }
        public void SetValue(GameTileVariable value)
        {
            Value = value.Value;
        }
    }
}

