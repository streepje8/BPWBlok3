using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public List<ItemType> drops = new List<ItemType>();
    public List<int> dropAmounts = new List<int>();

    void onClickedEvent()
    {
        int theItem = Random.Range(0, drops.Count);
        drops[theItem].Drop(new ItemStack(drops[theItem], dropAmounts[theItem]), transform.position);
    }
}
