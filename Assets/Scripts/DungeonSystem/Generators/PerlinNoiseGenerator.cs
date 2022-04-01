using Openverse.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * PerlinNoiseGenerator
 * Wessel Roelofse
 * 01/04/2022
 * 
 * A DungeonGenerator that uses Perlin Noise to generate a dungeon.
*/
[CreateAssetMenu(fileName = "NewDungeonGenerator", menuName = "Dungeon/Generators/PerlinNoiseGenerator")]
public class PerlinNoiseGenerator : DungeonGenerator
{
    public FloatReference perlinScale;
    public FloatReference treshhold;
    public Texture2D falloffMap;

    public override Vector2Int GetEndTile(GameTileController[,] tiles)
    {
        return GetRandomWalkableTile(tiles);
    }

    public override Vector2Int GetStartTile(GameTileController[,] tiles)
    {
        return GetRandomWalkableTile(tiles);
    }

    public override bool SampleTile(int x, int y)
    {
        return Mathf.PerlinNoise((seed + x) / (float)roomWidth * perlinScale, (seed + y) / (float)roomHeight * perlinScale) * falloffMap.GetPixelBilinear(x / (float)roomWidth, y / (float)roomHeight).r > treshhold; //Multiply this with 1 minus the distance of the center normalized with the map width or something like that
    }
}
