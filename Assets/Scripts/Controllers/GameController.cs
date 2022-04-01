using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;


/*
 * GameController
 * Wessel Roelofse
 * 01/04/2022
 * 
 * The main script that contains all the cross references required to control the game.
 */
public class GameController : Singleton<GameController>
{
    public RoomGenerator roomGenerator;
    public PlayerController player;
    public Transform cameraTarget;
    public FollowTarget followTarget;
    public Transform dollyParent;
    public InventoryManager inventory;
    public int score = 0;
    public PlayerStats playerstats;
    public List<ItemType> allItemTypes = new List<ItemType>();
    public Texture2D ActiveEndTile;
    private List<GameObject> spawnedEntities = new List<GameObject>();
    private Dictionary<int, ItemType> itemTypeBase = new Dictionary<int, ItemType>();
    
    private void Start()
    {
        foreach(ItemType type in allItemTypes)
        {
            RegisterItemType(type.ID, type);
        }
    }

    public void RegisterEntity(GameObject go)
    {
        spawnedEntities.Add(go);
    }

    public void WalkOnEndTile()
    {
        if(canFinish())
            roomGenerator.nextRoom();
    }

    public bool canFinish()
    {
        bool canFinish = true;
        foreach (GameObject go in spawnedEntities)
        {
            if (go.GetComponent<GameEnemy>() != null)
            {
                canFinish = false;
            }
        }
        return canFinish;
    }

    public void RegisterItemType(int iD, ItemType itemType)
    {
        if(!itemTypeBase.ContainsKey(iD))
        {
            itemTypeBase.Add(iD, itemType);
        } else
        {
            Debug.LogWarning("Item of type " + itemType.name + " tried to register on id " + iD + " but that id is already in use!");
        }
    }

    public ItemType ResolveItemType(int ID)
    {
        if(itemTypeBase.ContainsKey(ID))
        {
            return itemTypeBase[ID];
        }
        return null;
    }

    public void DeRegisterEntity(GameObject go)
    {
        spawnedEntities.Remove(go);
    }

    public void DestroyAllEntities()
    {
        TurnManager.Instance.RemoveAllTurns();
        player.GetComponent<TurnSubscriber>().ReSubscribe();
        foreach (GameObject entity in spawnedEntities)
        {
            Destroy(entity);
        }
        spawnedEntities.Clear();
    }

    private void Update()
    {
        followTarget.target = cameraTarget;
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;   
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void UpdateEndTile()
    {
        Vector2Int endTile = new Vector2Int(Mathf.RoundToInt(roomGenerator.endingPosition.Value.x), Mathf.RoundToInt(roomGenerator.endingPosition.Value.z));
        roomGenerator.tiles[endTile.x, endTile.y].GetComponent<GameTileController>().myMaterial.SetTexture("_MainTex", canFinish() ? ActiveEndTile : roomGenerator.possibleTiles.EndTile.Value.texture);
    }
}
