using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
