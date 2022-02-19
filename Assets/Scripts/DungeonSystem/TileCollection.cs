using Openverse.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGameTileReference", menuName = "Dungeon/TileCollection")]
public class TileCollection : ScriptableObject
{
    public GameTileReference LTCornerTile;
    public GameTileReference LWallTile;
    public GameTileReference LBCornerTile;
    public GameTileReference MTWallTile;
    public GameTileReference floorTile;
    public GameTileReference MBWallTile;
    public GameTileReference RTCornerTile;
    public GameTileReference RWallTile;
    public GameTileReference RBCornerTile;
    public GameTileReference VoidTile;
    public List<GameTileReference> DungeonSpecificTiles;
}
