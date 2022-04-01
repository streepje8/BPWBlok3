using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ItemAbilityHandler
 * Wessel Roelofse
 * 01/04/2022
 * 
 * An ItemHandler that handles items that grant the player an ability.
*/

[CreateAssetMenu(menuName = "Item/AbilityHandler")]
public class ItemAbilityHandler : ItemHandler
{
    public Ability abilityToGrant;
    public override void onUse(ItemDisplay i, GameObject user)
    {
        i.ConsumeItem();
        PlayerStats.Instance.ActivateAbility(abilityToGrant);
    }
}
