using Openverse.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGameTile", menuName = "Dungeon/GameTile")]
public class GameTile : ScriptableObject
{
    public Texture2D texture;
    public GameEvent tileEvent;
    public float rarity;

    public void triggerTile()
    {
        tileEvent?.Raise();
    }
}
