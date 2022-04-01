using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * ItemStatHandler
 * Wessel Roelofse
 * 01/04/2022
 * 
 * An ItemHanlder that handles items that influence the player's stats.
 */

public enum ChangeableStats
{
    Health,
    Shield
}

[CreateAssetMenu(menuName = "Item/StatHandler")]
public class ItemStatHandler : ItemHandler
{
    public ChangeableStats statToChange = ChangeableStats.Health;
    public float amount;
    public override void onUse(ItemDisplay i, GameObject user)
    {
        i.ConsumeItem();
        switch(statToChange)
        {
            case ChangeableStats.Health:
                if(amount < 0)
                {
                    PlayerStats.Instance.DamagePlayer(amount);
                }
                else
                {
                    PlayerStats.Instance.HealPlayer(amount);
                }
                break;
            case ChangeableStats.Shield:
                PlayerStats.Instance.shields += amount;
                PlayerStats.Instance.shields = Mathf.Max(PlayerStats.Instance.shields, 0);
                break;
        }
    }
}
