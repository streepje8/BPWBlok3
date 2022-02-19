using Openverse.Events;
using Openverse.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public GameEvent roomGenerated;
    public GameObject tilePrefab;
    public IntReference roomWidth;
    public IntReference roomHeight;
    public FloatReference perlinScale;
    public FloatReference treshhold;
    public TileCollection possibleTiles;
    private List<GameTileController> tiles = new List<GameTileController>();

    void Start()
    {
        GenerateRoom();
    }

    float temp = 0;

    private void Update()
    {
        //Debug
        temp += Time.deltaTime;
        if(temp > 1)
        {
            GenerateRoom();
            Debug.Log("New Room boi");
            temp = 0;
        }
    }

    void GenerateRoom()
    {
        for(int i = tiles.Count - 1; i > -1; i--)
        {
            Destroy(tiles[i].gameObject);
            tiles.Remove(tiles[i]);
        }
        for (int x = 0; x < roomWidth; x++)
        {
            for (int y = 0; y < roomHeight; y++)
            {
                bool isFloor = SampleTile(x, y);
                GameTileController tile = Instantiate(tilePrefab, new Vector3(x, 0, y), Quaternion.identity).GetComponent<GameTileController>();
                tiles.Add(tile);
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
        roomGenerated.Raise();
    }

    public byte boolArrayToSampleByte(bool[][] sampleArray) {
        byte result = 00000000;
        byte mask = 00000001;
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
        byte result = 00000000;
        byte mask = 00000001;
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
        return Mathf.PerlinNoise(x / (float)roomWidth * perlinScale, y / (float)roomHeight * perlinScale) > treshhold; //Multiply this with 1 minus the distance of the center normalized with the map width or something like that
    }
}
