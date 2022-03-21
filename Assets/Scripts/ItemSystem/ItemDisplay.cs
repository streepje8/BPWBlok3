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
            if (myItem != null && myItem.type != null)
            {
                myItem.type.UseItem(GameController.Instance.player.gameObject);
            } else
            {
                GameController.Instance.inventory.ItemFromCursor(this);
            }
        } else
        {
            GameController.Instance.inventory.ItemToCursor(this);
        }
    }
}
