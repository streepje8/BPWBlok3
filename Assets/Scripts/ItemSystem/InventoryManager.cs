using Openverse.Variables;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventoryReference inventory;
    public GameObjectReference itemDisplayPrefab;
    public GameObject visual;
    public Texture2D defaultIcon;
    private ItemDisplay[,] itemDisplays;

    private void Awake()
    {
        visual.SetActive(true);
        int x = 0, y = 0;
        itemDisplays = new ItemDisplay[inventory.Value.itemsPerRow, inventory.Value.maxItems / inventory.Value.itemsPerRow];
        for(int i = 0; i < inventory.Value.maxItems; i++)
        {
            ItemDisplay iD = Instantiate(itemDisplayPrefab.Value,visual.transform).GetComponent<ItemDisplay>();
            iD.transform.localPosition = new Vector3(-130 + 40 * x,55 - 40 * y,0);
            itemDisplays[x, y] = iD;
            x++;
            if(x >= inventory.Value.itemsPerRow)
            {
                x = 0;
                y++;
            }
        }
        visual.SetActive(false);
    }

    public ItemStack cursorItem;
    public Vector2Int cursorSource;

    public void ItemToCursor(ItemDisplay itemDisplay)
    {
        if (cursorItem == null)
        {
            cursorItem = itemDisplay.myItem;
            cursorSource = new Vector2Int(itemDisplay.x, itemDisplay.y);
            inventory.Value.items[itemDisplay.x, itemDisplay.y] = null;
            itemDisplay.myItem = null;
            itemDisplay.icon.texture = defaultIcon;
        }
    }

    public void ItemFromCursor(ItemDisplay itemDisplay)
    {
        if(cursorItem != null)
        {
            inventory.Value.items[itemDisplay.x, itemDisplay.y] = cursorItem;
            cursorItem = null;
            UpdateDisplays();
        }
    }

    public void DropItem(ItemDisplay itemDisplay)
    {
        inventory.Value.items[itemDisplay.x, itemDisplay.y].Drop(GameController.Instance.player.transform.position);
        inventory.Value.items[itemDisplay.x, itemDisplay.y] = null;
        UpdateDisplays();
    }

    private void Start()
    {
        UpdateDisplays();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            visual.SetActive(!visual.activeSelf);
            if (visual.activeSelf)
            {
                UpdateDisplays();
            } else
            {
                if(cursorItem != null)
                {
                    inventory.Value.items[cursorSource.x, cursorSource.y] = cursorItem;
                    cursorItem = null;
                }
            }
        }
    }

    public void UpdateDisplays()
    {
        for(int x = 0; x < itemDisplays.GetLength(0); x++)
        {
            for (int y = 0; y < itemDisplays.GetLength(1); y++)
            {
                ItemDisplay iD = itemDisplays[x, y];
                iD.x = x;
                iD.y = y;
                if ((x < inventory.Value.items.GetLength(0)) && (y < inventory.Value.items.GetLength(1)))
                {
                    ItemStack item = inventory.Value.items[x, y];
                    if(item?.count <= 0)
                    {
                        item = null;
                    }
                    if(item?.type == null)
                    {
                        inventory.Value.items[x, y] = null;
                        item = null;
                    }
                    if(item != null) { 
                        iD.enabled = true;
                        iD.icon.texture = item.type.icon;
                        iD.myItem = item;
                        iD.amountDisplay.text = item.count.ToString();
                        if(item.count < 2)
                        {
                            iD.amountDisplay.gameObject.SetActive(false);
                        } else
                        {
                            iD.amountDisplay.gameObject.SetActive(true);
                        }
                    } else
                    {
                        iD.enabled = false;
                        iD.icon.texture = defaultIcon;
                        iD.amountDisplay.gameObject.SetActive(false);
                    }
                } else
                {
                    if(iD != null) { 
                        iD.enabled = true;
                        iD.amountDisplay.gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}
