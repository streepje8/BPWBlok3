using Openverse.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGameTile", menuName = "Dungeon/GameTile")]
public class GameTile : ScriptableObject
{
    public Texture2D texture;
    public GameEvent clickOnTileEvent;
    public GameEvent walkOnTileEvent;
    public float rarity;
    public bool isWalkable = true;
    public bool CanBeEndOrStartTile = false;
    public bool spawnsEntity = false;
    public GameObject theEntityItSpawns;
}
