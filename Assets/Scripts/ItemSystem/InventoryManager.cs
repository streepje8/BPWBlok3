using Openverse.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventoryReference inventory;
    public GameObjectReference itemDisplayPrefab;
    public GameObject visual;
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

    private void Start()
    {
        UpdateDisplays();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            visual.SetActive(!visual.activeSelf);
        }
    }

    public void UpdateDisplays()
    {
        for(int x = 0; x < itemDisplays.GetLength(0); x++)
        {
            for (int y = 0; y < itemDisplays.GetLength(1); y++)
            {
                ItemDisplay iD = itemDisplays[x, y];
                if ((x < inventory.Value.items.GetLength(0)) && (y < inventory.Value.items.GetLength(1)))
                {
                    ItemStack item = inventory.Value.items[x, y];
                    if(item != null) { 
                        iD.enabled = true;
                        iD.icon.texture = item.type.icon;
                        iD.amountDisplay.text = item.count.ToString();
                    } else
                    {
                        iD.enabled = false;
                    }
                } else
                {
                    if(iD != null) { 
                        iD.enabled = true;
                    }
                }
            }
        }
    }
}
