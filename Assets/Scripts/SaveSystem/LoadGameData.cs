using Openverse.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * LoadGameData
 * Wessel Roelofse
 * 01/04/2022
 * 
 * Loads the game data from the SaveData to their variables
 */
public class LoadGameData : MonoBehaviour
{
    public GameEvent LoadDataReadyEvent;

    private float loadIN = 0.5f;
    public bool canLoad = false;

    public void Start()
    {
        prepareMove();
        loadIN = 0.5f;
    }

    private void Update()
    {
        if (canLoad)
        {
            loadIN -= Time.deltaTime;
            if (loadIN <= 0)
            {
                canLoad = false;
                LoadDataReadyEvent.Raise();
            }
        }
    }

    public void prepareMove()
    {
        canLoad = true;
    }

    //Loads the savedata to the correct places in the game
    public void moveData()
    {
        if (Savedata.Instance.getBool("player.savedData")) {
            GameController.Instance.score = Savedata.Instance.getInt("player.score");
            GameController.Instance.playerstats.HP = Savedata.Instance.getFloat("player.health");
            GameController.Instance.playerstats.shields = Savedata.Instance.getFloat("player.shield");
            GameController.Instance.DestroyAllEntities();
            GameController.Instance.roomGenerator.LoadRoom(Savedata.Instance.getFloat("player.seed"));
            GameController.Instance.player.currentPosition2D = new Vector2Int(Savedata.Instance.getInt("player.position.x"), Savedata.Instance.getInt("player.position.y"));
            for(int x = 0; x < GameController.Instance.inventory.inventory.Value.items.GetLength(0); x++)
            {
                for (int y = 0; y < GameController.Instance.inventory.inventory.Value.items.GetLength(1); y++)
                {
                    ItemStack boi = (ItemStack)Savedata.Instance.get("player.inventory.items[" + x + "][" + y + "]");
                    if (boi != null) {
                        GameController.Instance.inventory.inventory.Value.items[x, y] = boi;
                        boi.ResolveType();
                    }
                }
            }
        }
    }

    public void saveData()
    {
        Savedata.Instance.save("player.score", GameController.Instance.score);
        Savedata.Instance.save("player.seed", GameController.Instance.roomGenerator.seed);
        Savedata.Instance.save("player.position.x", GameController.Instance.player.currentPosition2D.x);
        Savedata.Instance.save("player.position.y", GameController.Instance.player.currentPosition2D.y);
        Savedata.Instance.save("player.health", GameController.Instance.playerstats.HP);
        Savedata.Instance.save("player.shield", GameController.Instance.playerstats.shields);
        for (int x = 0; x < GameController.Instance.inventory.inventory.Value.items.GetLength(0); x++)
        {
            for (int y = 0; y < GameController.Instance.inventory.inventory.Value.items.GetLength(1); y++)
            {
                if (GameController.Instance.inventory.inventory.Value.items[x, y] != null) {
                    Savedata.Instance.save("player.inventory.items[" + x + "][" + y + "]", GameController.Instance.inventory.inventory.Value.items[x, y]);
                }
            }
        }
        Savedata.Instance.save("player.savedData", true);
        Savedata.Instance.saveAll();
    }
}
