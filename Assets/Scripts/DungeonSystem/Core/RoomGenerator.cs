using Openverse.Events;
using Openverse.Variables;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public GameEvent roomGenerated;
    public GameObject tilePrefab;
    public DungeonGenerator generator;
    public IntReference roomWidth;
    public IntReference roomHeight;
    public TileCollection possibleTiles;
    public Vector3Reference startingPosition;
    public Vector3Reference endingPosition;
    public float seed = 0;
    public bool generateSeedOnRoomGeneration = false;
    [HideInInspector]public GameTileController[,] tiles;

    void Start()
    {
        GenerateRoom();
    }

    //float temp = 0;

    private void Update()
    {
        //Debug
        
        //temp += Time.deltaTime;
        //if(temp > 0.5)
        //{
        //    GenerateRoom();
        //    Debug.Log("New Room boi");
        //    temp = 0;
        //}
        
    }

    public bool isWalkable(int x, int y)
    {
        return tiles[x, y].type.isWalkable;
    }

    public bool isWalkableAndFree(int x, int y)
    {
        return tiles[x, y].type.isWalkable && !(tiles[x,y].containsEntity);
    }

    public void setContainsEntity(int x, int y, bool value)
    {
        tiles[x, y].containsEntity = value;
    }

    void GenerateRoom()
    {
        if(generateSeedOnRoomGeneration)
        {
            seed = Random.Range(0, 9999f);
        }
        if(tiles != null) { 
            for(int x = 0; x < tiles.GetLength(0); x++)
            {
                for(int y = 0; y < tiles.GetLength(1); y++)
                {
                    Destroy(tiles[x, y].gameObject);
                }
            }
        }
        tiles = new GameTileController[roomWidth, roomHeight];
        generator.seed = seed;
        generator.roomWidth = roomWidth;
        generator.roomHeight = roomHeight;
        for (int x = 0; x < roomWidth; x++)
        {
            for (int y = 0; y < roomHeight; y++)
            {
                bool isFloor = SampleTile(x, y);
                GameTileController tile = Instantiate(tilePrefab, new Vector3(x, 0, y), Quaternion.identity).GetComponent<GameTileController>();
                tile.transform.Rotate(new Vector3(90, 0, 180));
                tiles[x, y] = tile;
                tile.GetComponent<GameEventListener>().Event = roomGenerated;
                if (isFloor)
                {
                    byte surroundings = boolArrayToSampleByteZeroCorners(SampleSurroundings(x, y)); //Could be sped up with less samples since we only use the direct neighbors and not the diagonal neighbors
                    switch (surroundings)
                    {
                        case 0x5:
                            tile.type = possibleTiles.LTCornerTile;
                            break;
                        case 0xD:
                            tile.type = possibleTiles.MTWallTile;
                            break;
                        case 0xC:
                            tile.type = possibleTiles.RTCornerTile;
                            break;
                        case 0x7:
                            tile.type = possibleTiles.LWallTile;
                            break;
                        case 0xF:
                            tile.type = possibleTiles.floorTile;
                            List<GameTileReference> shuffeledTiles = possibleTiles.DungeonSpecificTiles.OrderBy(a => Random.Range(0, 50)).ToList();
                            foreach (GameTileReference rngTile in shuffeledTiles)
                            {
                                if (Mathf.RoundToInt(Random.Range(1, rngTile.Value.rarity)) == 1)
                                {
                                    tile.type = rngTile;
                                }
                            }
                            if(tile.type.spawnsEntity)
                            {
                                Instantiate(tile.type.theEntityItSpawns, tile.transform.position, Quaternion.identity).GetComponent<TileEntity>()?.SpawnAt(x,y);
                            }
                            break;
                        case 0xE:
                            tile.type = possibleTiles.RWallTile;
                            break;
                        case 0x3:
                            tile.type = possibleTiles.LBCornerTile;
                            break;
                        case 0xB:
                            tile.type = possibleTiles.MBWallTile;
                            break;
                        case 0xA:
                            tile.type = possibleTiles.RBCornerTile;
                            break;
                        case 0x1:
                            tile.type = possibleTiles.SoloRightTile;
                            break;
                        case 0x2:
                            tile.type = possibleTiles.SoloUpTile;
                            break;
                        case 0x4:
                            tile.type = possibleTiles.SoloDownTile;
                            break;
                        case 0x8:
                            tile.type = possibleTiles.SoloLeftTile;
                            break;
                        default:
                            tile.type = possibleTiles.VoidTile;
                            break;
                    }
                    tile.NeighborCode = surroundings;
                }
                else
                {
                    tile.type = possibleTiles.VoidTile;
                }
            }
        }
        Vector2Int startTile = generator.GetStartTile(tiles);
        tiles[startTile.x, startTile.y].type = possibleTiles.StartTile;
        startingPosition.Value = new Vector3(startTile.x, 0, startTile.y);
        Vector2Int endTile = generator.GetEndTile(tiles);
        tiles[endTile.x, endTile.y].type = possibleTiles.EndTile;
        roomGenerated.Raise();
    }

    public byte boolArrayToSampleByte(bool[][] sampleArray) {
        byte result = 0x0;
        byte mask = 0x1;
        for(int x = 0; x < 3; x++)
        {
            for(int y = 0; y < 3; y++)
            {
                if(!(x==1 && y==1))
                {
                    byte sample = unchecked((byte)(sampleArray[x][y] ? 0xF : 0x0));
                    result = unchecked((byte)(result | (sample & mask)));
                    mask = unchecked((byte)(mask << 1));
                }
            }
        }
        return result;
    }

    public byte boolArrayToSampleByteZeroCorners(bool[][] sampleArray)
    {
        byte result = 0x0;
        byte mask = 0x1;
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                if ((x == 1 && y != 1)|| (y == 1 && x != 1))
                {
                    byte sample = unchecked((byte)(sampleArray[x][y] ? 0xF : 0x0));
                    result = unchecked((byte)(result | (sample & mask)));
                    mask = unchecked((byte)(mask << 1));
                }
            }
        }
        return result;
    }

    public bool[][] SampleSurroundings(int x, int y)
    {
        bool[][] otherSamples = { new bool[3], new bool[3], new bool[3] };
        for (int xx = -1; xx < 2; xx++)
        {
            for (int yy = -1; yy < 2; yy++)
            {
                otherSamples[xx + 1][yy + 1] = SampleTile(x + xx, y + yy);
            }
        }
        return otherSamples;
    }

    public bool SampleTile(int x, int y)
    {
        return generator.SampleTile(x, y);
    }
}
