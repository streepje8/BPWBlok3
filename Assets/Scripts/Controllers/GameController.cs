using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    public RoomGenerator roomGenerator;
    public PlayerController player;
    public Transform cameraTarget;
    public FollowTarget followTarget;
    public Transform dollyParent;
    public InventoryManager inventory;
    public int score = 0;
    public List<ItemType> allItemTypes = new List<ItemType>();
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

    public void RegisterItemType(int iD, ItemType itemType)
    {
        Debug.Log("Item type got registered");
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
        foreach(GameObject entity in spawnedEntities)
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
}
