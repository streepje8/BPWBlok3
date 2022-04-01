using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * DungeonGenerator
 * Wessel Roelofse
 * 01/04/2022
 * 
 * A scriptable object base to write your own dungeon generator.
 */
public abstract class DungeonGenerator : ScriptableObject
{
    [HideInInspector] public float seed;
    [HideInInspector] public int roomWidth;
    [HideInInspector] public int roomHeight;

    public abstract bool SampleTile(int x, int y);
    public abstract Vector2Int GetStartTile(GameTileController[,] tiles);
    public abstract Vector2Int GetEndTile(GameTileController[,] tiles);

    public Vector2Int GetRandomWalkableTile(GameTileController[,] tiles)
    {
        int tileX = Random.Range(0, roomWidth);
        int tileY = Random.Range(0, roomHeight);
        int attempts = 0;
        bool foundTile = false;
        while ((attempts < roomWidth * roomHeight) && !foundTile)
        {
            if (!tiles[tileX, tileY].type.CanBeEndOrStartTile)
            {
                tileX++;
                if (tileX >= roomWidth)
                {
                    tileY++;
                    tileX = 0;
                }
                if (tileY >= roomHeight)
                {
                    tileY = 0;
                    tileX = 0;
                }
                attempts++;
            }
            else
            {
                foundTile = true;
            }
        }
        return new Vector2Int(tileX, tileY);
    }
}
