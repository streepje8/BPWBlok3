using Openverse.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Inventory
 * Wessel Roelofse
 * 01/04/2022
 * 
 * This class keeps track of items and defines the size of the inventory
 */
public class Inventory : MonoBehaviour
{
    public IntReference maxItems;
    public IntReference itemsPerRow;
    public ItemStack[,] items;

    private void Awake()
    {
        items = new ItemStack[itemsPerRow,maxItems/itemsPerRow];
    }
       
    public bool addItem(ItemStack i)
    {
        for (int x = 0; x < items.GetLength(0); x++)
        {
            for (int y = 0; y < items.GetLength(1); y++)
            {
                if(items[x,y] == null)
                {
                    items[x, y] = i;
                    return true;
                }
            }
        }
        return false;
    }

    public List<ItemStack> getAllItems()
    {
        List<ItemStack> itemList = new List<ItemStack>();
        for(int x = 0; x < items.GetLength(0); x++)
        {
            for(int y = 0; y < items.GetLength(1); y++)
            {
                if(items[x,y] != null)
                    itemList.Add(items[x, y]);
            }
        }
        return itemList;
    }
}
