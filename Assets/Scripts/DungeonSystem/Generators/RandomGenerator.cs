using Openverse.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDungeonGenerator", menuName = "Dungeon/Generators/RandomGenerator")]
public class RandomGenerator : DungeonGenerator
{
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
        return Random.Range(0,10) > 5f;
    }
}
