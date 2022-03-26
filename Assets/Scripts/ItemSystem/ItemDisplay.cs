using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    public RawImage icon;
    public TMP_Text amountDisplay;
    public ItemStack myItem = null;
    public int x;
    public int y;

    public void useItem()
    {
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            if (!Input.GetKey(KeyCode.LeftControl))
            {
                if (myItem != null && myItem.type != null)
                {
                    if (GameController.Instance.inventory.cursorItem == null)
                        myItem.type.UseItem(this, GameController.Instance.player.gameObject);
                }
                else
                {
                    GameController.Instance.inventory.ItemFromCursor(this);
                }
            } else
            {
                GameController.Instance.inventory.DropItem(this);
            }
        } else
        {
            GameController.Instance.inventory.ItemToCursor(this);
        }
    }

    public void ConsumeItem()
    {
        myItem.count--;
        if (myItem.count <= 0)
        {
            myItem = null;
            GameController.Instance.inventory.inventory.Value.items[x, y] = null;
        }
        GameController.Instance.inventory.UpdateDisplays();
    }
}
